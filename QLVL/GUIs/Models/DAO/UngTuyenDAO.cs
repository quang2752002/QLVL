using GUIs.Models.EF;
using GUIs.Models.VIEW;

namespace GUIs.Models.DAO
{
    public class UngTuyenDAO
    {
        private QLVLContext context = new QLVLContext();
        public void InsertOrUpdate(UngTuyen item)
        {
            if (item.Id == 0)
            {
                context.UngTuyens.Add(item);
            }
            context.SaveChanges();
        }
        public UngTuyen getItem(int id)
        {
            var query = context.UngTuyens.Where(x => x.Id == id).FirstOrDefault();
            return query;
        }
        public UngTuyenVIEW getItemView(int id)
        {
            var query = (from a in context.UngTuyens
                         where a.Id == id
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec=a.Idcongviec,
                             Idnguoilaodong=a.Idnguoilaodong,
                             Date=a.Date,
                             Salary=a.Salary,
                             Apply=a.Apply,
                             Danhgiacongviec=a.Danhgiacongviec,
                             Danhgialaodong=a.Danhgialaodong,
                             Nhanxetcongviec=a.Nhanxetcongviec,
                             Nhanxetlaodong=a.Nhanxetlaodong,

                         }).FirstOrDefault();
            if (query == null)
            {
                throw new InvalidOperationException($"No record found with Id: {id}");
            }
            return query;
        }
        public List<UngTuyenVIEW> Search(out int total, int tutuoi = 0, int dentuoi = 0, string vitri = "", int index = 1, int size = 10)
        {
           

            var query = (from a in context.UngTuyens
                        
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                             Date = a.Date,
                             Salary = a.Salary,
                             Apply = a.Apply,
                             Danhgiacongviec = a.Danhgiacongviec,
                             Danhgialaodong = a.Danhgialaodong,
                             Nhanxetcongviec = a.Nhanxetcongviec,
                             Nhanxetlaodong = a.Nhanxetlaodong,
                         }).ToList();
           
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="total"></param>
        /// <param name="idCv"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<UngTuyenVIEW> getListUngTuyen(out int total,int idCv, int index = 1, int size = 10,int apply=0)
        {
            var query = (from a in context.UngTuyens
                         join b in context.CongViecs on a.Idcongviec equals b.Id
                         join c in context.NguoiLaoDongs on a.Idnguoilaodong equals c.Id    
                         where b.Id==idCv
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                             Date = a.Date,
                             Salary = a.Salary,
                             Apply = a.Apply,
                             Tennguoilaodong=c.Name ?? "",
                             sex=c.Sex.Value,
                             heath=c.Heath??"",
                             image=c.Image??"",
                             phone=c.Phone??"",
                             age=DateTime.Now.Year- c.Birthday.Value.Year,
                             diachi=c.Address ?? ""
                         }).ToList();
            if(apply!=0)
            {
                query = query.Where(x=>x.Apply==apply).ToList();
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
            UngTuyen x = getItem(id);
            context.UngTuyens.Remove(x);
            context.SaveChanges();
        }
        public int Check(int idcongviec,int idnguoilaodong)
        {
            var query = (from a in context.UngTuyens
                         where a.Idcongviec == idcongviec&&a.Idnguoilaodong == idnguoilaodong
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                             Date = a.Date,
                             Salary = a.Salary,
                             Apply = a.Apply,
                             Danhgiacongviec = a.Danhgiacongviec,
                             Danhgialaodong = a.Danhgialaodong,
                             Nhanxetcongviec = a.Nhanxetcongviec,
                             Nhanxetlaodong = a.Nhanxetlaodong,
                         }).FirstOrDefault();
           if (query != null)
                return query.Id;
            return -1;
        }
    }
}
