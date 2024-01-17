using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class UngTuyen
    {
        public int Id { get; set; }
        public int? Idcongviec { get; set; }
        public int? Idnguoilaodong { get; set; }
        public DateTime? Date { get; set; }
        public int? Salary { get; set; }
        public int? Apply { get; set; }
        public int? Danhgialaodong { get; set; }
        public string? Nhanxetlaodong { get; set; }
        public int? Danhgiacongviec { get; set; }
        public string? Nhanxetcongviec { get; set; }

        public virtual CongViec? IdcongviecNavigation { get; set; }
        public virtual NguoiLaoDong? IdnguoilaodongNavigation { get; set; }
    }
}
