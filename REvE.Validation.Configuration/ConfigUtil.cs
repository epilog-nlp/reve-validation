/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace REvE.Validation.Configuration
{
    using Contracts;
    using Models;
    
    /// <summary>
    /// Utility Methods for searching the Configuration Models.
    /// </summary>
    public static class ConfigUtil
    {
        /// <summary>
        /// Extract all <see cref="RuleArgument"/> items from an <see cref="IRuleArgumentConfigProvider"/>.
        /// </summary>
        /// <param name="cfg">The Configuration data source to extract from.</param>
        /// <returns>All <see cref="RuleArgument"/> items for all Models.</returns>
        public static IEnumerable<RuleArgument> LookupArguments(this IRuleArgumentConfigProvider cfg)
            => cfg.Configuration.RuleDefinitions;

        /// <summary>
        /// Extract all <see cref="RuleArgument"/> items for a given Model.
        /// </summary>
        /// <param name="cfg">The Configuration data source to extract from.</param>
        /// <param name="modelName">The name of the Model to retrieve rules for.</param>
        /// <param name="comparisonType">The string comparison method to be used. Defaults to <see cref="StringComparison.CurrentCultureIgnoreCase"/>.</param>
        /// <returns>All <see cref="RuleArgument"/> items for the provided Model name.</returns>
        public static IEnumerable<RuleArgument> LookupArguments(this IRuleArgumentConfigProvider cfg, string modelName, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
            => cfg.LookupArguments().Where(p => string.Equals(p.Model, modelName, comparisonType));

        /// <summary>
        /// Extract all <see cref="RuleArgument"/> items for a given Model and Property.
        /// </summary>
        /// <param name="cfg">The Configuration data source to extract from.</param>
        /// <param name="modelName">The name of the Model to retrieve rules for.</param>
        /// <param name="propertyName">The name of the Property to retrieve rules for.</param>
        /// <param name="comparisonType">The string comparison method to be used. Defaults to <see cref="StringComparison.CurrentCultureIgnoreCase"/>.</param>
        /// <returns>All <see cref="RuleArgument"/> items for the provided Model and Property names.</returns>
        public static IEnumerable<RuleArgument> LookupArguments(this IRuleArgumentConfigProvider cfg, string modelName, string propertyName, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
            => cfg.LookupArguments(modelName, comparisonType).Where(p => string.Equals(p.Property, propertyName, comparisonType));

        /// <summary>
        /// Extract all <see cref="RuleArgument"/> items for a given Model, Property, and Rule Type.
        /// </summary>
        /// <param name="cfg">The Configuration data source to extract from.</param>
        /// <param name="modelName">The name of the Model to retrieve rules for.</param>
        /// <param name="propertyName">The name of the Property to retrieve rules for.</param>
        /// <param name="ruleType">The Type of Rule to retrieve arguments for.</param>
        /// <param name="comparisonType">The string comparison method to be used. Defaults to <see cref="StringComparison.CurrentCultureIgnoreCase"/>.</param>
        /// <returns>The <see cref="RuleArgument"/> items corresponding to the provided Model, Property, and Rule Type</returns>
        public static IEnumerable<RuleArgument> LookupArguments(this IRuleArgumentConfigProvider cfg, string modelName, string propertyName, RuleType ruleType, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
            => cfg.LookupArguments(modelName, propertyName, comparisonType).Where(p => p.Type == ruleType);

        /// <summary>
        /// Extract all <see cref="RuleArgument"/> items for a given Model, Property, and Rule Type.
        /// </summary>
        /// <param name="cfg">The Configuration data source to extract from.</param>
        /// <param name="modelName">The name of the Model to retrieve rules for.</param>
        /// <param name="propertyName">The name of the Property to retrieve rules for.</param>
        /// <param name="ruleType">The Type of Rule to retrieve arguments for.</param>
        /// <param name="ruleName">The name of the Rule to retrieve arguments for.</param>
        /// <param name="comparisonType">The string comparison method to be used. Defaults to <see cref="StringComparison.CurrentCultureIgnoreCase"/>.</param>
        /// <returns>The <see cref="RuleArgument"/> items corresponding to the provided Model, Property, Rule Type, and Name</returns>
        public static IEnumerable<RuleArgument> LookupArguments(this IRuleArgumentConfigProvider cfg, string modelName, string propertyName, RuleType ruleType, string ruleName, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
            => cfg.LookupArguments(modelName, propertyName, ruleType, comparisonType).Where(p => string.Equals(p.Name, ruleName));

        #region Shared.Core.Common.auxfunc
        internal static readonly Func<string, string> appSettingStr =
            k => System.Configuration.ConfigurationManager.AppSettings[k];

        [DebuggerStepThrough]
        internal static T AppSetting<T>(string key) =>
            ReadSetting(key, () => default(T));

        [DebuggerStepThrough]
        internal static T AppSetting<T>(string key, T defaultValue) =>
            ReadSetting(key, () => defaultValue);

        private static T ReadSetting<T>(string key, Func<T> defaultFunc)
        {
            var s = appSettingStr(key);
            if (string.IsNullOrEmpty(s))
                return defaultFunc();

            var o = (T)Convert.ChangeType(s, typeof(T));
            return o;
        }
        #endregion
    }
}
