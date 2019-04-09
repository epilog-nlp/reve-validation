/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.Collections.Generic;

namespace REvE.Validation.Configuration.Contracts
{
    using Models;

    /// <summary>
    /// Extends <see cref="REvE.Configuration.IConfigProvider{TResult}"/> with methods for searching the <see cref="ValidationConfig"/>.
    /// </summary>
    public interface IValidationConfigProvider : REvE.Configuration.IConfigProvider<ValidationConfig>
    {
        /// <summary>
        /// Retrieves a <see cref="Model"/> with the give <paramref name="name"/>, and all its Rules.
        /// </summary>
        /// <param name="name">The name of the <see cref="Model"/> to retrieve.</param>
        /// <returns>A <see cref="Model"/> matching the provided <paramref name="name"/>.</returns>
        Model GetRules(string name);

        /// <summary>
        /// Retrieves a <see cref="Model"/> with the given <paramref name="name"/> and <paramref name="alias"/>, and all its Rules.
        /// </summary>
        /// <param name="name">The name of the <see cref="Model"/> to retrieve.</param>
        /// <param name="alias">An optional contextual identifier for the <see cref="Model"/>.</param>
        /// <returns>A <see cref="Model"/> matching the provided <paramref name="name"/> and <paramref name="alias"/>.</returns>
        Model GetRules(string name, string alias);

        /// <summary>
        /// Retrieves all Models with all Rules.
        /// </summary>
        /// <returns>A collection of all Models.</returns>
        IEnumerable<Model> GetRules();
    }
}
