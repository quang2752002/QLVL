using GUIs.Helper;
using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System.Drawing;
using System.Xml.Linq;

namespace GUIs.Models.DAO
{
    public class NguoiTuyenDungDAO
    {
        private QLVLContext context = new QLVLContext();
        public void InsertOrUpdate(NguoiTuyenDung item)
        {
            if (item.Id == 0)
            {
                context.NguoiTuyenDungs.Add(item);
            }
            context.SaveChanges();
        }
        public NguoiTuyenDung getItem(int id)
        {
            var query = context.NguoiTuyenDungs.Where(x => x.Id == id).FirstOrDefault();
           
            return query;
        }
        public NguoiTuyenDung getItemByEmail(string email)
        {
            var query = context.NguoiTuyenDungs.Where(x => x.Email == email).FirstOrDefault();
          
            return query;
        }
        public NguoiTuyenDungVIEW getItemView(int id)
        {
            var query = (from a in context.NguoiTuyenDungs
                         where a.Id == id
                         select new NguoiTuyenDungVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Alias = a.Alias,
                             Email = a.Email,
                             Location = a.Location,
                             Diachi = a.Diachi,
                             Fanpage = a.Fanpage,
                             Sdt = a.Sdt,
                             Actived = a.Actived,
                             Locked = a.Locked,
                             Username = a.Username,
                             Password = a.Password,
                             Lastlogin = a.Lastlogin,
                             Image = a.Image,
                             Introduce = a.Introduce
                         }).FirstOrDefault();
            
            return query;
        }
        public List<NguoiTuyenDungVIEW> getList(String name, out int total, int index = 1, int size = 10)
        {
            if (name == null) name = "";
            var query = (from a in context.NguoiTuyenDungs
                         select new NguoiTuyenDungVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Alias = a.Alias,
                             Email = a.Email,
                             Location = a.Location,
                             Diachi = a.Diachi,
                             Fanpage = a.Fanpage,
                             Sdt = a.Sdt,
                             Actived = a.Actived,
                             Locked = a.Locked,
                             Username = a.Username,
                             Password = a.Password,
                             Lastlogin = a.Lastlogin,
                             Image = a.Image,
                             Introduce = a.Introduce
                         })
                         .OrderByDescending(a => a.Id)
                         .ToList();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name)).ToList();
            }

            total = query.Count();

            if (size != 0 && index != 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }

        public List<NguoiTuyenDungVIEW> Search(string name, int thang, int nam, out int total, int index = 1, int size = 10)
        {
            if (name == null) name = "";
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);

            var query = (from a in context.NguoiTuyenDungs
                         let congviecdang = context.CongViecs.Count(c => c.Idnguoituyendung == a.Id && c.Timework >= start && c.Timework <= end)
                         where congviecdang > 0
                         select new NguoiTuyenDungVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Alias = a.Alias,
                             Email = a.Email,
                             Location = a.Location,
                             Diachi = a.Diachi,
                             Fanpage = a.Fanpage,
                             Sdt = a.Sdt,
                             Actived = a.Actived,
                             Locked = a.Locked,
                             Username = a.Username,
                             Password = a.Password,
                             Lastlogin = a.Lastlogin,
                             Image = a.Image,
                             Introduce = a.Introduce,
                             Congviechoanthanh = context.CongViecs.Count(c => c.Idnguoituyendung == a.Id && c.State == 1 && c.Timework >= start && c.Timework <= end),
                             Congviecchuahoanthanh = context.CongViecs.Count(c => c.Idnguoituyendung == a.Id && c.State == 0 && c.Timework >= start && c.Timework <= end),
                             congviecdang = congviecdang
                         });

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            total = query.Count();

            if (size != 0 && index != 0)
            {
                query = query.Skip((index - 1) * size).Take(size);
            }

            return query.ToList();
        }




        public void Detele(int id)
        {
            NguoiTuyenDung x = getItem(id);
            context.NguoiTuyenDungs.Remove(x);
            context.SaveChanges();
        }
        public int Login(string username, string password)
        {
            var query = (from a in context.NguoiTuyenDungs
                         where a.Username == username && a.Password == password
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
            var query = (from a in context.NguoiTuyenDungs
                         where a.Email == email
                         select new NguoiTuyenDungVIEW
                         {
                             Id = a.Id,
                         }).FirstOrDefault();
            if (query != null)
                return query.Id;
            return -1;
        }
        public Boolean CheckDangky(string text)
        {

            var query = (from a in context.NguoiTuyenDungs
                         where(a.Sdt == text || a.Email== text || a.Username == text)
                         select new NguoiTuyenDungVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Alias = a.Alias,
                             Email = a.Email,
                             Location = a.Location,
                             Diachi = a.Diachi,
                             Fanpage = a.Fanpage,
                             Sdt = a.Sdt,
                             Actived = a.Actived,
                             Locked = a.Locked,
                             Username = a.Username,
                             Password = a.Password,
                             Lastlogin = a.Lastlogin,
                             Image = a.Image,
                             Introduce = a.Introduce
                         }).FirstOrDefault();
            if (query != null)
                return false;
            return true;
        }
        public int ChangePassword(string username, string password)
        {
            var query = (from a in context.NguoiTuyenDungs
                         where a.Username == username && a.Password == password
                         select new NguoiLaoDongVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                            
                             Fanpage = a.Fanpage,
                             Image = a.Image,
                             Introduce = a.Introduce,
                           
                            
                         }).FirstOrDefault();
            if (query == null)
                return -1;
            return query.Id;
        }
        public Boolean CheckDangKy(string username)
        {
            var query = (from a in context.NguoiTuyenDungs
                         where (a.Email == username || a.Sdt == username || a.Username == username)
                         select new NguoiTuyenDungVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                            
                             Fanpage = a.Fanpage,
                             Image = a.Image,
                             Introduce = a.Introduce,
                            
                         }).FirstOrDefault();
            if (query != null)
                return false;
            return true;
        }
    }
}
