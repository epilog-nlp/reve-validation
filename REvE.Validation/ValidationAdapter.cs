/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.Collections.Generic;
using System;
using System.Linq;

namespace REvE.Validation
{
    using Models;
    using Repository.Models;
    using static DataAnnotationAdapter;

    /// <summary>
    /// Adapter for converting between various REvE Models.
    /// </summary>
    internal static class ValidationAdapter
    {
        #region Cfg to Repo Translations
        public static ModelValidation ToRepo(this Model cfgModel, Type type)
        => cfgModel == default(Model)
            ? default
            : new ModelValidation
            {
                ModelName = cfgModel.Name,
                ModelAlias = cfgModel.Alias,
                Properties = cfgModel.Properties?.ToRepo(type)
            };


        public static IEnumerable<PropertyValidation> ToRepo(this IEnumerable<Property> cfgProperties, Type type)
        => cfgProperties == default(IEnumerable<Property>)
            ? new List<PropertyValidation>()
            : cfgProperties.Select(prop => prop.ToRepo(type));

        public static PropertyValidation ToRepo(this Property cfgProperty, Type type)
        => cfgProperty == default(Property)
            ? default
            : new PropertyValidation
            {
                PropertyName = cfgProperty.Name,
                PropertyGetter = type.GetProperty(cfgProperty.Name).GetMethod,
                ValidationRules = cfgProperty.Rules?.Select(rule => 
                    BuildValidation(rule, ruleSelector[rule.Type](null,null, rule.Arguments)))
            };


        #endregion

        #region Cfg to Service Translations
        public static IEnumerable<ValidationRule> ToService(this IEnumerable<Model> models)
            => models?.SelectMany(model => model.ToService());

        public static IEnumerable<ValidationRule> ToService(this Model model)
            => model?.Properties.SelectMany(prop
                => prop.Rules?.Select(rule =>
                {
                    var result = ruleSelector[rule.Type](rule.TechnicalDescription, rule.FriendlyDescription, rule.Arguments);
                    result.Model = model.Name;
                    result.Property = prop.Name;
                    return result;
                })).ToList();

        private static readonly IReadOnlyDictionary<RuleType, Func<string, string, IEnumerable<RuleArgument>, ValidationRule>> ruleSelector
            = new Dictionary<RuleType, Func<string, string, IEnumerable<RuleArgument>, ValidationRule>>
            {
                [RuleType.EmailAddress] = (s1, s2, args) => new EmailAddressRule(s1, s2),
                [RuleType.Url] = (s1, s2, args) => new UrlRule(s1, s2),
                [RuleType.CreditCard] = (s1, s2, args) => new CreditCardRule(s1, s2),
                [RuleType.Phone] = (s1, s2, args) => new PhoneRule(s1, s2),
                [RuleType.Required] = (s1, s2, args) => new RequiredRule(s1, s2),
                [RuleType.EnumDataType] = (s1, s2, args) => new EnumDataTypeRule(s1, s2).CheckArgCount(args).AddArgs(args),
                [RuleType.FileExtensions] = (s1, s2, args) => new FileExtensionsRule(s1, s2).CheckArgCount(args).AddArgs(args),
                [RuleType.Range] = (s1, s2, args) => new RangeRule(s1, s2).CheckArgCount(args, 2).AddArgs(args),
                [RuleType.MaxLength] = (s1, s2, args) => new MaxLengthRule(s1, s2).CheckArgCount(args).AddArgs(args),
                [RuleType.MinLength] = (s1, s2, args) => new MinLengthRule(s1, s2).CheckArgCount(args).AddArgs(args),
                [RuleType.StringLength] = (s1, s2, args) => new StringLengthRule(s1, s2).CheckArgCount(args, 2).AddArgs(args),
                [RuleType.Regex] = (s1, s2, args) => new RegexRule(s1, s2).CheckArgCount(args).AddArgs(args),
                [RuleType.Custom] = (s1, s2, args) => new CustomRule(s1, s2).AddArgs(args)
            };

