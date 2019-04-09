using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration.Tests
{
    using REvE.Configuration;
    using static Assertions;

    [TestClass]
    public abstract class ConfigProviderTests<TConfigProvider, TModel> where TConfigProvider : IConfigProvider<TModel>
    {
        protected abstract TModel FakeConfig { get; }

        protected ConfigProviderTests()
        {
            container.SatisfyImportsOnce(this);
        }

        private CompositionContainer container = new CompositionContainer(new DirectoryCatalog("."));

        protected abstract Lazy<TConfigProvider> Configuration { get; set; }

        protected TConfigProvider Config => Configuration.Value;

        protected DefaultConfigFactory DefaultConfigurationProvider = new DefaultConfigFactory();

        [TestMethod]
        public virtual void ValidateLazyInstantiation()
        {
            var a = Config;
            var b = FakeConfig;
        }

        [TestMethod]
        public virtual void CompareToDefault() => Compare(Config.Configuration, FakeConfig);

        [TestMethod]
        public virtual void ValidateComparison() => Compare(FakeConfig, FakeConfig);

        [ClassCleanup]
        public void Cleanup()
        {
            container.Dispose();
        }
    }
}
