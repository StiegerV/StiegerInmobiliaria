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
            try
            {
                int usr = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                int columnas = repositorio.BajaUser(id, usr);
                if (columnas == 1)
                {
                    Console.WriteLine(columnas);
                    return Json(new { success = true, mensaje = "Pago eliminado exitosamente." });
                }
                return Json(new { success = false, mensaje = "No se ah podido eliminar el pago." });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false, mensaje = "Error inesperado al eliminar el Pago." });
            }


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
            if (ModelState.IsValid)
            {


                try
                {
                    p.Creado_por = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    repositorio.Alta(p);

                    TempData["Mensaje"] = "Pago creado exitosamente.";
                    TempData["Alerta"] = "alert alert-succes";
                }
                catch (System.Exception ex)
                {
                    TempData["Mensaje"] = "Error al crear pago.";
                    TempData["Alerta"] = "alert alert-danger";
                    Console.WriteLine(ex);

                }
                return RedirectToAction("Indice");
            }
            else
            {
                var errores = ModelState.Values
     .SelectMany(v => v.Errors)
     .Select(e => e.ErrorMessage)
     .ToList();
                TempData["Mensaje"] = string.Join(" | ", errores);
                TempData["Alerta"] = "alert alert-danger";
                return View("NuevoEditar", p);
            }
        }

        public ActionResult EditarPago(PagoModel p)
        {
            repositorio.Modificacion(p);

            return RedirectToAction("Indice");
        }

        public ActionResult PagosXContrato(int id)
        {
            var pagos = repositorio.PagosXContrato(id);
            return Json(pagos);
        }

        [HttpPost]
        public IActionResult NuevoPagoModal(int Id_contrato, decimal Monto, DateTime Fecha, string Estado, string Observacion)
        {
            var pago = new PagoModel();
            pago.Id_contrato = Id_contrato;
            pago.Monto = Convert.ToDouble(Monto);

            pago.Fecha = Fecha;
            pago.Estado = Estado;
            pago.Observacion = Observacion;
            pago.Creado_por = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            repositorio.Alta(pago);
            return RedirectToAction("Detalle", "Contrato", new { id = Id_contrato });
        }

    }
}