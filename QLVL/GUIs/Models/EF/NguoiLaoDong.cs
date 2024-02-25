using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class NguoiLaoDong
    {
        public NguoiLaoDong()
        {
            DangKyNhomCongViecs = new HashSet<DangKyNhomCongViec>();
            UngTuyens = new HashSet<UngTuyen>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Heath { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public string? Refcode { get; set; }
        public string? Fanpage { get; set; }
        public string? Image { get; set; }
        public string? Introduce { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Guid { get; set; }

        public virtual ICollection<DangKyNhomCongViec> DangKyNhomCongViecs { get; set; }
        public virtual ICollection<UngTuyen> UngTuyens { get; set; }
    }
}
