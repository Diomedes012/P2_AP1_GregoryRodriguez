using System.ComponentModel.DataAnnotations;

namespace P2_AP1_GregoryRodriguez.Models;

public class PedidosDetalle
{
    [Key]
    public int PedidoDetalleId { get; set; }

    public int PedidoId { get; set; }

    public int ComponenteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage ="La cantidad no puede ser menor a 1")]
    public int Cantidad { get; set; }

    [Range(1, double.MaxValue, ErrorMessage ="El precio no puede ser menor a 1")]
    public double Precio { get; set; }  

}
