using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REvE.Validation.Configuration.Tests
{
    using Configuration.Contracts;
    using REvE.Configuration;
    using REvE.Models;

    internal class FakeConfigProvider : IValidationModelConfigProvider, IRuleArgumentConfigProvider
    {
        internal FakeConfigProvider(ValidationModelConfig modelConfig, RuleArgumentConfig parameterConfig)
        {
            ModelConfig = modelConfig;
            ParameterConfig = parameterConfig;
        }

        internal ValidationModelConfig ModelConfig { get; }

        internal RuleArgumentConfig ParameterConfig { get; }

        ValidationModelConfig IConfigProvider<ValidationModelConfig>.Configuration => ModelConfig;

        RuleArgumentConfig IConfigProvider<RuleArgumentConfig>.Configuration => ParameterConfig;
    }
}
