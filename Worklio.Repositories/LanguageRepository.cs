using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worklio.Entities;

namespace Worklio.Repositories
{
    public class LanguageRepository : IBaseRepository<Language>
    {
        private static string _lngList = ConfigurationManager.AppSettings["langList"];
        public async Task<IList<Language>> GetAll()
        {
            return await GetAllLanguageAsync();
        }

        private async Task<IList<Language>> GetAllLanguageAsync()
        {
            IList<Language> langList = new List<Language>();
            int index = 1;
            try
            {
                await Task.Run(() =>
                {
                    Array.ForEach(_lngList.Split(','), x =>
                    {
                        langList.Add(new Language
                        {
                            ID = index++,
                            Name = x,
                            Code = x.Substring(0, 2) == "Sp" ? "es" : x.Substring(0, 2) == "Ge" ? "de" : x.Substring(0, 2).ToLower()
                        });
                    });
                });
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.Write(ex);
            }
            return langList;
        }


        public Language Get(int id)
        {
            return GetAllLanguageAsync().Result.Where(x => x.ID == id).FirstOrDefault();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}