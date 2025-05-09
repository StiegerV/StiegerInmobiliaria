using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;

namespace StiegerInmobiliaria.Controllers
{
    public class InquilinoController : Controller
    {
        private readonly IrepositorioInquilino repositorio;

        //IrepositorioPropietario repositorio
        public InquilinoController()
        {
            this.repositorio = new RepositorioInquilino();
        }

        //el nombre del metodo es el que devuelve la vista
        public ActionResult Indice()
        {
            var inquilinos = repositorio.TraerTodos();
            return View(inquilinos);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Inquilino eliminado exitosamente.";
                return RedirectToAction("Indice");
            }
            catch (System.Exception)
            {
                TempData["Mensaje"] = "Error al eliminar Inquilino.";
                return RedirectToAction("Indice");
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
            Console.WriteLine(inquilino);
            return View(inquilino);
        }

        [HttpPost]
        public ActionResult NuevoInquilino(InquilinoModel inquilino)
        {
            try
            {
                repositorio.Alta(inquilino);
                TempData["Mensaje"] = "Inquilino Creado exitosamente.";
                return RedirectToAction("indice");
            }
            catch (System.Exception)
            {
                TempData["Mensaje"] = "Error al crear el inquilino";
                return RedirectToAction("indice");
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
