/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;

namespace REvE.Validation.Contracts
{
    using Models;

    /// <summary>
    /// Contract for exposing methods to request Validation Rule summaries.
    /// </summary>
    public interface IValidationRuleProvider
    {
        /// <summary>
        /// Retrieves summaries for Rules matching the provided <paramref name="type"/> and <paramref name="alias"/>.
        /// </summary>
        /// <param name="type">The Type of Model to retrieve Rules for.</param>
        /// <param name="alias">An optional, globally unique, name for the Model to retrieve Rules for.</param>
        /// <returns>A collection containing <see cref="ValidationRule"/> elements matching the provided criteria.</returns>
        IEnumerable<ValidationRule> GetRules(Type type, string alias);

        /// <summary>
        /// Retrieves summaries for Rules matching the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The Type of Model to retrieve Rules for.</param>
        /// <returns>A collection containing <see cref="ValidationRule"/> elements matching the provided criteria.</returns>
        IEnumerable<ValidationRule> GetRules(Type type);

        /// <summary>
        /// Retrieves summaries for Rules matching the provided <paramref name="type"/> and <paramref name="alias"/>.
        /// </summary>
        /// <param name="type">The name of the Model to retrieve Rules for.</param>
        /// <param name="alias">An optional, globally unique, name for the Model to retrieve Rules for.</param>
        /// <returns>A collection containing <see cref="ValidationRule"/> elements matching the provided criteria.</returns>
        IEnumerable<ValidationRule> GetRules(string type, string alias);

        /// <summary>
        /// Retrieves summaries for Rules matching the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The name of the Model to retrieve Rules for.</param>
        /// <returns>A collection containing <see cref="ValidationRule"/> elements matching the provided criteria.</returns>
        IEnumerable<ValidationRule> GetRules(string type);

        /// <summary>
        /// Retrieves summaries for all configured Rules.
        /// </summary>
        /// <returns>A collection containing all configured <see cref="ValidationRule"/> elements.</returns>
        IEnumerable<ValidationRule> GetRules();

    }
}
