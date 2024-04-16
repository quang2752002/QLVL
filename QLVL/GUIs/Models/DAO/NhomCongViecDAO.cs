using GUIs.Helper;
using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System.Xml.Linq;

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
        public List<NhomCongViecVIEW> getListNhomCongViec(int thang, int nam)
        {
            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);

            var query = (from a in context.NhomCongViecs
                         join b in context.CongViecNhoms on a.Id equals b.Idnhomcongviec into bg
                         from b in bg.DefaultIfEmpty()
                         join c in context.CongViecs on b.Idcongviec equals c.Id into cg
                         from c in cg.DefaultIfEmpty()
                         where (c.Timework >= start && c.Timework <= end)
                         group new { a, c } by new { a.Id, a.Name, a.Note } into grouped
                         select new NhomCongViecVIEW
                         {
                             Id = grouped.Key.Id,
                             Name = grouped.Key.Name,
                             Note = grouped.Key.Note,
                             soluong = grouped.Count()
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
        public List<int> BieuDo(int thang, int nam)
        {
            List<int> rs = new List<int>();

            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);

            var query = (from a in context.NhomCongViecs
                         join b in context.CongViecNhoms on a.Id equals b.Idnhomcongviec into bg
                         from b in bg.DefaultIfEmpty()
                         join c in context.CongViecs on b.Idcongviec equals c.Id into cg
                         from c in cg.DefaultIfEmpty()
                         where (c.Timework >= start && c.Timework <= end)
                         group new { a, c } by new { a.Id, a.Name, a.Note } into grouped
                         select new NhomCongViecVIEW
                         {
                             Id = grouped.Key.Id,
                             Name = grouped.Key.Name,
                             Note = grouped.Key.Note,
                             soluong = grouped.Count()
                         }).ToList();

            // Extract counts from query and add them to the result list
            foreach (var item in query)
            {
                rs.Add(item.soluong);
            }

            return rs;
        }
        public List<string> ListNameNhomcongViec(int thang, int nam)
        {
            List<string> rs = new List<string>();

            DateTime start = DateServices.GetFirstDayOfMonth(thang, nam);
            DateTime end = DateServices.GetLastDayOfMonth(thang, nam);

            var query = (from a in context.NhomCongViecs
                         join b in context.CongViecNhoms on a.Id equals b.Idnhomcongviec into bg
                         from b in bg.DefaultIfEmpty()
                         join c in context.CongViecs on b.Idcongviec equals c.Id into cg
                         from c in cg.DefaultIfEmpty()
                         where (c.Timework >= start && c.Timework <= end)
                         group new { a, c } by new { a.Id, a.Name, a.Note } into grouped
                         select new NhomCongViecVIEW
                         {
                             Id = grouped.Key.Id,
                             Name = grouped.Key.Name,
                             Note = grouped.Key.Note,
                             soluong = grouped.Count()
                         }).ToList();
 
            foreach (var item in query)
            {
                rs.Add(item.Name);
            }

            return rs;
        }
    }
}
