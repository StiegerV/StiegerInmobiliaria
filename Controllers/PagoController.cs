using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.DTOs;
using StiegerInmobiliaria.Models;

namespace StiegerInmobiliaria.Controllers
{
    public class PagoController : Controller
    {

        private readonly IrepositorioPagos repositorio;
        private readonly iRepositorioContrato repositorioContrato;


        public PagoController()
        {
            this.repositorio = new RepositorioPago();
            this.repositorioContrato = new RepositorioContrato();
        }


        public ActionResult Indice()
        {
            List<PagoDTO> pagos = repositorio.TraerTodosDTO();
            return View(pagos);
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
            PagoDTO p = repositorio.traerIdDTO(id);
            Console.WriteLine(p.Test());
            return View(p);
        }


        public ActionResult NuevoEditar(int id)
        {
            var p = new PagoModel();

            if (id > 0)
            {
                p = repositorio.TraerId(id);
            }

            return View(p);
        }

        public ActionResult NuevoPago(PagoModel p)
        {
            try
            {
                repositorio.Alta(p);
                TempData["Mensaje"] = "Pago creado exitosamente.";
            }
            catch (System.Exception)
            {
                TempData["Mensaje"] = "Error al crear pago.";

            }
            return RedirectToAction("Indice");
        }

        public ActionResult EditarPago(PagoModel p){
            repositorio.Modificacion(p);

            return RedirectToAction("Indice");
        }
    }
}