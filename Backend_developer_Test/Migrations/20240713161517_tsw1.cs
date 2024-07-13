using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_developer_Test.Migrations
{
    /// <inheritdoc />
    public partial class tsw1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubDeprtments_Departments_SubDeptId",
                table: "SubDeprtments");

            migrationBuilder.DropIndex(
                name: "IX_SubDeprtments_SubDeptId",
                table: "SubDeprtments");

            migrationBuilder.RenameColumn(
                name: "SubDeptId",
                table: "SubDeprtments",
                newName: "SubDepartmentId");

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "SubDeprtments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "SubDeprtments");

            migrationBuilder.RenameColumn(
                name: "SubDepartmentId",
                table: "SubDeprtments",
                newName: "SubDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_SubDeprtments_SubDeptId",
                table: "SubDeprtments",
                column: "SubDeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubDeprtments_Departments_SubDeptId",
                table: "SubDeprtments",
                column: "SubDeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
