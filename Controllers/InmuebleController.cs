using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;

namespace StiegerControllers
{
    public class InmuebleController : Controller
    {
        private readonly IrepositorioInmueble repositorio;

        public InmuebleController(){
            repositorio=new RepositorioInmueble();
        }

        public ActionResult indice(){
            var inmuebles=repositorio.TraerTodos();
            return View(inmuebles);
        }
    }
}