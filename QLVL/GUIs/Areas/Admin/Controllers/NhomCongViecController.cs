using GUIs.Helper;
using GUIs.Models.DAO;
using GUIs.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhomCongViecController : BaseAdminController
    {
        public IActionResult Index()
        {
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }   
        public JsonResult getListNhomCongViec(string name, int index, int size)
        {
            NhomCongViecDAO x = new NhomCongViecDAO();
            int total = 0;
            var query = x.Search(out total, name, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        public JsonResult CreateOrUpdate(int id,string name,string mota)
        {

            NhomCongViecDAO nhanvien = new NhomCongViecDAO();
            NhomCongViec item = new NhomCongViec();
            if (id != 0) item = nhanvien.getItem(id);
            item.Name = name;
            item.Note = mota;     
            nhanvien.InsertOrUpdate(item);
            if(id!=0)
                return Json(new { mess = "Chỉnh sửa nhóm công việc thành công" });
            return Json(new { mess = "Thêm nhóm công việc thành công" });
        }
        public JsonResult getNhomcongviec(int id)
        {
            NhomCongViecDAO nhomCongViec = new NhomCongViecDAO();

            var query = nhomCongViec.getItemView(id);
            return Json(new { data = query });
        }

    }
}
