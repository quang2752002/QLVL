using GUIs.Helper;
using GUIs.Models.DAO;
using Microsoft.AspNetCore.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        public IActionResult NguoiLaoDong()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        [HttpPost]
        public JsonResult ShowlistNguoiLaoDong(string name = "", int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            NguoiLaoDongDAO x = new NguoiLaoDongDAO();

            int total = 0;
            var query = x.getList(out total, name, thang, nam, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        public IActionResult NguoiTuyenDung()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        [HttpPost]
        public JsonResult ShowlistNguoiTuyenDung(string name = "", int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            NguoiTuyenDungDAO x = new NguoiTuyenDungDAO();
            int total = 0;
            var query = x.Search(name, thang, nam, out total, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        public ActionResult Chart()
        {
            ViewBag.Month = DataServices.Months();
            ViewBag.Year = DataServices.Year();
       
            return View();
        }
        [HttpPost]
        public JsonResult BieuDo(int month,int year)
        {
            NhomCongViecDAO nhomCongViecDAO = new NhomCongViecDAO();
            var rs = nhomCongViecDAO.BieuDo(month,year);
            int imax = rs.Max();
            int[] text = rs.ToArray();
            var listname = nhomCongViecDAO.ListNameNhomcongViec(month, year);

            string[] list = listname.ToArray();
            return Json(new { data = text.ToArray(), max = imax,list=list });
        }
    }
}