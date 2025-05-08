using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;
using StiegerInmobiliaria.DTOs;
using Microsoft.VisualBasic;

public class ContratoController : Controller
{
    private readonly iRepositorioContrato repositorio;
    private readonly IrepositorioInmueble repositorioInmueble;
    private readonly IrepositorioInquilino repositorioInquilino;

    private readonly IrepositorioPropietario repositorioPropietario;

    private readonly IrepositorioPagos repositorioPago;

    public ContratoController()
    {
        repositorio = new RepositorioContrato();
        repositorioInmueble = new RepositorioInmueble();
        repositorioInquilino = new RepositorioInquilino();
        repositorioPropietario = new RepositorioPropietario();
        repositorioPago = new RepositorioPago();
    }


    public ActionResult Indice()
    {
        var listaContratos = new List<ContratoDTO>();
        //cambiar a join en tabla
        foreach (var i in repositorio.TraerTodos())
        {

            var contratoDTO = new ContratoDTO();
            contratoDTO.Id_contrato = i.Id_contrato;
            contratoDTO.Fecha_inicio = i.FechaInicio;
            contratoDTO.Fecha_fin = i.FechaFin;
            contratoDTO.Fecha_fin_original = i.Fecha_fin_original;

            var inquilinoDTO = repositorioInquilino.traerIdDTO(i.Id_inquilino);
            contratoDTO.Inquilino = inquilinoDTO;

            var propietarioDTO = repositorioInmueble.TraerIdPropietarioDTO(i.Id_inmueble);
            contratoDTO.Propietario = propietarioDTO;

            contratoDTO.Inmueble = repositorioInmueble.TraerIdDTO(i.Id_inmueble);
            listaContratos.Add(contratoDTO);
        }


        return View(listaContratos);
    }


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
        Console.WriteLine("test");
        var OG = repositorio.TraerId(c.id_contrato);


        int mesesTotales = ((OG.FechaFin.Year - OG.FechaInicio.Year) * 12) + OG.FechaFin.Month - OG.FechaInicio.Month;
        int mesesCumplidos = ((c.fechaCancelacion.Year - OG.FechaInicio.Year) * 12) + c.fechaCancelacion.Month - OG.FechaInicio.Month;
        int mesesMulta = mesesCumplidos < mesesTotales / 2 ? 2 : 1;
        float multa = OG.Monto * mesesMulta;

        OG.FechaFin = c.fechaCancelacion;
        repositorio.Modificacion(OG);
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

            int id=repositorio.Alta(c);
            int mesesTotales = ((c.FechaFin.Year - c.FechaInicio.Year) * 12) + c.FechaFin.Month - c.FechaInicio.Month;
            var fecha = c.FechaInicio;
            for (int i = 0; i < mesesTotales; i++)
            {
                var p = new PagoModel();
                p.Id_contrato = id;
                p.Monto = c.Monto;
                p.Fecha = fecha;
                p.Observacion = $"Pago {fecha.Month}";
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


    //pasar a el controlador de inmueble
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

}

