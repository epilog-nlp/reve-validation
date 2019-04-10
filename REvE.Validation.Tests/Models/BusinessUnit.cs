namespace REvE.Models.Mock
{
    public class BusinessUnit
    {
        public BusinessUnit() { }
        public BusinessUnit(int id, string name, string desc = null)
        {
            Id = id;
            Name = name;
            Desc = desc;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }
    }
}
