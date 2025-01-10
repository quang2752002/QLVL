using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class NguoiTuyenDung
    {
        public NguoiTuyenDung()
        {
            CongViecs = new HashSet<CongViec>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public string? Diachi { get; set; }
        public string? Fanpage { get; set; }
        public string? Sdt { get; set; }
        public int? Actived { get; set; }
        public int? Locked { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime? Lastlogin { get; set; }
        public string? Image { get; set; }
        public string? Introduce { get; set; }
        public string? Guid { get; set; }

        public virtual ICollection<CongViec> CongViecs { get; set; }
    }
}
