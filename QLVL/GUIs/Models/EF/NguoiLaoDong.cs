using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class NguoiLaoDong
    {
        public NguoiLaoDong()
        {
            DanhGia = new HashSet<DanhGium>();
            UngTuyens = new HashSet<UngTuyen>();
        }

        public int Id { get; set; }
        public string? Hoten { get; set; }
        public string? Cccd { get; set; }
        public int? Age { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string? Diachi { get; set; }
        public string? Sdt { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Img { get; set; }
        public bool? Trangthai { get; set; }

        public virtual ICollection<DanhGium> DanhGia { get; set; }
        public virtual ICollection<UngTuyen> UngTuyens { get; set; }
    }
}
