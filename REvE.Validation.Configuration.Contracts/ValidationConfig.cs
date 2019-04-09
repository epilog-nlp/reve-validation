/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

namespace REvE.Models
{
    using Validation.Configuration.Contracts;

    /// <summary>
    /// A unified Configuration composed of <see cref="IValidationModelConfigProvider"/> and <see cref="IRuleArgumentConfigProvider"/>.
    /// </summary>
    public class ValidationConfig
    {
        /// <summary>
        /// The provider for retrieving Validation Model Configuration details.
        /// </summary>
        public IValidationModelConfigProvider ModelProvider { get; set; }

        /// <summary>
        /// The provider for retrieving the Rule Parameters associated with the Validation Models.
        /// </summary>
        public IRuleArgumentConfigProvider RuleValueProvider { get; set; }

    }
}
