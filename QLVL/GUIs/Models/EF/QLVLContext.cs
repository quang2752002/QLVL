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
        public virtual DbSet<DanhGium> DanhGia { get; set; } = null!;
        public virtual DbSet<NguoiLaoDong> NguoiLaoDongs { get; set; } = null!;
        public virtual DbSet<NguoiTuyenDung> NguoiTuyenDungs { get; set; } = null!;
        public virtual DbSet<UngTuyen> UngTuyens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-CPTPR8L;Initial Catalog=QLVL;Persist Security Info=True;User ID=sa;Password=1; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CongViec>(entity =>
            {
                entity.ToTable("CONG_VIEC");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.Idtuyendung).HasColumnName("idtuyendung");

                entity.Property(e => e.Idungtuyen).HasColumnName("idungtuyen");

                entity.Property(e => e.Luongbatdau).HasColumnName("luongbatdau");

                entity.Property(e => e.Luongketthuc).HasColumnName("luongketthuc");

                entity.Property(e => e.Mota).HasColumnName("mota");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Tencongviec)
                    .HasMaxLength(50)
                    .HasColumnName("tencongviec");

                entity.Property(e => e.Thoigianbatdau)
                    .HasColumnType("datetime")
                    .HasColumnName("thoigianbatdau");

                entity.Property(e => e.Thoigianketthuc)
                    .HasColumnType("datetime")
                    .HasColumnName("thoigianketthuc");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.Property(e => e.Tuyenbatdau)
                    .HasColumnType("datetime")
                    .HasColumnName("tuyenbatdau");

                entity.Property(e => e.Tuyenketthuc)
                    .HasColumnType("datetime")
                    .HasColumnName("tuyenketthuc");

                entity.HasOne(d => d.IdtuyendungNavigation)
                    .WithMany(p => p.CongViecs)
                    .HasForeignKey(d => d.Idtuyendung)
                    .HasConstraintName("FK_CONG_VIEC_NGUOI_TUYEN_DUNG");

                entity.HasOne(d => d.IdungtuyenNavigation)
                    .WithMany(p => p.CongViecs)
                    .HasForeignKey(d => d.Idungtuyen)
                    .HasConstraintName("FK_CONG_VIEC_UNG_TUYEN");
            });

            modelBuilder.Entity<DanhGium>(entity =>
            {
                entity.ToTable("DANH_GIA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Danhgia).HasColumnName("danhgia");

                entity.Property(e => e.Idcongviec).HasColumnName("idcongviec");

                entity.Property(e => e.Idlaodong).HasColumnName("idlaodong");

                entity.Property(e => e.Idtuyendung).HasColumnName("idtuyendung");

                entity.Property(e => e.Sao).HasColumnName("sao");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.IdcongviecNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.Idcongviec)
                    .HasConstraintName("FK_DANH_GIA_CONG_VIEC");

                entity.HasOne(d => d.IdlaodongNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.Idlaodong)
                    .HasConstraintName("FK_DANH_GIA_NGUOI_LAO_DONG");

                entity.HasOne(d => d.IdtuyendungNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.Idtuyendung)
                    .HasConstraintName("FK_DANH_GIA_NGUOI_TUYEN_DUNG");
            });

            modelBuilder.Entity<NguoiLaoDong>(entity =>
            {
                entity.ToTable("NGUOI_LAO_DONG");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(50)
                    .HasColumnName("cccd");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(50)
                    .HasColumnName("hoten");

                entity.Property(e => e.Img).HasColumnName("img");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .HasColumnName("sdt");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<NguoiTuyenDung>(entity =>
            {
                entity.ToTable("NGUOI_TUYEN_DUNG");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(50)
                    .HasColumnName("cccd");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(50)
                    .HasColumnName("hoten");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .HasColumnName("sdt");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.Property(e => e.Tuoi).HasColumnName("tuoi");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UngTuyen>(entity =>
            {
                entity.ToTable("UNG_TUYEN");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idcongviec).HasColumnName("idcongviec");

                entity.Property(e => e.Idlaodong).HasColumnName("idlaodong");

                entity.Property(e => e.Mota).HasColumnName("mota");

                entity.Property(e => e.Tienluong).HasColumnName("tienluong");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.IdlaodongNavigation)
                    .WithMany(p => p.UngTuyens)
                    .HasForeignKey(d => d.Idlaodong)
                    .HasConstraintName("FK_UNG_TUYEN_NGUOI_LAO_DONG");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
