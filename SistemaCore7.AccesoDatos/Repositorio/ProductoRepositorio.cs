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
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public ProductoRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Actualizar(Producto producto)
        {
            var productoDb = await dbContext.Productos.FirstOrDefaultAsync(p => p.Id == producto.Id);

            if (productoDb != null)
            {
                productoDb.Codigo = producto.Codigo;
                productoDb.Descripcion = producto.Descripcion;
                productoDb.Costo = producto.Costo;
                productoDb.Precio = producto.Precio;
                productoDb.Estado = producto.Estado;
                productoDb.ImagenUrl = producto.ImagenUrl;
                productoDb.CategoriaId = producto.CategoriaId;
                productoDb.MarcaId = producto.MarcaId;
            }
        }
    }
}
