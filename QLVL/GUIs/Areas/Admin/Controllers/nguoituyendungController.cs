using GUIs.Models.DAO;
using Microsoft.AspNetCore.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    public class Lopchung
    {
        public int ID { set; get; }
    }
    public class Year
    {
        public int year { set; get; }
    }
    public class Month
    {
        public int month { set; get; }
    }
    [Area("Admin")]
    public class nguoituyendungController : Controller
    {
        public ActionResult Index()
        {
            // Create a list to store Lopchung objects
            List<Lopchung> pagesize = new List<Lopchung>();

            // Add Lopchung objects to the list with different ID values
            pagesize.Add(new Lopchung { ID = 10 });
            pagesize.Add(new Lopchung { ID = 20 });
            pagesize.Add(new Lopchung { ID = 30 });
            pagesize.Add(new Lopchung { ID = 40 });
            pagesize.Add(new Lopchung { ID = 50 });

            // Add current year and month to ViewBag
            ViewBag.CurrentYear = DateTime.Now.Year;
            ViewBag.CurrentMonth = DateTime.Now.Month;

            // Create a list to store recent years, including the current year
            List<Year> recentYears = new List<Year>();

            // Add the current year to the beginning of the list
            recentYears.Add(new Year { year = DateTime.Now.Year });

            // Specify the number of additional recent years you want to include (e.g., 4 years)
            int numberOfRecentYears = 4;

            // Add recent years to the list
            for (int i = 1; i <= numberOfRecentYears; i++)
            {
                recentYears.Add(new Year { year = DateTime.Now.Year - i });
            }

            // Add the list of recent years to ViewBag
            ViewBag.RecentYears = recentYears;

            // Create a list to store months
            List<Month> months = new List<Month>();

            // Add the current month to the beginning of the list
            months.Add(new Month { month = DateTime.Now.Month });

            // Add subsequent months to the list in ascending order
            for (int i = DateTime.Now.Month + 1; i <= 12; i++)
            {
                months.Add(new Month { month = i });
            }

            // Add preceding months to the list in descending order
            for (int i = DateTime.Now.Month - 1; i >= 1; i--)
            {
                months.Add(new Month { month = i });
            }

            // Add the list of months to ViewBag
            ViewBag.Months = months;

            // Add the list of Lopchung objects to ViewBag
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
        public JsonResult ShowList(string name = "",int thang=0,int nam = 0, int index = 1, int size = 10)
        {
            if (thang == 0 || nam == 0)
            {
                // Set default values here, for example, the current month and year
                thang = DateTime.Now.Month;
                nam = DateTime.Now.Year;
            }
            nguoituyendungDAO x = new nguoituyendungDAO();
            int total = 0;
            var query = x.Search(name, thang, nam, out total, index, size);
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
