using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using P2_AP1_GregoryRodriguez.DAL;
using P2_AP1_GregoryRodriguez.Models;
using System.Linq.Expressions;

namespace P2_AP1_GregoryRodriguez.Services;

public class ComponenteService(IDbContextFactory<Contexto> factory)
{
    public async Task<Componente?> Buscar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Componentes
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ComponenteId == id);
    }

    public async Task<List<Componente>> Listar(Expression<Func<Componente, bool>> criterio)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Componentes
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
