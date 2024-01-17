using GUIs.Helper;
using GUIs.Models.EF;
using GUIs.Models.VIEW;

namespace GUIs.Models.DAO
{
    public class CongViecDAO
    {
        private QLVLContext context = new QLVLContext();
        public void InsertOrUpdate(CongViec item)
        {
            if (item.Id == 0)
            {
                context.CongViecs.Add(item);
            }
            context.SaveChanges();
        }
        public CongViec getItem(int id)
        {
            var query = context.CongViecs.Where(x => x.Id == id).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public CongViecVIEW getItemView(int id)
        {
            var query = (from a in context.CongViecs
                         join b in context.NguoiTuyenDungs on a.Idnguoituyendung equals b.Id
                         where a.Id == id

                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung=a.Idnguoituyendung,
                             Name = a.Name,
                             Alias=a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi=a.Maxtuoi,
                             Timework=a.Timework,
                             Location=a.Location,
                             Address = a.Address,
                             Salary=a.Salary,
                             Note=a.Note,
                             State=a.State,
                             tennguoituyendung=b.Name,

                         }).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public List<CongViecVIEW> Search(out int total, String name = "",  int index = 1, int size = 10)
        {
            if (name == null) name = "";

            var query = (from a in context.CongViecs
                         where (a.Name.Contains(name))
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                             Timework = a.Timework,
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                         }).ToList();
            if (!string.IsNullOrEmpty(name) && name != "")
            {
                query = query.Where(x => x.Name.Contains(name)).ToList();
            }
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }
        /// <summary>
        /// Hhàm này dùng để: 
        /// </summary>
        /// <param name="total">Tổng số bản ghi trả về</param>
        /// <param name="name"></param>
        /// <param name="thang"></param>
        /// <param name="nam"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<CongViecVIEW> Search(out int total, string name="",int thang=0,int nam=0,int index=1,int size=10)
        {
            if (name == null) name = "";
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);
            var query = (from a in context.CongViecs
                         where (a.Name.Contains(name)&&a.Timework>=start&&a.Timework<=end)
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                             Timework = a.Timework,
                             TimeworkS=a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                         }).ToList();
            if (!string.IsNullOrEmpty(name) && name != "")
            {
                query = query.Where(x => x.Name.Contains(name)).ToList();
            }
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }
        /// <summary>
        /// Hàm này dùng: 
        /// </summary>
        /// <param name="total"></param>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<CongViecVIEW> getCongviec(out int total,int id, int index = 1, int size = 10)
        {
            
            var query = (from a in context.CongViecs
                         join b in context.NguoiTuyenDungs on a.Idnguoituyendung equals b.Id
                         where b.Id==id&&a.State==1                     
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                             Timework = a.Timework,
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                         }).ToList();           
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }
       
        public void Detele(int id)
        {
            CongViec x = getItem(id);
            context.CongViecs.Remove(x);
            context.SaveChanges();
        }
        /// <summary>
        /// Lấy danh sách công việc theo id người lao động đã làm theo tháng năm để hiển thị lên mục các công việc đã làm 
        /// </summary>
        /// <param name="total"></param>
        /// <param name="name"></param>
        /// <param name="thang"></param>
        /// <param name="nam"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<CongViecVIEW> DanhSachCongViec(out int total, int idnguoilaodong, string name = "", int apply = 1, int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            if (name == null) name = "";
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);
            var query = (from a in context.CongViecs
                         join b in context.NguoiTuyenDungs on a.Idnguoituyendung equals b.Id
                         join c in context.UngTuyens on a.Id equals c.Idcongviec       
                         where (a.Name.Contains(name) && a.Timework >= start && a.Timework <= end&&c.Idnguoilaodong==idnguoilaodong&&c.Apply==apply)
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                             Timework = a.Timework,
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                             luong=c.Salary.Value,
                             tennguoituyendung=b.Name,
                             apply=c.Apply.Value,
                         }).ToList();
            if (!string.IsNullOrEmpty(name) && name != "")
            {
                query = query.Where(x => x.Name.Contains(name)).ToList();
            }
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }
        public List<CongViecVIEW> getListCongViec(out int total, string name ,int id, int thang, int nam, int state, int index, int size)
        {
            if (name == null) name = "";
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);
            var query = (from a in context.CongViecs                                             
                         where (a.Name.Contains(name) && a.Timework >= start && a.Timework <= end && a.Idnguoituyendung == id && a.State ==state)
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                             Timework = a.Timework,
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,                         
                         }).ToList();
            if (!string.IsNullOrEmpty(name) && name != "")
            {
                query = query.Where(x => x.Name.Contains(name)).ToList();
            }
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }

    }
}
