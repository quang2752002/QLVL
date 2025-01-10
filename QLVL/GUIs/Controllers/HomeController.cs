using GUIs.Helper;
using GUIs.Models;
using GUIs.Models.DAO;
using GUIs.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace GUIs.Controllers
{
    public class HomeController : Controller
    {
        private const string ID_CV = "ID_CV";
        private const string ID_CONGVIECNHOM = "ID_CONGVIECNHOM";
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
            string  link = "<div class='collapse navbar-collapse' id='navbarCollapse'>";
            link += " <div class='navbar-nav ms-auto p-4 p-lg-0'>";
            link += "<a href='/Home/Index' class='nav-item nav-link'>Trang chủ</a>";
            link += "<div class='nav-item '> <a href='#' class='nav-link'>Thông tin công việc</a>";
            link += "</div>";
            link += "<div class='nav-item '><a href='#' class='nav-link' >Thông tin cá nhân</a>";
            link += "</div>";
            link += "<div class='nav-item '>";
            link += "<a href='/Home/GopY' class='nav-item nav-link'>Góp ý</a>";
            link += "</div>";
            link += "<div class='nav-item '>";
            link += "<a href='/Login/Index' class='nav-item nav-link'>Đăng nhập </a>";
            link += "</div>";
            link += "</div>";
            link += "</div>";

            string tt = DataServices.getRouoter(HttpContext);
            if (state != 0) { 
                if (tt =="NguoiTuyenDung")
                {
                    var query = new NguoiTuyenDungDAO().getItemView(state);
                   
                    link = "<div class='collapse navbar-collapse' id='navbarCollapse'>";
                    link += " <div class='navbar-nav ms-auto p-4 p-lg-0'>";
                    link += "<a href='/NguoituyenDung/Home/' class='nav-item nav-link'>Trang chủ</a>";
                    link += "<div class='nav-item '> <a href='/NguoituyenDung/Home/' class='nav-link'>Thông tin công việc</a>";
                    link += "</div>";
                    link += "<div class='nav-item '><a href='/NguoituyenDung/Home/Edit' class='nav-link' >Thông tin cá nhân</a>";
                    link += "</div>";
                    link += "<div class='nav-item '>";
                    link += "<a href='/Home/GopY' class='nav-item nav-link'>Góp ý</a>";
                    link += "</div>";
                    link += "<div class='nav-item '>";
                    link += "<a href='#' type='btutton' id ='logout' class='nav-item nav-link'>Đăng xuất  </a>";
                    link += "</div>";
                    link += "</div>";
                    link += "</div>";


                }
                else
                {
                    if (tt == "NguoiLaoDong") { 
                    var query = new NguoiLaoDongDAO().getItemView(state);
                   
                    link = "<div class='collapse navbar-collapse' id='navbarCollapse'>";
                    link += " <div class='navbar-nav ms-auto p-4 p-lg-0'>";
                    link += "<a href='/Home/Index' class='nav-item nav-link'>Trang chủ</a>";
                    link += "<div class='nav-item '> <a href='/NguoiLaoDong/Home/Danhsachcongviec' class='nav-link'>Thông tin công việc</a>";
                    link += "</div>";
                    link += "<div class='nav-item '><a href='/NguoiLaoDong/Home/ThongTinCaNhan' class='nav-link' >Thông tin cá nhân</a>";
                    link += "</div>";
                    link += "<div class='nav-item '>";
                    link += "<a href='/Home/GopY' class='nav-item nav-link'>Góp ý</a>";
                    link += "</div>";
                    link += "<div class='nav-item '>";
                    link += "<a href='#' type='btutton' id ='logout' class='nav-item nav-link'>Đăng xuất </a>";
                    link += "</div>";
                    link += "</div>";
                    link += "</div>";
                    }
                    else
                    {
                        var query = new UserDAO().getItemView(state);                    
                        link = "<div class='collapse navbar-collapse' id='navbarCollapse'>";
                        link += " <div class='navbar-nav ms-auto p-4 p-lg-0'>";
                        link += "<a href='/Home/Index' class='nav-item nav-link'>Trang chủ</a>";
                        link += "<a href='/Admin/Home/Index' class='nav-item nav-link'>Trang Admin</a>";
                        link += "<div class='nav-item '> <a href='#' class='nav-link'>Thông tin công việc</a>";
                        link += "</div>";
                        link += "<div class='nav-item '><a href='#' class='nav-link' >Thông tin cá nhân</a>";
                        link += "</div>";
                        link += "<div class='nav-item '>";
                        link += "<a href='/Home/GopY' class='nav-item nav-link'>Góp ý</a>";
                        link += "</div>";
                        link += "<div class='nav-item'>";
                        link += "<a href='#' type='btutton' id ='logout' class='nav-item nav-link'>Đăng xuất  </a>";
                        link += "</div>";
                        link += "</div>";
                        link += "</div>";
                    }
                }

            }
            ViewBag.Login = link;
            return View();
        }
        public JsonResult ShowList(string name = "", int thang = 0, int nam = 0, int index = 1, int size = 10,string arr="")
        {
            CongViecDAO x = new CongViecDAO();
            int total = 0;
            if (arr == null) arr = "";
             List<int> intList = new List<int>();
            if (arr != "")
            {
                List<string> list = arr.Split('-').ToList();
                intList = list.Select(s =>
                {
                    int result;
                    return int.TryParse(s, out result) ? result : 0; 
                }).ToList();
            }
            var query = x.Search(intList, out total, name, thang, nam, index, size );

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
            try
            {
                int trangthai = -1;
                string mess = "Bạn chưa đăng nhập";

                if (DataServices.getRouoter(HttpContext) == "NguoiTuyenDung")
                {
                    mess = "Người tuyển dụng không được ứng tuyển";
                    trangthai = 0;
                    return Json(new { mess = mess, status = trangthai });
                }



                int idnguoilaodong = DataServices.getUserId(HttpContext);

                if (idnguoilaodong != 0)
                {

                    CongViecDAO congviec = new CongViecDAO();
                    var query = congviec.getCongViecUngTuyen(id);

                    UngTuyen item = new UngTuyen();
                    UngTuyenDAO khachhang = new UngTuyenDAO();

                    item.Idcongviec = id;
                    item.Idnguoilaodong = idnguoilaodong;
                    item.Date = DateTime.Now;
                    item.Salary = luong;
                    item.Apply = 0;
                    if (khachhang.Check(id, idnguoilaodong) == -1)
                    {
                        khachhang.InsertOrUpdate(item);
                        mess = "Ứng tuyển thành công";
                        trangthai = 1;
                    }

                    else
                    {
                        mess = "Bạn đã ứng tuyển công việc này rồi";
                        trangthai = 0;
                    }
                }
                return Json(new { mess = mess, status = trangthai });
            }
            catch (Exception ex)
            {
                return Json(new { mess = "Đã có lỗi vui lòng thử lại sau", status = 0 });
            }
                
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
      
       
        public JsonResult ListNhomCongViec(int thang,int nam)
        {      
            NhomCongViecDAO x = new NhomCongViecDAO();
            var query = x.getListNhomCongViec(thang,nam);
            return Json(new { data = query });
        }
     
        public ActionResult GopY()
        {
            return View();
        }
        public JsonResult Nhanxet(string email, string noidung)
        {
            GopY item = new GopY();
            GopYDAO gopYDAO = new GopYDAO();
            item.Email = email;
            item.Noidung = noidung;
            gopYDAO.InsertOrUpdate(item);
            return Json(new { mess = "Cảm ơn ý kiến của bạn" });
        }
    }
}
