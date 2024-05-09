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

        public List<UngTuyenVIEW> getListUngTuyen(out int total, int idCv, int index = 1, int size = 10, int apply = 0)
        {
            // Bắt đầu bằng truy vấn cơ bản
            var query = (from a in context.UngTuyens
                         join b in context.CongViecs on a.Idcongviec equals b.Id
                         join c in context.NguoiLaoDongs on a.Idnguoilaodong equals c.Id
                         where b.Id == idCv
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                             Date = a.Date,
                             Salary = a.Salary,
                             Apply = a.Apply,
                             Tennguoilaodong = c.Name ?? "",
                             timeworkS = b.Timework.Value.ToString("dd/MM/yyyy hh:mm"),
                             sex = c.Sex.HasValue ? c.Sex.Value : 1,
                             heath = c.Heath ?? "",
                             image = c.Image ?? "",
                             phone = c.Phone ?? "",
                             age = DateTime.Now.Year - c.Birthday.Value.Year,
                             diachi = c.Address ?? ""
                         }).ToList();

            // Điều chỉnh điều kiện truy vấn dựa trên giá trị của apply
           
                if (apply == 1)
                {
                    // Nếu apply = 1, lấy apply = 1, 2, 3
                    query = query.Where(x => x.Apply == 1 || x.Apply == 2 || x.Apply == 3).ToList();
                }
                else
                {
                    // Nếu apply không phải 1, lọc theo giá trị apply cụ thể
                    query = query.Where(x => x.Apply == apply).ToList();
                }
            

            // Tính tổng số mục trước khi phân trang
            total = query.Count();

            // Thực hiện phân trang nếu size và index hợp lệ
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
        public string getEmail(int id)
        {

            var query = (from a in context.UngTuyens
                         join b in context.NguoiLaoDongs on a.Idnguoilaodong equals b.Id
                         where a.Id==id
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                             
                             email=b.Email
                         }).FirstOrDefault();
            return query.email;
        }
       public int getIdUngTuyen(int idcongviec,int idnguoilaodong)
        {
            var query = (from a in context.UngTuyens
                       
                         where a.Idcongviec == idcongviec&&a.Idnguoilaodong==idnguoilaodong
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                         }).FirstOrDefault();
            return query.Id;  
        }
        public bool CheckDuyetHoSo(int IdNguoiLaoDong, DateTime? start, DateTime end)
        {
            var query = (from a in context.UngTuyens
                         join b in context.CongViecs on a.Idcongviec equals b.Id
                         join c in context.NguoiLaoDongs on a.Idnguoilaodong equals c.Id
                         where (c.Id == IdNguoiLaoDong &&
                                (((b.Timework <= start && b.Finish >= start) || (end >= b.Timework && end <= b.Finish)) &&
                                (a.Apply == 1||a.Apply==2)))
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong
                         }).ToList();

            return query.Count() == 0;
        }

        public List<UngTuyenVIEW> ListDanhgia(out int total,int id, int index = 1, int size = 10)
        {
            var query = (from a in context.UngTuyens
                         join b in context.NguoiLaoDongs on a.Idnguoilaodong equals b.Id
                         join c in context.CongViecs on a.Idcongviec equals c.Id
                         join d in context.NguoiTuyenDungs on c.Idnguoituyendung equals d.Id
                         where b.Id==id
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                             Date = a.Date,
                             Salary = a.Salary,
                             Apply = a.Apply,                          
                             Danhgialaodong = a.Danhgialaodong,                          
                             Nhanxetlaodong = a.Nhanxetlaodong,
                             Tennguoilaodong=b.Name,
                             image=b.Image,
                             Nguoituyendung=d.Name,
                             tencongviec=c.Name
                         }).ToList();

            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }
        public Boolean CheckDanhGiaLaoDong(int id)
        {
            var query = (from a in context.UngTuyens

                         where a.Id == id
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                            
                             Danhgiacongviec = a.Danhgiacongviec,
                             Danhgialaodong = a.Danhgialaodong,
                             Nhanxetcongviec = a.Nhanxetcongviec,
                             Nhanxetlaodong = a.Nhanxetlaodong,

                         }).FirstOrDefault();
            if (query.Danhgialaodong == null && query.Nhanxetlaodong == null)
               return true;
            return false;          
        }
        public Boolean CheckDanhGiaCongviec(int id)
        {
            var query = (from a in context.UngTuyens
                         where a.Id == id
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                             Danhgiacongviec = a.Danhgiacongviec,
                             Danhgialaodong = a.Danhgialaodong,
                             Nhanxetcongviec = a.Nhanxetcongviec,
                             Nhanxetlaodong = a.Nhanxetlaodong,

                         }).FirstOrDefault();
            if (query.Danhgialaodong == null && query.Nhanxetlaodong == null)
                return true;
            return false;
        }
        public List<UngTuyenVIEW> getListLaoDongByIdCongViec(int id)
        {
            var query = (from a in context.UngTuyens
                         join b in context.CongViecs on a.Idcongviec equals b.Id
                         join c in context.NguoiLaoDongs on a.Idnguoilaodong equals c.Id
                         where b.Id == id && (a.Apply == 1 || a.Apply == 2 || a.Apply == 3)
                         select new UngTuyenVIEW
                         {
                             Id = a.Id,
                             Idcongviec = a.Idcongviec,
                             Idnguoilaodong = a.Idnguoilaodong,
                             phone=c.Phone,
                             Salary = a.Salary,
                             Apply = a.Apply,
                             diachi=c.Address,
                             Tennguoilaodong = c.Name,
                             image = c.Image,

                         }).ToList();
            return query;
        }
        public List<int> getIdUngTuyenByIdCongViec(int idcongviec)
        {
            var query = (from a in context.UngTuyens
                         where a.Idcongviec == idcongviec &&(a.Apply==1 || a.Apply==2)
                         select new UngTuyenVIEW
                         {
                             Id=a.Id
                         }).ToList();
            List<int> result = new List<int>();
            foreach (var a in query)
            {
                result.Add(a.Id);
            }
            return result;
        }
    }
}
