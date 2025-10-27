using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using P2_AP1_GregoryRodriguez.DAL;
using P2_AP1_GregoryRodriguez.Models;

namespace P2_AP1_GregoryRodriguez.Services;

public class RegistroService(IDbContextFactory<Contexto> factory)
{
    public async Task<List<Registro>> Listar(Expression<Func<Registro, bool>> criterio)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Registro
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
