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

        public UnidadTrabajo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Bodega = new BodegaRepositorio(dbContext);
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
