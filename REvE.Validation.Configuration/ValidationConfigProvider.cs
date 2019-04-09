/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration
{
    using Contracts;
    using REvE.Configuration;
    using Models;
    using static AppSettings;

    /// <summary>
    /// Implementation of <see cref="IValidationConfigProvider"/>. 
    /// Unifies the results from <see cref="IValidationModelConfigProvider"/> and <see cref="IRuleArgumentConfigProvider"/>.
    /// </summary>
    [Export("real", typeof(IValidationConfigProvider))]
    [Export(typeof(IValidationConfigProvider))]
    public sealed class ValidationConfigProvider : IValidationConfigProvider
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ValidationConfigProvider()
        {
            Configuration = new ValidationConfig
            {
                ModelProvider = ConfigFactory.GetConfig<IValidationModelConfigProvider, ValidationModelConfig>(ModelProviderContract),
                RuleValueProvider = ConfigFactory.GetConfig<IRuleArgumentConfigProvider, RuleArgumentConfig>(RuleValueProviderContract)
            };

            lookupCache = Models.Configuration.Models.ToDictionary(
                    k => k.FullName.ToLowerInvariant(), v => v);
        }

        private readonly Dictionary<string, Model> lookupCache;

        /// <summary>
        /// The exposed <see cref="ValidationConfig"/>.
        /// </summary>
        public ValidationConfig Configuration { get; set; }

        /// <summary>
        /// Exposes the resolved <see cref="IValidationModelConfigProvider"/>.
        /// </summary>
        public IValidationModelConfigProvider Models => Configuration.ModelProvider;

        /// <summary>
        /// Exposes the resolved <see cref="IRuleArgumentConfigProvider"/>.
        /// </summary>
        public IRuleArgumentConfigProvider Arguments => Configuration.RuleValueProvider;

        /// <summary>
        /// Retrieves a <see cref="Model"/> with the provided <paramref name="name"/> and <paramref name="alias"/>.
        /// Populates all rules if they haven't been already.
        /// </summary>
        /// <param name="name">Name of the Model.</param>
        /// <param name="alias">Contextual identifier for the Model.</param>
        /// <returns>The <see cref="Model"/> matching the provided <paramref name="name"/> and <paramref name="alias"/>.</returns>
        public Model GetRules(string name, string alias)
            => GetRules($"{name.ToLowerInvariant()}.{alias}");

        /// <summary>
        /// Retrieves a <see cref="Model"/> with the provided <paramref name="name"/>.
        /// Populates all rules if they haven't been already.
        /// </summary>
        /// <param name="name">Name of the Model.</param>
        /// <returns>The <see cref="Model"/> matching the provided <paramref name="name"/>.</returns>
        public Model GetRules(string name)
        {
            if (!lookupCache.TryGetValue(name.ToLowerInvariant(), out var model))
                return null;

            return model.LookupComplete
                ? model
                : PopulateRules(model);
        }

        /// <summary>
        /// Ties all associated <see cref="RuleArgument"/> values to the matching <see cref="Rule"/> for the provided <paramref name="model"/>.
        /// </summary>
        /// <param name="model">The <see cref="Model"/> to populate rules on.</param>
        /// <returns>The completed <see cref="Model"/>.</returns>
        private Model PopulateRules(Model model)
        {
            if (model == null)
            {
                throw new System.ArgumentNullException(nameof(model));
            }

            foreach (var prop in model.Properties)
            {
                if(prop.Rules != null)
                    foreach (var rule in prop.Rules)
                    {
                        rule.Arguments = Arguments.LookupArguments(model.Alias ?? model.Name, prop.Name, rule.Type, rule.Name).ToList();
                    }
            }
            model.LookupComplete = true;
            return model;
        }

        /// <summary>
        /// Retrieves all defined Models with rules.
        /// </summary>
        /// <returns>A collection containing all defined Models with rules.</returns>
        public IEnumerable<Model> GetRules() 
            => Models.Configuration.Models.Select(m => m.LookupComplete ? m : PopulateRules(m));
    }
}
