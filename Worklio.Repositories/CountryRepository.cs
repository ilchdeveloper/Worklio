using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Worklio.Entities;

namespace Worklio.Repositories
{
    public class CountryRepository : IBaseRepository<Country>
    {
        public Country Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Country>> GetAll()
        {
            return await GetAllCountryAsync();
        }

        private async Task<IList<Country>> GetAllCountryAsync()
        {
            IList<Country> countryList = null;
            try
            {
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var json = await client.DownloadStringTaskAsync(ConfigurationManager.AppSettings["dataPath"]);
                    JArray jArray = JArray.Parse(json);
                    countryList = jArray.Select((cnt, index) => new Country
                    {
                        ID = index + 1,
                        Name = cnt["name"].ToString(),
                        Capital = cnt["capital"].ToString(),
                        Alpha3Code = cnt["alpha3Code"].ToString(),
                        BorderingCountries = cnt["borders"].Select(b => b.ToString()).ToList(),
                        Translations = cnt["translations"].Select(f => f.ToString().Split(':')).ToDictionary(q => q[0], q => q[1])
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.Write(ex.Message);
            }
            return countryList;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}