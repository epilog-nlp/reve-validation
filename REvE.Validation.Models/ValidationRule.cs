/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.Collections.Generic;

namespace REvE.Models
{
    /// <summary>
    /// Contains summary information about configured validations. Base type for all specialized rule types.
    /// </summary>
    public abstract class ValidationRule
    {
        /// <summary>
        /// Called by derived classes to set the <see cref="TechnicalDescription"/> and <see cref="FriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        protected ValidationRule(string technical = null, string friendly = null)
        {
            CustomTechnicalDescription = technical;
            CustomFriendlyDescription = friendly;
        }

        /// <summary>
        /// Name of the Model being validated.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Name of the Property being validated.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public abstract RuleType RuleType { get; }

        /// <summary>
        /// A User-Friendly description of the Validation being applied. Will provide the <see cref="DefaultFriendlyDescription"/> if no value is provided to the constructor.
        /// </summary>
        public string FriendlyDescription => CustomFriendlyDescription ?? DefaultFriendlyDescription;

        /// <summary>
        /// Overrides <see cref="DefaultFriendlyDescription"/> if value is provided in the constructor.
        /// </summary>
        protected string CustomFriendlyDescription { get; }

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected abstract string DefaultFriendlyDescription { get; }

        /// <summary>
        /// A technical description of the validation rule (in the format {<see cref="Model"/>}.{<see cref="Property"/>} {Description}) being applied. 
        /// Will provide the <see cref="DefaultTechnicalDescription"/> if no value is provided to the constructor.
        /// </summary>
        public string TechnicalDescription => $"{Model}.{Property} {CustomTechnicalDescription ?? DefaultTechnicalDescription}";

        /// <summary>
        /// Overrides <see cref="DefaultTechnicalDescription"/> if value is provided in the constructor.
        /// </summary>
        protected string CustomTechnicalDescription { get; }

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected abstract string DefaultTechnicalDescription { get; }
    }

    #region Specialized Types

    /// <summary>
    /// Contains summary information about a configured Email Address Rule.
    /// </summary>
    public class EmailAddressRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public EmailAddressRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.EmailAddress;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => "must be a string containing a correctly formatted Email Address.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => "Valid Email Address";
    }

    /// <summary>
    /// Contains summary information about a configured URL Rule.
    /// </summary>
    public class UrlRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public UrlRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.Url;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => "must be a string containing a correctly formatted URL.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => "Valid URL";
    }

    /// <summary>
    /// Contains summary information about a configured Credit Card Rule.
    /// </summary>
    public class CreditCardRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public CreditCardRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.CreditCard;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => "must be a string containing a correctly formatted Credit Card Number.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => "Valid Credit Card Number";
    }

    /// <summary>
    /// Contains summary information about a configured Enumeration Value Rule.
    /// </summary>
    public class EnumDataTypeRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public EnumDataTypeRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.EnumDataType;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => $"must contain a valid value from the Enum Type {EnumName}.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => "";

        /// <summary>
        /// Name of source <see cref="System.Enum"/> to validate against.
        /// </summary>
        public string EnumName { get; set; }

    }

    /// <summary>
    /// Contains summary information about a configured Phone Number Rule.
    /// </summary>
    public class PhoneRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public PhoneRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.Phone;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => "must be a string containing a valid Phone Number.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => "Valid Telephone Number";
    }

    /// <summary>
    /// Contains summary information about a configured File Extension Rule.
    /// </summary>
    public class FileExtensionsRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public FileExtensionsRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.FileExtensions;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => $"must be a string containing one of the following valid File Extensions: {Extensions}.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => $"Accepted File Extensions: {Extensions}";

        /// <summary>
        /// A comma-separated list of valid File Extensions.
        /// </summary>
        public string Extensions { get; set; }
    }

    /// <summary>
    /// Contains summary information about a configured Range Rule.
    /// </summary>
    public class RangeRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public RangeRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.Range;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => $"must have a value between {Min} and {Max}.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => $"Acceptable Range: {Min} - {Max}";

        /// <summary>
        /// The (inclusive) Minimum accepted value.
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// The (inclusive) Maximum accepted value.
        /// </summary>
        public double Max { get; set; }
    }

    /// <summary>
    /// Contains summary information about a configured Maximum Length Rule.
    /// </summary>
    public class MaxLengthRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public MaxLengthRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.MaxLength;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => $"must have a length no greater than {Max}.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => $"Maximum Length: {Max}";

        /// <summary>
        /// The (inclusive) Maximum accepted length.
        /// </summary>
        public int Max { get; set; }
    }

    /// <summary>
    /// Contains summary information about a configured Minimum Length Rule.
    /// </summary>
    public class MinLengthRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public MinLengthRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.MinLength;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => $"must have a length of {Min} or greater.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => $"Minimum Length: {Min}";

        /// <summary>
        /// The (inclusive) Minimum accepted length.
        /// </summary>
        public int Min { get; set; }
    }

    /// <summary>
    /// Contains summary information about a configured Required Value Rule.
    /// </summary>
    public class RequiredRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public RequiredRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.Required;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => $"must have a non-null value.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => "Is Required";
    }

    /// <summary>
    /// Contains summary information about a configured String Length Rule.
    /// </summary>
    public class StringLengthRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public StringLengthRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.StringLength;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => $"must be a string between {Min} and {Max} characters long.";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => $"Between {Min} and {Max} characters.";

        /// <summary>
        /// The (inclusive) Minimum accepted string length.
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// The (inclusive) Maximum accepted string length.
        /// </summary>
        public int Max { get; set; }
    }

    /// <summary>
    /// Contains summary information about a configured Regular Expression Rule.
    /// </summary>
    public class RegexRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public RegexRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.Regex;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => $"must be a string matching the Regular Expression /{Pattern}/";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => "";

        /// <summary>
        /// The <see cref="System.Text.RegularExpressions.Regex"/> pattern for which the provided value must be a complete match.
        /// </summary>
        public string Pattern { get; set; }
    }

    /// <summary>
    /// Contains summary information about a configured Custom Rule.
    /// </summary>
    public class CustomRule : ValidationRule
    {
        /// <summary>
        /// Instantiates a new <see cref="ValidationRule"/> with optional overrides for <see cref="DefaultTechnicalDescription"/> and <see cref="DefaultFriendlyDescription"/>.
        /// </summary>
        /// <param name="technical">Overrides <see cref="DefaultTechnicalDescription"/> with the provided string.</param>
        /// <param name="friendly">Overrides <see cref="DefaultFriendlyDescription"/> with the provided string.</param>
        public CustomRule(string technical = null, string friendly = null) : base(technical, friendly) { }

        /// <summary>
        /// Indicates the type of rule being applied.
        /// </summary>
        public override RuleType RuleType => RuleType.Custom;

        /// <summary>
        /// The default description portion of the technical description. Will be used when no <see cref="ValidationRule.CustomTechnicalDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultTechnicalDescription => "";

        /// <summary>
        /// The default User-Friendly description of the Validation being applied. Will be used when no <see cref="ValidationRule.CustomFriendlyDescription"/> is provided to the constructor.
        /// </summary>
        protected override string DefaultFriendlyDescription => "";

        /// <summary>
        /// A collection of parameters to be provided to the custom validation rule.
        /// </summary>
        public IEnumerable<string> Parameters { get; set; }
    }
    #endregion
}
