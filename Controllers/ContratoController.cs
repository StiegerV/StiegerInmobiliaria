using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;
using StiegerInmobiliaria.DTOs;

public class ContratoController : Controller
{
    private readonly iRepositorioContrato repositorio;
    private readonly IrepositorioInmueble repositorioInmueble;
    private readonly IrepositorioInquilino repositorioInquilino;

    private readonly IrepositorioPropietario repositorioPropietario;

    public ContratoController()
    {
        repositorio = new RepositorioContrato();
        repositorioInmueble = new RepositorioInmueble();
        repositorioInquilino = new RepositorioInquilino();
        repositorioPropietario = new RepositorioPropietario();
    }


    public ActionResult Indice()
    {
        var listaContratos = new List<ContratoDTO>();
        foreach (var i in repositorio.TraerTodos())
        {

            var contratoDTO = new ContratoDTO();
            contratoDTO.Id_contrato = i.Id_contrato;
            contratoDTO.Fecha_inicio = i.FechaInicio;
            contratoDTO.Fecha_fin = i.FechaFin;

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
        }

        return View(contrato);
    }

    public ActionResult NuevoContrato(ContratoModel contrato){
        Console.WriteLine("id del inmueble");
        Console.WriteLine(contrato.Id_inmueble);
        repositorio.Alta(contrato);

        return RedirectToAction("Indice");
    }

[Route("[controller]/buscarInmuebles")]
    public ActionResult buscarInmuebles(string inicio, string fin)
    {
        List<InmuebleDTO> disponibles = repositorioInmueble.traerDesocupados(inicio, fin);

        return Json(disponibles);
    }
}

