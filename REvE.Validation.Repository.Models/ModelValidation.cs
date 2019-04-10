/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.Collections.Generic;

namespace REvE.Repository.Models
{
    /// <summary>
    /// Represents a class (as a Model) and all its validated Properties.
    /// </summary>
    public class ModelValidation
    {
        /// <summary>
        /// The name or alias of the Model.
        /// </summary>
        public string Name => ModelAlias ?? ModelName;

        /// <summary>
        /// The name of the Model.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// An optional, globally unique, name used to validate a Model in a certain context.
        /// </summary>
        public string ModelAlias { get; set; }

        /// <summary>
        /// A collection containing all the Model's validated Properties.
        /// </summary>
        public IEnumerable<PropertyValidation> Properties { get; set; }
    }
}
