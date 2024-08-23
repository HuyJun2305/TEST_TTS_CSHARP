using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true),
                    created_date = table.Column<long>(type: "bigint", nullable: true),
                    last_modified_date = table.Column<long>(type: "bigint", nullable: true),
                    code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "facility",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true),
                    created_date = table.Column<long>(type: "bigint", nullable: true),
                    last_modified_date = table.Column<long>(type: "bigint", nullable: true),
                    code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facility", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "major",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true),
                    created_date = table.Column<long>(type: "bigint", nullable: true),
                    last_modified_date = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_major", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true),
                    created_date = table.Column<long>(type: "bigint", nullable: true),
                    last_modified_date = table.Column<long>(type: "bigint", nullable: true),
                    account_fe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    account_fpt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    staff_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "department_facility",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true),
                    created_date = table.Column<long>(type: "bigint", nullable: true),
                    last_modified_date = table.Column<long>(type: "bigint", nullable: true),
                    id_department = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_facility = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_staff = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department_facility", x => x.id);
                    table.ForeignKey(
                        name: "FK__departmen__id_de__3D5E1FD2",
                        column: x => x.id_department,
                        principalTable: "department",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__departmen__id_fa__3E52440B",
                        column: x => x.id_facility,
                        principalTable: "facility",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__departmen__id_st__3F466844",
                        column: x => x.id_staff,
                        principalTable: "Staff",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "major_facility",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true),
                    created_date = table.Column<long>(type: "bigint", nullable: true),
                    last_modified_date = table.Column<long>(type: "bigint", nullable: true),
                    id_department_facility = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_major = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_major_facility", x => x.id);
                    table.ForeignKey(
                        name: "FK__major_fac__id_de__440B1D61",
                        column: x => x.id_department_facility,
                        principalTable: "department_facility",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__major_fac__id_ma__44FF419A",
                        column: x => x.id_major,
                        principalTable: "major",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "staff_major_facility",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true),
                    created_date = table.Column<long>(type: "bigint", nullable: true),
                    last_modified_date = table.Column<long>(type: "bigint", nullable: true),
                    id_major_facility = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_staff = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff_major_facility", x => x.id);
                    table.ForeignKey(
                        name: "FK__staff_maj__id_ma__47DBAE45",
                        column: x => x.id_major_facility,
                        principalTable: "major_facility",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__staff_maj__id_st__48CFD27E",
                        column: x => x.id_staff,
                        principalTable: "Staff",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_department_facility_id_department",
                table: "department_facility",
                column: "id_department");

            migrationBuilder.CreateIndex(
                name: "IX_department_facility_id_facility",
                table: "department_facility",
                column: "id_facility");

            migrationBuilder.CreateIndex(
                name: "IX_department_facility_id_staff",
                table: "department_facility",
                column: "id_staff");

            migrationBuilder.CreateIndex(
                name: "IX_major_facility_id_department_facility",
                table: "major_facility",
                column: "id_department_facility");

            migrationBuilder.CreateIndex(
                name: "IX_major_facility_id_major",
                table: "major_facility",
                column: "id_major");

            migrationBuilder.CreateIndex(
                name: "IX_staff_major_facility_id_major_facility",
                table: "staff_major_facility",
                column: "id_major_facility");

            migrationBuilder.CreateIndex(
                name: "IX_staff_major_facility_id_staff",
                table: "staff_major_facility",
                column: "id_staff");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "staff_major_facility");

            migrationBuilder.DropTable(
                name: "major_facility");

            migrationBuilder.DropTable(
                name: "department_facility");

            migrationBuilder.DropTable(
                name: "major");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "facility");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
