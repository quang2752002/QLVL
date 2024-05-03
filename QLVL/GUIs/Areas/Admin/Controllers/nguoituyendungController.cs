using GUIs.Helper;
using GUIs.Models.DAO;
using Microsoft.AspNetCore.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NguoiTuyenDungController : BaseAdminController
    {
        public IActionResult Index()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        public JsonResult ShowList(string name = "", int thang=0,int nam=0,int index = 1, int size = 10)
        {
            NguoiTuyenDungDAO x = new NguoiTuyenDungDAO();
            int total = 0;
            var query = x.getList(name,out total, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }

    }
}
