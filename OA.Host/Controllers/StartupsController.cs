using Microsoft.AspNetCore.Mvc;

namespace OA.Host.Controllers
{
    [Route("api/[controller]")]
    public class StartupsController : Controller
    {

        [HttpGet]
        public bool Get()
        {
            return true;
        }
    }
}
