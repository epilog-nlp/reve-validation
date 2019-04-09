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
    /// Represents a class as a validated Model with configured Validation Rules for its Properties.
    /// </summary>
    [DataContract]
    public class Model : IComparable
    {
        /// <summary>
        /// The default Constructor.
        /// </summary>
        public Model() { }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="model">The <see cref="Model"/> to be copied.</param>
        public Model(Model model)
        {
            Name = model.Name;
            Alias = model.Alias;
            Properties = new List<Property>(model.Properties);
        }

        /// <summary>
        /// Parameterized Constructor.
        /// </summary>
        /// <param name="name">The name of the Model.</param>
        /// <param name="alias">The optional alias for the Model.</param>
        /// <param name="properties">A collection of validated <see cref="Property"/> elements.</param>
        public Model(string name, string alias = null, IEnumerable<Property> properties = null)
        {
            Name = name;
            Alias = alias;
            Properties = new List<Property>(properties);
        }

        /// <summary>
        /// The name of the Model.
        /// </summary>
        [DataMember, XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The optional Alias for the Model. Represents validations that should only be applied in a given context.
        /// </summary>
        [DataMember, XmlAttribute(AttributeName = "alias")]
        public string Alias { get; set; }

        /// <summary>
        /// A collection of the validated Properties on the Model. Unvalidated Properties are optional.
        /// </summary>
        [DataMember]
        public List<Property> Properties { get; set; } = new List<Property>();

        /// <summary>
        /// The value used externally to lookup this <see cref="Model"/>.
        /// </summary>
        public string LookupName => string.IsNullOrWhiteSpace(Alias) 
            ? Name 
            : Alias;

        /// <summary>
        /// The name of the Model, extended optionally by its Alias.
        /// </summary>
        public string FullName => $"{Name}{(string.IsNullOrWhiteSpace(Alias) ? "" : $".{Alias}")}";

        /// <summary>
        /// Used to determine if all children have been initialized.
        /// </summary>
        public bool LookupComplete = false;

        /// <summary>
        /// <see cref="IComparable.CompareTo(object)"/> method implementation.
        /// </summary>
        /// <param name="obj">Item to compare with the current <see cref="Model"/>.</param>
        /// <returns>An integer value representing the difference between this <see cref="Model"/> and the provided <paramref name="obj"/>.</returns>
        public int CompareTo(object obj)
            => GetHashCode() - obj?.GetHashCode() ?? 0;

        /// <summary>
        /// Override of the default <see cref="object.GetHashCode"/> method.
        /// </summary>
        /// <returns>An integer value representing the Hash of this <see cref="Model"/>.</returns>
        public override int GetHashCode()
            => LookupName.GetHashCode();
    }
}
