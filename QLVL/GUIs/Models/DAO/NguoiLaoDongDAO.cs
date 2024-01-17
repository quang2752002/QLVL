using GUIs.Helper;
using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System.Drawing;

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
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
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
        public List<NguoiLaoDongVIEW> getList(out int total,int thang,int nam, int index = 1, int size = 10)
        {
            var query = (from a in context.NguoiLaoDongs
                         join b in context.UngTuyens on a.Id equals b.Idnguoilaodong
                         join c in context.CongViecs on b.Idcongviec equals c.Id
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
                         }).ToList();
            if (query != null)
                return false;
            return true;      
        }
        
    }
}
