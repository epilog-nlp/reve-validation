using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace REvE.Validation.Configuration.Tests
{
    using Models;
    using Contracts;

    internal static class Assertions
    {
        internal static void Compare<TItem>(TItem first, TItem second)
        {
            switch(first)
            {
                case RuleArgumentConfig f: Compare(f, second as RuleArgumentConfig);
                    break;
                case ValidationModelConfig m: Compare(m, second as ValidationModelConfig);
                    break;
                case ValidationConfig v: Compare(v, second as ValidationConfig);
                    break;
            }
        }

        internal static void Compare(ValidationConfig first, ValidationConfig second)
        {
            Compare(first.ModelProvider.Configuration, second.ModelProvider.Configuration);
            Compare(first.RuleValueProvider.Configuration, second.RuleValueProvider.Configuration);
        }

        internal static void Compare(RuleArgumentConfig first, RuleArgumentConfig second)
        {
            if ((first.RuleDefinitions?.Count ?? 0) > 0)
            {
                Assert.AreEqual(first.RuleDefinitions.Count, second.RuleDefinitions.Count);
                first.RuleDefinitions.Sort();
                second.RuleDefinitions.Sort();
                for (int i = 0; i < first.RuleDefinitions.Count; i++)
                {

                }
            }
        }

        internal static void Compare(RuleArgument first, RuleArgument second)
        {
            Assert.AreEqual(first.Type, second.Type);
            Assert.AreEqual(first.Name, second.Name);
            Assert.AreEqual(first.Rule, second.Rule);
            Assert.AreEqual(first.FullName, second.FullName);
            Assert.AreEqual(first.Model, second.Model);
            Assert.AreEqual(first.Parameter, second.Parameter);
            Assert.AreEqual(first.Value, second.Value);
        }

        internal static void Compare(ValidationModelConfig first, ValidationModelConfig second)
        {
            if ((first.Models?.Count ?? 0) > 0)
            {
                Assert.AreEqual(first.Models.Count, second.Models.Count);
                first.Models.Sort();
                second.Models.Sort();
                for (int i = 0; i < first.Models.Count; i++)
                {
                    Compare(first.Models[i], second.Models[i]);
                }
            }
        }

        internal static void Compare(Model first, Model second, bool withArgs = false)
        {
            Assert.AreEqual(first.LookupName, second.LookupName);
            if ((first.Properties?.Count ?? 0) > 0)
            {
                Assert.AreEqual(first.Properties.Count, second.Properties.Count);
                first.Properties.Sort();
                second.Properties.Sort();
                for (int i = 0; i < first.Properties.Count; i++)
                {
                    Compare(first.Properties[i], second.Properties[i]);
                }
            }

        }

        internal static void Compare(Property first, Property second, bool withArgs = false)
        {
            Assert.AreEqual(first.Name, second.Name);
            if ((first.Rules?.Count ?? 0) > 0)
            {
                Assert.AreEqual(first.Rules.Count, second.Rules.Count);
                first.Rules.Sort();
                second.Rules.Sort();
                for (int i = 0; i < first.Rules.Count; i++)
                {
                    Compare(first.Rules[i], second.Rules[i]);
                }
            }
        }

        internal static void Compare(Rule first, Rule second, bool withArgs = false)
        {
            Assert.AreEqual(first.Type, second.Type);
            Assert.AreEqual(first.Name, second.Name);
            Assert.AreEqual(first.ErrorMessage, second.ErrorMessage);
            Assert.AreEqual(first.TechnicalDescription, second.TechnicalDescription);
            Assert.AreEqual(first.FriendlyDescription, second.FriendlyDescription);
            if (withArgs && (first.Arguments?.Count ?? 0) > 0) 
            {
                first.Arguments.Sort();
                second.Arguments.Sort();
                for (int i = 0; i < first.Arguments.Count; i++)
                {
                    Compare(first.Arguments[i], second.Arguments[i]);
                }
            }
        }

    }
}
