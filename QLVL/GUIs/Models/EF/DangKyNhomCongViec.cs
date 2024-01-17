using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class DangKyNhomCongViec
    {
        public int Id { get; set; }
        public int? Idnguoilaodong { get; set; }
        public int? Idnhomcongviec { get; set; }

        public virtual NguoiLaoDong? IdnguoilaodongNavigation { get; set; }
        public virtual NhomCongViec? IdnhomcongviecNavigation { get; set; }
    }
}
