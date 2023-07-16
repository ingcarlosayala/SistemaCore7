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
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriaRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Categoria categoria)
        {
            var categoriaDb = await dbContext.Categorias.FirstOrDefaultAsync(b => b.Id == categoria.Id);

            if (categoriaDb != null)
            {
                categoriaDb.Nombre = categoria.Nombre;
                categoriaDb.Estado = categoria.Estado;
            }
        }
    }
}
