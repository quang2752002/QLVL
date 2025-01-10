using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class CongViecNhom
    {
        public int Id { get; set; }
        public int? Idcongviec { get; set; }
        public int? Idnhomcongviec { get; set; }

        public virtual CongViec? IdcongviecNavigation { get; set; }
        public virtual NhomCongViec? IdnhomcongviecNavigation { get; set; }
    }
}
