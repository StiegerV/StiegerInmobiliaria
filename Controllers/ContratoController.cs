using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;
using StiegerInmobiliaria.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize]
public class ContratoController : Controller
{
    private readonly iRepositorioContrato repositorio;
    private readonly IrepositorioInmueble repositorioInmueble;
    private readonly IrepositorioInquilino repositorioInquilino;

    private readonly IrepositorioPropietario repositorioPropietario;

    private readonly IrepositorioPagos repositorioPago;

    public ContratoController(iRepositorioContrato con, IrepositorioInmueble inm, IrepositorioInquilino inq, IrepositorioPropietario prop, IrepositorioPagos pag)
    {
        repositorio = con;
        repositorioInmueble = inm;
        repositorioInquilino = inq;
        repositorioPropietario = prop;
        repositorioPago = pag;
    }


    public ActionResult Indice(int pagina = 1)
    {
        int tamPagina = 5;
        var contratos = repositorio.TraerTodosDTO(pagina, tamPagina);


        ViewBag.PaginaActual = pagina;
        ViewBag.TamPagina = tamPagina;
        ViewBag.Accion = "Indice";
        ViewBag.Title = "Contratos";
        var totalRegistros = repositorio.TraerCantidad();
        ViewBag.TotalPaginas = repositorio.ObtenerTotalPaginas(tamPagina, totalRegistros);

        return View(contratos);
    }


    [Authorize(Policy = "administrador")]
    public ActionResult Eliminar(int id)
    {
        int columnas = repositorio.Baja(id);
        if (columnas == 1)
        {
            TempData["Mensaje"] = "Contrato eliminado exitosamente.";
        }
        else
        {
            TempData["Mensaje"] = "Error al eliminar Contrato.";
        }
        return RedirectToAction("Indice");
    }

    public ActionResult Cancelar(int id)
    {
        var c = new CancelarDTO();
        var contrato = repositorio.TraerId(id);
        c.id_contrato = contrato.Id_contrato;
        c.fechaCancelacion = contrato.FechaFin;
        return View(c);
    }

    public ActionResult CancelarContrato(CancelarDTO c)
    {
        var OG = repositorio.TraerId(c.id_contrato);

        //calculo de multa
        int mesesTotales = ((OG.FechaFin.Year - OG.FechaInicio.Year) * 12) + OG.FechaFin.Month - OG.FechaInicio.Month;
        int mesesCumplidos = ((c.fechaCancelacion.Year - OG.FechaInicio.Year) * 12) + c.fechaCancelacion.Month - OG.FechaInicio.Month;
        int mesesMulta = mesesCumplidos < mesesTotales / 2 ? 2 : 1;
        float multa = OG.Monto * mesesMulta;
        //quien cancelo y modificacion en bd
        OG.FechaFin = c.fechaCancelacion;
        int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        OG.Terminado_por = idUser;
        repositorio.Cancelar(OG);
        //cancelacion de pagos de los meses que vienen
        var pagosAcancelar = repositorioPago.IdPagosXFechaFin(OG.FechaFin.ToString("yyyy-MM-dd"), OG.Id_contrato);
        foreach (var item in pagosAcancelar)
        {
            item.Estado = "anulado";
            repositorioPago.Modificacion(item);
        }
        var p = new PagoModel();
        p.Id_contrato = c.id_contrato;
        p.Monto = multa;
        p.Fecha = DateTime.Now;
        p.Observacion = "Multa por cancelacion";
        p.Estado = "en proceso";
        repositorioPago.Alta(p);

        TempData["Mensaje"] = $"Se ah aplicado una multa de {mesesMulta} meses.";


        return RedirectToAction("Indice");
    }

    public ActionResult Detalle(int id)
    {
        var c = new ContratoDTO();
        var contrato = repositorio.TraerId(id);
        c.Id_contrato = contrato.Id_contrato;
        c.Fecha_inicio = contrato.FechaInicio;
        c.Fecha_fin = contrato.FechaFin;
        c.Inmueble = repositorioInmueble.TraerIdDTO(contrato.Id_inmueble);
        c.Propietario = repositorioInmueble.TraerIdPropietarioDTO(contrato.Id_inmueble);
        c.Inquilino = repositorioInquilino.traerIdDTO(contrato.Id_inquilino);
        var creador = new UsuarioDTO();
        creador.Id_usuario = 1;
        creador.Nombre = "pepe";
        creador.Rol = "Administrador";
        c.CreadoPor = creador;
        return View(c);
    }

    public ActionResult NuevoEditar(int id)
    {
        var contrato = new ContratoModel();

        if (id > 0)
        {
            contrato = repositorio.TraerId(id);
            if (contrato.FechaFin > DateTime.Now)
            {
                TempData["Mensaje"] = "El contrato todavia no ah terminado.";
                return RedirectToAction("Indice");
            }
            TempData["Inmueble"] = repositorioInmueble.TraerIdDTO(contrato.Id_inmueble);
            TempData["Inquilino"] = repositorioInquilino.traerIdDTO(contrato.Id_inquilino);
        }

        return View(contrato);
    }

    public ActionResult NuevoContrato(ContratoModel c)
    {
        try
        {
            int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            c.Creador_por = idUser;
            int id = repositorio.Alta(c);
            //cuanto pagos hay que hacer
            int mesesTotales = ((c.FechaFin.Year - c.FechaInicio.Year) * 12) + c.FechaFin.Month - c.FechaInicio.Month;
            var fecha = c.FechaInicio;
            //generacion de pagos
            for (int i = 0; i < mesesTotales; i++)
            {
                var p = new PagoModel();
                p.Id_contrato = id;
                p.Monto = c.Monto;
                p.Fecha = fecha;

                string[] meses = {"enero", "febrero", "marzo", "abril", "mayo", "junio",
                                "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"};
                p.Observacion = $"Pago {meses[fecha.Month - 1]}";
                p.Estado = "en proceso";
                repositorioPago.Alta(p);
                fecha = fecha.AddMonths(1);
            }
            TempData["Mensaje"] = "Contrato creado exitosamente.";

            return RedirectToAction("Indice");
        }
        catch (System.Exception ex)
        {
            TempData["Mensaje"] = "Ah ocurrido un error al crear contrato";
            Console.WriteLine(ex);
            return RedirectToAction("Indice");
        }

    }

    public ActionResult BuscarVigenteXFechas(string inicio, string fin, int pagina = 1)
    {
        int tamPagina = 5;
        //formatear los strings

        var contratos = repositorio.BuscarVigenteXFechas(pagina, tamPagina, inicio, fin);


        ViewBag.PaginaActual = pagina;
        ViewBag.TamPagina = tamPagina;
        ViewBag.Accion = "BuscarVigenteXFechas";
        ViewBag.Title = $"Contratos entre {inicio} y {fin}";
        //preguntar si conviene por sql el total o si sirve el count
        var totalRegistros = contratos.Count;
        ViewBag.TotalPaginas = repositorio.ObtenerTotalPaginas(tamPagina, totalRegistros);


        return View("Indice", contratos);

    }



    [Route("[controller]/buscarInmuebles")]
    public ActionResult buscarInmuebles(string inicio, string fin)
    {
        List<InmuebleDTO> disponibles = repositorioInmueble.traerDesocupados(inicio, fin);

        return Json(disponibles);
    }


    [Route("[controller]/buscarContrato")]
    public ActionResult buscarContrato(string term)
    {
        var disponibles = repositorio.Busqueda(term);


        return Json(disponibles);
    }

    public ActionResult ContratoXInmueble(int id)
    {
       var contratos= repositorio.ContratoXInmueble(id);

        return Json(contratos);
        }

}

