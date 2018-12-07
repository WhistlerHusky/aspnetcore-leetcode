using Microsoft.AspNetCore.Mvc;

namespace Leetcode.Web.Host.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
