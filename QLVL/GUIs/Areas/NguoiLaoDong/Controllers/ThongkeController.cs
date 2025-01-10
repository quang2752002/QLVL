using Microsoft.AspNetCore.Mvc;

namespace GUIs.Areas.NguoiLaoDong.Controllers
{
    public class ThongkeController : BaseNLDController
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
