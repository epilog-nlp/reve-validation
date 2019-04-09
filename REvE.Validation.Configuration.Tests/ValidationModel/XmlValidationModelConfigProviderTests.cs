using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration.Tests
{
    using Models;
    using Contracts;

    [TestClass]
    public class XmlValidationModelConfigProviderTests : ValidationModelConfigProviderTests<XmlValidationModelConfigProvider>
    {
        [Import("xml", typeof(IValidationModelConfigProvider))]
        protected override Lazy<XmlValidationModelConfigProvider> Configuration { get; set; }
    }
}
