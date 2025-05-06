using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;

namespace StiegerInmobiliaria.Controllers
{
    public class PagoController : Controller
    {

        private readonly IrepositorioPagos repositorio;
        private readonly iRepositorioContrato repositorioContrato;


        PagoController()
        {
            repositorio = new RepositorioPago();
            repositorioContrato = new RepositorioContrato();
        }


        public ActionResult Indice()
        {
            List<ContratoModel> contratos = repositorioContrato.TraerTodos();

            return View(contratos);
        }


        public ActionResult Eliminar(int id)
        {
            int columnas = repositorio.Baja(id);
            if (columnas == 1)
            {
                TempData["Mensaje"] = "Pago eliminado exitosamente.";
            }
            else
            {
                TempData["Mensaje"] = "Error al eliminar Pago.";
            }
            return RedirectToAction("Indice");
        }


        public ActionResult Detalle(int id)
        {
            PagoModel p = repositorio.TraerId(id);
            return View(p);
        }


        public ActionResult NuevoEditar(int id){
            var p=new PagoModel();
            
            if (id>0)
            {
                p=repositorio.TraerId(id);
            }

            return View(p);
        }
    }
}