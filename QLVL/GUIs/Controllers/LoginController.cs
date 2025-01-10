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
using System.Text.RegularExpressions;
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
            string matkhau = Support.Support.HashPassword(password);
            int x = nguoiTuyenDung.Login(username, matkhau);
            string router = "";
            string mess = "Đăng nhập không thành công.Vui lòng kiểm tra lại tài khoản mật khẩu";
            bool status = false;
            if (x != -1)
            {
                HttpContext.Session.SetInt32(CustomeCommon.USER_ID, x);
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
                    x = new UserDAO().Login(username, pass);
                    if (x != -1)
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
        public JsonResult Dangky(string name, string phone, string address, string email, string username, string password, DateTime ngaysinh, string state)
        {
            try
            {
                NguoiTuyenDungDAO nguoiTuyenDung = new NguoiTuyenDungDAO();
                NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
                UserDAO userDAO = new UserDAO();
                bool _state = true;
                string mess = "";

                string emailRegex = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
                string phoneRegex = @"^(0[3|5|7|8|9])+([0-9]{8})$"; // Định dạng cho số điện thoại Việt Nam phổ biến

                if (!Regex.IsMatch(email, emailRegex))
                {
                    mess = "Định dạng email không hợp lệ. Vui lòng nhập đúng định dạng email";
                    return Json(new { mess = mess, trangthai = false });
                }
                // Kiểm tra số điện thoại
                if (string.IsNullOrEmpty(phone) || !Regex.IsMatch(phone, phoneRegex))
                {
                    mess = "Định dạng số điện thoại không hợp lệ. Vui lòng nhập đúng định dạng số điện thoại";
                    return Json(new { mess = mess, trangthai = false });
                }
                if (state == "ngld")
                {


                    if (nguoiLaoDong.CheckDangKy(phone) == false || nguoiTuyenDung.CheckDangky(phone) == false)
                    {
                        mess = "Số điện thoại đã được sử dụng ";
                        _state = false;
                    }
                    if (nguoiLaoDong.CheckDangKy(email) == false || nguoiTuyenDung.CheckDangky(email) == false || userDAO.CheckDangKy(email) == false)
                    {
                        mess = "Email đã được sử dụng ";
                        _state = false;
                    }
                    if (nguoiLaoDong.CheckDangKy(username) == false || nguoiTuyenDung.CheckDangky(username) == false || userDAO.CheckDangKy(username) == false)
                    {
                        mess = "Username đã được sử dụng ";
                        _state = false;
                    }

                    if (_state)
                    {
                        string matkhau = Support.Support.HashPassword(password);
                        NguoiLaoDong item = new NguoiLaoDong();
                        item.Name = name;
                        item.Phone = phone;
                        item.Address = address;
                        item.Email = email;
                        item.Username = username;
                        item.Password = matkhau;
                        item.Birthday = ngaysinh;
                        nguoiLaoDong.InsertOrUpdate(item);
                        mess = "Đăng ký thành công";
                    }
                }
                if (state == "ngtd")
                {

                    if (nguoiLaoDong.CheckDangKy(phone) == false || nguoiTuyenDung.CheckDangky(phone) == false)
                    {
                        mess = "Số điện thoại đã được sử dụng ";
                        _state = false;
                    }
                    if (nguoiLaoDong.CheckDangKy(email) == false || nguoiTuyenDung.CheckDangky(email) == false || userDAO.CheckDangKy(email) == false)
                    {
                        mess = "Email đã được sử dụng ";
                        _state = false;
                    }
                    if (nguoiLaoDong.CheckDangKy(username) == false || nguoiTuyenDung.CheckDangky(username) == false || userDAO.CheckDangKy(username) == false)
                    {
                        mess = "Username đã được sử dụng ";
                        _state = false;
                    }

                    if (_state)
                    {
                        string matkhau = Support.Support.HashPassword(password);
                        NguoiTuyenDung item = new NguoiTuyenDung();
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
                return Json(new { mess = mess, trangthai = _state });
            }
            catch (Exception ex)
            {
                return Json(new { mess = "Đã có lỗi xảy ra vui lòng thử lại sau", trangthai = false });
            }
        }
        public IActionResult Forget()
        {
            return View();
        }
        public JsonResult ForgetPassword(string email)
        {
            try
            {
                NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
                NguoiTuyenDungDAO nguoiTuyenDung = new NguoiTuyenDungDAO();
                string responseMessage = string.Empty;

                switch (true)
                {
                    case bool _ when nguoiLaoDong.Check(email) != -1:

                        string domain = HttpContext.Request.Host.Value.Trim();
                        string Http = HttpContext.Request.Scheme.Trim();
                        var guid = Guid.NewGuid();
                        var item = nguoiLaoDong.getItemByEmail(email);
                        item.Guid = guid.ToString();
                        nguoiLaoDong.InsertOrUpdate(item);
                        EmailServeices emailServeices = new EmailServeices();
                        emailServeices.MailFrom = "quang2752002@gmail.com";
                        emailServeices.MailTo = email;
                        emailServeices.Chude = "Đây là email xác nhận mật khẩu";
                        emailServeices.Noidung = "Ấn vào link này để lấy lại mật khẩu " + Http + "://" + domain + "/login/reset/" + guid.ToString() + " Nếu không phải vui lòng bỏ qua";
                        emailServeices.Password = "kheq lrou tfck dwht";

                        emailServeices.SendMail();
                        responseMessage = "Gửi thành công";
                        break;



                    case bool _ when nguoiTuyenDung.Check(email) != -1:
                        string domains = HttpContext.Request.Host.Value.Trim();
                        string Https = HttpContext.Request.Scheme.Trim();
                        var guids = Guid.NewGuid();
                        var items = nguoiTuyenDung.getItemByEmail(email);
                        items.Guid = guids.ToString();
                        nguoiTuyenDung.InsertOrUpdate(items);
                        EmailServeices emailServeice = new EmailServeices();
                        emailServeice.MailFrom = "quang2752002@gmail.com";
                        emailServeice.MailTo = email;
                        emailServeice.Chude = "Đây là email xác nhận mật khẩu";
                        emailServeice.Noidung = "Ấn vào link này để lấy lại mật khẩu " + Https + "://" + domains + "/login/reset/" + guids.ToString() + " Nếu không phải vui lòng bỏ qua";
                        emailServeice.Password = "kheq lrou tfck dwht";

                        emailServeice.SendMail();
                        responseMessage = "Gửi thành công";
                        break;
                    default:
                        responseMessage = "Email không đúng";
                        break;
                }

                return Json(new { mess = responseMessage });

            }
            catch (Exception ex)
            {
                return Json(new { mess = "Đã có lỗi xảy ra vui lòng thử lại sau" });
            }

        }
        public IActionResult Reset(string id)
        {
            NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
            if (nguoiLaoDong.CheckGuid(id) != null)
            {
                HttpContext.Session.SetString(PASSWORD, id);
                return View();
            }
            return RedirectToAction("Index");
        }
        public JsonResult ResetPassword(string password)
        {
            string id = HttpContext.Session.GetString(PASSWORD);
            NguoiLaoDongDAO nguoiLaoDong = new NguoiLaoDongDAO();
            var item = nguoiLaoDong.CheckGuid(id);
            if (item != null)
            {
                string matkhau = Support.Support.HashPassword(password);
                item.Guid = null;
                item.Password = matkhau;
                nguoiLaoDong.InsertOrUpdate(item);
                return Json(new { mess = "Đặt mật khẩu thành công" });
            }
            return Json(new { mess = "Email không đúng" });

        }
    }
}
