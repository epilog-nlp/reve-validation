using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace REvE.Validation.Tests
{
    using Models.Mock;
    using Contracts;
    [TestClass]
    public class ValidationRuleProviderTests
    {
        public IValidationRuleProvider RuleProvider => ValidationRepoTests.Repo;

        [DataTestMethod]
        [DataRow("company", 5)]
        [DataRow("Company", 5)]
        [DataRow("BusinessUnit", 0)]
        [DataRow("businessUnit", 0)]
        [DataRow("Businessunit", 0)]
        [DataRow("businessunit", 0)]
        [DataRow("not", 0)]
        public void GetRules_ByName_Test(string name, int count)
        {
            var result = RuleProvider.GetRules(name);
            Assert.AreEqual(result?.Count() ?? 0, count);
        }

        [DataTestMethod]
        [DataRow("businessUnit", "BU", 5)]
        [DataRow("businessUnit", "bu", 5)]
        [DataRow("Businessunit", "bu", 5)]
        [DataRow("businessunit", "bu", 5)]
        public void GetRules_ByNameWithAlias_Test(string name, string alias, int count)
        {
            var result = RuleProvider.GetRules(name, alias);
            Assert.AreEqual(result?.Count() ?? 0, count);
        }

        [DataTestMethod]
        [DataRow(typeof(Company), 5)]
        [DataRow(typeof(BusinessUnit), 0)]
        [DataRow(typeof(ValidationRuleProviderTests), 0)]
        public void GetRules_ByType_Test(Type type, int count)
        {
            var result = RuleProvider.GetRules(type);
            Assert.AreEqual(result?.Count() ?? 0, count);
        }

        [DataTestMethod]
        [DataRow(typeof(Company), "alias", 0)]
        [DataRow(typeof(BusinessUnit), "bu", 5)]
        [DataRow(typeof(BusinessUnit), "something", 0)]
        [DataRow(typeof(BusinessUnit), "BU", 5)]
        [DataRow(typeof(ValidationRuleProviderTests), "abc", 0)]
        public void GetRules_ByTypeWithAlias_Test(Type type, string alias, int count)
        {
            var result = RuleProvider.GetRules(type, alias);
            Assert.AreEqual(result?.Count() ?? 0, count);
        }

        [TestMethod]
        public void GetRules_Test()
        {
            var result = RuleProvider.GetRules();
            Assert.AreEqual(result.Count(), 10);
        }
    }
}
