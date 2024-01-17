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

namespace GUIs.Areas.NguoiTuyenDung.Controllers
{
    [Area("NguoiTuyenDung")]
    public class HomeController : Controller
    {
        private const string ID_CONGVIEC = "ID_CONGVIEC";
        private const string DANH_GIA = "DANH_GIA";
        public IActionResult Index()
        {
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
            return View();
        }
        public JsonResult Themmoi(string name,int mintuoi,int maxtuoi,DateTime timework,string address,int salary,string note)
        {

            CongViecDAO cv = new CongViecDAO();
            CongViec item = new CongViec();
            item.Name = name;
            item.Alias = "";
            item.Mintuoi = mintuoi;
            item.Maxtuoi = maxtuoi;
            item.Timework = timework;
            
            item.Address = address;
            item.Salary = salary;
            item.Note = note;
            item.State = 1;
            cv.InsertOrUpdate(item);
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
            HttpContext.Session.SetInt32(ID_CONGVIEC,id);
            ViewBag.Pagesize = DataServices.Pagesize()  ;          
            return View();
        }
        [HttpPost]
        public JsonResult getListNguoiLaoDong(int index, int size)
        {
            UngTuyenDAO cv = new UngTuyenDAO();
            int total = 0;
            int id = HttpContext.Session.GetInt32(ID_CONGVIEC)??0 ;
            var query = cv.getListUngTuyen(out total, id, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        [HttpPost]
        public JsonResult DuyetHoSo(int id)
        {
            UngTuyenDAO nhanvien = new UngTuyenDAO();
            var item = nhanvien.getItem(id);           
            item.Apply = 1;
            nhanvien.InsertOrUpdate(item);
            return Json(new { mess = "Duyệt hồ sơ thành công" });
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
            return View();
        }
        [HttpPost]
        public JsonResult DanhGiaCongViec(int sao, string nhanxet)
        {
            UngTuyenDAO ungTuyen = new UngTuyenDAO();
            int id = HttpContext.Session.GetInt32(DANH_GIA) ?? 0;
            var item = ungTuyen.getItem(id);         
            item.Danhgialaodong = sao;
            item.Nhanxetlaodong = nhanxet;        
            ungTuyen.InsertOrUpdate(item);
            return Json(new { mess = "Đánh giá người lao động thành công" });
        }
    }

}
///