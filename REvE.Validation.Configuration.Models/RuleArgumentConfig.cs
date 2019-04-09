/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace REvE.Models
{
    /// <summary>
    /// Represents the complete, deserialized contents of the <see cref="RuleArgument"/> configuration source.
    /// </summary>
    [Serializable, XmlType(AnonymousType = true), XmlRoot(Namespace = "", IsNullable = false), DataContract]
    public class RuleArgumentConfig
    {
        /// <summary>
        /// A collection of all configured <see cref="RuleArgument"/> elements.
        /// </summary>
        [XmlArrayItem("Def", IsNullable = true), DataMember]
        public List<RuleArgument> RuleDefinitions { get; set; } = new List<RuleArgument>();
    }
}
