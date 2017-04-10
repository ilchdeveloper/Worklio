using System.Collections.Generic;

namespace Worklio.Entities
{
    public class CountryView : IBaseEntity
    {
        public CountryView() 
        {
            BorderingCountries = new List<string>();          
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Alpha3Code { get; set; }
        public IList<string> BorderingCountries { get; set; }       
    }
}