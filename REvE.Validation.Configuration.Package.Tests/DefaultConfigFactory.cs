using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REvE.Validation.Configuration.Tests
{
    using Contracts;
    using Models;
    using static GenerationUtil;
    public class DefaultConfigFactory
    {
        #region Creation
        public DefaultConfigFactory()
        {
            foreach (var model in modelConfig.Value.Models)
                foreach (var prop in model.Properties)
                    foreach (var rule in prop.Rules)
                        rule.Arguments = GetDefaultArguments(model.Name, prop.Name, rule.Type, rule.Name).ToList();
        }

        #endregion

        #region Lazy Backing
        private Lazy<ValidationModelConfig> modelConfig
            = new Lazy<ValidationModelConfig>(() => new ValidationModelConfig
            {
                Models = FakeData.Models.Select(model => GenerateModel(model.name, model.alias)
                                        .AddProperties(model.properties))
                                        .ToList()
            });

        private Lazy<RuleArgumentConfig> parameterConfig
            = new Lazy<RuleArgumentConfig>(() => new RuleArgumentConfig
            {
                RuleDefinitions = FakeData.Models
                    .SelectMany(m => m.properties
                        .SelectMany(p => p.rules
                            .SelectMany(r => GetDefaultArguments(m.name, p.propertyName, r.type, r.ruleName))))
                    .ToList()
            });


        //private static Lazy<RuleParameterConfig> parameterConfig
        //    = new Lazy<RuleParameterConfig>(() => new RuleParameterConfig
        //    {
        //        RuleParameters = FakeData.Models
        //            .SelectMany(m =>
        //            {
        //                var t1 = m;
        //                return m.properties
        //                .SelectMany(p =>
        //                {
        //                    var p1 = p;
        //                    return p.rules
        //                      .SelectMany(r =>
        //                      {
        //                          var r1 = r;
        //                          return GetDefaultArguments(m.name, p.propertyName, r.type, r.ruleName);
        //                      });
        //                });
        //            })
        //            .ToList()
        //    });

        private FakeConfigProvider FakeConfig
            => new FakeConfigProvider(modelConfig.Value, parameterConfig.Value);

        private ValidationConfig validationConfig
            => new ValidationConfig
            {
                ModelProvider = FakeConfig,
                RuleValueProvider = FakeConfig
            };
        #endregion

        #region Exposed
        public IValidationModelConfigProvider ModelConfig => FakeConfig;

        public IRuleArgumentConfigProvider ParameterConfig => FakeConfig;


        #endregion
    }
}
