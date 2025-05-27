using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.DTOs;
using StiegerInmobiliaria.Models;

namespace StiegerInmobiliaria.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {

        private readonly IrepositorioPagos repositorio;
        private readonly iRepositorioContrato repositorioContrato;


        public PagoController(IrepositorioPagos repo, iRepositorioContrato rep)
        {
            this.repositorio = repo;
            this.repositorioContrato = rep;
        }


        public ActionResult Indice(int pagina = 1)
        {
            int tamPagina = 5;
            List<PagoDTO> pagos = repositorio.TraerTodosDTO(pagina, tamPagina);
            ViewBag.PaginaActual = pagina;
            ViewBag.TamPagina = tamPagina;
            var totalRegistros = repositorio.TraerCantidad();
            ViewBag.TotalPaginas = repositorio.ObtenerTotalPaginas(tamPagina, totalRegistros);

            return View(pagos);
        }


        [Authorize(Policy = "administrador")]
        public ActionResult Eliminar(int id)
        {
            int usr = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            int columnas = repositorio.BajaUser(id, usr);
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
                p.Creado_por = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                repositorio.Alta(p);
                //controlar monto>0

                TempData["Mensaje"] = "Pago creado exitosamente.";
            }
            catch (System.Exception ex)
            {
                TempData["Mensaje"] = "Error al crear pago.";
                Console.WriteLine(ex);

            }
            return RedirectToAction("Indice");
        }

        public ActionResult EditarPago(PagoModel p)
        {
            repositorio.Modificacion(p);

            return RedirectToAction("Indice");
        }
    }
}