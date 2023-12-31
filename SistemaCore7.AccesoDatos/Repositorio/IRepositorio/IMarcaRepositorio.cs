﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCore7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore7.AccesoDatos.Repositorio.IRepositorio
{
    public interface IMarcaRepositorio : IRepositorio<Marca>
    {
        Task Actualizar(Marca marca);
        IEnumerable<SelectListItem> ListaMarcas();
    }
}
