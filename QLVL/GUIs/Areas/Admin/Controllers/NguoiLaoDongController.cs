using GUIs.Helper;
using GUIs.Models.DAO;
using Microsoft.AspNetCore.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    public class NguoiLaoDongController : Controller
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
        public IActionResult Index()
        {

            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            return View();
        }
        //public JsonResult showlist(string name = "", int thang=0,int nam=0, int index = 1, int size = 10)
        //{
        //    NguoiLaoDongDAO x = new NguoiLaoDongDAO();
          
        //        int total = 0;
        //        var query = x.search(total, tuoi, vitri, out total, index, size);
        //        string page = Support.Support.InTrang(total, index, size);

        //        return Json(new { data = query, page = page });
        //}
    }
}
