using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoragewithComputerParts.Data.Migrations
{
    /// <inheritdoc />
    public partial class PoprawkiBazaDanych : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Contractor_ContractorId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Protocol_ProtocolId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryProducts_Delivery_DeliveryId",
                table: "DeliveryProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryProducts_Product_ProductId",
                table: "DeliveryProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Release_Contractor_ContractorId",
                table: "Release");

            migrationBuilder.DropForeignKey(
                name: "FK_Release_Protocol_ProtocolId",
                table: "Release");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseProducts_Product_ProductId",
                table: "ReleaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseProducts_Release_ReleaseId",
                table: "ReleaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Product_ProductId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_ProductId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Release",
                table: "Release");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Protocol",
                table: "Protocol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contractor",
                table: "Contractor");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameTable(
                name: "Release",
                newName: "Releases");

            migrationBuilder.RenameTable(
                name: "Protocol",
                newName: "Protocols");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Delivery",
                newName: "Deliveries");

            migrationBuilder.RenameTable(
                name: "Contractor",
                newName: "Contractors");

            migrationBuilder.RenameIndex(
                name: "IX_Release_ProtocolId",
                table: "Releases",
                newName: "IX_Releases_ProtocolId");

            migrationBuilder.RenameIndex(
                name: "IX_Release_ContractorId",
                table: "Releases",
                newName: "IX_Releases_ContractorId");

            migrationBuilder.RenameIndex(
                name: "IX_Delivery_ProtocolId",
                table: "Deliveries",
                newName: "IX_Deliveries_ProtocolId");

            migrationBuilder.RenameIndex(
                name: "IX_Delivery_ContractorId",
                table: "Deliveries",
                newName: "IX_Deliveries_ContractorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Releases",
                table: "Releases",
                column: "ReleaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Protocols",
                table: "Protocols",
                column: "ProtocolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries",
                column: "DeliveryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Contractors_ContractorId",
                table: "Deliveries",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Protocols_ProtocolId",
                table: "Deliveries",
                column: "ProtocolId",
                principalTable: "Protocols",
                principalColumn: "ProtocolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryProducts_Deliveries_DeliveryId",
                table: "DeliveryProducts",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "DeliveryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryProducts_Products_ProductId",
                table: "DeliveryProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseProducts_Products_ProductId",
                table: "ReleaseProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseProducts_Releases_ReleaseId",
                table: "ReleaseProducts",
                column: "ReleaseId",
                principalTable: "Releases",
                principalColumn: "ReleaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Releases_Contractors_ContractorId",
                table: "Releases",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Releases_Protocols_ProtocolId",
                table: "Releases",
                column: "ProtocolId",
                principalTable: "Protocols",
                principalColumn: "ProtocolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Products_ProductId",
                table: "Stocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Contractors_ContractorId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Protocols_ProtocolId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryProducts_Deliveries_DeliveryId",
                table: "DeliveryProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryProducts_Products_ProductId",
                table: "DeliveryProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseProducts_Products_ProductId",
                table: "ReleaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseProducts_Releases_ReleaseId",
                table: "ReleaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Releases_Contractors_ContractorId",
                table: "Releases");

            migrationBuilder.DropForeignKey(
                name: "FK_Releases_Protocols_ProtocolId",
                table: "Releases");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Products_ProductId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Releases",
                table: "Releases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Protocols",
                table: "Protocols");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "Releases",
                newName: "Release");

            migrationBuilder.RenameTable(
                name: "Protocols",
                newName: "Protocol");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Deliveries",
                newName: "Delivery");

            migrationBuilder.RenameTable(
                name: "Contractors",
                newName: "Contractor");

            migrationBuilder.RenameIndex(
                name: "IX_Releases_ProtocolId",
                table: "Release",
                newName: "IX_Release_ProtocolId");

            migrationBuilder.RenameIndex(
                name: "IX_Releases_ContractorId",
                table: "Release",
                newName: "IX_Release_ContractorId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_ProtocolId",
                table: "Delivery",
                newName: "IX_Delivery_ProtocolId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_ContractorId",
                table: "Delivery",
                newName: "IX_Delivery_ContractorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                columns: new[] { "ProductId", "Quantity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Release",
                table: "Release",
                column: "ReleaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Protocol",
                table: "Protocol",
                column: "ProtocolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery",
                column: "DeliveryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contractor",
                table: "Contractor",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductId",
                table: "Stock",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Contractor_ContractorId",
                table: "Delivery",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Protocol_ProtocolId",
                table: "Delivery",
                column: "ProtocolId",
                principalTable: "Protocol",
                principalColumn: "ProtocolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryProducts_Delivery_DeliveryId",
                table: "DeliveryProducts",
                column: "DeliveryId",
                principalTable: "Delivery",
                principalColumn: "DeliveryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryProducts_Product_ProductId",
                table: "DeliveryProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Release_Contractor_ContractorId",
                table: "Release",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Release_Protocol_ProtocolId",
                table: "Release",
                column: "ProtocolId",
                principalTable: "Protocol",
                principalColumn: "ProtocolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseProducts_Product_ProductId",
                table: "ReleaseProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseProducts_Release_ReleaseId",
                table: "ReleaseProducts",
                column: "ReleaseId",
                principalTable: "Release",
                principalColumn: "ReleaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Product_ProductId",
                table: "Stock",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
