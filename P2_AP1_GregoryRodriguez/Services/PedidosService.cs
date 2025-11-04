using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using P2_AP1_GregoryRodriguez.DAL;
using P2_AP1_GregoryRodriguez.Models;

namespace P2_AP1_GregoryRodriguez.Services;

public class PedidosService(IDbContextFactory<Contexto> factory)
{
    private enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }

    public async Task<bool> Guardar(Pedidos Pedido)
    {
        await using var contexto = await factory.CreateDbContextAsync();

         await using var transaccion = await contexto.Database.BeginTransactionAsync();

        try
        {
            var (guardado, mensajerror) = Pedido.PedidoId == 0
                ? await Insertar(Pedido, contexto)
                : await Modificar(Pedido, contexto);

            if(!guardado)
            {
                await transaccion.RollbackAsync();
                return false;
            }

            transaccion.CommitAsync();
            return true;
        }
        catch(Exception)
        {
            await transaccion.RollbackAsync();
            return false;
        }
    }

    private async Task<(bool, string)> Insertar(Pedidos Pedido, Contexto contexto)
    {
        var (afectado, error) = await AfectarExistencia(Pedido.Detalles.ToArray(), TipoOperacion.Resta, contexto);

        if(!afectado)
        {
            return (false, error);
        }

        contexto.Pedidos.Add(Pedido);

        var guardados = await contexto.SaveChangesAsync() > 0;
        return (guardados, string.Empty);
    }

    private async Task<(bool, string)> Modificar(Pedidos Pedido, Contexto contexto)
    {
        var detallesAntiguos = await contexto.PedidoDetalles
            .AsNoTracking()
            .Where(d => d.PedidoId == Pedido.PedidoId)
            .ToArrayAsync();

        await AfectarExistencia(detallesAntiguos, TipoOperacion.Suma, contexto);

        await contexto.PedidoDetalles
            .Where(d => d.PedidoId == Pedido.PedidoId)
            .ExecuteDeleteAsync();

        var (afectado, error) = await AfectarExistencia(Pedido.Detalles.ToArray(), TipoOperacion.Resta, contexto);

        if(!afectado)
        {
            return (false, error);
        }

        contexto.Update(Pedido);

        foreach (var detalle in Pedido.Detalles)
        {
            detalle.PedidoId = Pedido.PedidoId;

            detalle.PedidoDetalleId = 0;
        }

        var guardados = await contexto.SaveChangesAsync() > 0;

        return (guardados, string.Empty);
    }

    private async Task<(bool, string)> AfectarExistencia(PedidosDetalle[] detalles, TipoOperacion operacion, Contexto contexto)
    {
        foreach(var detalle in detalles)
        {
            var componente = await contexto.Componentes.FindAsync(detalle.ComponenteId);
            if (componente == null)
            {
                return (false, $"ComponenteId {detalle.ComponenteId} no existe");
            }

            if (operacion == TipoOperacion.Suma)
            {
                componente.Existencia += detalle.Cantidad;
            }
            else
            {
                if(componente.Existencia < detalle.Cantidad)
                {
                    return (false, $"No hay existencua para {componente.Descripcion}");
                }
                componente.Existencia -= detalle.Cantidad;
            }
        }
        return (true, string.Empty);
    }

    public async Task<bool> Eliminar(int pedidoId)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        await using var transaccion = await contexto.Database.BeginTransactionAsync();

        try
        {
            var Pedido = await contexto.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);

            if(Pedido == null)
            {
                return false;
            }

            await AfectarExistencia(Pedido.Detalles.ToArray(), TipoOperacion.Suma, contexto);

            contexto.Pedidos.Remove(Pedido);

            var guardado = await contexto.SaveChangesAsync() > 0;
            await transaccion.CommitAsync();

            return guardado;
        }
        catch(Exception)
        {
            await transaccion.RollbackAsync();
            return false;
        }
    }

    public async Task<Pedidos?> Buscar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Pedidos
            .Include(p => p.Detalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PedidoId == id);
    }

    public async Task<List<Pedidos>> Listar(Expression<Func<Pedidos, bool>> criterio)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Pedidos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
