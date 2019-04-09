using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration.Tests
{
    using Contracts;

    [TestClass]
    public class JsonRuleArgumentConfigProviderTests : RuleArgumentConfigProviderTests<JsonRuleArgumentConfigProvider>
    {
        [Import("json", typeof(IRuleArgumentConfigProvider))]
        protected override Lazy<JsonRuleArgumentConfigProvider> Configuration { get; set; }
    }
}
