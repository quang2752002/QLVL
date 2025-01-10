namespace GUIs.Models.VIEW
{
    public class NguoiTuyenDungVIEW
    {
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
        public int? Congviechoanthanh { set; get; }   
        public int? Congviecchuahoanthanh {  get; set; }
        public int congviecdang { set; get; }
    }
}
