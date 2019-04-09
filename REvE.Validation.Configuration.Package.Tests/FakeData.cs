using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REvE.Validation.Configuration.Tests
{
    using Models;
    using static GenerationUtil;

    internal static class FakeData
    {
        internal static readonly IReadOnlyList<(string name, string alias, IEnumerable<(string propertyName, IEnumerable<(RuleType type, MessageFlag flags, string ruleName)> rules)> properties)> Models
            = new List<(string, string, IEnumerable<(string, IEnumerable<(RuleType, MessageFlag, string)>)>)>
            {
                AddModel("company",null,
                    AddProperty("Id",
                        AddRule(RuleType.Required, MessageFlag.All)),
                    AddProperty("Name",
                        AddRule(RuleType.Required, MessageFlag.Error | MessageFlag.Tech),
                        AddRule(RuleType.MinLength),
                        AddRule(RuleType.MaxLength)),
                    AddProperty("Desc",
                        AddRule(RuleType.MaxLength, MessageFlag.Friendly)),
                    AddProperty("BusinessUnits")),
                AddModel("businessunit", "bu",
                    AddProperty("Id",
                        AddRule(RuleType.Required, MessageFlag.Friendly | MessageFlag.Tech)),
                    AddProperty("Name",
                        AddRule(RuleType.Required, MessageFlag.Error),
                        AddRule(RuleType.StringLength)),
                    AddProperty("Desc",
                        AddRule(RuleType.Regex, MessageFlag.None, "BusinessUnitRegexRule1"),
                        AddRule(RuleType.Regex, MessageFlag.All, "BusinessUnitRegexRule2")))
            };

        private static (string, string, IEnumerable<(string, IEnumerable<(RuleType, MessageFlag, string)>)>) AddModel(string name, string alias, params (string, IEnumerable<(RuleType, MessageFlag, string)>)[] properties)
            => (name, alias, properties);

        private static (string, IEnumerable<(RuleType, MessageFlag, string)>) AddProperty(string name, params (RuleType, MessageFlag, string)[] rules)
            => (name, rules);

        private static (RuleType, MessageFlag, string) AddRule(RuleType type, MessageFlag flags = MessageFlag.None, string ruleName = null)
            => (type, flags, ruleName);
    }
}
