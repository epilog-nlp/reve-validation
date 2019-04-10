/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace REvE.Repository.Models
{
    /// <summary>
    /// Represents a single Property of a <see cref="ModelValidation"/> and all its validation rules.
    /// </summary>
    public class PropertyValidation
    {
        /// <summary>
        /// Name of the validated Property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The cached Get Accessor for the Property.
        /// </summary>
        public MethodInfo PropertyGetter { get; set; }

        /// <summary>
        /// A collection of cached validations that apply to the Property.
        /// </summary>
        public IEnumerable<ValidationAttribute> ValidationRules { get; set; }

        /// <summary>
        /// Utility method for calling the <see cref="PropertyGetter"/> on an instance of the parent Model.
        /// </summary>
        /// <param name="model">An instance of parent Model.</param>
        /// <returns>The result of invoking the <see cref="PropertyGetter"/> on the provided instance.</returns>
        public object CallGetter(object model)
            => PropertyGetter?.Invoke(model, null);
    }
}
