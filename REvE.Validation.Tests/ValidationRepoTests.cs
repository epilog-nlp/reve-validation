using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace REvE.Validation.Tests
{
    using Models.Mock;

    [TestClass]
    public class ValidationRepoTests
    {
        private static readonly Lazy<ValidationRepo> repo = new Lazy<ValidationRepo>(() => ValidationRepo.Instance);
        internal static ValidationRepo Repo => repo.Value;

        public static readonly Company TestCompany = new Company(1, "Company", "Desc");
        public static readonly BusinessUnit TestBu = new BusinessUnit(2, "Business", "Test Businesss Unit Description");

        [TestMethod]
        public void Instantiate()
        {
            var a = Repo;
            //var b = a.Config;

        }

        [TestMethod]
        public void MyTestMethod()
        {
            var q = Repo.GetRules().ToList();
            var r = Repo.Validate(TestCompany);
        }
        //[TestMethod]
        //public void PopulateCompany_Test()
        //{
        //    var result = Repo.Populate(typeof(Company));
        //    CollectionAssert.AreEquivalent(result.ToList(), ValidationRepo.PropertiesCache[typeof(Company)].ToList());
        //    Assert.IsTrue(result.Count() == 3);
        //    var validations = result.SelectMany(mi => ValidationRepo.Validations[mi]);
        //    Assert.IsTrue(validations.Count() == 5);
        //    Assert.IsTrue(validations.All(v => v(TestCompany)));
        //}

        //[TestMethod]
        //public void PopulateBusinessUnit_Test()
        //{
        //    var result = Repo.Populate(typeof(BusinessUnit), "bu");
        //    CollectionAssert.AreEquivalent(result.ToList(), ValidationRepo.PropertiesCache[typeof(BusinessUnit)].ToList());
        //    Assert.IsTrue(result.Count() == 3);
        //    var validations = result.SelectMany(mi => ValidationRepo.Validations[mi]);
        //    Assert.IsTrue(validations.Count() == 4);
        //    Assert.IsTrue(validations.All(v => v(TestBu)));
        //}

        //[TestMethod]
        //public void Validate_ValidModels()
        //{
        //    Assert.IsTrue(Repo.Validate(TestCompany));
        //    Assert.IsTrue(Repo.Validate(TestCompany, typeof(Company)));
        //    Assert.IsTrue(Repo.Validate(TestBu, typeof(BusinessUnit), "bu"));
        //    Assert.IsTrue(Repo.Validate(TestBu, typeof(BusinessUnit)));
        //    Assert.IsTrue(Repo.Validate(TestBu));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(System.Reflection.TargetException))]
        //public void Validate_TypeMismatch()
        //{
        //    Assert.IsTrue(Repo.Validate("TestString", typeof(Company)));
        //}

        //[TestMethod]
        //public void Validate_UnconfiguredModel()
        //{
        //    string model = "TestString";
        //    Assert.IsTrue(Repo.Validate(model, typeof(string)));
        //}

    }
}
