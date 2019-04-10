/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using REvE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace REvE.Validation
{
    using Repository.Models;

    internal static class ValidationUtil
    {
        
        #region Extension Methods
        public static IEnumerable<ValidationErrorDetail> GetValidationResults<TObject>(this PropertyValidation validation, TObject itemToValidate, string displayName = null)
        {
            displayName = displayName ?? itemToValidate?.GetType().Name; // Set displayName to Type of TObject if not provided.
            var results = new Lazy<List<ValidationErrorDetail>>();
            foreach (var rule in validation.ValidationRules)
            {
                try
                {
                    rule.Validate(
                        validation.CallGetter(itemToValidate), // Calls the Property getter on the Model
                        $"{displayName}.{validation.PropertyName}"); // Name that will be displayed in the error message
                }
                catch (ValidationException e)
                {
                    results.Value.Add(new ValidationErrorDetail{
                        ValidationResult = e.ValidationResult,
                        PropertyName = validation.PropertyName,
                        Value = e.Value,
                        Message = e.Message
                    });
                    continue;
                }
            }

            return results.IsValueCreated // If no results, don't instantiate
                ? results.Value
                : new List<ValidationErrorDetail>();
        }

        public static IEnumerable<ValidationErrorDetail> GetValidationResults<TObject>(this ModelValidation validation, TObject itemToValidate)
            => validation.Properties.SelectMany(prop => prop.GetValidationResults(itemToValidate, validation.Name))
                                    .Select(error => 
                                    {
                                        error.ModelName = validation.ModelName;
                                        return error;
                                    });

        public static Result<IEnumerable<ValidationErrorDetail>> Validate<TObject>(this ModelValidation validation, TObject itemToValidate)
        {
            var result = GetValidationResults(validation, itemToValidate);
            return result?.Any() ?? false
                ? Result<IEnumerable<ValidationErrorDetail>>.Error(result)
                : Result<IEnumerable<ValidationErrorDetail>>.Success(result);
        }
        #endregion
    }
}
