using GUIs.Helper;
using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System.Xml.Linq;

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
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish=a.Finish.Value,
                             finishS=a.Finish.Value.ToString("dd/MM/yyyy hh:mm"),
                         }).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public List<CongViecVIEW> Search(out int total, string name = "", int index = 1, int size = 10)
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
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                             finish = a.Finish.Value,
                             finishS = a.Finish.Value.ToString("dd/MM/yyyy hh:mm"),
                         });

            if (!string.IsNullOrEmpty(name) && name != "")
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            // Sắp xếp giảm dần theo Id
            query = query.OrderByDescending(x => x.Id);

            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size);
            }

            return query.ToList();
        }


        public List<CongViecVIEW> Search(List<int> list, out int total, string name = "", int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            if (name == null) name = "";
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);
            List<CongViecVIEW> query = new List<CongViecVIEW>();
            if (list.Count > 0)
            {
                query = (from a in context.CongViecs
                         join b in context.CongViecNhoms on a.Id equals b.Idcongviec
                         join c in context.NhomCongViecs on b.Idnhomcongviec equals c.Id
                         where (a.Name.Contains(name) && a.Timework >= start && a.Timework <= end) && list.Contains(c.Id)
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                             Timework = a.Timework.Value,
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish = a.Finish.HasValue ? a.Finish.Value : default(DateTime),
                             finishS = a.Finish.HasValue ? a.Finish.Value.ToString("dd/MM/yyyy hh:mm") : null,
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                         }).GroupBy(x => x.Id) // Group by Id
                           .Select(g => g.First()) // Take the first item from each group
                           .ToList();
            }
            else
            {
                query = (from a in context.CongViecs
                         join b in context.CongViecNhoms on a.Id equals b.Idcongviec
                         join c in context.NhomCongViecs on b.Idnhomcongviec equals c.Id
                         where (a.Name.Contains(name) && a.Timework >= start && a.Timework <= end)
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                             Timework = a.Timework.Value,
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish = a.Finish.HasValue ? a.Finish.Value : default(DateTime),
                             finishS = a.Finish.HasValue ? a.Finish.Value.ToString("dd/MM/yyyy hh:mm") : null,
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                         }).GroupBy(x => x.Id) // Group by Id
                           .Select(g => g.First()) // Take the first item from each group
                           .ToList();

            }
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
                             TimeworkS=a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish = a.Finish.Value,
                             finishS = a.Finish.Value.ToString("dd/MM/yyyy hh:mm"),
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
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish = a.Finish.Value,
                             finishS = a.Finish.Value.ToString("dd/MM/yyyy hh:mm"),
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                             luong=c.Salary.Value,
                             tennguoituyendung=b.Name,
                             apply=c.Apply.Value,
                             sdt=b.Sdt,
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
                            
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish = a.Finish.Value,
                             finishS = a.Finish.Value.ToString("dd/MM/yyyy hh:mm"),
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
        public List<CongViecVIEW> ListCongViecYeuThich(out int total, int idnguoilaodong, int index = 1, int size = 10)
        {
            int thang = DateTime.Now.Month;
            int nam = DateTime.Now.Year;
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);
            var query = (from a in context.CongViecs
                         join b in context.CongViecNhoms on a.Id equals b.Idcongviec
                         join c in context.NhomCongViecs on b.Idnhomcongviec equals c.Id
                         join d in context.DangKyNhomCongViecs on c.Id equals d.Idnhomcongviec
                         where (a.Timework >= start && a.Timework <= end&&d.Idnguoilaodong==idnguoilaodong)
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                           
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                             Timework = a.Timework,
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish = a.Finish.Value,
                             finishS = a.Finish.Value.ToString("dd/MM/yyyy hh:mm"),
                         }).ToList();
           
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }
        public List<CongViecVIEW> getCongViec(out int total, int idnhomcongviec, int thang=0,int nam=0,int index = 1, int size = 10)
        {
            if (thang==0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);
            var query = (from a in context.CongViecs
                         join b in context.CongViecNhoms on a.Id equals b.Idcongviec
                         join c in context.NhomCongViecs on b.Idnhomcongviec equals c.Id                        
                         where c.Id == idnhomcongviec && a.Timework >= start && a.Timework <= end
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                             Timework = a.Timework,
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish = a.Finish.Value,
                             finishS = a.Finish.Value.ToString("dd/MM/yyyy hh:mm"),
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
        public CongViecVIEW getCongViecUngTuyen(int id)
        {
            var query = (from a in context.CongViecs
                         join b in context.UngTuyens on a.Id equals b.Idcongviec
                         where b.Id == id
                         select new CongViecVIEW
                         {
                             Id = a.Id,
                             Idnguoituyendung = a.Idnguoituyendung,
                             Name = a.Name,
                             Alias = a.Alias,
                             Mintuoi = a.Mintuoi,
                             Maxtuoi = a.Maxtuoi,
                             Timework = a.Timework,
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish = a.Finish.Value,
                             finishS = a.Finish.Value.ToString("dd/MM/yyyy hh:mm"),
                             Location = a.Location,
                             Address = a.Address,
                             Salary = a.Salary,
                             Note = a.Note,
                             State = a.State,
                         }).FirstOrDefault();
            return query;
        }
        public List<CongViecVIEW> ShowList(out int total, string name, int thang, int nam, int state, int index, int size)
        {
            if (name == null) name = "";
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);
            var query = (from a in context.CongViecs
                         join b in context.NguoiTuyenDungs on a.Idnguoituyendung equals b.Id
                         where (a.Name.Contains(name) && a.Timework >= start && a.Timework <= end && a.State == state)
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
                             tennguoituyendung=b.Name,
                             TimeworkS = a.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             finish = a.Finish.Value,
                             finishS = a.Finish.Value.ToString("dd/MM/yyyy hh:mm"),
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
