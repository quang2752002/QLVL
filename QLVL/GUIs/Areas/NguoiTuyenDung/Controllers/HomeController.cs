using GUIs.Controllers;
using GUIs.Helper;
using GUIs.Models.DAO;
using GUIs.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Policy;
using System.Xml.Linq;
using static GUIs.Areas.Admin.Controllers.NguoiLaoDongController;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;
using testLib;
using System.Globalization;

namespace GUIs.Areas.NguoiTuyenDung.Controllers
{
    [Area("NguoiTuyenDung")]
    public class HomeController : BaseNguoiTuyenDungController
    {
        private const string ID_CONGVIEC = "ID_CONGVIEC";
        private const string DANH_GIA = "DANH_GIA";
        private const string LIST_DANH_GIA = "LIST_DANH_GIA";
        public IActionResult Index()
        {
            
            ViewBag.Login = getMenu();
            ViewBag.Pagesize = DataServices.Pagesize();
            return View();
        }
        public JsonResult ShowList(string name,int index,int size)
        {
            CongViecDAO x = new CongViecDAO();
            try
            {           
                int total = 0;
                var query = x.Search( out total, name, index, size);
                string page = Support.Support.InTrang(total, index, size);
                return Json(new { data = query, page = page });
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error in ShowList method: {ex.Message}");
                return Json(new { error = "An error occurred while processing your request." });
            }
        }
        public IActionResult Create()
        {
            try {
                ViewBag.Login = getMenu();
                NhomCongViecDAO nhomCongViec = new NhomCongViecDAO();
                ViewBag.nhom = nhomCongViec.getList();
            }

            catch ( Exception ex)
            {

            }
            return View();
        }
        public JsonResult Themmoi(string name,int mintuoi,int maxtuoi,DateTime timework,string address,int salary,string note,string nhomcongviec,DateTime finish)
        {
            int userid = DataServices.getUserId(HttpContext);
            CongViecNhomDAO congviecnhom = new CongViecNhomDAO();
            CongViecDAO cv = new CongViecDAO();
            CongViec item = new CongViec();
            item.Name = name;
            item.Alias = "";
            item.Mintuoi = mintuoi;
            item.Maxtuoi = maxtuoi;
            item.Timework=timework;
            item.Finish= finish;
            item.Idnguoituyendung = userid;
            item.Address = address;
            item.Salary = salary;
            item.Note = note;
            item.State = 1;
            cv.InsertOrUpdate(item);
           
            string[] stringArray = nhomcongviec.Split(',');

            int[] intArray = stringArray.Select(s =>
            {
                int result;
                return int.TryParse(s, out result) ? result : 0; // Hoặc giá trị mặc định khác nếu không thể chuyển đổi
            }).ToArray();

            foreach (var part in intArray)
            {
                CongViecNhom nhom=new CongViecNhom();
                nhom.Idcongviec = item.Id;
                nhom.Idnhomcongviec = part;
                congviecnhom.InsertOrUpdate(nhom);
            }
            return Json(new { mess = "Đăng tuyển thành công" });
        }
        public IActionResult TuyenDung()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
          
