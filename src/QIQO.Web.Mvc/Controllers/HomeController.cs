using Entities.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace QIQO.Web.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public ViewResult Index()
        {
            var model = new HomePageViewModel();
            return View(model);
        }
    }
}
