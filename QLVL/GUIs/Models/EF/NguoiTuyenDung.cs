using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class NguoiTuyenDung
    {
        public NguoiTuyenDung()
        {
            CongViecs = new HashSet<CongViec>();
            DanhGia = new HashSet<DanhGium>();
        }

        public int Id { get; set; }
        public string? Hoten { get; set; }
        public string? Cccd { get; set; }
        public int? Tuoi { get; set; }
        public string? Sdt { get; set; }
        public string? Diachi { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? Trangthai { get; set; }

        public virtual ICollection<CongViec> CongViecs { get; set; }
        public virtual ICollection<DanhGium> DanhGia { get; set; }
    }
}
