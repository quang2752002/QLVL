using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class DanhGium
    {
        public int Id { get; set; }
        public int? Idtuyendung { get; set; }
        public int? Idcongviec { get; set; }
        public int? Idlaodong { get; set; }
        public int? Sao { get; set; }
        public string? Danhgia { get; set; }
        public bool? Trangthai { get; set; }

        public virtual CongViec? IdcongviecNavigation { get; set; }
        public virtual NguoiLaoDong? IdlaodongNavigation { get; set; }
        public virtual NguoiTuyenDung? IdtuyendungNavigation { get; set; }
    }
}
