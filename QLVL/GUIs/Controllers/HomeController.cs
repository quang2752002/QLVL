using GUIs.Helper;
using GUIs.Models;
using GUIs.Models.DAO;
using GUIs.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace GUIs.Controllers
{
    public class HomeController : Controller
    {
        private const string ID_CV = "ID_CV";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.AreaName = "";
             
            base.OnActionExecuting(filterContext);
        }
        public IActionResult Index()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            var state = DataServices.getUserId(HttpContext);
            string link = "<a href='/login/'>Login</a>";
            if (state != 0)
            {
                link = " <div class='dropdown position-static'>";
                link += "<a class='nav-link dropdown-toggle' href='#' id='navbarDropdown' role='button' ";
                link += "data-bs-toggle='dropdown' aria-expanded='false'>   ";
                link+="<img src='https://d1hjkbq40fs2x4.cloudfront.net/2017-08-21/files/landscape-photography_1645.jpg'" ;
                link += "style='border-radius: 50%; width: 50px; height: 50px;";
                link += "class='btn btn-outline-success p-0'>";
                link+="</a>         ";
                link += "<ul class='dropdown-menu dropdown-menu-end position-absolute' aria-labelledby='navbarDropdown'>   ";
                link += "<li><a class='dropdown-item' href='/NguoiLaoDong/Home/ThongTinCaNhan'>Thông tin cá nhân</a></li>          ";
                link += "<li><a class='dropdown-item' href='/NguoiLaoDong/Home/Danhsachcongviec'>Thông tin công việc</a></li>  ";
                link += "                     <li><hr class='dropdown-divider'></li>        ";
                link += "<li><button class='dropdown-item' href='#' id='logout'>Đăng xuất</button></li>      ";
                link += "</ul>    ";
                link += "</div>";
                
            }
            ViewBag.Login = link;
            return View();
        }
        public JsonResult ShowList(string name = "", int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            CongViecDAO x = new CongViecDAO();
            int total = 0;
            var query = x.Search(out total, name, thang, nam, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        public IActionResult ChiTietCongViec(int id)
        {
            HttpContext.Session.SetInt32(ID_CV, id);
            return View();
        }
        public JsonResult getCongViec()
        {
            int id = HttpContext.Session.GetInt32(ID_CV) ?? 0;
            CongViecDAO cv = new CongViecDAO();
            var query = cv.getItemView(id);
            return Json(new { data = query });
        }
        [HttpPost]
        public JsonResult Apply(int id, int luong)
        {
            UngTuyenDAO khachhang = new UngTuyenDAO();
            int idnguoilaodong = DataServices.getUserId(HttpContext);
            UngTuyen item = new UngTuyen();
            item.Idcongviec = id;
            item.Idnguoilaodong = idnguoilaodong;
            item.Date = DateTime.Now;
            item.Salary = luong;
            item.Apply = 0;
            if (khachhang.Check(id, idnguoilaodong) == -1)
            {
                khachhang.InsertOrUpdate(item);
                return Json(new { mess = "Ứng tuyển thành công" });
            }
            return Json(new { mess = "Ứng tuyển công việc không thành công" });
        }
        public IActionResult Danhsachcongviec()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        [HttpPost]
        public JsonResult ListCongViec(string name = "", int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            int idnguoilaodong = DataServices.getUserId(HttpContext);
            CongViecDAO x = new CongViecDAO();
            int total = 0;
            var query = x.DanhSachCongViec(out total, idnguoilaodong, name, thang, nam, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        public JsonResult Logout()
        {         
            HttpContext.Session.Clear();
            return Json(new { mess = "Đăng xuất thành công" });
        }

    }
}
