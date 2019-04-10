/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.ComponentModel.DataAnnotations;

namespace REvE.Models
{
    /// <summary>
    /// Provides detail on the reason a validation failed.
    /// </summary>
    public class ValidationErrorDetail
    {
        /// <summary>
        /// The raw Result from <see cref="System.ComponentModel.DataAnnotations"/>.
        /// </summary>
        public ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// The name of the Model the validation failed on.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// The name of the Property the validation failed on.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The value of the Property provided to the <see cref="ValidationAttribute"/>
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// A message explaining why the validation failed.
        /// </summary>
        public string Message { get; set; }
    }
}
