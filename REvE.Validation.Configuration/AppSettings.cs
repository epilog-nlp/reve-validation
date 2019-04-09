/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration
{
    using static ConfigUtil;

    /// <summary>
    /// Constant Application Configuration Setting Keys.
    /// </summary>
    public static class AppSettings
    {

        #region Model Provider
        /// <summary>
        /// Key for retrieving the name of the <see cref="Contracts.IValidationModelConfigProvider"/> implementation to use.
        /// </summary>
        public const string ModelProviderContractKey = "reve-models-contract";

        /// <summary>
        /// The name of the <see cref="Contracts.IValidationModelConfigProvider"/> implementation to use.
        /// </summary>
        public static readonly string ModelProviderContract = AppSetting(ModelProviderContractKey, "json");

        /// <summary>
        /// Key for retrieving the source location used by the <see cref="Contracts.IValidationModelConfigProvider"/> implementation.
        /// </summary>
        public const string ModelProviderSourceKey = "reve-models-src";

        /// <summary>
        /// Key for retrieving the source location used by the <see cref="JsonValidationModelConfigProvider"/>.
        /// </summary>
        public const string JsonModelProviderSourceKey = "reve-models-src-json";

        /// <summary>
        /// Key for retrieving the source location used by the <see cref="XmlValidationModelConfigProvider"/>.
        /// </summary>
        public const string XmlModelProviderSourceKey = "reve-models-src-xml";

        /// <summary>
        /// The default name of the JSON file used by the <see cref="JsonValidationModelConfigProvider"/>.
        /// </summary>
        public const string JsonDefaultModelSource = "validations.json";

        /// <summary>
        /// The default name of the XML file used by the <see cref="XmlValidationModelConfigProvider"/>.
        /// </summary>
        public const string XmlDefaultModelSource = "validations.xml";

        /// <summary>
        /// The name of the file that will be passed to the <see cref="Contracts.IValidationModelConfigProvider"/>.
        /// </summary>
        public static readonly string ModelProviderSource =
            ModelProviderContract.Equals("json", System.StringComparison.CurrentCultureIgnoreCase)
            ? JsonModelSource
            : XmlModelSource;

        /// <summary>
        /// The configuration key used to find the XML file provided to the <see cref="XmlValidationModelConfigProvider"/>.
        /// </summary>
        [Export("xml-cfg-models")]
        public static readonly string XmlModelSource =
            AppSetting<string>(XmlModelProviderSourceKey) == null
            ? ModelProviderSourceKey
            : XmlModelProviderSourceKey;

        /// <summary>
        /// The configuration key used to find the JSON file provided to the <see cref="JsonValidationModelConfigProvider"/>.
        /// </summary>
        [Export("json-cfg-models")]
        public static readonly string JsonModelSource =
            AppSetting<string>(JsonModelProviderSourceKey) == null
            ? ModelProviderSourceKey
            : JsonModelProviderSourceKey;
        #endregion

        #region Rule Values
        /// <summary>
        /// Key for retrieving the name of the <see cref="Contracts.IRuleArgumentConfigProvider"/> implementation to use.
        /// </summary>
        public const string RuleValueProviderContractKey = "reve-values-contract";

        /// <summary>
        /// The name of the <see cref="Contracts.IRuleArgumentConfigProvider"/> implementation to use.
        /// </summary>
        public static readonly string RuleValueProviderContract = AppSetting(RuleValueProviderContractKey, "json");

        /// <summary>
        /// Key for retrieving the source location used by the <see cref="Contracts.IRuleArgumentConfigProvider"/> implementation.
        /// </summary>
        public const string RuleValueProviderSourceKey = "reve-values-src";

        /// <summary>
        /// Key for retrieving the source location used by the <see cref="JsonRuleArgumentConfigProvider"/>.
        /// </summary>
        public const string JsonRuleValueProviderSourceKey = "reve-values-src-json";

        /// <summary>
        /// Key for retrieving the source location used by the <see cref="XmlRuleArgumentConfigProvider"/>.
        /// </summary>
        public const string XmlRuleValueProviderSourceKey = "reve-values-src-xml";

        /// <summary>
        /// The default name of the JSON file used by the <see cref="JsonRuleArgumentConfigProvider"/>.
        /// </summary>
        public const string JsonDefaultRuleValueSource = "rules.json";

        /// <summary>
        /// The default name of the XML file used by the <see cref="XmlRuleArgumentConfigProvider"/>.
        /// </summary>
        public const string XmlDefaultRuleValueSource = "rules.xml";

        /// <summary>
        /// The name of the file that will be passed to the <see cref="Contracts.IRuleArgumentConfigProvider"/>.
        /// </summary>
        public static readonly string RuleValueProviderSource =
            RuleValueProviderContract.Equals("json", System.StringComparison.CurrentCultureIgnoreCase)
            ? JsonRuleValueSource
            : XmlRuleValueSource;

        /// <summary>
        /// The configuration key used to find the XML file provided to the <see cref="XmlRuleArgumentConfigProvider"/>.
        /// </summary>
        [Export("xml-cfg-rules")]
        public static readonly string XmlRuleValueSource =
            AppSetting<string>(XmlRuleValueProviderSourceKey) == null
            ? RuleValueProviderSourceKey
            : XmlRuleValueProviderSourceKey;

        /// <summary>
        /// The configuration key used to find the JSON file provided to the <see cref="JsonRuleArgumentConfigProvider"/>.
        /// </summary>
        [Export("json-cfg-rules")]
        public static readonly string JsonRuleValueSource =
            AppSetting<string>(JsonRuleValueProviderSourceKey) == null
            ? RuleValueProviderSourceKey
            : JsonRuleValueProviderSourceKey;

        #endregion
    }
}
