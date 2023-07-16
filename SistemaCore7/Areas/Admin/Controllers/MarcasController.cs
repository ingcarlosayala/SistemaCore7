using Microsoft.AspNetCore.Mvc;
using SistemaCore7.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore7.Models;

namespace SistemaCore7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcasController : Controller
    {
        private readonly IUnidadTrabajo unidad;

        public MarcasController(IUnidadTrabajo unidad)
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
        public async Task<IActionResult> Crear(Marca marca)
        {
            if (ModelState.IsValid)
            {
                await unidad.Marca.Agregar(marca);
                await unidad.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaaDb = await unidad.Marca.Obtener(id.GetValueOrDefault());

            if (marcaaDb == null)
            {
                return NotFound();
            }

            return View(marcaaDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Marca marca)
        {
            if (ModelState.IsValid)
            {
                await unidad.Marca.Actualizar(marca);
                await unidad.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todo = await unidad.Marca.ObtenerTodos();
            return Json(new { data = todo });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error al eliminar la marca" });
            }

            var marcaDb = await unidad.Marca.Obtener(id.GetValueOrDefault());
            unidad.Marca.Remover(marcaDb);
            await unidad.Guardar();
            return Json(new { success = true, message = "Marca eliminada exitosamente" });
        }

        #endregion
    }
}
