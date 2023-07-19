using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCore7.AccesoDatos.Data;
using SistemaCore7.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore7.AccesoDatos.Repositorio
{
    public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public MarcaRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Marca marca)
        {
            var marcaDb = await dbContext.Marcas.FirstOrDefaultAsync(b => b.Id == marca.Id);

            if (marcaDb != null)
            {
                marcaDb.Nombre = marca.Nombre;
                marcaDb.Estado = marca.Estado;
            }
        }

        public IEnumerable<SelectListItem> ListaMarcas()
        {
            return dbContext.Marcas.Select(m => new SelectListItem
            {
                Text = m.Nombre,
                Value = m.Id.ToString()
            });
        }
    }
}
