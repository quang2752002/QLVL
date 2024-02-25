using GUIs.Models.EF;
using GUIs.Models.VIEW;

namespace GUIs.Models.DAO
{
    public class GopYDAO
    {
        private QLVLContext context = new QLVLContext();
        public void InsertOrUpdate(GopY item)
        {
            if (item.Id == 0)
            {
                context.Gopies.Add(item);
            }
            context.SaveChanges();
        }
        public GopY getItem(int id)
        {
            var query = context.Gopies.Where(x => x.Id == id).FirstOrDefault();

            return query;
        }
        public GopYVIEW getItemView(int id)
        {
            var query = (from a in context.Gopies
                         where a.Id == id
                         select new GopYVIEW
                         {
                             Id = a.Id,
                             Email=a.Email,
                             Noidung=a.Noidung
                         }).FirstOrDefault();

            return query;
        }
        public List<GopYVIEW> Search(out int total, int index = 1, int size = 10)
        {
          

            var query = (from a in context.Gopies
                       
                         select new GopYVIEW
                         {
                             Id = a.Id,
                             Email = a.Email,
                             Noidung = a.Noidung
                         }).ToList();
           
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }


        public List<GopYVIEW> getGopY(out int total, int id, int index = 1, int size = 10)
        {

            var query = (from a in context.Gopies
                         where a.Id == id
                         select new GopYVIEW
                         {

                             Id = a.Id,
                             Email = a.Email,
                             Noidung = a.Noidung
                         }).ToList();
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }

       
    }
}
