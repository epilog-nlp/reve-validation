/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using System;

namespace REvE.Validation
{
    /// <summary>
    /// Exceptions thrown while converting Configuration Models to Repository and Service Models.
    /// </summary>
    public class ValidationParseException : Exception
    {
        /// <summary>
        /// Creates a new instance of the Exception.
        /// </summary>
        public ValidationParseException() : base() { }

        /// <summary>
        /// Creates a new instance of the Exception with the provided <paramref name="message"/>.
        /// </summary>
        /// <param name="message">A message with the Exception details.</param>
        public ValidationParseException(string message) : base(message) { }

        /// <summary>
        /// Creates a new instance of the Exception with the provided <paramref name="message"/> wrapping another Exception.
        /// </summary>
        /// <param name="message">A message with the Exception details.</param>
        /// <param name="innerException">The <see cref="Exception"/> wrapped by the <see cref="ValidationParseException"/>.</param>
        public ValidationParseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
