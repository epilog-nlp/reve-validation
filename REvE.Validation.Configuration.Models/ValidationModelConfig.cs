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
    /// Represents the deserialized hierarchy of validated Models, Properties, and their Validation Rules defined in the <see cref="Model"/> configuration source.
    /// </summary>
    /// <remarks>
    /// Does not include the <see cref="RuleArgument"/> elements defined in their own source. These must be bound to the <see cref="Rule"/> elements directly.
    /// </remarks>
    [DataContract, Serializable, XmlType(AnonymousType = true)]
    public class ValidationModelConfig
    {
        /// <summary>
        /// The complete hierarchy of validated <see cref="Model"/> elements defined in the configuration source.
        /// </summary>
        [DataMember]
        public List<Model> Models { get; set; } = new List<Model>();

    }
}