        private static TRule CheckArgCount<TRule>(this TRule rule, IEnumerable<RuleArgument> args, int count = 1)
            where TRule : ValidationRule
        {
            if (args == null)
                throw new ValidationParseException($"Error parsing configuration for {rule.RuleType.ToString()} Rule. Reason: null Arguments",
                    new ArgumentNullException(nameof(args)));
            if (args.Count() != count)
                throw new ValidationParseException($"Error parsing configuration for {rule.RuleType.ToString()} Rule. \r\n Expected Arguments: {count}\r\n Actual: {args.Count()}");
            return rule;
        }

        private static EnumDataTypeRule AddArgs(this EnumDataTypeRule rule, IEnumerable<RuleArgument> args)
        {
            rule.EnumName = args.Single().Value;
            return rule;
        }

        private static FileExtensionsRule AddArgs(this FileExtensionsRule rule, IEnumerable<RuleArgument> args)
        {
            rule.Extensions = args.Single().Value;
            return rule;
        }

        private static RangeRule AddArgs(this RangeRule rule, IEnumerable<RuleArgument> args)
        {
            try
            {
                rule.Min = RetrieveRangeRuleValue("min", args);
            } catch (Exception e)
            {
                throw new ValidationParseException($"Error parsing minimum value for {rule.RuleType.ToString()}:\r\n {e.Message}", e);
            }

            try
            {
                rule.Max = RetrieveRangeRuleValue("max", args);
            } catch (Exception e)
            {
                throw new ValidationParseException($"Error parsing maximum value for {rule.RuleType.ToString()}:\r\n {e.Message}", e);
            }

            return rule;
        }

        private static double RetrieveRangeRuleValue(string parameterName, IEnumerable<RuleArgument> args)
        {
            var result = args.Where(a => string.Equals(a.Parameter, parameterName, StringComparison.CurrentCultureIgnoreCase))
                                               .Single().Value;
            if (DateTime.TryParse(result, out var date))
            {
                return date.Ticks;
            }
            else if (double.TryParse(result, out var doubleNum))
            {
                return doubleNum;
            }
            else
            {
                return Convert.ToInt32(result);
            }
        }

        private static MaxLengthRule AddArgs(this MaxLengthRule rule, IEnumerable<RuleArgument> args)
        {
            rule.Max = Convert.ToInt32(args.Single().Value);
            return rule;
        }

        private static MinLengthRule AddArgs(this MinLengthRule rule, IEnumerable<RuleArgument> args)
        {
            rule.Min = Convert.ToInt32(args.Single().Value);
            return rule;
        }

        private static StringLengthRule AddArgs(this StringLengthRule rule, IEnumerable<RuleArgument> args)
        {
            try
            {
                rule.Min = Convert.ToInt32(args.Where(a => string.Equals(a.Parameter, "minlength", StringComparison.CurrentCultureIgnoreCase))
                                               .Single().Value);
            }
            catch (Exception e)
            {
                throw new ValidationParseException($"Error parsing minimum value for {rule.RuleType.ToString()}:\r\n {e.Message}", e);
            }

            try
            {
                rule.Max = Convert.ToInt32(args.Where(a => string.Equals(a.Parameter, "maxlength", StringComparison.CurrentCultureIgnoreCase))
                                               .Single().Value);
            }
            catch (Exception e)
            {
                throw new ValidationParseException($"Error parsing maximum value for {rule.RuleType.ToString()}:\r\n {e.Message}", e);
            }
            return rule;
        }

        private static RegexRule AddArgs(this RegexRule rule, IEnumerable<RuleArgument> args)
        {
            rule.Pattern = args.Single().Value;
            return rule;
        }

        private static CustomRule AddArgs(this CustomRule rule, IEnumerable<RuleArgument> args)
        {
            rule.Parameters = args.Select(a => $"{a.Parameter} : {a.Value}").ToList();
            return rule;
        }

        #endregion
    }
}
