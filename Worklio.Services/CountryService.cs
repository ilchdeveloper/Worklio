using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worklio.Entities;
using Worklio.Helpers;
using Worklio.Repositories;

namespace Worklio.Services
{
    public class CountryService : IBaseRepository<Country>
    {
        private readonly IBaseRepository<Country> _repository;
        private readonly Cacher _cache = new Cacher();
        public CountryService()
        {
            this._repository = new CountryRepository();
        }
      
        public Task<IList<Country>> GetAll()
        {
            IList<Country> res = _cache.Get("contries") as IList<Country>;
            if (res != null)
            {
                return Task.FromResult(res);
            }
            else
            {
                var cnt = Task.Run(() => _repository.GetAll().Result);
                _cache.Add("contries", cnt.Result);
                return cnt;
            }
        }

        public CountryView Get(int id, string curlang)
        {
            IList<Country> res = _cache.Get("contries") as IList<Country>;
            var cnt = res.Where(x => x.ID == id).First();
            var brd = cnt.BorderingCountries;
            CountryView cntvw = new CountryView();
            IList<string> brdcount = new List<string>();
            if (brd.Count > 0 )
            {
                foreach (string tmp in brd)
                {
                    if (curlang.Equals("en"))
                    {
                        brdcount.Add(res.Where(x => x.Alpha3Code == tmp).Select(s => s.Name).First());
                    }
                    else
                    {
                        brdcount.Add(res.Where(x => x.Alpha3Code == tmp).Select(s => s.Translations).First().FirstOrDefault(kvp => kvp.Key.Contains(curlang)).Value.Replace("\"", String.Empty).Replace("null", "N/A").ToString());
                    }
                }
            }
            cntvw.ID = cnt.ID;
            cntvw.Alpha3Code = cnt.Alpha3Code;
            cntvw.Capital = cnt.Capital;
            cntvw.Name = cnt.Name;
            cntvw.BorderingCountries = brdcount;
            return cntvw;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (_repository != null) _repository.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Country Get(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
