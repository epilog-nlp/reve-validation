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
    /// Retrieves and deserializes a <see cref="ValidationModelConfig"/> from an external XML File.
    /// </summary>
    [Export("xml", typeof(IValidationModelConfigProvider))]
    public sealed class XmlValidationModelConfigProvider 
        : XmlConfigProvider<ValidationModelConfig>, IValidationModelConfigProvider
    {
        /// <summary>
        /// Instantiates a new class with parameters provided by an IoC Container.
        /// </summary>
        /// <param name="dataSourceKey">The application configuration key pointing to the XML source file.</param>
        [ImportingConstructor]
        private XmlValidationModelConfigProvider([Import("xml-cfg-models", AllowDefault = true)] string dataSourceKey = XmlModelProviderSourceKey) 
            : base(dataSourceKey) { }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public XmlValidationModelConfigProvider() : base(XmlModelSource) { }
    }
}
