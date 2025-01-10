using GUIs.Models.EF;
using GUIs.Models.VIEW;

namespace GUIs.Models.DAO
{
    public class DangKyNhomCongViecDAO
    {
        private QLVLContext context = new QLVLContext();
        public void InsertOrUpdate(DangKyNhomCongViec item)
        {
            if (item.Id == 0)
            {
                context.DangKyNhomCongViecs.Add(item);
            }
            context.SaveChanges();
        }
        public DangKyNhomCongViec getItem(int id)
        {
            var query = context.DangKyNhomCongViecs.Where(x => x.Id == id).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public DangKyNhomCongViecVIEW getItemView(int id)
        {
            var query = (from a in context.DangKyNhomCongViecs
                         where a.Id == id
                         select new DangKyNhomCongViecVIEW
                         {
                             Id = a.Id,
                             Idnhomcongviec=a.Idnhomcongviec,
                             Idnguoilaodong=a.Idnguoilaodong,
                         }).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public List<DangKyNhomCongViecVIEW> Search(out int total, int index = 1, int size = 10)
        {
         

            var query = (from a in context.DangKyNhomCongViecs
                        
                         select new DangKyNhomCongViecVIEW
                         {

                             Id = a.Id,
                             Idnhomcongviec = a.Idnhomcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
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
            DangKyNhomCongViec x = getItem(id);
            context.DangKyNhomCongViecs.Remove(x);
            context.SaveChanges();
        }
    }
}
