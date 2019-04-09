/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace REvE.Models
{
    /// <summary>
    /// Represents a configured Validation Rule for a Property.
    /// </summary>
    [DataContract]
    public class Rule : IComparable
    {
        /// <summary>
        /// The default Constructor.
        /// </summary>
        public Rule() { }

        /// <summary>
        /// Extended Parameterized Constructor.
        /// </summary>
        /// <param name="type">The <see cref="RuleType"/> of the configured Validation Rule.</param>
        /// <param name="name">The name of the configured Validation Rule.</param>
        /// <param name="parameters">A collection of the arguments used to initialize the Validation Rule. Optional if the <see cref="Rule"/> has no arguments.</param>
        public Rule(RuleType type, string name, IEnumerable<RuleArgument> parameters = null) : this(type, parameters)
        {
            Name = name;
        }

        /// <summary>
        /// Parameterized Constructor.
        /// </summary>
        /// <param name="type">The <see cref="RuleType"/> of the configured Validation Rule.</param>
        /// <param name="parameters">A collection of the arguments used to initialize the Validation Rule. Optional if the <see cref="Rule"/> has no arguments.</param>
        public Rule(RuleType type, IEnumerable<RuleArgument> parameters = null)
        {
            Type = type;
            Arguments = parameters?.ToList();
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="rule">The <see cref="Rule"/> to be copied.</param>
        public Rule(Rule rule)
        {
            Type = rule.Type;
            Arguments = rule.Arguments;
        }

        /// <summary>
        /// The <see cref="RuleType"/> of the configured Validation Rule.
        /// </summary>
        [DataMember, XmlAttribute(AttributeName = "type")]
        public RuleType Type { get; set; }

        /// <summary>
        /// The name of the configured Validation Rule. Only required when disambiguating Rules.
        /// </summary>
        [DataMember, XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// A collection of <see cref="RuleArgument"/> elements representing the Validation Rule's arguments. Optional for Rule Types without arguments.
        /// </summary>
        public List<RuleArgument> Arguments { get; set; } = new List<RuleArgument>();

        /// <summary>
        /// An optional Error Message to be returned when the Validation fails. If not provided, a default value will be used.
        /// </summary>
        [DataMember(Name = "Error"), XmlElement(ElementName = "Error")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// An optional Technical Description used to describe the Validation Rule's intent. If not provided, a default value will be used.
        /// </summary>
        [DataMember(Name = "Technical"), XmlElement(ElementName = "Technical")]
        public string TechnicalDescription { get; set; }

        /// <summary>
        /// An optional Friendly Description used to describe the Validation Rule's intent. If not provided, a default value will be used.
        /// </summary>
        [DataMember(Name = "Friendly"), XmlElement(ElementName = "Friendly")]
        public string FriendlyDescription { get; set; }

        /// <summary>
        /// <see cref="IComparable.CompareTo(object)"/> method implementation.
        /// </summary>
        /// <param name="obj">Item to compare with the current <see cref="Rule"/>.</param>
        /// <returns>An integer value representing the difference between this <see cref="Rule"/> and the provided <paramref name="obj"/>.</returns>
        public int CompareTo(object obj)
            => GetHashCode() - obj?.GetHashCode() ?? 0;

        /// <summary>
        /// Override of the default <see cref="object.GetHashCode"/> method.
        /// </summary>
        /// <returns>An integer value representing the Hash of this <see cref="Rule"/>.</returns>
        public override int GetHashCode()
            => Type.GetHashCode()
            + Name?.GetHashCode() ?? int.MinValue
            + ErrorMessage?.GetHashCode() ?? int.MinValue
            + TechnicalDescription?.GetHashCode() ?? int.MinValue
            + FriendlyDescription?.GetHashCode() ?? int.MinValue;
    }

}
