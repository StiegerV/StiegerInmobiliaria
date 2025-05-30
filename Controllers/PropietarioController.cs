using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;

namespace StiegerInmobiliaria.Controllers
{
    [Authorize]
    public class PropietarioController : Controller
    {
        private readonly IrepositorioPropietario repositorio;

        public PropietarioController(IrepositorioPropietario repositorio)
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
                int columnas = repositorio.Baja(id);
                if (columnas == 1)
                {
                    return Json(new { success = true, mensaje = "Propietario eliminado exitosamente." });
                }
                return Json(new { success = false, mensaje = "No se ah podido eliminar al propietario" });
            }
            catch (System.Exception)
            {
                return Json(new { success = false, mensaje = "Error inesperado al eliminar el propietario." });
            }
        }


        public ActionResult NuevoEditar(int id)
        {
            PropietarioModel propietario;
            if (id > 0)
            {
                propietario = repositorio.TraerId(id);
            }
            else
            {
                propietario = new PropietarioModel();
            }
            Console.WriteLine(propietario);
            return View(propietario);
        }

        [HttpPost]
        public ActionResult NuevoPropietario(PropietarioModel propietario)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    repositorio.Alta(propietario);
                    TempData["Mensaje"] = "Propietario Creado .";
                    TempData["Alerta"] = "alert alert-succes";
                    return RedirectToAction("indice");
                }
                catch (System.Exception)
                {
                    TempData["Mensaje"] = "Error al crear propietario.";
                    TempData["Alerta"] = "alert alert-danger";
                    return RedirectToAction("Indice");
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
                return View("NuevoEditar", propietario);
            }
        }


        [HttpPost]
        public ActionResult EditarPropietario(PropietarioModel propietario)
        {
            Console.WriteLine(propietario.Id_propietario);
            repositorio.Modificacion(propietario);
            return RedirectToAction("indice");
        }

        public ActionResult Detalle(int id)
        {
            PropietarioModel propietario = repositorio.TraerId(id);
            return View(propietario);
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