            return View();
        }
        public JsonResult getCongViec(int index,int size)
        {

            int userid = DataServices.getUserId(HttpContext);
            CongViecDAO cv = new CongViecDAO();
            int total = 0;
            var query = cv.getCongviec(out total, userid, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
   
       public IActionResult NguoiLaoDong(int id)
        {
            var state = DataServices.getUserId(HttpContext);
            string link = "<div class='collapse navbar-collapse' id='navbarCollapse'>";
            link += " <div class='navbar-nav ms-auto p-4 p-lg-0'>";
            link += "<a href='/Home/Index' class='nav-item nav-link'>Trang chủ</a>";
            link += "<div class='nav-item '> <a href='#' class='nav-link'>Thông tin công việc</a>";
            link += "</div>";
            link += "<div class='nav-item '><a href='#' class='nav-link' >Thông tin cá nhân</a>";
            link += "</div>";
            link += "<div class='nav-item '>";
            link += "<a href='contact.html' class='nav-item nav-link'>Góp ý</a>";
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
                    link += "<a href='contact.html' class='nav-item nav-link'>Góp ý</a>";
                    link += "</div>";
                    link += "<div class='nav-item '>";
                    link += "<a href='#' type='btutton' id ='logout' class='nav-item nav-link'>Đăng xuất  </a>";
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
                        link += "<a href='contact.html' class='nav-item nav-link'>Góp ý</a>";
                        link += "</div>";
                        link += "<div class='nav-item '>";
                        link += "<a href='#' type='btutton' id ='logout' class='nav-item nav-link'>Đăng xuất  </a>";
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
                        link += "<a href='contact.html' class='nav-item nav-link'>Góp ý</a>";
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
            HttpContext.Session.SetInt32(ID_CONGVIEC,id);
            ViewBag.Pagesize = DataServices.Pagesize()  ;          
            return View();
        }
        [HttpPost]
        public JsonResult getListNguoiLaoDong(int index, int size,int status)
        {
            UngTuyenDAO cv = new UngTuyenDAO();
            int total = 0;
            int id = HttpContext.Session.GetInt32(ID_CONGVIEC)??0 ;
            var query = cv.getListUngTuyen(out total, id, index, size,status);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        [HttpPost]
        public JsonResult DuyetHoSo(int id)
        {
	        String mess="Người lao động đã nhận công việc khác";

            NguoiLaoDongDAO nguoiLaoDongDAO = new NguoiLaoDongDAO();
            int IdNguoiLaoDong = nguoiLaoDongDAO.getIdbyUngTuyen(id);

            CongViecDAO congviec = new CongViecDAO();
            var cv = congviec.getCongViecUngTuyen(id);
            UngTuyenDAO ungTuyenDAO= new UngTuyenDAO();
            
	       if(ungTuyenDAO.CheckDuyetHoSo(IdNguoiLaoDong,cv.Timework,cv.finish)==true){
            var item = ungTuyenDAO.getItem(id);
            item.Apply = 1;
            ungTuyenDAO.InsertOrUpdate(item);
            string domain = HttpContext.Request.Host.Value.Trim();
            string Http = HttpContext.Request.Scheme.Trim();
            var guid = Guid.NewGuid();
            string email = ungTuyenDAO.getEmail(id);
            EmailServeices emailServeices = new EmailServeices();
            emailServeices.MailFrom = "quang2752002@gmail.com";
            emailServeices.MailTo = email;
            emailServeices.Chude = "Email thông báo ứng tuyển thành công  ";
            emailServeices.Noidung = "Bạn đã ứng tuyển thành công công việc "+cv.Name +" thời gian bắt đầu từ "+cv.TimeworkS+" và kết thúc vào "+cv.finishS+" Địa chỉ tại "+cv.Address+". Vui lòng kiểm tra lại công việc trên hệ thống. Xin cảm ơn";
            emailServeices.Password = "ovvl kmtq zaok jjuw";
            emailServeices.SendMail();
                mess = "Duyệt hồ sơ ứng viên thành công";
            }
            return Json(new { mess = mess});
        }
        public JsonResult getNguoiLaoDong(int id)
        {
            NguoiLaoDongDAO laoDong=new NguoiLaoDongDAO();
            var query = laoDong.getItemView(id);
            return Json(new { data = query});
        }
        public IActionResult CongViec()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        [HttpPost]
        public JsonResult getListCongViec(string name,int thang ,int nam,int state,int index, int size)
        {
            CongViecDAO congViec = new CongViecDAO();
            int total = 0;
            int id = HttpContext.Session.GetInt32(ID_CONGVIEC) ?? 0;
            var query = congViec.getListCongViec(out total,name, id, thang, nam,  state, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        public ActionResult Danhgia(int id)
        {
            HttpContext.Session.SetInt32(DANH_GIA, id);
            ViewBag.Login = getMenu();
            return View();
        }
        [HttpPost]
        public JsonResult DanhGiaCongViec(int sao, string nhanxet)
        {
            string mess = "Bạn đã đánh giá người lao động này ";
            UngTuyenDAO ungTuyen = new UngTuyenDAO();
            int id = HttpContext.Session.GetInt32(DANH_GIA) ?? 0;
            var item = ungTuyen.getItem(id);
            if (ungTuyen.CheckDanhGiaLaoDong(id))
            {
                item.Danhgialaodong = sao;
                item.Nhanxetlaodong = nhanxet;
                ungTuyen.InsertOrUpdate(item);
                mess = "Đánh giá lao động thành công";
            }
           
            return Json(new { mess = mess });
        }
        [HttpGet]
        public JsonResult getLaoDongByIdUngTuyen()
        {
            int id = HttpContext.Session.GetInt32(DANH_GIA) ?? 0;
            NguoiLaoDongDAO nguoiLaoDongDAO = new NguoiLaoDongDAO();
            var query = nguoiLaoDongDAO.getLaoDongByIdUngTuyen(id);
            return Json(new { data = query});
        }
        public IActionResult Edit()
        {
            ViewBag.Login = getMenu();
            return View();
        }
        public JsonResult Update(string name,string address,string fanpage,string sdt,string image,string introduce)
        {
            int userid = DataServices.getUserId(HttpContext);
            NguoiTuyenDungDAO tuyenDungDAO = new NguoiTuyenDungDAO();
            var item= tuyenDungDAO.getItem(userid);
            item.Diachi = address;
            item.Fanpage = fanpage;
            item.Name= name;
            item.Sdt = sdt;
            item.Image = image;
            item.Introduce = introduce;
           
            tuyenDungDAO.InsertOrUpdate(item);
            return Json(new { mess = "Chỉnh sửa thông tin cá nhân thành công" });
        }
        public JsonResult getNguoiTuyenDung() 
        {
            int userid = DataServices.getUserId(HttpContext);
            var query = new NguoiTuyenDungDAO().getItemView(userid);
            return Json(new { data=query });
        }
        public JsonResult Logout()
        {
            HttpContext.Session.Clear();
            return Json(new { mess = "Đăng xuất thành công" });
        }
       
        public IActionResult ListDanhGia(int id) {
            HttpContext.Session.SetInt32(LIST_DANH_GIA, id);
            return View();
        }
        [HttpGet]
        public JsonResult DanhSachDanhGia(int id)
        {
            UngTuyenDAO ungTuyen = new UngTuyenDAO();
            int total = 0;
            int idld = HttpContext.Session.GetInt32(LIST_DANH_GIA) ?? 0;
            var query = ungTuyen.ListDanhgia(out total, idld, id,4);
            string page = Support.Support.InTrang(total, id, 4);
            return Json(new { data = query, page = page });
        }
        [HttpPost]
        public JsonResult HoanThanhCongviec(int id)
        {
            string mess = "";
            CongViecDAO congViecDAO = new CongViecDAO();
            var item = congViecDAO.getItem(id);
            if (item.Timework>DateTime.Now)
            {
                mess = "Thời gian làm việc chưa bắt đầu";
            }
            else
            {
                item.State = 2;
                congViecDAO.InsertOrUpdate(item);
                List<int> rs = new List<int>();
                UngTuyenDAO ungTuyenDAO= new UngTuyenDAO();
                rs=ungTuyenDAO.getIdUngTuyenByIdCongViec(id);
                if(rs.Count>0)
                {
                    foreach(int i in rs)
                    {
                        var x = ungTuyenDAO.getItem(i);
                        x.Apply = 3;
                        ungTuyenDAO.InsertOrUpdate(x);
                    }
                    mess = "Đã xác nhận hoàn thành công việc";
                }
                else
                {
                    mess = "Không có lao động đăng kí công việc";
                }
            }
            return Json(new { mess = mess });
        }
        public IActionResult ChangePassWord()
        {
            return View();

        }
        [HttpPost]
        public JsonResult ThayDoiMatKhau(string username, string password, string newpassword)
        {
            string mess = "Thông tin không chính xác";
            NguoiTuyenDungDAO ntd = new NguoiTuyenDungDAO();

            string matkhau = Support.Support.HashPassword(password);
            if (ntd.ChangePassword(username, matkhau) != -1)
            {
                int id = DataServices.getUserId(HttpContext);
                var item = ntd.getItem(id);
                string matkhaumoi = Support.Support.HashPassword(newpassword);
                item.Password = matkhaumoi;
                ntd.InsertOrUpdate(item);
                mess = "Đổi mật khẩu thành công";
            }

            return Json(new { mess = mess });
        }
        public string getMenu()
        {
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
                    link += "<a href='/Home/Index/' class='nav-item nav-link'>Trang chủ</a>";
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
                        link += "<a href='#' type='btutton' id ='logout' class='nav-item nav-link'>Đăng xuất  </a>";
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
            return link;
        }
    }
   

}
