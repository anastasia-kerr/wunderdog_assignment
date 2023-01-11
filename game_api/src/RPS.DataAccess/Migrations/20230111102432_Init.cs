using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPS.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("01f789d9-7196-4b16-8917-812a4381d307"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("37972735-9a22-488c-bb58-5845df781a72"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("3fffbd87-5b14-41f6-ab71-40dc00d849ef"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("53ba288f-4077-4523-839f-9ead21f90b23"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("5bcfd02b-0dc3-4e1b-bff2-d7f732516201"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("81347c4a-d2cb-42c4-a0c4-f7b3092f17e5"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("9450694d-763a-440f-8944-a5d63104daa8"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("998e5174-cf34-4807-bae0-f9284db1f593"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("b87e7283-8d0a-4688-a121-b117cd5d0dfa"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("bb0ccb69-5fb8-4d4f-8313-a999d47bc41b"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("c3760a9f-b274-4619-9866-33e1383a31d6"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("c4047d73-0af1-4e53-9836-807844a405f1"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("d0718a1a-05e8-48d8-a8a3-e950eb7a3efa"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("d73d2013-8164-4ff6-ae08-e5e19ca4cfe8"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("fae4f9b6-f5cf-4ab9-a8d5-9308628ae1b5"));

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Tasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStopped",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Importance", "IsOff", "LastStopped", "Title" },
                values: new object[,]
                {
                    { new Guid("145a0447-1281-441c-bdfb-09b636d129d7"), 1, false, null, "E2" },
                    { new Guid("23763ad7-17f6-4c11-9665-cc47408ea9ee"), 1, false, null, "E5" },
                    { new Guid("366687e6-85cf-4e96-9865-18ae0b504240"), 2, false, null, "I3" },
                    { new Guid("36aeb599-113f-4448-9321-6371284b9c96"), 2, false, null, "I2" },
                    { new Guid("50a01356-5e97-4224-a246-3d2d7633dd9f"), 2, false, null, "I5" },
                    { new Guid("5c3bdd84-8f8f-4780-8286-33ccaee032e3"), 1, false, null, "E4" },
                    { new Guid("72c92151-d493-4974-9e84-c00c2096c0f1"), 0, false, null, "C1" },
                    { new Guid("856bd3e9-d0cf-476e-b365-31c366d22bad"), 2, false, null, "I1" },
                    { new Guid("9d329a76-68d3-4fc9-9071-5071e648c14b"), 2, false, null, "I4" },
                    { new Guid("ade07bc4-2630-4ac8-b275-f5fec9db91de"), 1, false, null, "E7" },
                    { new Guid("c6c18398-0424-4604-a41c-6ae293097eb0"), 1, false, null, "E6" },
                    { new Guid("d01f5e00-eab7-4f77-a27b-2f351a714379"), 0, false, null, "C3" },
                    { new Guid("d395ea08-d11e-4245-be48-4446e40f5ae2"), 1, false, null, "E1" },
                    { new Guid("d7104107-ca65-4064-92c4-bc0e0f664efd"), 0, false, null, "C2" },
                    { new Guid("fd3ca987-fc42-459e-a2b7-98015c243e20"), 1, false, null, "E3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("145a0447-1281-441c-bdfb-09b636d129d7"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("23763ad7-17f6-4c11-9665-cc47408ea9ee"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("366687e6-85cf-4e96-9865-18ae0b504240"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("36aeb599-113f-4448-9321-6371284b9c96"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("50a01356-5e97-4224-a246-3d2d7633dd9f"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("5c3bdd84-8f8f-4780-8286-33ccaee032e3"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("72c92151-d493-4974-9e84-c00c2096c0f1"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("856bd3e9-d0cf-476e-b365-31c366d22bad"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("9d329a76-68d3-4fc9-9071-5071e648c14b"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("ade07bc4-2630-4ac8-b275-f5fec9db91de"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("c6c18398-0424-4604-a41c-6ae293097eb0"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("d01f5e00-eab7-4f77-a27b-2f351a714379"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("d395ea08-d11e-4245-be48-4446e40f5ae2"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("d7104107-ca65-4064-92c4-bc0e0f664efd"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("fd3ca987-fc42-459e-a2b7-98015c243e20"));

            migrationBuilder.DropColumn(
                name: "LastStopped",
                table: "Tasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Importance", "IsOff", "LastUpdated", "Title" },
                values: new object[,]
                {
                    { new Guid("01f789d9-7196-4b16-8917-812a4381d307"), 0, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C2" },
                    { new Guid("37972735-9a22-488c-bb58-5845df781a72"), 2, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "I2" },
                    { new Guid("3fffbd87-5b14-41f6-ab71-40dc00d849ef"), 2, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "I3" },
                    { new Guid("53ba288f-4077-4523-839f-9ead21f90b23"), 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E5" },
                    { new Guid("5bcfd02b-0dc3-4e1b-bff2-d7f732516201"), 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E6" },
                    { new Guid("81347c4a-d2cb-42c4-a0c4-f7b3092f17e5"), 2, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "I5" },
                    { new Guid("9450694d-763a-440f-8944-a5d63104daa8"), 0, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C3" },
                    { new Guid("998e5174-cf34-4807-bae0-f9284db1f593"), 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E4" },
                    { new Guid("b87e7283-8d0a-4688-a121-b117cd5d0dfa"), 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E2" },
                    { new Guid("bb0ccb69-5fb8-4d4f-8313-a999d47bc41b"), 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E7" },
                    { new Guid("c3760a9f-b274-4619-9866-33e1383a31d6"), 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E1" },
                    { new Guid("c4047d73-0af1-4e53-9836-807844a405f1"), 2, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "I1" },
                    { new Guid("d0718a1a-05e8-48d8-a8a3-e950eb7a3efa"), 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E3" },
                    { new Guid("d73d2013-8164-4ff6-ae08-e5e19ca4cfe8"), 2, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "I4" },
                    { new Guid("fae4f9b6-f5cf-4ab9-a8d5-9308628ae1b5"), 0, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C1" }
                });
        }
    }
}
