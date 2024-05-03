using GUIs.Helper;
using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System.Drawing;
using System.Xml.Linq;

namespace GUIs.Models.DAO
{
    public class NguoiLaoDongDAO
    {
        private QLVLContext context=new QLVLContext();  
        public void InsertOrUpdate(NguoiLaoDong item)
        {
            if (item.Id == 0)
            {
                context.NguoiLaoDongs.Add(item);
            }
            context.SaveChanges();
        }
        public NguoiLaoDong getItem(int id)
        {
            var query = context.NguoiLaoDongs.Where(x => x.Id == id).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public NguoiLaoDongVIEW getItemView(int id)
        {
            var query = (from a in context.NguoiLaoDongs
                         where a.Id == id
                         select new NguoiLaoDongVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Sex = a.Sex,
                             Birthday = a.Birthday,
                             Heath = a.Heath,
                             Phone = a.Phone,
                             Email=a.Email,
                             Code=a.Code,
                             Fanpage=a.Fanpage,
                             Image=a.Image,
                             Introduce=a.Introduce,
                             Address=a.Address,
                             Age = DateTime.Now.Year - a.Birthday.Value.Year,
                         }).FirstOrDefault();
            
            return query;
        }
        public List<NguoiLaoDongVIEW> Search(out int total, String name = "", int tutuoi = 0, int dentuoi = 0, string vitri = "", int index = 1, int size = 10)
        {
            if (name == null) name = "";

            var query = (from a in context.NguoiLaoDongs
                         where (a.Name.Contains(name))
                         select new NguoiLaoDongVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Sex = a.Sex,
                             Age = DateTime.Now.Year - a.Birthday.Value.Year,
                             Birthday = a.Birthday,
                             Heath = a.Heath,
                             Phone = a.Phone,
                             Email = a.Email,
                             Code = a.Code,
                             Fanpage = a.Fanpage,
                             Image = a.Image,
                             Introduce = a.Introduce,
                             Address = a.Address
                         }).ToList();
            if (tutuoi > 0)
            {
                query = query.Where(x => x.Age >= tutuoi && x.Age <= dentuoi).ToList();
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

        public List<NguoiLaoDongVIEW> getList(out int total, string name = "", int thang = 0, int nam = 0, int index = 1, int size = 10)
        {
            if (name == null) name = "";
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);

            var query = (from a in context.NguoiLaoDongs
                         join b in context.UngTuyens on a.Id equals b.Idnguoilaodong
                         join c in context.CongViecs on b.Idcongviec equals c.Id
                         where (b.Apply == 1 || b.Apply == 2 || b.Apply == 3) && b.Date >= start && b.Date <= end // Filter for completed jobs within the specified month and year
                         group b by new { a.Id, a.Name, a.Sex, a.Birthday, a.Heath, a.Phone, a.Email, a.Code, a.Fanpage, a.Image, a.Introduce, a.Address } into grouped
                         select new NguoiLaoDongVIEW
                         {
                             Id = grouped.Key.Id,
                             Name = grouped.Key.Name,
                             Sex = grouped.Key.Sex,
                             Age = DateTime.Now.Year - grouped.Key.Birthday.Value.Year,
                             Birthday = grouped.Key.Birthday,
                             Heath = grouped.Key.Heath,
                             Phone = grouped.Key.Phone,
                             Email = grouped.Key.Email,
                             Code = grouped.Key.Code,
                             Fanpage = grouped.Key.Fanpage,
                             Image = grouped.Key.Image,
                             Introduce = grouped.Key.Introduce,
                             Address = grouped.Key.Address,
                             congviechoanthanh = grouped.Count() // Count completed jobs
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
            NguoiLaoDong x = getItem(id);
            context.NguoiLaoDongs.Remove(x);
            context.SaveChanges();
        }
        public int Login(string username, string password)
        {
            var query = (from a in context.NguoiLaoDongs
                         where a.Username == username && a.Password== password
                         select new NguoiTuyenDungVIEW
                         {
                             Id = a.Id,
                         }).FirstOrDefault();
            if (query != null)
                return query.Id;
            return -1;
        }
        public int Check(string email)
        {
            var query = (from a in context.NguoiLaoDongs
                         where a.Email == email
                         select new NguoiLaoDongVIEW
                         {
                             Id = a.Id,
                         }).FirstOrDefault();
            if (query != null)
                return query.Id;
            return -1;
        }
        public NguoiLaoDong getItemByEmail(string email)
        {
            var query = context.NguoiLaoDongs.Where(a => a.Email == email).FirstOrDefault();
            return query;
        }
        public NguoiLaoDong CheckGuid(string guid)
        {
            var query = context.NguoiLaoDongs.Where(a => a.Guid == guid).FirstOrDefault();
            return query;
        }
        public Boolean CheckDangKy(string username)
        {
            var query = (from a in context.NguoiLaoDongs    
                         where (a.Email == username || a.Phone==username||a.Username==username)
                         select new NguoiLaoDongVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Sex = a.Sex,
                             Age = DateTime.Now.Year - a.Birthday.Value.Year,
                             Birthday = a.Birthday,
                             Heath = a.Heath,
                             Phone = a.Phone,
                             Email = a.Email,
                             Code = a.Code,
                             Fanpage = a.Fanpage,
                             Image = a.Image,
                             Introduce = a.Introduce,
                             Address = a.Address,
                         }).FirstOrDefault();
            if (query != null)
                return false;
            return true;      
        }
        
        public int getIdbyUngTuyen(int id)
        {
            var query = (from a in context.NguoiLaoDongs
                         join b in context.UngTuyens on a.Id equals b.Idnguoilaodong
                         where b.Id == id
                         select new NguoiLaoDongVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Sex = a.Sex,
                             Birthday = a.Birthday,
                             Heath = a.Heath,
                             Phone = a.Phone,
                             Email = a.Email,
                             Code = a.Code,
                             Fanpage = a.Fanpage,
                             Image = a.Image,
                             Introduce = a.Introduce,
                             Address = a.Address,
                             Age = DateTime.Now.Year - a.Birthday.Value.Year,
                         }).FirstOrDefault();

            return query.Id;
        }
        public List<NguoiLaoDongVIEW> ShowList(out int total, String name = "", int index = 1, int size = 10)
        {
            if (name == null) name = "";

            var query = (from a in context.NguoiLaoDongs
                         where (a.Name.Contains(name))
                         select new NguoiLaoDongVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Sex = a.Sex,
                             Age = DateTime.Now.Year - a.Birthday.Value.Year,
                             Birthday = a.Birthday,
                             Heath = a.Heath,
                             Phone = a.Phone,
                             Email = a.Email,
                             Code = a.Code,
                             Fanpage = a.Fanpage,
                             Image = a.Image,
                             Introduce = a.Introduce,
                             Address = a.Address
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
        public NguoiLaoDongVIEW getLaoDongByIdUngTuyen(int id)
        {
            var query = (from a in context.NguoiLaoDongs
                         join b in context.UngTuyens on a.Id equals b.Idnguoilaodong
                         join c in context.CongViecs on b.Idcongviec equals c.Id
                         where b.Id==id
                         select new NguoiLaoDongVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Sex = a.Sex,
                             Age = DateTime.Now.Year - a.Birthday.Value.Year,
                             Birthday = a.Birthday,
                             Heath = a.Heath,
                             Phone = a.Phone,
                             Email = a.Email,
                             Code = a.Code,
                             Fanpage = a.Fanpage,
                             Image = a.Image,
                             Introduce = a.Introduce,
                             Address = a.Address,
                             tencongviec=c.Name
                         }).FirstOrDefault();
            return query;
        }
        public int ChangePassword(string username,string password)
        {
            var query = (from a in context.NguoiLaoDongs
                         where a.Username==username&&a.Password==password
                         select new NguoiLaoDongVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Sex = a.Sex,
                             Birthday = a.Birthday,
                             Heath = a.Heath,
                             Phone = a.Phone,
                             Email = a.Email,
                             Code = a.Code,
                             Fanpage = a.Fanpage,
                             Image = a.Image,
                             Introduce = a.Introduce,
                             Address = a.Address,
                             Age = DateTime.Now.Year - a.Birthday.Value.Year,
                         }).FirstOrDefault();
            if (query == null)
                return -1;
            return query.Id;
        }


    }

}
