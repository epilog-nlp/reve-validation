/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using REvE.Configuration;
using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration
{
    using Contracts;
    using Models;
    using static AppSettings;

    /// <summary>
    /// Retrieves and deserializes a <see cref="RuleArgumentConfig"/> from an external JSON File.
    /// </summary>
    [Export("json", typeof(IRuleArgumentConfigProvider))]
    public sealed class JsonRuleArgumentConfigProvider : JsonConfigProvider<RuleArgumentConfig>, IRuleArgumentConfigProvider
    {
        /// <summary>
        /// Instantiates a new class with parameters provided by an IoC Container.
        /// </summary>
        /// <param name="dataSourceKey">The application configuration key pointing to the JSON source file.</param>
        [ImportingConstructor]
        private JsonRuleArgumentConfigProvider([Import("json-cfg-rules", AllowDefault = true)]string dataSourceKey = JsonRuleValueProviderSourceKey) 
            : base(dataSourceKey) { }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public JsonRuleArgumentConfigProvider() : base(JsonRuleValueSource) { }

    }
}
