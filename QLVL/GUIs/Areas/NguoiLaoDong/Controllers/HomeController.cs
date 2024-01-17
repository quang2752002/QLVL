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
    public class HomeController : Controller
    {
        private const string ID_CV = "ID_CV";
        private const string DANH_GIA_CV = "DANH_GIA_CV";

        public IActionResult Index()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        public JsonResult ShowList(string name = "", int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            CongViecDAO x = new CongViecDAO();          
             int total = 0;
             var query = x.Search(out total, name, thang, nam,  index, size);
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
        public JsonResult Apply(int id,int luong)
        {
            UngTuyenDAO khachhang = new UngTuyenDAO();
            int idnguoilaodong = DataServices.getUserId(HttpContext);
            UngTuyen item = new UngTuyen();
            item.Idcongviec = id;
            item.Idnguoilaodong = idnguoilaodong;
            item.Date = DateTime.Now;
            item.Salary = luong;
            item.Apply = 0;
            if (khachhang.Check(id,idnguoilaodong)==-1){
                khachhang.InsertOrUpdate(item);
                return Json(new { mess = "Ứng tuyển thành công" });
            }
            return Json(new { mess ="Ứng tuyển công việc không thành công" });
        }
        public IActionResult Danhsachcongviec()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
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
            HttpContext.Session.SetInt32(DANH_GIA_CV, id);
            return View();
        }
        public JsonResult DanhGiaCongViec(int sao,string nhanxet)
        {
            UngTuyenDAO ungTuyen = new UngTuyenDAO();
            int id = HttpContext.Session.GetInt32(DANH_GIA_CV) ?? 0;
            var item = ungTuyen.getItem(id);
            item.Danhgiacongviec = sao;
            item.Nhanxetcongviec = nhanxet;
            ungTuyen.InsertOrUpdate(item);
            return Json(new { mess ="Đánh giá công việc thành công" });
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
           
    }
}
