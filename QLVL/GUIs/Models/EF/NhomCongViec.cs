using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class NhomCongViec
    {
        public NhomCongViec()
        {
            CongViecNhoms = new HashSet<CongViecNhom>();
            DangKyNhomCongViecs = new HashSet<DangKyNhomCongViec>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<CongViecNhom> CongViecNhoms { get; set; }
        public virtual ICollection<DangKyNhomCongViec> DangKyNhomCongViecs { get; set; }
    }
}
