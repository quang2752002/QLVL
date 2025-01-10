using GUIs.Helper;
using GUIs.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GUIs.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : BaseAdminController
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
        
        [HttpPost]
        public JsonResult BieuDo(int month,int year)
        {
            NhomCongViecDAO nhomCongViecDAO = new NhomCongViecDAO();
            var rs = nhomCongViecDAO.BieuDo(month, year);

            if (rs == null || !rs.Any())
            {
                return Json(new { data = Array.Empty<int>(), max = 0, list = Array.Empty<string>() });
            }

            int imax = rs.Max();
            int[] text = rs.ToArray();
            var listname = nhomCongViecDAO.ListNameNhomcongViec(month, year);

            string[] list = listname.ToArray();
            return Json(new { data = text, max = imax, list = list });

        }
        public IActionResult CongViec()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        [HttpPost]
        public JsonResult ShowlistCongViec(string name = "", int thang = 0, int nam = 0,int status=1, int index = 1, int size = 10)
        {
            CongViecDAO x = new CongViecDAO();
            int total = 0;
            var query = x.ShowList(out total,name, thang, nam,status, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        public JsonResult getNguoiLaoDong(int id)
        {
            UngTuyenDAO ungTuyenDAO = new UngTuyenDAO();
            var query = ungTuyenDAO.getListLaoDongByIdCongViec(id);
            return Json(new { data = query });
        }
    }
}