using SistemaCore7.AccesoDatos.Data;
using SistemaCore7.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore7.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext dbContext;

        public IBodegaRepositorio Bodega { get; private set; }
        public ICategoriaRepositorio Categoria { get; private set; }
        public IMarcaRepositorio Marca { get; private set; }
        public IProductoRepositorio Producto { get; private set; }

        public UnidadTrabajo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Bodega = new BodegaRepositorio(dbContext);
            Categoria = new CategoriaRepositorio(dbContext);
            Marca = new MarcaRepositorio(dbContext);
            Producto = new ProductoRepositorio(dbContext);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public async Task Guardar()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
