using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;

namespace REvE.Validation.Configuration.Tests
{
    using Contracts;

    [TestClass]
    public class XmlRuleArgumentConfigProviderTests : RuleArgumentConfigProviderTests<XmlRuleArgumentConfigProvider>
    {
        [Import("xml", typeof(IRuleArgumentConfigProvider))]
        protected override Lazy<XmlRuleArgumentConfigProvider> Configuration { get; set; }
    }
}
