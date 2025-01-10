using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GUIs.Models.EF
{
    public partial class QLVLContext : DbContext
    {
        public QLVLContext()
        {
        }

        public QLVLContext(DbContextOptions<QLVLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CongViec> CongViecs { get; set; } = null!;
        public virtual DbSet<CongViecNhom> CongViecNhoms { get; set; } = null!;
        public virtual DbSet<DangKyNhomCongViec> DangKyNhomCongViecs { get; set; } = null!;
        public virtual DbSet<GopY> Gopies { get; set; } = null!;
        public virtual DbSet<NguoiLaoDong> NguoiLaoDongs { get; set; } = null!;
        public virtual DbSet<NguoiTuyenDung> NguoiTuyenDungs { get; set; } = null!;
        public virtual DbSet<NhomCongViec> NhomCongViecs { get; set; } = null!;
        public virtual DbSet<UngTuyen> UngTuyens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("qlvl");

            modelBuilder.Entity<CongViec>(entity =>
            {
                entity.ToTable("CONG_VIEC", "dbo");

                entity.HasIndex(e => e.Idnguoituyendung, "IX_CONG_VIEC_idnguoituyendung");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .HasColumnName("alias");

                entity.Property(e => e.Finish)
                    .HasColumnType("datetime")
                    .HasColumnName("finish");

                entity.Property(e => e.Idnguoituyendung).HasColumnName("idnguoituyendung");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Maxtuoi).HasColumnName("maxtuoi");

                entity.Property(e => e.Mintuoi).HasColumnName("mintuoi");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Timework)
                    .HasColumnType("datetime")
                    .HasColumnName("timework");

                entity.HasOne(d => d.IdnguoituyendungNavigation)
                    .WithMany(p => p.CongViecs)
                    .HasForeignKey(d => d.Idnguoituyendung)
                    .HasConstraintName("FK_CONG_VIEC_NGUOI_TUYEN_DUNG");
            });

            modelBuilder.Entity<CongViecNhom>(entity =>
            {
                entity.ToTable("CONG_VIEC_NHOM", "dbo");

                entity.HasIndex(e => e.Idcongviec, "IX_CONG_VIEC_NHOM_idcongviec");

                entity.HasIndex(e => e.Idnhomcongviec, "IX_CONG_VIEC_NHOM_idnhomcongviec");

                entity.Property(e => e.Idcongviec).HasColumnName("idcongviec");

                entity.Property(e => e.Idnhomcongviec).HasColumnName("idnhomcongviec");

                entity.HasOne(d => d.IdcongviecNavigation)
                    .WithMany(p => p.CongViecNhoms)
                    .HasForeignKey(d => d.Idcongviec)
                    .HasConstraintName("FK_CONG_VIEC_NHOM_CONG_VIEC");

                entity.HasOne(d => d.IdnhomcongviecNavigation)
                    .WithMany(p => p.CongViecNhoms)
                    .HasForeignKey(d => d.Idnhomcongviec)
                    .HasConstraintName("FK_CONG_VIEC_NHOM_NHOM_CONG_VIEC");
            });

            modelBuilder.Entity<DangKyNhomCongViec>(entity =>
            {
                entity.ToTable("DANG_KY_NHOM_CONG_VIEC", "dbo");

                entity.HasIndex(e => e.Idnguoilaodong, "IX_DANG_KY_NHOM_CONG_VIEC_idnguoilaodong");

                entity.HasIndex(e => e.Idnhomcongviec, "IX_DANG_KY_NHOM_CONG_VIEC_idnhomcongviec");

                entity.Property(e => e.Idnguoilaodong).HasColumnName("idnguoilaodong");

                entity.Property(e => e.Idnhomcongviec).HasColumnName("idnhomcongviec");

                entity.HasOne(d => d.IdnguoilaodongNavigation)
                    .WithMany(p => p.DangKyNhomCongViecs)
                    .HasForeignKey(d => d.Idnguoilaodong)
                    .HasConstraintName("FK_DANG_KY_NHOM_CONG_VIEC_NGUOI_LAO_DONG");

                entity.HasOne(d => d.IdnhomcongviecNavigation)
                    .WithMany(p => p.DangKyNhomCongViecs)
                    .HasForeignKey(d => d.Idnhomcongviec)
                    .HasConstraintName("FK_DANG_KY_NHOM_CONG_VIEC_NHOM_CONG_VIEC");
            });

            modelBuilder.Entity<GopY>(entity =>
            {
                entity.ToTable("GOP_Y", "dbo");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Noidung)
                    .HasMaxLength(50)
                    .HasColumnName("noidung");
            });

            modelBuilder.Entity<NguoiLaoDong>(entity =>
            {
                entity.ToTable("NGUOI_LAO_DONG", "dbo");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Fanpage)
                    .HasMaxLength(300)
                    .HasColumnName("fanpage");

                entity.Property(e => e.Guid).HasColumnName("guid");

                entity.Property(e => e.Heath).HasColumnName("heath");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Introduce).HasColumnName("introduce");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.Refcode).HasColumnName("refcode");

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<NguoiTuyenDung>(entity =>
            {
                entity.ToTable("NGUOI_TUYEN_DUNG", "dbo");

                entity.Property(e => e.Actived).HasColumnName("actived");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .HasColumnName("alias");

                entity.Property(e => e.Diachi).HasColumnName("diachi");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Fanpage).HasColumnName("fanpage");

                entity.Property(e => e.Guid).HasColumnName("guid");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Introduce).HasColumnName("introduce");

                entity.Property(e => e.Lastlogin)
                    .HasColumnType("datetime")
                    .HasColumnName("lastlogin");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Locked).HasColumnName("locked");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .HasColumnName("sdt");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<NhomCongViec>(entity =>
            {
                entity.ToTable("NHOM_CONG_VIEC", "dbo");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");
            });

            modelBuilder.Entity<UngTuyen>(entity =>
            {
                entity.ToTable("UNG_TUYEN", "dbo");

                entity.HasIndex(e => e.Idcongviec, "IX_UNG_TUYEN_idcongviec");

                entity.HasIndex(e => e.Idnguoilaodong, "IX_UNG_TUYEN_idnguoilaodong");

                entity.Property(e => e.Apply).HasColumnName("apply");

                entity.Property(e => e.Danhgiacongviec).HasColumnName("danhgiacongviec");

                entity.Property(e => e.Danhgialaodong).HasColumnName("danhgialaodong");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Idcongviec).HasColumnName("idcongviec");

                entity.Property(e => e.Idnguoilaodong).HasColumnName("idnguoilaodong");

                entity.Property(e => e.Nhanxetcongviec).HasColumnName("nhanxetcongviec");

                entity.Property(e => e.Nhanxetlaodong).HasColumnName("nhanxetlaodong");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.HasOne(d => d.IdcongviecNavigation)
                    .WithMany(p => p.UngTuyens)
                    .HasForeignKey(d => d.Idcongviec)
                    .HasConstraintName("FK_UNG_TUYEN_CONG_VIEC");

                entity.HasOne(d => d.IdnguoilaodongNavigation)
                    .WithMany(p => p.UngTuyens)
                    .HasForeignKey(d => d.Idnguoilaodong)
                    .HasConstraintName("FK_UNG_TUYEN_NGUOI_LAO_DONG");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER", "dbo");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Guid).HasColumnName("guid");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(300)
                    .HasColumnName("password");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .HasColumnName("state");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
