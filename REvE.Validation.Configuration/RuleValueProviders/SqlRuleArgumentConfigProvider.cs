/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

using REvE.Configuration;
using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration
{
    using Contracts;
    using Models;

    /// <summary>
    /// Retrieves and deserializes a <see cref="RuleArgumentConfig"/> from an external SQL Database.
    /// </summary>
    [Export("sql", typeof(IRuleArgumentConfigProvider))]
    public class SqlRuleArgumentConfigProvider : SqlConfigProvider<RuleArgumentConfig>, IRuleArgumentConfigProvider
    {
        /// <summary>
        /// Instantiates a new class with parameters provided by an IoC Container.
        /// </summary>
        /// <param name="dataSourceKey">The application configuration connection string pointing to the target database.</param>
        public SqlRuleArgumentConfigProvider(string dataSourceKey) : base(dataSourceKey)
        {
        }
    }
}
