using Microsoft.AspNetCore.Mvc;
using SistemaCore7.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore7.Models;
using SistemaCore7.Models.ViewsModels;

namespace SistemaCore7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductosController(IUnidadTrabajo unidadTrabajo,IWebHostEnvironment webHostEnvironment)
        {
            this.unidadTrabajo = unidadTrabajo;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                ListaCategorias = unidadTrabajo.Categoria.ListaCategorias(),
                ListaMarcas = unidadTrabajo.Marca.ListaMarcas()
            };

            productoVM.Producto.Estado = true;

            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = webHostEnvironment.WebRootPath;
                var archivo = HttpContext.Request.Form.Files;

                if (productoVM.Producto.Id == 0)
                {
                    //Subir una imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    string subida = Path.Combine(rutaPrincipal,@"imagenes\producto");
                    var extension = Path.GetExtension(archivo[0].FileName);

                    using (var fileStrems = new FileStream(Path.Combine(subida,nombreArchivo + extension),FileMode.Create))
                    {
                        archivo[0].CopyTo(fileStrems);
                    }

                    productoVM.Producto.ImagenUrl = @"\imagenes\producto\" + nombreArchivo + extension;

                    await unidadTrabajo.Producto.Agregar(productoVM.Producto);
                    await unidadTrabajo.Guardar();
                    return RedirectToAction("Index");
                }
            }

            productoVM.ListaCategorias = unidadTrabajo.Categoria.ListaCategorias();
            productoVM.ListaMarcas = unidadTrabajo.Marca.ListaMarcas();

            return View(productoVM);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                ListaCategorias = unidadTrabajo.Categoria.ListaCategorias(),
                ListaMarcas = unidadTrabajo.Marca.ListaMarcas()
            };

            productoVM.Producto = await unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());

            if (productoVM.Producto == null)
            {
                return NotFound();
            }

            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = webHostEnvironment.WebRootPath;
                var archivo = HttpContext.Request.Form.Files;
                var productoDb = await unidadTrabajo.Producto.ObtenerPrimero(p => p.Id == productoVM.Producto.Id);

                if (archivo.Count() > 0)
                {
                    //Subir una imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    string subida = Path.Combine(rutaPrincipal, @"imagenes\producto");
                    var extension = Path.GetExtension(archivo[0].FileName);

                    var imagenRuta = Path.Combine(rutaPrincipal,productoDb.ImagenUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(imagenRuta))
                    {
                        System.IO.File.Delete(imagenRuta);
                    }

                    using (var fileStrems = new FileStream(Path.Combine(subida, nombreArchivo + extension), FileMode.Create))
                    {
                        archivo[0].CopyTo(fileStrems);
                    }

                    productoVM.Producto.ImagenUrl = @"\imagenes\producto\" + nombreArchivo + extension;

                    await unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                    await unidadTrabajo.Guardar();
                    return RedirectToAction("Index");
                }
                else
                {
                    productoVM.Producto.ImagenUrl = productoDb.ImagenUrl;
                }

                await unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                await unidadTrabajo.Guardar();
                return RedirectToAction("Index");
            }

            productoVM.ListaCategorias = unidadTrabajo.Categoria.ListaCategorias();
            productoVM.ListaMarcas = unidadTrabajo.Marca.ListaMarcas();

            return View(productoVM);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todo = await unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todo });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var productoDb = await unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
            var rutaPrincipal = webHostEnvironment.WebRootPath;

            if (productoDb == null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            var imagenRuta = Path.Combine(rutaPrincipal, productoDb.ImagenUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imagenRuta))
            {
                System.IO.File.Delete(imagenRuta);
            }

            unidadTrabajo.Producto.Remover(productoDb);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Producto eliminado exitosamente" });
        }


        #endregion
    }
}
