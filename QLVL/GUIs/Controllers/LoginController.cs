using GUIs.Helper;
using GUIs.Models.DAO;
using GUIs.Models.EF;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            int x = nguoiTuyenDung.Login(username, password);
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
                x = new NguoiLaoDongDAO().Login(username, password);
                if (x != -1)
                {
                    HttpContext.Session.SetInt32(CustomeCommon.USER_ID, x);
                    router = "NguoiLaoDong";
                    HttpContext.Session.SetString(CustomeCommon.ROUTER, router);
                    status = true;
                }
                //else
                //{
                //    x=
                //}

            }
            return Json(new { mess = mess, status = status, router = router });
        }
        public IActionResult Register()
        {
            return View();
        }
        public JsonResult Dangky(string name,string phone, string address,string email,string username ,string password,string state)
        {
            bool _state = true;
            string mess = "";
            if(state=="nld")
            {
                
                NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
                if (nguoiLaoDong.CheckDangKy(phone))
                {
                    mess = "Số điện thoại đã được sử dụng";
                    _state = false;
                }
                if (nguoiLaoDong.CheckDangKy(email))
                {
                    mess += "Email đã được sử dụng";
                    _state = false;
                }
                if (nguoiLaoDong.CheckDangKy(username))
                {
                    mess += "Username đã được sử dụng";
                    _state = false;
                }

                if (_state)
                {
                    NguoiLaoDong item = new NguoiLaoDong();
                    item.Name = name;       
                    item.Phone = phone;
                    item.Address = address;
                    item.Email = email;
                    item.Username = username;
                    item.Password = password;

                    nguoiLaoDong.InsertOrUpdate(item);
                }           
            }
            else
            {
                NguoiTuyenDungDAO nguoiTuyenDung = new NguoiTuyenDungDAO();
                
                if (nguoiTuyenDung.CheckDangky(username) ) { 
                    _state = false;
                    mess = "";
                }
                if (nguoiTuyenDung.CheckDangky(email))
                {
                    _state = false;
                    mess = "";
                }
                if (nguoiTuyenDung.CheckDangky(phone))
                {
                    _state = false;
                    mess = "";
                }
                if (_state)
                {
                    NguoiTuyenDung item=new NguoiTuyenDung();
                    item.Name = name;
                    item.Sdt = phone;
                    item.Diachi = address;
                    item.Email = email;
                    item.Username = username;
                    item.Password = password;
                    nguoiTuyenDung.InsertOrUpdate(item);
                }
            }
           return Json(new { mess = mess,state =_state });
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
