using Microsoft.AspNetCore.Mvc;
using StiegerModels;

namespace StiegerControllers
{
    public class PropietarioController : Controller
    {
        private readonly IrepositorioPropietario repositorio;

        //IrepositorioPropietario repositorio
        public PropietarioController()
        {
            this.repositorio = new RepositorioPropietario();

        }

        //el nombre del metodo es el que devuelve la vista
        public ActionResult Indice()
        {
            var propietarios = repositorio.TraerTodos();
            return View(propietarios);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Propietario eliminado exitosamente.";
                return RedirectToAction("Indice");
            }
            catch (System.Exception)
            {
                TempData["Mensaje"] = "Error al eliminar propietario.";
                return RedirectToAction("Indice");
                throw;
            }
        }

        [HttpPost]
        public ActionResult Editar(int id)
        {
            var propietario = repositorio.TraerId(id);

            return View(propietario);
        }

        [HttpPost]
        public ActionResult EditarPropietario(PropietarioModel propietario)
        {
            int columnas = repositorio.Modificacion(propietario);
            Console.WriteLine(columnas);
            Console.WriteLine(propietario.Id_propietario);
            return RedirectToAction("indice");
        }
    }
}