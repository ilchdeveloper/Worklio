using System.Collections.Generic;

namespace Worklio.Entities
{
    public class Country : IBaseEntity
    {
        public Country()
        {
            BorderingCountries = new List<string>();
            Translations = new Dictionary<string, string>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Alpha3Code { get; set; }
        public IList<string> BorderingCountries { get; set; }
        public IDictionary<string, string> Translations { get; set; }
    }
}