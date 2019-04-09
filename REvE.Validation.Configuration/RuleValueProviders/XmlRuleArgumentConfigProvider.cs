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
    /// Retrieves and deserializes a <see cref="RuleArgumentConfig"/> from an external XML File.
    /// </summary>
    [Export("xml", typeof(IRuleArgumentConfigProvider))]
    public sealed class XmlRuleArgumentConfigProvider : XmlConfigProvider<RuleArgumentConfig>, IRuleArgumentConfigProvider
    {
        /// <summary>
        /// Instantiates a new class with parameters provided by an IoC Container.
        /// </summary>
        /// <param name="dataSourceKey">The application configuration key pointing to the XML source file.</param>
        [ImportingConstructor]
        private XmlRuleArgumentConfigProvider([Import("xml-cfg-rules", AllowDefault = true)]string dataSourceKey = XmlRuleValueProviderSourceKey) 
            : base(dataSourceKey) { }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public XmlRuleArgumentConfigProvider() : base(XmlRuleValueSource) { }
    }
}
