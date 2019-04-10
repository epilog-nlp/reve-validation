/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.Collections.Generic;

namespace REvE.Validation.Contracts
{
    using Models;

    /// <summary>
    /// Contract for exposing methods to validate the contents of a provided Model.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validates a provided <paramref name="model"/> against the configured Rules.
        /// </summary>
        /// <typeparam name="TModel">The <see cref="System.Type"/> to interpret the <paramref name="model"/> as. Can be implied unless validating as a base type.</typeparam>
        /// <param name="model">The <typeparamref name="TModel"/> instance to Validate.</param>
        /// <returns>A Result wrapping a collection of Error Details for all the failed validations.</returns>
        Result<IEnumerable<ValidationErrorDetail>> Validate<TModel>(TModel model);

        /// <summary>
        /// Validates a provided <paramref name="model"/> against the Rules configured for the <paramref name="alias"/>.
        /// </summary>
        /// <typeparam name="TModel">The <see cref="System.Type"/> to interpret the <paramref name="model"/> as. Can be implied unless validating as a base type.</typeparam>
        /// <param name="model">The <typeparamref name="TModel"/> instance to Validate.</param>
        /// <param name="alias">The contextual name of the <paramref name="model"/> to validate against.</param>
        /// <returns>A Result wrapping a collection of Error Details for all the failed validations.</returns>
        Result<IEnumerable<ValidationErrorDetail>> Validate<TModel>(TModel model, string alias);

    }
}
