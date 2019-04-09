using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration.Tests
{
    using Models;
    using Contracts;
    using static Assertions;

    [TestClass]
    public class ValidationConfigProviderTests : ConfigProviderTests<ValidationConfigProvider, ValidationConfig>
    {
        public ValidationConfigProviderTests()
        {
            FakeConfig = new ValidationConfig
            {
                ModelProvider = DefaultConfigurationProvider.ModelConfig,
                RuleValueProvider = DefaultConfigurationProvider.ParameterConfig
            };
        }

        protected override ValidationConfig FakeConfig { get; }

        [Import(typeof(IValidationConfigProvider))]
        protected override Lazy<ValidationConfigProvider> Configuration { get; set; }

        [TestMethod]
        public void GetRulesByName_Test()
        {
            var result = Configuration.Value.GetRules("company");
            var fake = FakeConfig.ModelProvider.Configuration.Models.Where(m => m.Name.Equals("company")).Single();
            Compare(result, fake, true);
        }

        [TestMethod]
        public void GetRulesByAlias_Test()
        {
            var result = Configuration.Value.GetRules("businessunit", "bu");
            var fake = FakeConfig.ModelProvider.Configuration.Models.Where(m => m.Name.Equals("businessunit")).Single();
            Compare(result, fake, true);
        }
    }

}
