using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class CongViec
    {
        public CongViec()
        {
            CongViecNhoms = new HashSet<CongViecNhom>();
            UngTuyens = new HashSet<UngTuyen>();
        }

        public int Id { get; set; }
        public int? Idnguoituyendung { get; set; }
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public int? Mintuoi { get; set; }
        public int? Maxtuoi { get; set; }
        public DateTime? Timework { get; set; }
        public DateTime? Finish { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public int? Salary { get; set; }
        public string? Note { get; set; }
        public int? State { get; set; }

        public virtual NguoiTuyenDung? IdnguoituyendungNavigation { get; set; }
        public virtual ICollection<CongViecNhom> CongViecNhoms { get; set; }
        public virtual ICollection<UngTuyen> UngTuyens { get; set; }
    }
}
