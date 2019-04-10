/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace REvE.Validation
{
    using Models;

    /// <summary>
    /// Adapter for converting from the REvE Models to specialized <see cref="ValidationAttribute"/> Types.
    /// </summary>
    internal static class DataAnnotationAdapter
    {
        internal static ValidationAttribute BuildValidation(Rule cfg, ValidationRule svc)
        {
            var validation = GetValidationAttribute(cfg, svc);
            if (!IsDefaultError(cfg))
                validation.ErrorMessage = cfg.ErrorMessage;

            return validation;
        }

        private static ValidationAttribute GetValidationAttribute(Rule cfg, ValidationRule svc)
        => IsDefaultValue(cfg)
            ? IsDefaultError(cfg)
                ? singularValidationAttributes[cfg.Type].Value // Default Error Message and Rule Value, so use cached instances.
                : defaultConstructors[cfg.Type]                // Provided Error Message with default Rule Value. Create new instance.
            : Construct(svc, nonDefaultConstructors[cfg.Type]);    // Provided Rule Value. Create new instance.


        private static bool IsDefaultError(Rule rule) => string.IsNullOrWhiteSpace(rule.ErrorMessage);

        private static bool IsDefaultValue(Rule rule) => singularValidationAttributes.ContainsKey(rule.Type);

        #region Delegate Maps

        /// <summary>
        /// ValidationAttributes without constructor parameters don't need separate instances.
        /// </summary>
        private static readonly IReadOnlyDictionary<RuleType, Lazy<ValidationAttribute>> singularValidationAttributes
            = new Dictionary<RuleType, Lazy<ValidationAttribute>>
            {
                [RuleType.EmailAddress] = new Lazy<ValidationAttribute>(() => defaultConstructors[RuleType.EmailAddress]),
                [RuleType.Url] = new Lazy<ValidationAttribute>(() => defaultConstructors[RuleType.Url]),
                [RuleType.CreditCard] = new Lazy<ValidationAttribute>(() => defaultConstructors[RuleType.CreditCard]),
                [RuleType.Phone] = new Lazy<ValidationAttribute>(() => defaultConstructors[RuleType.Phone]),
                [RuleType.Required] = new Lazy<ValidationAttribute>(() => defaultConstructors[RuleType.Required]),
                [RuleType.FileExtensions] = new Lazy<ValidationAttribute>(() => defaultConstructors[RuleType.FileExtensions]),
                [RuleType.MaxLength] = new Lazy<ValidationAttribute>(() => defaultConstructors[RuleType.MaxLength])
            };

        /// <summary>
        /// Wraps <see cref="nonDefaultConstructors"/> delegate calls to provide exception details on invalid boxing scenarios.
        /// </summary>
        /// <param name="rule">The <see cref="ValidationRule"/> to convert to <see cref="ValidationAttribute"/>.</param>
        /// <param name="constructor">Delegate to the conversion method.</param>
        /// <returns>A <see cref="ValidationAttribute"/> created using details from the provided <paramref name="rule"/>.</returns>
        private static ValidationAttribute Construct(ValidationRule rule, Func<ValidationRule, ValidationAttribute> constructor)
            => constructor(rule) ?? throw ConstructionException(rule);

        private static ValidationParseException ConstructionException(ValidationRule rule)
            => new ValidationParseException($"Error converting rule from {rule.GetType()} to {typeof(ValidationAttribute)}. \r\n" +
            $"Rule: {rule.Model}.{rule.Property}.{rule.RuleType}");

        /// <summary>
        /// Delegates to call <see cref="ValidationAttribute"/> constructors that take arguments.
        /// </summary>
        private static readonly IReadOnlyDictionary<RuleType, Func<ValidationRule, ValidationAttribute>> nonDefaultConstructors
            = new Dictionary<RuleType, Func<ValidationRule, ValidationAttribute>>
            {
                [RuleType.EnumDataType] = p => (p as EnumDataTypeRule)?.Convert(),
                [RuleType.FileExtensions] = p => (p as FileExtensionsRule)?.Convert(), // *Should* have built in validations, but may need to write a Parse method
                [RuleType.Range] = p => (p as RangeRule)?.Convert(),
                [RuleType.MaxLength] = p => (p as MaxLengthRule)?.Convert(),
                [RuleType.MinLength] = p => (p as MinLengthRule)?.Convert(),
                [RuleType.StringLength] = p => (p as StringLengthRule)?.Convert(),
                [RuleType.Regex] = p => (p as RegexRule)?.Convert()
            };

        /// <summary>
        /// Delegates to default constructors of ValidationAttributes that have no/optional parameters.
        /// </summary>
        private static readonly IReadOnlyDictionary<RuleType, ValidationAttribute> defaultConstructors
            = new Dictionary<RuleType, ValidationAttribute>
            {
                [RuleType.EmailAddress] = new EmailAddressAttribute(),
                [RuleType.Url] = new UrlAttribute(),
                [RuleType.CreditCard] = new CreditCardAttribute(),
                [RuleType.Phone] = new PhoneAttribute(),
                [RuleType.FileExtensions] = new FileExtensionsAttribute(),
                [RuleType.MaxLength] = new MaxLengthAttribute(),
                [RuleType.Required] = new RequiredAttribute()
            };

        private static EnumDataTypeAttribute Convert(this EnumDataTypeRule rule)
            => new EnumDataTypeAttribute(ValidatedEnumAttribute.GetEnumType(rule.EnumName));

        private static FileExtensionsAttribute Convert(this FileExtensionsRule rule)
            => new FileExtensionsAttribute { Extensions = rule.Extensions };

        private static RangeAttribute Convert(this RangeRule rule)
            => new RangeAttribute(rule.Min, rule.Max);

        private static MaxLengthAttribute Convert(this MaxLengthRule rule)
            => new MaxLengthAttribute(rule.Max);

        private static MinLengthAttribute Convert(this MinLengthRule rule)
            => new MinLengthAttribute(rule.Min);

        private static StringLengthAttribute Convert(this StringLengthRule rule)
            => new StringLengthAttribute(rule.Max) { MinimumLength = rule.Min };

        private static RegularExpressionAttribute Convert(this RegexRule rule)
            => new RegularExpressionAttribute(rule.Pattern);
        #endregion
    }
}
