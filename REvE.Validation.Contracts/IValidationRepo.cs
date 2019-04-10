/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

namespace REvE.Validation.Contracts
{
    /// <summary>
    /// Unified contract for exposing all <see cref="IValidationRuleProvider"/> and <see cref="IValidator"/> methods.
    /// </summary>
    public interface IValidationRepo : IValidationRuleProvider, IValidator
    {
    }
}
