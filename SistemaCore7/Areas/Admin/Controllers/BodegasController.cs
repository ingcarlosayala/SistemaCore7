using Microsoft.AspNetCore.Mvc;
using SistemaCore7.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore7.Models;

namespace SistemaCore7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegasController : Controller
    {
        private readonly IUnidadTrabajo unidad;

        public BodegasController(IUnidadTrabajo unidad)
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
        public async Task<IActionResult> Crear(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                await unidad.Bodega.Agregar(bodega);
                await unidad.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(bodega);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bodegaDb = await unidad.Bodega.Obtener(id.GetValueOrDefault());

            if (bodegaDb == null)
            {
                return NotFound();
            }

            return View(bodegaDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                await unidad.Bodega.Actualizar(bodega);
                await unidad.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(bodega);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todo = await unidad.Bodega.ObtenerTodos();
            return Json(new { data = todo });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error al eliminar la bodega" });
            }

            var bodegaDb = await unidad.Bodega.Obtener(id.GetValueOrDefault());
            unidad.Bodega.Remover(bodegaDb);
            await unidad.Guardar();
            return Json(new { success = true, message = "Bodega eliminada exitosamente" });
        }

        #endregion
    }
}
