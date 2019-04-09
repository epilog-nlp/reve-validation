using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REvE.Validation.Configuration.Tests
{
    using Models;

    public static class GenerationUtil
    {
        #region Model Config
        internal const string TestErrorMessage = "Test Error Message";
        internal const string TestTechDescription = "Test Technical Description";
        internal const string TestFriendlyDescription = "Test Friendly Description";

        public static Model GenerateModel(string name, string alias = null)
            => new Model
            {
                Name = name,
                Alias = alias
            };

        public static Model AddProperties(this Model model, IEnumerable<(string propertyName, IEnumerable<(RuleType type, MessageFlag flags, string ruleName)> rules)> properties)
        {
            model.Properties = properties.Select(prop => GenerateProperty(prop.propertyName, prop.rules)).ToList();
            return model;
        }

        public static Property GenerateProperty(string name, IEnumerable<(RuleType rule, MessageFlag flags, string ruleName)> rules)
            => new Property
            {
                Name = name,
                Rules = rules.Select(r => new Rule(r.rule, r.ruleName).AddMessages(r.flags))
                             .ToList()
            };

        #region Messages
        private static readonly IReadOnlyCollection<Func<Rule, MessageFlag, Rule>> messageDelegates
            = new List<Func<Rule, MessageFlag, Rule>>
            {
                (r,f) => f.HasFlag(MessageFlag.Error) ? r.AddErrorMessage() : r,
                (r,f) => f.HasFlag(MessageFlag.Tech) ? r.AddTechnicalDescription() : r,
                (r,f) => f.HasFlag(MessageFlag.Friendly) ? r.AddFriendlyDescription() : r
            };

        public static Rule AddMessages(this Rule rule, MessageFlag flags = MessageFlag.None)
            => messageDelegates.Aggregate(rule, (r, fn) => fn(r, flags));

        private static Rule AddErrorMessage(this Rule rule)
        {
            rule.ErrorMessage = TestErrorMessage;
            return rule;
        }

        private static Rule AddTechnicalDescription(this Rule rule)
        {
            rule.TechnicalDescription = TestTechDescription;
            return rule;
        }

        private static Rule AddFriendlyDescription(this Rule rule)
        {
            rule.FriendlyDescription = TestFriendlyDescription;
            return rule;
        }
        #endregion // Messages

        #endregion // Model Config

        #region Argument Config

        public static IEnumerable<RuleArgument> GetDefaultArguments(string modelName, string propertyName, RuleType type, string ruleName = null)
        => defaultArgumentDelegates.ContainsKey(type)
            ? defaultArgumentDelegates[type].Select(arg =>
                new RuleArgument
                {
                    Rule = $"{GeneratePrefix(modelName, propertyName)}{arg}",
                    Type = type,
                    Name = ruleName
                })
            : new List<RuleArgument>();

        private static readonly IReadOnlyDictionary<RuleType, IEnumerable<string>> defaultArgumentDelegates
            = new Dictionary<RuleType, IEnumerable<string>>
            {
                [RuleType.FileExtensions] = FileExtensionsArguments,
                [RuleType.Range] = RangeArguments,
                [RuleType.MaxLength] = MaxLengthArguments,
                [RuleType.MinLength] = MinLengthArguments,
                [RuleType.StringLength] = StringLengthArguments,
                [RuleType.Regex] = RegexArguments
            };

        private static string GeneratePrefix(string modelName, string propertyName)
            => $"{modelName}.{propertyName}.";

        private static IEnumerable<string> FileExtensionsArguments
            => new[] { "extensions=.txt,.json,.html" };

        private static IEnumerable<string> RangeArguments
            => new[] { "min=1", "max=10" };

        private static IEnumerable<string> MaxLengthArguments
            => new[] { "maxlength=10" };

        private static IEnumerable<string> MinLengthArguments
            => new[] { "minlength=1" };

        private static IEnumerable<string> StringLengthArguments
            => new[] { "minlength=1", "maxlength=10" };

        private static IEnumerable<string> RegexArguments
            => new[] { @"pattern=^[a-zA-Z''-'\s]{1,40}$" }; // 1-40 Upper/Lower chars

        #endregion // Argument Config
    }

    [Flags]
    public enum MessageFlag
    {
        None = 0 << 0,
        Error = 1 << 0,
        Tech = 1 << 1,
        Friendly = 1 << 2,
        All = Error | Tech | Friendly
    }

}
