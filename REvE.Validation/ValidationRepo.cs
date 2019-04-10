/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using REvE.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static REvE.Configuration.ConfigUtil;

namespace REvE.Validation
{
    using Configuration.Contracts;
    using Contracts;
    using Models;
    using Repository.Models;


    /// <summary>
    /// Singleton Implementation of the <see cref="IValidationRepo"/> Contract. Exposes methods for retrieving configured rules and validating instances against them.
    /// </summary>
    public sealed class ValidationRepo : IValidator, IValidationRuleProvider, IValidationRepo
    {
        private static readonly Lazy<ValidationRepo> instance = new Lazy<ValidationRepo>(() => new ValidationRepo());

        /// <summary>
        /// The singleton instance of the <see cref="ValidationRepo"/>. Exports <see cref="IValidator"/>, <see cref="IValidationRuleProvider"/>, and <see cref="IValidationRepo"/>.
        /// </summary>
        [Export(typeof(IValidator)), Export(typeof(IValidationRuleProvider)), Export(typeof(IValidationRepo))]
        public static ValidationRepo Instance => instance.Value;

        private ValidationRepo() { }

        #region Configuration
        private readonly Lazy<IValidationConfigProvider> configuration
            = new Lazy<IValidationConfigProvider>(ResolveConfig);

        private IValidationConfigProvider Config => configuration.Value;

        private static IValidationConfigProvider ResolveConfig()
            => ConfigFactory.GetConfig<IValidationConfigProvider, ValidationConfig>(cfgContract.Value);

        private static readonly Lazy<string> cfgContract
            = new Lazy<string>(() => AppSetting("validation-config-contract", "real"));

        #endregion

        #region Delegate Maps
        private static readonly ConcurrentDictionary<Type, ModelValidation> cachedValidationsByType
            = new ConcurrentDictionary<Type, ModelValidation>();

        private static readonly ConcurrentDictionary<string, ModelValidation> cachedValidationsByName
            = new ConcurrentDictionary<string, ModelValidation>();

        #endregion

        #region Utility
        private ModelValidation GetValidations(Type type)
            => cachedValidationsByType.GetOrAdd(type, LookupValidations(type));

        private ModelValidation GetValidations(Type type, string alias)
            => cachedValidationsByName.GetOrAdd(alias, LookupValidations(type, alias));
        
        private ModelValidation LookupValidations(Type type) => Config.GetRules(type.Name).ToRepo(type);

        private ModelValidation LookupValidations(Type type, string alias) => Config.GetRules(type.Name, alias).ToRepo(type);

        #endregion

        #region IValidationRuleProvider Implementations

        /// <summary>
        /// Retrieves summaries for Rules matching the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The Type of Model to retrieve Rules for.</param>
        /// <returns>A collection containing <see cref="ValidationRule"/> elements matching the provided criteria.</returns>
        public IEnumerable<ValidationRule> GetRules(Type type) => Config.GetRules(type.Name).ToService();

        /// <summary>
        /// Retrieves summaries for Rules matching the provided <paramref name="type"/> and <paramref name="alias"/>.
        /// </summary>
        /// <param name="type">The Type of Model to retrieve Rules for.</param>
        /// <param name="alias">An optional, globally unique, name for the Model to retrieve Rules for.</param>
        /// <returns>A collection containing <see cref="ValidationRule"/> elements matching the provided criteria.</returns>
        public IEnumerable<ValidationRule> GetRules(Type type, string alias) => Config.GetRules(type.Name, alias).ToService();

        /// <summary>
        /// Retrieves summaries for Rules matching the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The name of the Model to retrieve Rules for.</param>
        /// <returns>A collection containing <see cref="ValidationRule"/> elements matching the provided criteria.</returns>
        public IEnumerable<ValidationRule> GetRules(string type) => Config.GetRules(type).ToService();

        /// <summary>
        /// Retrieves summaries for Rules matching the provided <paramref name="type"/> and <paramref name="alias"/>.
        /// </summary>
        /// <param name="type">The name of the Model to retrieve Rules for.</param>
        /// <param name="alias">An optional, globally unique, name for the Model to retrieve Rules for.</param>
        /// <returns>A collection containing <see cref="ValidationRule"/> elements matching the provided criteria.</returns>
        public IEnumerable<ValidationRule> GetRules(string type, string alias) => Config.GetRules(type, alias).ToService();

        /// <summary>
        /// Retrieves summaries for all configured Rules.
        /// </summary>
        /// <returns>A collection containing all configured <see cref="ValidationRule"/> elements.</returns>
        public IEnumerable<ValidationRule> GetRules() => Config.GetRules().ToService();

        #endregion

        #region IValidator Implementations

        /// <summary>
        /// Validates a provided <paramref name="model"/> against the configured Rules.
        /// </summary>
        /// <typeparam name="TModel">The <see cref="System.Type"/> to interpret the <paramref name="model"/> as. Can be implied unless validating as a base type.</typeparam>
        /// <param name="model">The <typeparamref name="TModel"/> instance to Validate.</param>
        /// <returns>A Result wrapping a collection of Error Details for all the failed validations.</returns>
        public Result<IEnumerable<ValidationErrorDetail>> Validate<TModel>(TModel model)
        {
            if (model == null)
                return Result<IEnumerable<ValidationErrorDetail>>.Success();
            return GetValidations(typeof(TModel))?.Validate(model) ?? Result<IEnumerable<ValidationErrorDetail>>.Success();
        }

        /// <summary>
        /// Validates a provided <paramref name="model"/> against the Rules configured for the <paramref name="alias"/>.
        /// </summary>
        /// <typeparam name="TModel">The <see cref="System.Type"/> to interpret the <paramref name="model"/> as. Can be implied unless validating as a base type.</typeparam>
        /// <param name="model">The <typeparamref name="TModel"/> instance to Validate.</param>
        /// <param name="alias">The contextual name of the <paramref name="model"/> to validate against.</param>
        /// <returns>A Result wrapping a collection of Error Details for all the failed validations.</returns>
        public Result<IEnumerable<ValidationErrorDetail>> Validate<TModel>(TModel model, string alias)
        {
            if (model == null)
                return Result<IEnumerable<ValidationErrorDetail>>.Success();
            return GetValidations(typeof(TModel), alias)?.Validate(model) ?? Result<IEnumerable<ValidationErrorDetail>>.Success();
        }

        #endregion

    }
}
