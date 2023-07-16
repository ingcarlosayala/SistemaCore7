using SistemaCore7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore7.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio : IRepositorio<Categoria>
    {
        Task Actualizar(Categoria categoria);
    }
}
