using GUIs.Models.DAO;
using Microsoft.AspNetCore.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    public class Lopchung
    {
        public int ID { set; get; }
    }
    [Area("Admin")]
    public class nguoituyendungController : Controller
    {
        public IActionResult Index()
        {
            List<Lopchung> pagesize = new List<Lopchung>();
            pagesize.Add(new Lopchung { ID = 10 });
            pagesize.Add(new Lopchung { ID = 20 });
            pagesize.Add(new Lopchung { ID = 30 });
            pagesize.Add(new Lopchung { ID = 40 });
            pagesize.Add(new Lopchung { ID = 50 });
            ViewBag.Pagesize = pagesize;
            return View();       
        }
        public JsonResult Update(int id, string hoten, string cccd, int tuoi, string sdt, string diachi, string username, string password)
        {
            nguoituyendungDAO x = new nguoituyendungDAO();
            var item = x.getItem(id);
            item.Hoten = hoten;
            item.Cccd = cccd;
            item.Tuoi = tuoi;
            item.Sdt = sdt;
            item.Diachi = diachi;
            item.Username = username;
            item.Password = password;
            x.InsertOrUpdate(item);
            return Json(new { mess = "Chỉnh sửa thông tin cá nhân thành công" });
        }
        [HttpPost]
        public JsonResult ShowList(string name = "", int index = 1, int size = 10)
        {
            nguoituyendungDAO x = new nguoituyendungDAO();
            int total = 0;
            var query = x.Search(name, out total, index, size);
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = query, page = page });
        }
        public JsonResult Delete(int id)
        {
            nguoituyendungDAO x = new nguoituyendungDAO();
            x.Detele(id);
            return Json(new { mess = "Xóa tài khoản thành công" });
        }
        public JsonResult getNguoituyendung(int id)
        {
            nguoituyendungDAO x = new nguoituyendungDAO();
            var query = x.getItemView(id);
            return Json(new { data = query });
        }
    }
}
