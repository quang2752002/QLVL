using GUIs.Models.EF;
using GUIs.Models.VIEW;

namespace GUIs.Models.DAO
{
    public class CongViecNhomDAO
    {
        private QLVLContext context = new QLVLContext();
        public void InsertOrUpdate(CongViecNhom item)
        {
            if (item.Id == 0)
            {
                context.CongViecNhoms.Add(item);
            }
            context.SaveChanges();
        }
        public CongViecNhom getItem(int id)
        {
            var query = context.CongViecNhoms.Where(x => x.Id == id).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public CongViecNhomVIEW getItemView(int id)
        {
            var query = (from a in context.CongViecNhoms
                         where a.Id == id
                         select new CongViecNhomVIEW
                         {
                             Id=a.Id,
                             Idcongviec=a.Idcongviec.Value,
                             Idnhomcongviec=a.Idnhomcongviec.Value,
                         }).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public List<CongViecNhomVIEW> Search(out int total, int index = 1, int size = 10)
        {
         

            var query = (from a in context.CongViecNhoms
                      
                         select new CongViecNhomVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec.Value,
                             Idnhomcongviec = a.Idnhomcongviec.Value,
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
            CongViecNhom x = getItem(id);
            context.CongViecNhoms.Remove(x);
            context.SaveChanges();
        }
    }
}
