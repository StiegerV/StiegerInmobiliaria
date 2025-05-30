using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;

namespace StiegerInmobiliaria.Controllers
{
    [Authorize]
    public class InquilinoController : Controller
    {
        private readonly IrepositorioInquilino repositorio;

        public InquilinoController(IrepositorioInquilino repositorio)
        {
            this.repositorio = repositorio;
        }

        public ActionResult Indice(int pagina = 1)
        {
            int tamPagina = 5;
            var inmuebles = repositorio.TraerTodos(pagina, tamPagina);

            ViewBag.PaginaActual = pagina;
            ViewBag.TamPagina = tamPagina;
            var totalRegistros = repositorio.TraerCantidad();
            ViewBag.TotalPaginas = repositorio.ObtenerTotalPaginas(tamPagina, totalRegistros);

            return View(inmuebles);
        }



        [Authorize(Policy = "administrador")]
        public ActionResult Eliminar(int id)
        {
            try
            {
                int col = repositorio.Baja(id);
                return Json(new { success = true, mensaje = "Inquilino eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false, mensaje = "Error inesperado al eliminar el Inquilino." });
            }
        }

        public ActionResult NuevoEditar(int id)
        {
            InquilinoModel inquilino;
            if (id > 0)
            {
                inquilino = repositorio.TraerId(id);
            }
            else
            {
                inquilino = new InquilinoModel();
            }
            return View(inquilino);
        }

        [HttpPost]
        public ActionResult NuevoInquilino(InquilinoModel inquilino)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    repositorio.Alta(inquilino);
                    TempData["Mensaje"] = "Inquilino Creado exitosamente.";
                    TempData["Alerta"] = "alert alert-success";
                    return RedirectToAction("indice");
                }
                catch (System.Exception)
                {
                    TempData["Mensaje"] = "Error al crear el inquilino";
                    TempData["Alerta"] = "alert alert-danger";
                    return RedirectToAction("indice");
                }
            }
            else
            {
                var errores = ModelState.Values
     .SelectMany(v => v.Errors)
     .Select(e => e.ErrorMessage)
     .ToList();
                TempData["Mensaje"] = string.Join(" | ", errores);
                TempData["Alerta"] = "alert alert-danger";
                return View("NuevoEditar", inquilino);
            }


        }

        [HttpPost]
        public ActionResult EditarInquilino(InquilinoModel inquilino)
        {
            Console.WriteLine(inquilino.Id_inquilino);
            repositorio.Modificacion(inquilino);
            return RedirectToAction("indice");
        }

        public ActionResult Detalle(int id)
        {
            InquilinoModel inquilino = repositorio.TraerId(id);
            return View(inquilino);
        }


        [HttpGet]
        [Route("[controller]/Buscar")]
        public ActionResult Buscar(string term)
        {
            var propietarios = repositorio.Busqueda(term);
            return Json(propietarios);
        }
    }
}
