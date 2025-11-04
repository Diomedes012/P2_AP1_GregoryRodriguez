using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_AP1_GregoryRodriguez.Models;

public class Pedidos
{
    [Key]
    public int PedidoId { get; set; } 

    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage="El nombre del cliente es requerido")]
    public string NombreCliente { get; set; }

    [Range(0, double.MaxValue, ErrorMessage ="El precio no puede ser inferior a 0")]
    public double Precio { get; set; }

    [ForeignKey("PedidoId")]
    public virtual ICollection<PedidosDetalle> Detalles { get; set; } = new List<PedidosDetalle>();
}
