using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class CongViec
    {
        public CongViec()
        {
            DanhGia = new HashSet<DanhGium>();
        }

        public int Id { get; set; }
        public int? Idtuyendung { get; set; }
        public int? Idungtuyen { get; set; }
        public string? Tencongviec { get; set; }
        public DateTime? Thoigianbatdau { get; set; }
        public DateTime? Thoigianketthuc { get; set; }
        public string? Diachi { get; set; }
        public int? Luongbatdau { get; set; }
        public int? Luongketthuc { get; set; }
        public int? Soluong { get; set; }
        public DateTime? Tuyenbatdau { get; set; }
        public DateTime? Tuyenketthuc { get; set; }
        public string? Mota { get; set; }
        public bool? Trangthai { get; set; }

        public virtual NguoiTuyenDung? IdtuyendungNavigation { get; set; }
        public virtual UngTuyen? IdungtuyenNavigation { get; set; }
        public virtual ICollection<DanhGium> DanhGia { get; set; }
    }
}
