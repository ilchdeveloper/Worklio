using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worklio.Entities;
using Worklio.Repositories;

namespace Worklio.Services
{
    public class LanguageService : IBaseRepository<Language>
    {
        private readonly IBaseRepository<Language> _repository;
        public LanguageService()
        {
            this._repository = new LanguageRepository();
        }
        public Task<IList<Language>> GetAll()
        {
            return _repository.GetAll();
        }

        public Language Get(int id)
        {
            return _repository.Get(id);
        }
        public void Dispose()
        {
        }

    }
}