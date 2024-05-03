using GUIs.Helper;
using GUIs.Models.DAO;
using GUIs.Models.EF;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Net;

using testLib;
using Microsoft.AspNetCore.Http;
namespace GUIs.Controllers
{
    
    public class LoginController : Controller
    {
        private const string PASSWORD = "PASSWORD";
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult DangNhap(string username, string password)
        {
            NguoiTuyenDungDAO nguoiTuyenDung = new NguoiTuyenDungDAO();
            string matkhau=Support.Support.HashPassword(password);  
            int x = nguoiTuyenDung.Login(username, matkhau);
            string router = "";
            string mess = "Đăng nhập không thành công";
            bool status = false;
            if (x != -1)
            {
                HttpContext.Session.SetInt32(CustomeCommon.USER_ID,x);
                status = true;
                router = "NguoiTuyenDung";
                HttpContext.Session.SetString(CustomeCommon.ROUTER, router);
            }
            else
            {
                string ma = Support.Support.HashPassword(password);
                x = new NguoiLaoDongDAO().Login(username, ma);
                if (x != -1)
                {
                    HttpContext.Session.SetInt32(CustomeCommon.USER_ID, x);
                    router = "NguoiLaoDong";
                    HttpContext.Session.SetString(CustomeCommon.ROUTER, router);
                    status = true;
                }
                else
                {
                    string pass = Support.Support.HashPassword(password);
                    x = new UserDAO().Login(username,pass);
                    if(x!=-1)
                    {
                      
                        HttpContext.Session.SetInt32(CustomeCommon.USER_ID, x);
                        router = "Admin";
                        HttpContext.Session.SetString(CustomeCommon.ROUTER, router);
                        status = true;
                    }
                }

            }
            return Json(new { mess = mess, status = status, router = router });
        }
        public IActionResult Register()
        {
            return View();
        }
        public JsonResult Dangky(string name,string phone, string address,string email,string username ,string password,string state)
        {
            NguoiTuyenDungDAO nguoiTuyenDung = new NguoiTuyenDungDAO();
            NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
            UserDAO userDAO = new UserDAO();
            bool _state = true;
            string mess = "";
            if(state=="ngld")
            {


                //if (nguoiLaoDong.CheckDangKy(phone)==true|| nguoiTuyenDung.CheckDangky(phone)==true)
                //{
                //    mess = "Số điện thoại đã được sử dụng ";
                //    _state = false;
                //}
                //if (nguoiLaoDong.CheckDangKy(email)==true|| nguoiTuyenDung.CheckDangky(email)==true || userDAO.CheckDangKy(email)==true)
                //{
                //    mess += "Email đã được sử dụng ";
                //    _state = false;
                //}
                //if (nguoiLaoDong.CheckDangKy(username)==true || nguoiTuyenDung.CheckDangky(username)==true || userDAO.CheckDangKy(username)==true)
                //{
                //    mess += "Username đã được sử dụng ";
                //    _state = false;
                //}

                if (_state)
                {
                    string matkhau=Support.Support.HashPassword(password);
                    NguoiLaoDong item = new NguoiLaoDong();
                    item.Name = name;       
                    item.Phone = phone;
                    item.Address = address;
                    item.Email = email;
                    item.Username = username;
                    item.Password = matkhau;
                    
                    nguoiLaoDong.InsertOrUpdate(item);
                    mess = "Đăng ký thành công";
                }           
            }
            if(state == "ngtd")
            {


                //if (nguoiLaoDong.CheckDangKy(phone)==true|| nguoiTuyenDung.CheckDangky(phone)==true)
                //{
                //    mess = "Số điện thoại đã được sử dụng";
                //    _state = false;
                //}
                //if (nguoiLaoDong.CheckDangKy(email)==true|| nguoiTuyenDung.CheckDangky(email)==true|| userDAO.CheckDangKy(email)==true)
                //{
                //    mess += "Email đã được sử dụng";
                //    _state = false;
                //}
                //if (nguoiLaoDong.CheckDangKy(username)==true || nguoiTuyenDung.CheckDangky(username)==true || userDAO.CheckDangKy(username)==true)
                //{
                //    mess += "Username đã được sử dụng";
                //    _state = false;
                //}
                if (_state)
                {
                    string matkhau = Support.Support.HashPassword(password);
                    NguoiTuyenDung item=new NguoiTuyenDung();
                    item.Name = name;
                    item.Sdt = phone;
                    item.Diachi = address;
                    item.Email = email;
                    item.Username = username;
                    item.Password = matkhau;
                    nguoiTuyenDung.InsertOrUpdate(item);
                    mess = "Đăng ký thành công";
                }
            }
           return Json(new { mess = mess,trangthai =_state });
        }
        public IActionResult Forget()
        {
            return View();
        }
        public JsonResult ForgetPassword(string email)
        {
            NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
            NguoiTuyenDungDAO nguoiTuyenDung=new NguoiTuyenDungDAO();
            if (nguoiLaoDong.Check(email) != -1)
            {
                string domain=HttpContext.Request.Host.Value.Trim();
                string Http=HttpContext.Request.Scheme.Trim();
                var guid=Guid.NewGuid();
                var item=nguoiLaoDong.getItemByEmail(email);
                item.Guid = guid.ToString();
                nguoiLaoDong.InsertOrUpdate(item);
                EmailServeices emailServeices = new EmailServeices();
                emailServeices.MailFrom = "quang2752002@gmail.com";
                emailServeices.MailTo= email;
                emailServeices.Chude = "Đây là email xác nhận mật khẩu";
                emailServeices.Noidung ="Ấn vào link này để lấy lại mật khẩu "+Http+"://"+domain+"/login/reset/"+guid.ToString()+" Nếu không phải vui lòng bỏ qua";   
                emailServeices.Password = "ovvl kmtq zaok jjuw";
                emailServeices.SendMail();
                return Json(new { mess = "Gửi thành công" });
            }
            else
            {
                if(nguoiTuyenDung.Check(email) != -1)
                {
                    var smtpClient = new SmtpClient("smtp.example.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("quang2752002@gmail.com", "quangquang"),
                        EnableSsl = true,
                    };
                    smtpClient.Send("quang2752002@gmail.com", email, "subject", "body");
                    return Json(new { });
                }
                else
                {
                    return Json(new {mess="Email không đúng"});
                }
            }

        }//reset
        public IActionResult Reset(string id)
        {
            NguoiLaoDongDAO nguoiLaoDong=new NguoiLaoDongDAO();
            if (nguoiLaoDong.CheckGuid(id) != null) { 
                HttpContext.Session.SetString(PASSWORD, id);
            return View();
            }
            return RedirectToAction("Index");
        }
        public JsonResult ResetPassword(string password)
        {
            string id = HttpContext.Session.GetString(PASSWORD);
            NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
            var item =nguoiLaoDong.CheckGuid(id);
            if (item != null)
            {
                item.Guid = null;
                item.Password= password;
                nguoiLaoDong.InsertOrUpdate(item);
                return Json(new { mess = "Đặt mật khẩu thành công" });
            }
            return Json(new { mess = "Email không đúng" });
        
         }
    }
}
