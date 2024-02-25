using GUIs.Models.EF;
using GUIs.Models.VIEW;

namespace GUIs.Models.DAO
{
    public class NhomCongViecDAO
    {
        private QLVLContext context = new QLVLContext();
        public void InsertOrUpdate(NhomCongViec item)
        {
            if (item.Id == 0)
            {
                context.NhomCongViecs.Add(item);
            }
            context.SaveChanges();
        }
        public NhomCongViec getItem(int id)
        {
            var query = context.NhomCongViecs.Where(x => x.Id == id).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public NhomCongViecVIEW getItemView(int id)
        {
            var query = (from a in context.NhomCongViecs
                         where a.Id == id
                         select new NhomCongViecVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                            Note=a.Note,

                         }).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public List<NhomCongViecVIEW> getList()
        {
            var query = (from a in context.NhomCongViecs          
                         select new NhomCongViecVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Note = a.Note,

                         }).ToList();
            
            return query;
        }
        public List<NhomCongViecVIEW> Search(out int total, String name = "", int index = 1, int size = 10)
        {
            if (name == null) name = "";

            var query = (from a in context.NhomCongViecs
                         where (a.Name.Contains(name))
                         select new NhomCongViecVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Note = a.Note,
                            
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
        public void Detele(int id)
        {
            NhomCongViec x = getItem(id);
            context.NhomCongViecs.Remove(x);
            context.SaveChanges();
        }
    }
}
