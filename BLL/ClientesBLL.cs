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
    public class ClientesBLL
    {
        private Contexto Contexto { get;  set; }

        public ClientesBLL(Contexto contexto)
        {
            this.Contexto = contexto;
        }

        public async Task<Clientes> Buscar(int id)
        {
            Clientes persona;

            try
            {
                persona = await Contexto.Clientes.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }

            return persona;
        }

        public async Task<List<Clientes>> GetClientes()
        {
            List<Clientes> lista = new List<Clientes>();

            try
            {
                lista = await Contexto.Clientes.ToListAsync();
            }
            catch(Exception)
            {
                throw;
            }

            return lista;
        }

        public async Task<List<Clientes>> GetClientes(Expression<Func<Clientes, bool>> criterio)
        {
            List<Clientes> lista = new List<Clientes>();

            try
            {
                lista = await Contexto.Clientes.Where(criterio).Include(c => c.Venta).ToListAsync();
            }
            catch(Exception)
            {
                throw;
            }
            return lista;
        }
    }
}
