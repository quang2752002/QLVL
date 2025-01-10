using GUIs.Helper;
using GUIs.Models.DAO;
using GUIs.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Net;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GUIs.Areas.NguoiLaoDong.Controllers
{
    [Area("NguoiLaoDong")]
    public class HomeController : BaseNLDController
    {
        private const string ID_CV = "ID_CV";
        private const string DANH_GIA_CV = "DANH_GIA_CV";

        public IActionResult Index()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            var state = DataServices.getUserId(HttpContext);
            string link = "<a href='/login/' class='btn btn-primary custom-link'>Login</a>";

            if (state != 0)
            {
                var query = new NguoiLaoDongDAO().getItemView(state);
                link = " <div class='dropdown position-static'>";
                link += "<a class='nav-link dropdown-toggle' href='#' id='navbarDropdown' role='button' ";
                link += "data-bs-toggle='dropdown' aria-expanded='false'>   ";
                link += "<img src='"+query.Image+"'";
                link += "style='border-radius: 50%; width: 50px; height: 50px;";
                link += "class='btn btn-outline-success p-0'>";
                link += "</a>         ";
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
        public IActionResult Danhsachcongviec()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            var state = DataServices.getUserId(HttpContext);
            string link = "<div class='collapse navbar-collapse' id='navbarCollapse'>";
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
            if (state != 0)
            {
                if (tt == "NguoiTuyenDung")
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
                    link += "<a href='#' type='btutton' id ='logout' class='nav-item nav-link'>Đăng xuất </a>";
                    link += "</div>";
                    link += "</div>";
                    link += "</div>";


                }
                else
                {
                    if (tt == "NguoiLaoDong")
                    {
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
                        link += "<a href='#' type='btutton' id ='logout' class='nav-item nav-link'>Đăng xuất Admin </a>";
                        link += "</div>";
                        link += "</div>";
                        link += "</div>";
                    }
                }

            }
            ViewBag.Login = link;
            return View();
        }
        [HttpPost]
        public JsonResult ListCongViec(string name = "",int apply=1, int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            int idnguoilaodong = DataServices.getUserId(HttpContext);
            CongViecDAO x = new CongViecDAO();
            int total = 0;
            var query = x.DanhSachCongViec(out total,idnguoilaodong, name,apply, thang, nam, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        public IActionResult Danhgia(int id)
        {
            var state = DataServices.getUserId(HttpContext);

            string link = "<a href='/login/' class='btn btn-primary custom-link'>Login</a>";

            if (state != 0)
            {
                link = " <div class='dropdown position-static'>";
                link += "<a class='nav-link dropdown-toggle' href='#' id='navbarDropdown' role='button' ";
                link += "data-bs-toggle='dropdown' aria-expanded='false'>   ";
                link += "<img src='https://d1hjkbq40fs2x4.cloudfront.net/2017-08-21/files/landscape-photography_1645.jpg'";
                link += "style='border-radius: 50%; width: 50px; height: 50px;";
                link += "class='btn btn-outline-success p-0'>";
                link += "</a>         ";
                link += "<ul class='dropdown-menu dropdown-menu-end position-absolute' aria-labelledby='navbarDropdown'>   ";
                link += "<li><a class='dropdown-item' href='/NguoiLaoDong/Home/ThongTinCaNhan'>Thông tin cá nhân</a></li>          ";
                link += "<li><a class='dropdown-item' href='/NguoiLaoDong/Home/Danhsachcongviec'>Thông tin công việc</a></li>  ";
                link += "<li><hr class='dropdown-divider'></li>        ";
                link += "<li><button class='dropdown-item' href='#' id='logout'>Đăng xuất</button></li>      ";
                link += "</ul>    ";
                link += "</div>";

            }
            ViewBag.Login = link;
            HttpContext.Session.SetInt32(DANH_GIA_CV, id);
            return View();
        }
        public JsonResult DanhGiaCongViec(int sao,string nhanxet)
        {
            string mess = "Bạn đã đánh giá công việc này ";
            UngTuyenDAO ungTuyen = new UngTuyenDAO();
            int idcongviec = HttpContext.Session.GetInt32(DANH_GIA_CV) ?? 0;
            int idnguoilaodong = DataServices.getUserId(HttpContext);
            int id = ungTuyen.getIdUngTuyen(idcongviec,idnguoilaodong);
            var item = ungTuyen.getItem(id);
            if (ungTuyen.CheckDanhGiaCongviec(id))
            {
                item.Danhgiacongviec = sao;
                item.Nhanxetcongviec = nhanxet;
                ungTuyen.InsertOrUpdate(item);
                mess = "Đánh giá công việc thành công";
            }
            return Json(new { mess =mess });
        }
        public IActionResult ThongTinCaNhan()
        {
           
            return View();
        }
        public JsonResult getThongTinCaNhan()
        {
            int idnguoilaodong = DataServices.getUserId(HttpContext);
            NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
            
            var query = nguoiLaoDong.getItemView(idnguoilaodong);
          
            return Json(new { data=query });
        }
        public JsonResult saveThongTinCaNhan(string name,DateTime birthday ,string phone ,string address,string heath,string state)
        {

            bool _state = true;
            string mess = "";
            
                NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
                if (nguoiLaoDong.CheckDangKy(phone))
                {
                    mess = "Số điện thoại đã được sử dụng";
                    _state = false;
                }

            if (_state)
            {
                int id = DataServices.getUserId(HttpContext);
                var item = nguoiLaoDong.getItem(id);
                item.Name = name;
                item.Birthday = birthday;
                item.Heath = heath;
                item.Phone = phone;
                item.Address = address;
                if (state == "nam")
                {
                    item.Sex = 1;
                    nguoiLaoDong.InsertOrUpdate(item);

                }
                else
                {
                    item.Sex = 0;
                    nguoiLaoDong.InsertOrUpdate(item);
                }
                mess = "Cập nhật thông tin thành công";
            }
            return Json(new {mess=mess});
        }
       public JsonResult HoanThanhCongviec(int id)
        {
            UngTuyenDAO ungTuyen = new UngTuyenDAO();
           
            int idnguoilaodong = DataServices.getUserId(HttpContext);
            int idUngTuyen = ungTuyen.getIdUngTuyen(id, idnguoilaodong);
            var item = ungTuyen.getItem(idUngTuyen);
            string mess = "";
            if (item.Apply == 3)
            {
                mess = "Người tuyển dụng đã xác nhận hoàn thành";
            }
            else
            {
                item.Apply = 2;
                ungTuyen.InsertOrUpdate(item);
                mess = "Đã xác nhận hoàn thành công việc";
            }
            
            return Json(new {mess=mess});
        }
        public JsonResult getCongViecById()
        {
            int idcongviec = HttpContext.Session.GetInt32(DANH_GIA_CV) ?? 0;
            CongViecDAO congViecDAO = new CongViecDAO();    
            var query=congViecDAO.getItemView(idcongviec);
            return Json(new { data = query });
        }
        public IActionResult ChangePassWord()
        {
           return View();

        }
        [HttpPost]
        public JsonResult ThayDoiMatKhau(string username,string password,string newpassword)
        {
            string mess = "Thông tin không chính xác";
            NguoiLaoDongDAO nguoiLaoDongDAO = new NguoiLaoDongDAO();
            
            string matkhau = Support.Support.HashPassword(password);
            if (nguoiLaoDongDAO.ChangePassword(username, matkhau) != -1)
            {
                int idnguoilaodong = DataServices.getUserId(HttpContext);
                var item = nguoiLaoDongDAO.getItem(idnguoilaodong);
                string matkhaumoi= Support.Support.HashPassword(newpassword);
                item.Password = matkhaumoi;
                nguoiLaoDongDAO.InsertOrUpdate(item);
                mess = "Đổi mật khẩu thành công";
            }
           
            return Json(new { mess=mess});
        }
    }
}
