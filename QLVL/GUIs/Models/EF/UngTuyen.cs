using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class UngTuyen
    {
        public UngTuyen()
        {
            CongViecs = new HashSet<CongViec>();
        }

        public int Id { get; set; }
        public int? Idlaodong { get; set; }
        public int? Idcongviec { get; set; }
        public int? Tienluong { get; set; }
        public string? Mota { get; set; }
        public bool? Trangthai { get; set; }

        public virtual NguoiLaoDong? IdlaodongNavigation { get; set; }
        public virtual ICollection<CongViec> CongViecs { get; set; }
    }
}
