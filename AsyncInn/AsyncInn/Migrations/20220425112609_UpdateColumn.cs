using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncInn.Migrations
{
    public partial class UpdateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomAmenities",
                table: "RoomAmenities");

            migrationBuilder.DropIndex(
                name: "IX_RoomAmenities_AmenityId",
                table: "RoomAmenities");

            migrationBuilder.DropColumn(
                name: "AmenetiesID",
                table: "RoomAmenities");

            migrationBuilder.RenameColumn(
                name: "AmenityId",
                table: "RoomAmenities",
                newName: "AmenityID");

            migrationBuilder.AlterColumn<int>(
                name: "AmenityID",
                table: "RoomAmenities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomAmenities",
                table: "RoomAmenities",
                columns: new[] { "AmenityID", "RoomID" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityID",
                table: "RoomAmenities",
                column: "AmenityID",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityID",
                table: "RoomAmenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomAmenities",
                table: "RoomAmenities");

            migrationBuilder.RenameColumn(
                name: "AmenityID",
                table: "RoomAmenities",
                newName: "AmenityId");

            migrationBuilder.AlterColumn<int>(
                name: "AmenityId",
                table: "RoomAmenities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AmenetiesID",
                table: "RoomAmenities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomAmenities",
                table: "RoomAmenities",
                columns: new[] { "AmenetiesID", "RoomID" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
