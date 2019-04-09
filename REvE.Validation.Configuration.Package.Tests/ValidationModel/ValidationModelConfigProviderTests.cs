using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace REvE.Validation.Configuration.Tests
{
    using Models;
    using Contracts;

    [TestClass]
    public abstract class ValidationModelConfigProviderTests<TConfigProvider> : ConfigProviderTests<TConfigProvider, ValidationModelConfig>
        where TConfigProvider : IValidationModelConfigProvider
    {
        private IValidationModelConfigProvider defaultProvider
            => DefaultConfigurationProvider.ModelConfig;

        protected override ValidationModelConfig FakeConfig => defaultProvider.Configuration;

    }
}
