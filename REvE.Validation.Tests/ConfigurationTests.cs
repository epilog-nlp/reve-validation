//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Linq;

//namespace REvE.Framework.Validations.Tests
//{
//    using Models;
//    using Configuration.Contracts;

//    [TestClass]
//    public class ConfigurationTests
//    {
//        private static readonly Lazy<IValidationModelConfigProvider> cfg
//            = new Lazy<IValidationModelConfigProvider>(() => ValidationRepo.Instance.Config);
//        internal static IValidationModelConfigProvider Config => cfg.Value;

//        [TestMethod]
//        public void Config_Generation()
//        {
//            Assert.IsNotNull(Config);
//            CollectionAssert.AllItemsAreNotNull(Config.Models.ToList());
//        }

//        [TestMethod]
//        public void Config_Model_GetByName()
//        {
//            var configModel = Config.Models[typeof(Company).Name.ToLowerInvariant()];
//            Assert.IsNotNull(configModel);
//            configModel = Config.Models[typeof(BusinessUnit).Name.ToLowerInvariant()];
//            Assert.IsNotNull(configModel);
//        }

//        [TestMethod]
//        public void Config_Model_GetByAlias()
//        {
//            var aliasModel = Config.Models["bu"];
//            var typeModel = Config.Models[typeof(BusinessUnit).Name.ToLowerInvariant()];
//            Assert.AreSame(aliasModel, typeModel);
//        }

//        [TestMethod]
//        public void Config_Model_GetPropertiesForCompany()
//        {
//            var configModel = Config.Models[typeof(Company).Name.ToLowerInvariant()];
//            var props = configModel.Properties.Select(prop => typeof(Company).GetProperty(prop.Name)).ToList();
//            CollectionAssert.AllItemsAreNotNull(props);
//            CollectionAssert.AllItemsAreUnique(props);
//            var company = new Company(1, "CompanyName", "Company Desc");
//            var result = props.Select(prop => prop.GetMethod.Invoke(company, null)).ToList();
//            CollectionAssert.AllItemsAreUnique(result);
//        }

//        [TestMethod]
//        public void Config_Model_GetPropertiesForBusinessUnit()
//        {
//            var configModel = Config.Models["bu"];
//            var props = configModel.Properties.Select(prop => typeof(BusinessUnit).GetProperty(prop.Name)).ToList();
//            CollectionAssert.AllItemsAreNotNull(props);
//            CollectionAssert.AllItemsAreUnique(props);
//            var bu = new BusinessUnit(1, "BuName", "BusinessUnit Desc");
//            var result = props.Select(prop => prop.GetMethod.Invoke(bu, null)).ToList();
//            CollectionAssert.AllItemsAreNotNull(result);
//            CollectionAssert.AllItemsAreUnique(result);
//        }

//        [TestMethod]
//        public void Config_Model_GetRulesForCompany()
//        {
//            var properties = Config.Models[typeof(Company).Name.ToLowerInvariant()].Properties;
//            var rules = properties.SelectMany(p => p.Rules).ToList();
//            CollectionAssert.AllItemsAreNotNull(rules);
//        }

//        public void Config_Model_GetRulesForBusinessUnit()
//        {
//            var properties = Config.Models["bu"].Properties;
//            var rules = properties.SelectMany(p => p.Rules).ToList();
//            CollectionAssert.AllItemsAreNotNull(rules);
//        }
//    }
//}
