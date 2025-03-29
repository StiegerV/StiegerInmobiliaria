using Microsoft.AspNetCore.Mvc;
using StiegerModels;

namespace StiegerControllers
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
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(int id)
        {
            var inquilino = repositorio.TraerId(id);

            return View(inquilino);
        }

        [HttpPost]
        public ActionResult EditarInquilino(InquilinoModel inquilino)
        {
            int columnas = repositorio.Modificacion(inquilino);
            Console.WriteLine(columnas);
            Console.WriteLine(inquilino.Id_inquilino);
            return RedirectToAction("indice");
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoInquilino(InquilinoModel inquilino)
        {
            repositorio.Alta(inquilino);
            return RedirectToAction("indice");
        }
    }
}