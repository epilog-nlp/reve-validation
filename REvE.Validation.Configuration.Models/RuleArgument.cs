/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace REvE.Models
{
    /// <summary>
    /// Represents a required parameter argument for a configured Validation Rule.
    /// </summary>
    [DataContract]
    public class RuleArgument : IComparable
    {
        /// <summary>
        /// The unparsed text retrieved from the configuration representing the Model, Property, Argument, and Value.
        /// </summary>
        [DataMember, XmlText]
        public string Rule { get; set; }

        /// <summary>
        /// The name of the Configured Rule. Corresponds to <see cref="Rule.Name"/>.
        /// </summary>
        [DataMember, XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The type of Validation Rule. Corresponds to <see cref="Rule.Type"/>.
        /// </summary>
        [DataMember, XmlAttribute(AttributeName = "type")]
        public RuleType Type { get; set; }

        /// <summary>
        /// The portion of the <see cref="Rule"/> used to determine the Model, Property, and Argument names.
        /// </summary>
        public string FullName => Rule.Split(valueDelimiters)?[0] ?? "";
        
        /// <summary>
        /// The portion of the <see cref="FullName"/> used to determine the Model. Corresponds to <see cref="Model.LookupName"/>.
        /// </summary>
        /// <remarks>
        /// If the <see cref="Rule"/> is configured for an Aliased Model, this value should correspond to <see cref="Model.Alias"/> instead.
        /// </remarks>
        public string Model => FullName.Split(nameDelimiters)?[0] ?? "";

        /// <summary>
        /// The portion of the <see cref="FullName"/> used to determine the corresponding <see cref="Models.Property"/> on the <see cref="Models.Model"/>.
        /// </summary>
        public string Property => FullName.Split(nameDelimiters)?[1] ?? "";

        /// <summary>
        /// The portion of the <see cref="FullName"/> used to determine the name of the argument being configured.
        /// </summary>
        public string Parameter => FullName.Split(nameDelimiters)?[2] ?? "";

        /// <summary>
        /// The portion of the <see cref="Rule"/> representing the argument's configured value.
        /// </summary>
        public string Value => Rule.Split(valueDelimiters)?[1] ?? "";

        private static readonly char[] nameDelimiters = new[] { ',', '.', ';', '_', '-' };
        private static readonly char[] valueDelimiters = new[] { '=', ':' };

        /// <summary>
        /// <see cref="IComparable.CompareTo(object)"/> method implementation.
        /// </summary>
        /// <param name="obj">Item to compare with the current <see cref="RuleArgument"/>.</param>
        /// <returns>An integer value representing the difference between this <see cref="RuleArgument"/> and the provided <paramref name="obj"/>.</returns>
        public int CompareTo(object obj) 
            => GetHashCode() - obj?.GetHashCode() ?? 0;

        /// <summary>
        /// Override of the default <see cref="object.GetHashCode"/> method.
        /// </summary>
        /// <returns>An integer value representing the Hash of this <see cref="RuleArgument"/>.</returns>
        public override int GetHashCode()
            => Type.GetHashCode()
            + Name?.GetHashCode() ?? int.MinValue
            + Rule?.GetHashCode() ?? int.MinValue;
    }
}