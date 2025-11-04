using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2_AP1_GregoryRodriguez.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTipoDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosDetalle_Pedidos_PedidoId",
                table: "PedidosDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidosDetalle",
                table: "PedidosDetalle");

            migrationBuilder.RenameTable(
                name: "PedidosDetalle",
                newName: "PedidoDetalles");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosDetalle_PedidoId",
                table: "PedidoDetalles",
                newName: "IX_PedidoDetalles_PedidoId");

            migrationBuilder.AlterColumn<double>(
                name: "Precio",
                table: "Componentes",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoDetalles",
                table: "PedidoDetalles",
                column: "PedidoDetalleId");

            migrationBuilder.UpdateData(
                table: "Componentes",
                keyColumn: "ComponenteId",
                keyValue: 1,
                column: "Precio",
                value: 1580.0);

            migrationBuilder.UpdateData(
                table: "Componentes",
                keyColumn: "ComponenteId",
                keyValue: 2,
                column: "Precio",
                value: 4200.0);

            migrationBuilder.UpdateData(
                table: "Componentes",
                keyColumn: "ComponenteId",
                keyValue: 3,
                column: "Precio",
                value: 10000.0);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoDetalles_Pedidos_PedidoId",
                table: "PedidoDetalles",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "PedidoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoDetalles_Pedidos_PedidoId",
                table: "PedidoDetalles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoDetalles",
                table: "PedidoDetalles");

            migrationBuilder.RenameTable(
                name: "PedidoDetalles",
                newName: "PedidosDetalle");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoDetalles_PedidoId",
                table: "PedidosDetalle",
                newName: "IX_PedidosDetalle_PedidoId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Componentes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidosDetalle",
                table: "PedidosDetalle",
                column: "PedidoDetalleId");

            migrationBuilder.UpdateData(
                table: "Componentes",
                keyColumn: "ComponenteId",
                keyValue: 1,
                column: "Precio",
                value: 1580m);

            migrationBuilder.UpdateData(
                table: "Componentes",
                keyColumn: "ComponenteId",
                keyValue: 2,
                column: "Precio",
                value: 4200m);

            migrationBuilder.UpdateData(
                table: "Componentes",
                keyColumn: "ComponenteId",
                keyValue: 3,
                column: "Precio",
                value: 10000m);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosDetalle_Pedidos_PedidoId",
                table: "PedidosDetalle",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "PedidoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
