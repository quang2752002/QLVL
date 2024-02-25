using GUIs.Helper;
using GUIs.Models.DAO;
using Microsoft.AspNetCore.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NguoiLaoDongController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        [HttpPost]
        public JsonResult Showlist(string name = "", int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            NguoiLaoDongDAO x = new NguoiLaoDongDAO();

            int total = 0;
            var query = x.getList(out total,name, thang, nam,index,size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
    }
}
