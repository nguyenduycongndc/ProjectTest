using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebReport.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SUBJECT",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    subject_type = table.Column<short>(type: "smallint", nullable: false),
                    create_time = table.Column<int>(type: "int", nullable: true),
                    update_time = table.Column<long>(type: "bigint", nullable: true),
                    email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_reseted = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    real_name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pinyin = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<short>(type: "smallint", nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    department = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    department_pinyin = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mobile_os = table.Column<int>(type: "int", nullable: true),
                    birthday = table.Column<DateTime>(type: "date", nullable: true),
                    entry_date = table.Column<DateTime>(type: "date", nullable: true),
                    job_number = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remark = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    purpose = table.Column<int>(type: "int", nullable: true),
                    interviewee = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    interviewee_pinyin = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    come_from = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    inviter_id = table.Column<int>(type: "int", nullable: true),
                    visited = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    visit_notify = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    start_time = table.Column<int>(type: "int", nullable: true),
                    end_time = table.Column<int>(type: "int", nullable: true),
                    extra_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wg_number = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_use = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    people_type = table.Column<int>(type: "int", nullable: true),
                    credential_type = table.Column<int>(type: "int", nullable: true),
                    credential_no = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nation = table.Column<int>(type: "int", nullable: true),
                    origin = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    domicile_province_code = table.Column<long>(type: "bigint", nullable: true),
                    domicile_city_code = table.Column<long>(type: "bigint", nullable: true),
                    domicile_district_code = table.Column<long>(type: "bigint", nullable: true),
                    domicile_street_code = table.Column<long>(type: "bigint", nullable: true),
                    domicile_address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    residence_province_code = table.Column<long>(type: "bigint", nullable: true),
                    residence_city_code = table.Column<long>(type: "bigint", nullable: true),
                    residence_district_code = table.Column<long>(type: "bigint", nullable: true),
                    residence_street_code = table.Column<long>(type: "bigint", nullable: true),
                    residence_address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    education_code = table.Column<int>(type: "int", nullable: true),
                    marital_status_code = table.Column<int>(type: "int", nullable: true),
                    nationality_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    source = table.Column<int>(type: "int", nullable: true),
                    village = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    building = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    house = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    house_rel_code = table.Column<int>(type: "int", nullable: true),
                    entrance_people_type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUBJECT", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ATTENDANCE",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    subject_id = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateTime>(type: "date", nullable: true),
                    earliest_record = table.Column<int>(type: "int", nullable: true),
                    latest_record = table.Column<int>(type: "int", nullable: true),
                    clock_in = table.Column<int>(type: "int", nullable: true),
                    clock_out = table.Column<int>(type: "int", nullable: true),
                    worktime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTENDANCE", x => x.id);
                    table.ForeignKey(
                        name: "FK_ATTENDANCE_SUBJECT_subject_id",
                        column: x => x.subject_id,
                        principalTable: "SUBJECT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EVENT",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    subject_id = table.Column<int>(type: "int", nullable: true),
                    screen_id = table.Column<int>(type: "int", nullable: true),
                    photo = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    age = table.Column<double>(type: "double", nullable: true),
                    gender = table.Column<double>(type: "double", nullable: true),
                    group = table.Column<int>(type: "int", nullable: true),
                    short_group = table.Column<int>(type: "int", nullable: true),
                    quality = table.Column<double>(type: "double", nullable: true),
                    confidence = table.Column<double>(type: "double", nullable: true),
                    event_type = table.Column<int>(type: "int", nullable: false),
                    subject_type = table.Column<short>(type: "smallint", nullable: true),
                    real_name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pinyin = table.Column<string>(type: "varchar(612)", maxLength: 612, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subject_photo = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    timestamp = table.Column<int>(type: "int", nullable: true),
                    fmp = table.Column<double>(type: "double", nullable: true),
                    fmp_error = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    camera_position = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uuid = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pass_type = table.Column<int>(type: "int", nullable: true),
                    verification_mode = table.Column<int>(type: "int", nullable: true),
                    is_entrance = table.Column<short>(type: "smallint", nullable: true),
                    temperature = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    temperature_type = table.Column<int>(type: "int", nullable: true),
                    mask_type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENT", x => x.id);
                    table.ForeignKey(
                        name: "FK_EVENT_SUBJECT_subject_id",
                        column: x => x.subject_id,
                        principalTable: "SUBJECT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDANCE_subject_id",
                table: "ATTENDANCE",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_EVENT_subject_id",
                table: "EVENT",
                column: "subject_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATTENDANCE");

            migrationBuilder.DropTable(
                name: "EVENT");

            migrationBuilder.DropTable(
                name: "SUBJECT");
        }
    }
}
