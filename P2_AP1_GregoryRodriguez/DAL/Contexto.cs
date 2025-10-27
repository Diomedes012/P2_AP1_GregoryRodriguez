using Microsoft.EntityFrameworkCore;
using P2_AP1_GregoryRodriguez.Models;
namespace P2_AP1_GregoryRodriguez.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {

    }

    public DbSet<Registro> Registro { get; set; }
}
