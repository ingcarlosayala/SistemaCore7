using SistemaCore7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore7.AccesoDatos.Repositorio.IRepositorio
{
    public interface IBodegaRepositorio : IRepositorio<Bodega>
    {
        Task Actualizar(Bodega bodega);
    }
}
