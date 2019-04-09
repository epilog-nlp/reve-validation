using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace REvE.Validation.Configuration.Tests
{
    using Models;
    using Contracts;

    [TestClass]
    public abstract class RuleArgumentConfigProviderTests<TConfigProvider> : ConfigProviderTests<TConfigProvider, RuleArgumentConfig>
        where TConfigProvider : IRuleArgumentConfigProvider
    {
        private IRuleArgumentConfigProvider defaultProvider
            => DefaultConfigurationProvider.ParameterConfig;

        protected override RuleArgumentConfig FakeConfig => defaultProvider.Configuration;
    }
}
