using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerMgt.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Customers",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customers",
                newName: "Gender");
        }
    }
}
