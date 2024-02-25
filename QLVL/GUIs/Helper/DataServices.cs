using static GUIs.Areas.Admin.Controllers.NguoiLaoDongController;

namespace GUIs.Helper
{
    public static class DataServices
    {
        public static int getUserId(HttpContext context)
        {
            int id = context.Session.GetInt32(CustomeCommon.USER_ID) ?? 0;
            return id;
        }
        public static string getRouoter(HttpContext context)
        {
            string id = context.Session.GetString(CustomeCommon.ROUTER);
            return id;
        }
        public static List<Lopchung> Pagesize() {
            List<Lopchung> pagesize = new List<Lopchung>();
            pagesize.Add(new Lopchung { ID = 10 });
            pagesize.Add(new Lopchung { ID = 20 });
            pagesize.Add(new Lopchung { ID = 30 });
            pagesize.Add(new Lopchung { ID = 40 });
            pagesize.Add(new Lopchung { ID = 50 });
            return pagesize;
        }
        public static List<Year> Year()
        {
            List<Year> recentYears = new List<Year>();
            recentYears.Add(new Year { year = DateTime.Now.Year });
            int numberOfRecentYears = 4;
            for (int i = 1; i <= numberOfRecentYears; i++)
            {
                recentYears.Add(new Year { year = DateTime.Now.Year - i });
            }
           
            return recentYears;
        }
        public static List<Month> Months()
        {
            List<Month> months = new List<Month>();
            months.Add(new Month { month = DateTime.Now.Month });
            for (int i = DateTime.Now.Month + 1; i <= 12; i++)
            {
                months.Add(new Month { month = i });
            }
            for (int i = DateTime.Now.Month - 1; i >= 1; i--)
            {
                months.Add(new Month { month = i });
            }
            return months;
        }
    }
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
}
