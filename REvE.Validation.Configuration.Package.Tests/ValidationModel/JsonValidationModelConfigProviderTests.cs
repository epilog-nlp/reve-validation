using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration.Tests
{
    using Contracts;
    using Models;

    [TestClass]
    public class JsonValidationModelConfigProviderTests : ValidationModelConfigProviderTests<JsonValidationModelConfigProvider>
    {
        [Import("json", typeof(IValidationModelConfigProvider))]
        protected override Lazy<JsonValidationModelConfigProvider> Configuration { get; set; }

        
    }
}
