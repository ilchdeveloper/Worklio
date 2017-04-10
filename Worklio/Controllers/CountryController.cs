using System.Threading.Tasks;
using System.Web.Http;
using Worklio.Services;

namespace Worklio.Controllers
{
    public class CountryController : ApiController
    {
        private readonly CountryService _service = new CountryService();

        public IHttpActionResult Get()
        {
            var cnt = Task.Run(() => _service.GetAll().Result);
            if (cnt != null)
                return Ok(cnt);
            else
                return NotFound();
        }

        public IHttpActionResult Get(int id, [FromUri] string curlang)
        {
            var cnt = Task.Run(() => _service.Get(id, curlang));
            if (cnt != null)
                return Ok(cnt);
            else
                return NotFound();
        }
        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}