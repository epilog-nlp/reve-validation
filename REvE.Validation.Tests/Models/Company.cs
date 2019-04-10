using System.Collections.Generic;

namespace REvE.Models.Mock
{
    public class Company : BaseType
    {
        public Company() { }
        public Company(int id, string name, string desc = null)
        {
            Id = id;
            Name = name;
            Desc = desc;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public List<BusinessUnit> BusinessUnits { get; set; }

    }
}
