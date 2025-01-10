using System;
using System.Collections.Generic;

namespace GUIs.Models.EF
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? State { get; set; }
        public string? Guid { get; set; }
    }
}
