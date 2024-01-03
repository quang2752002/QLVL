using GUIs.Models.EF;
using GUIs.Models.VIEW;

namespace GUIs.Models.DAO
{
    public class nguoituyendungDAO
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
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public nguoituyendungVIEW getItemView(int id)
        {
            var query = (from a in context.NguoiTuyenDungs
                         where a.Id == id
                         select new nguoituyendungVIEW
                         {
                             Id = a.Id,
                             Hoten = a.Hoten,
                             Cccd = a.Cccd,
                             Tuoi = a.Tuoi,
                             Sdt = a.Sdt,
                             Diachi = a.Diachi,
                             Username = a.Username,
                             Password = a.Password,
                             Trangthai = a.Trangthai,
                         }).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }

        public List<nguoituyendungVIEW> Search(String name, out int total, int index = 1, int size = 10)
        {
            if (name == null) name = "";
            var query = (from a in context.NguoiTuyenDungs
                         select new nguoituyendungVIEW
                         {
                             Id = a.Id,
                             Hoten = a.Hoten,
                             Cccd = a.Cccd,
                             Tuoi = a.Tuoi,
                             Sdt = a.Sdt,
                             Diachi = a.Diachi,
                             Username = a.Username,
                             Password = a.Password,
                             Trangthai = a.Trangthai,
                         }).ToList();
            if (name != "")
                query = query.Where(x => x.Hoten.Contains(name)).ToList();
            total = query.Count();
            if (size != 0 && index != 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }
        public void Detele(int id)
        {
            NguoiTuyenDung x = getItem(id);
            context.NguoiTuyenDungs.Remove(x);
            context.SaveChanges();
        }
    }
}
