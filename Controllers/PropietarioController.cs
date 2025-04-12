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
        public ActionResult Indice() {
            var propietarios = repositorio.TraerTodos();
            return View(propietarios);
        }

        [HttpPost]
        public ActionResult Eliminar(int id){
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
            Console.WriteLine(propietario.Id_propietario);
            repositorio.Alta(propietario);
            return RedirectToAction("indice");
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


    }
}