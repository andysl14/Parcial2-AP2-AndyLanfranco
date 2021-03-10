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
    public class CobrosBLL
    {
        private Contexto Contexto { get; set; }

        public CobrosBLL(Contexto contexto)
        {
            this.Contexto = contexto;
        }

        public async Task<bool> Guardar(Cobros cobro)
        {
            return await Insertar(cobro);
        }

        private async Task<bool> Insertar(Cobros cobro)
        {
            bool pass = false;

            try
            {
                foreach(var item in cobro.CobrosDetalle)
                {
                    item.Venta = await Contexto.Ventas.FindAsync(item.VentaId);
                    item.Venta.Balance -= item.Cobrado;
                    Contexto.Entry(item.Venta).State = EntityState.Modified;
                }

                await Contexto.Cobros.AddAsync(cobro);
                pass = await Contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }

            return pass;
        }

        public async Task<Cobros> Buscar(int id)
        {
            Cobros cobro;

            try
            {
                cobro = await Contexto.Cobros
                    .Where(e => e.CobroId == id)
                    .Include(e => e.CobrosDetalle)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return cobro;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool Eliminado = false;

            try
            {
                var cobro = await Buscar(id);
                if(cobro != null)
                {
                    Contexto.Cobros.Remove(cobro);
                    Eliminado = await Contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Eliminado;
        }

        public async Task<List<Cobros>> GetCobros()
        {
            List<Cobros> lista = new List<Cobros>();

            try
            {
                lista = await Contexto.Cobros.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return lista;
        }

        public async Task<List<Cobros>> GetCobros(Expression<Func<Cobros, bool>> criterio)
        {
            List<Cobros> lista = new List<Cobros>();

            try
            {
                lista = await Contexto.Cobros.Where(criterio).ToListAsync();
            }
            catch(Exception)
            {
                throw;
            }

            return lista;
        }
    }
}
