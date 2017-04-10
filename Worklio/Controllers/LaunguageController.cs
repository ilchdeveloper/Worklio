using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Worklio.Entities;
using Worklio.Helpers;
using Worklio.Services;

namespace Worklio.Controllers
{
    public class LaunguageController : ApiController
    {
        private readonly LanguageService _service = new LanguageService();
        private readonly Cacher _cache = new Cacher();
        // GET api/country 
        public IHttpActionResult Get()
        {
            IList<Language> res =  _cache.Get("languages") as IList<Language>;
            if (res != null)
            {
                return Ok(Task.FromResult(res));
            }
            else
            {
                var cnt = Task.Run(() => _service.GetAll().Result);
                _cache.Add("languages", cnt.Result);
                return Ok(cnt);
            }

        }

        // GET api/country/5
        public IHttpActionResult Get(int id)
        {
            var cnt = Task.Run(() => _service.Get(id));
            return Ok(cnt.Result);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
