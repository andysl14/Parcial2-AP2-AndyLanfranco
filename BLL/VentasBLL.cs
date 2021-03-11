using Microsoft.EntityFrameworkCore;
using Parcial2_AP2_AndyLanfranco.DAL;
using Parcial2_AP2_AndyLanfranco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Parcial2_AP2_AndyLanfranco.BLL
{
    public class VentasBLL
    {
        private Contexto Contexto { get; set; }

        public VentasBLL(Contexto contexto)
        {
            this.Contexto = contexto;
        }

        public async Task<Ventas> Buscar(int id)
        {
            Ventas venta;
            
            try
            {
                venta = await Contexto.Ventas
                    .Where(v => v.VentaId == id)
                    .Include(d => d.CobrosDetalle)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

            }
            catch (Exception)
            {
                throw;
            }
            
            return venta;
        }

        public async Task<List<Ventas>> GetList(Expression<Func<Ventas, bool>> criterio)
        {
            List<Ventas> lista = new List<Ventas>();

            try
            {
                lista = await Contexto.Ventas.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        public async Task<List<CobrosDetalle>> GetVentasPendientes(int clienteId)
        {
            var pendientes = new List<CobrosDetalle>();

            var ventas = await Contexto.Ventas
                .Where(v => v.ClienteId == clienteId && v.Balance > 0)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in ventas)
            {
                pendientes.Add(new CobrosDetalle
                {
                    VentaId = item.VentaId,
                    Venta = item,
                    Cobrado = 0
                });
            }

            return pendientes;
        }

        public async Task<List<CobrosDetalle>> GetVentasCobradas(int clienteId)
        {
            var pendientes = new List<CobrosDetalle>();

            var ventas = await Contexto.Ventas
                .Where(v => v.ClienteId == clienteId && v.Balance > 0)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in ventas)
            {
                pendientes.Add(new CobrosDetalle
                {
                    VentaId = item.VentaId,
                    Venta = item,
                    Cobrado = 0
                });
            }

            return pendientes;
        }
    }
}
