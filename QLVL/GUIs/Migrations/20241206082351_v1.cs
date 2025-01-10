using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GUIs.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GOP_Y",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    noidung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GOP_Y", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NGUOI_LAO_DONG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    sex = table.Column<int>(type: "int", nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    heath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fanpage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    introduce = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGUOI_LAO_DONG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NGUOI_TUYEN_DUNG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fanpage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sdt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    actived = table.Column<int>(type: "int", nullable: true),
                    locked = table.Column<int>(type: "int", nullable: true),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastlogin = table.Column<DateTime>(type: "datetime", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    introduce = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGUOI_TUYEN_DUNG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NHOM_CONG_VIEC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHOM_CONG_VIEC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    state = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CONG_VIEC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idnguoituyendung = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    mintuoi = table.Column<int>(type: "int", nullable: true),
                    maxtuoi = table.Column<int>(type: "int", nullable: true),
                    timework = table.Column<DateTime>(type: "datetime", nullable: true),
                    finish = table.Column<DateTime>(type: "datetime", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    salary = table.Column<int>(type: "int", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONG_VIEC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CONG_VIEC_NGUOI_TUYEN_DUNG",
                        column: x => x.idnguoituyendung,
                        principalTable: "NGUOI_TUYEN_DUNG",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DANG_KY_NHOM_CONG_VIEC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idnguoilaodong = table.Column<int>(type: "int", nullable: true),
                    idnhomcongviec = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DANG_KY_NHOM_CONG_VIEC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DANG_KY_NHOM_CONG_VIEC_NGUOI_LAO_DONG",
                        column: x => x.idnguoilaodong,
                        principalTable: "NGUOI_LAO_DONG",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DANG_KY_NHOM_CONG_VIEC_NHOM_CONG_VIEC",
                        column: x => x.idnhomcongviec,
                        principalTable: "NHOM_CONG_VIEC",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CONG_VIEC_NHOM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idcongviec = table.Column<int>(type: "int", nullable: true),
                    idnhomcongviec = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONG_VIEC_NHOM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CONG_VIEC_NHOM_CONG_VIEC",
                        column: x => x.idcongviec,
                        principalTable: "CONG_VIEC",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CONG_VIEC_NHOM_NHOM_CONG_VIEC",
                        column: x => x.idnhomcongviec,
                        principalTable: "NHOM_CONG_VIEC",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UNG_TUYEN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idcongviec = table.Column<int>(type: "int", nullable: true),
                    idnguoilaodong = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    salary = table.Column<int>(type: "int", nullable: true),
                    apply = table.Column<int>(type: "int", nullable: true),
                    danhgialaodong = table.Column<int>(type: "int", nullable: true),
                    nhanxetlaodong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    danhgiacongviec = table.Column<int>(type: "int", nullable: true),
                    nhanxetcongviec = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNG_TUYEN", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UNG_TUYEN_CONG_VIEC",
                        column: x => x.idcongviec,
                        principalTable: "CONG_VIEC",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UNG_TUYEN_NGUOI_LAO_DONG",
                        column: x => x.idnguoilaodong,
                        principalTable: "NGUOI_LAO_DONG",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONG_VIEC_idnguoituyendung",
                table: "CONG_VIEC",
                column: "idnguoituyendung");

            migrationBuilder.CreateIndex(
                name: "IX_CONG_VIEC_NHOM_idcongviec",
                table: "CONG_VIEC_NHOM",
                column: "idcongviec");

            migrationBuilder.CreateIndex(
                name: "IX_CONG_VIEC_NHOM_idnhomcongviec",
                table: "CONG_VIEC_NHOM",
                column: "idnhomcongviec");

            migrationBuilder.CreateIndex(
                name: "IX_DANG_KY_NHOM_CONG_VIEC_idnguoilaodong",
                table: "DANG_KY_NHOM_CONG_VIEC",
                column: "idnguoilaodong");

            migrationBuilder.CreateIndex(
                name: "IX_DANG_KY_NHOM_CONG_VIEC_idnhomcongviec",
                table: "DANG_KY_NHOM_CONG_VIEC",
                column: "idnhomcongviec");

            migrationBuilder.CreateIndex(
                name: "IX_UNG_TUYEN_idcongviec",
                table: "UNG_TUYEN",
                column: "idcongviec");

            migrationBuilder.CreateIndex(
                name: "IX_UNG_TUYEN_idnguoilaodong",
                table: "UNG_TUYEN",
                column: "idnguoilaodong");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONG_VIEC_NHOM");

            migrationBuilder.DropTable(
                name: "DANG_KY_NHOM_CONG_VIEC");

            migrationBuilder.DropTable(
                name: "GOP_Y");

            migrationBuilder.DropTable(
                name: "UNG_TUYEN");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "NHOM_CONG_VIEC");

            migrationBuilder.DropTable(
                name: "CONG_VIEC");

            migrationBuilder.DropTable(
                name: "NGUOI_LAO_DONG");

            migrationBuilder.DropTable(
                name: "NGUOI_TUYEN_DUNG");
        }
    }
}
