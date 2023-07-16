using Microsoft.AspNetCore.Mvc;
using SistemaCore7.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore7.Models;

namespace SistemaCore7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IUnidadTrabajo unidad;

        public CategoriasController(IUnidadTrabajo unidad)
        {
            this.unidad = unidad;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await unidad.Categoria.Agregar(categoria);
                await unidad.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaDb = await unidad.Categoria.Obtener(id.GetValueOrDefault());

            if (categoriaDb == null)
            {
                return NotFound();
            }

            return View(categoriaDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await unidad.Categoria.Actualizar(categoria);
                await unidad.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todo = await unidad.Categoria.ObtenerTodos();
            return Json(new { data = todo });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error al eliminar la categoria" });
            }

            var categoriaDb = await unidad.Categoria.Obtener(id.GetValueOrDefault());
            unidad.Categoria.Remover(categoriaDb);
            await unidad.Guardar();
            return Json(new { success = true, message = "Categoria eliminada exitosamente" });
        }

        #endregion
    }
}
