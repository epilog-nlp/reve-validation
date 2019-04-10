/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;

namespace REvE.Validation
{

    /// <summary>
    /// Attribute for marking an <see cref="Enum"/> as discoverable for use in a <see cref="System.ComponentModel.DataAnnotations.EnumDataTypeAttribute"/> validation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public class ValidatedEnumAttribute : Attribute
    {
        /// <summary>
        /// Constructor for creating an instance with the <see cref="Enum"/> Type and an optional alias.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of the decorated <see cref="Enum"/>.</param>
        /// <param name="alias">An optional alias the <see cref="Enum"/> can be discovered as.</param>
        public ValidatedEnumAttribute(Type type, string alias = null)
        {
            typeStore[alias ?? type.Name] = type;
        }

        private static readonly Dictionary<string, Type> typeStore
            = new Dictionary<string, Type>();

        /// <summary>
        /// Retrieves the <see cref="Type"/> of <see cref="Enum"/> matching the provided <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name or alias of the decorated <see cref="Enum"/>.</param>
        /// <returns>The <see cref="Type"/> of the <see cref="Enum"/> matching the provided <paramref name="name"/>.</returns>
        public static Type GetEnumType(string name)
            => typeStore[name];

        /// <summary>
        /// Tries to retrieve the <see cref="Type"/> of the <see cref="Enum"/> matching the provided <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name or alias of the decorated <see cref="Enum"/>.</param>
        /// <param name="type">Will contain the <see cref="Type"/> of the <see cref="Enum"/> matching the provided <paramref name="name"/> if found.</param>
        /// <returns>true if the <paramref name="type"/> is found. Otherwise, false.</returns>
        public static bool TryGetEnumType(string name, out Type type)
            => typeStore.TryGetValue(name, out type);
       
    }
}
