/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

namespace REvE.Validation
{
    // Remnants from another conversion approach. May use in the future.
    //internal static class RuleParsers
    //{

    //    internal static EnumDataTypeAttribute ParseEnumDataTypeAttributeParameters(string parameters)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrWhiteSpace(parameters))
    //                throw new ArgumentNullException($"{nameof(parameters)} cannot be null");

    //            if (!ValidatedEnumAttribute.TryGetEnumType(parameters, out var type))
    //                throw new ArgumentException($"No decorated Enum found matching Type/Alias={parameters}. Ensure name is correct and Enum is decorated with ValidatedEnumAttribute(Type,{parameters})");

    //            return new EnumDataTypeAttribute(type);
    //        }
    //        catch (Exception e)
    //        {
    //            throw new ValidationParseException($"Error Instantiating new EnumDataTypeAttribute: {e.Message}", e);
    //        }
    //    }

    //    internal static RangeAttribute ParseRangeAttributeParameters(string parameters)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrWhiteSpace(parameters))
    //                throw new ArgumentNullException($"{nameof(parameters)} cannot be null");

    //            var split = parameters.Split(',', ';');
    //            if (split.Length != 2)
    //                throw new ArgumentException($"Too {(split.Length < 2 ? "few" : "many")} arguments. Expected: 2. Actual: {split.Length}");

    //            if (double.TryParse(split[0], out var doubleMin))
    //            {
    //                if (!double.TryParse(split[1], out var doubleMax))
    //                    throw new ArgumentException($"Type mismatch. Cannot convert to double: {{{split[1]}}}");
    //                return new RangeAttribute(doubleMin, doubleMax);
    //            }
    //            else if (int.TryParse(split[0], out var intMin))
    //            {
    //                if (!int.TryParse(split[1], out var intMax))
    //                    throw new ArgumentException($"Type mismatch. Cannot convert to int: {{{split[1]}}}");
    //                return new RangeAttribute(intMin, intMax);
    //            }
    //            else
    //            {
    //                throw new ArgumentException($"Could not parse {{{parameters}}}. Parameters must contain delimited int or double values. {RangeAttributeExamples}");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw new ValidationParseException($"Error Instantiating new RangeAttribute: {e.Message}", e);
    //        }
    //    }

    //    private const string RangeAttributeExamples = @"Example: ""1,1024"" ""1;1024"" ""1.5,1024.5"" ""1.5;1024.5""";

    //    internal static MaxLengthAttribute ParseMaxLengthAttributeParameters(string parameters)
    //    {
    //        try
    //        {
    //            if (!int.TryParse(parameters, out var max))
    //                throw new ArgumentException($"Type mismatch. Cannot convert to int: {{{parameters}}}");

    //            return new MaxLengthAttribute(max);
    //        }
    //        catch (Exception e)
    //        {
    //            throw new ValidationParseException($"Error Instantiating new MaxLengthAttribute: {e.Message}", e);
    //        }
    //    }

    //    internal static MinLengthAttribute ParseMinLengthAttributeParameters(string parameters)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrWhiteSpace(parameters))
    //                throw new ArgumentNullException($"{nameof(parameters)} cannot be null");

    //            if (!int.TryParse(parameters, out var min))
    //                throw new ArgumentException($"Type mismatch. Cannot convert to int: {{{parameters}}}");

    //            return new MinLengthAttribute(min);
    //        }
    //        catch (Exception e)
    //        {
    //            throw new ValidationParseException($"Error Instantiating new MinLengthAttribute: {e.Message}", e);
    //        }
    //    }

    //    internal static StringLengthAttribute ParseStringLengthAttributeParameters(string parameters)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrWhiteSpace(parameters))
    //                throw new ArgumentNullException($"{nameof(parameters)} cannot be null");

    //            var split = parameters.Split(',', ';');
    //            if (!stringLengthParameterValidation.IsValid(split.Length))
    //                throw new ArgumentException($"Too {(split.Length < 1 ? "few" : "many")} arguments. Expected: 1-2. Actual: {split.Length}");

    //            if (!int.TryParse(split[0], out var first))
    //                throw new ArgumentException($"Type mismatch. Cannot convert to int: {{{split[0]}}}");

    //            int second = -1;

    //            if (split.Length > 1 && !int.TryParse(split[1], out second))
    //                throw new ArgumentException($"Type mismatch. Cannot convert to int: {{{split[1]}}}");

    //            return split.Length > 1
    //                ? new StringLengthAttribute(first)
    //                : new StringLengthAttribute(second) { MinimumLength = first };

    //        }
    //        catch (Exception e)
    //        {
    //            throw new ValidationParseException($"Error Instantiating new StringLengthAttribute: {e.Message}", e);
    //        }
    //    }

    //    private static readonly RangeAttribute stringLengthParameterValidation = new RangeAttribute(1, 2); // Excessive? Of course.

    //    internal static RegularExpressionAttribute ParseRegularExpressionAttributeParameters(string parameters)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrWhiteSpace(parameters))
    //                throw new ArgumentNullException($"{nameof(parameters)} cannot be null");

    //            try
    //            {
    //                Regex.Match("", parameters);
    //                return new RegularExpressionAttribute(parameters);
    //            }
    //            catch (ArgumentException)
    //            {
    //                throw new ArgumentException($"\\{parameters}\\ is not a valid Regular Expression");
    //            }

    //        }
    //        catch (Exception e)
    //        {
    //            throw new ValidationParseException($"Error Instantiating new RegularExpressionAttribute: {e.Message}", e);
    //        }
    //    }
    //}
}
