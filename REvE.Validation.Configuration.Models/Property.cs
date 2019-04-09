/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace REvE.Models
{
    /// <summary>
    /// Represents a Property of a validated Model with optional configured Validation Rules.
    /// </summary>
    [DataContract]
    public class Property : IComparable
    {
        /// <summary>
        /// The default Constructor.
        /// </summary>
        public Property() { }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="property">The <see cref="Property"/> to be copied.</param>
        public Property(Property property)
        {
            Name = property.Name;
            Rules = new List<Rule>(property.Rules);
        }

        /// <summary>
        /// Parameterized Constructor.
        /// </summary>
        /// <param name="name">The name of the Property. Case-Sensitive.</param>
        /// <param name="rules">A collection of <see cref="Rule"/> elements.</param>
        public Property(string name, IEnumerable<Rule> rules)
        {
            Name = name;
            Rules = new List<Rule>(rules);
        }

        /// <summary>
        /// The name of the Property. Case-Sensitive.
        /// </summary>
        [DataMember, XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// A collection of the configured Validation Rules for the Property. Optional if no Rules are configured.
        /// </summary>
        [DataMember]
        public List<Rule> Rules { get; set; } = new List<Rule>();

        /// <summary>
        /// <see cref="IComparable.CompareTo(object)"/> method implementation.
        /// </summary>
        /// <param name="obj">Item to compare with the current <see cref="Property"/>.</param>
        /// <returns>>An integer value representing the difference between this <see cref="Property"/> and the provided <paramref name="obj"/>.</returns>
        public int CompareTo(object obj)
            => GetHashCode() - obj?.GetHashCode() ?? 0;

        /// <summary>
        /// Override of the default <see cref="object.GetHashCode"/> method.
        /// </summary>
        /// <returns>An integer value representing the Hash of this <see cref="Property"/>.</returns>
        public override int GetHashCode()
            => Name?.GetHashCode() ?? int.MinValue;
    }
}
