using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;
using StiegerInmobiliaria.DTOs;

namespace StiegerInmobiliaria.Controllers
{
    public class InmuebleController : Controller
    {
        private readonly IrepositorioInmueble repositorio;
        private readonly IrepositorioPropietario repositorioPropietario;

        public InmuebleController()
        {
            repositorio = new RepositorioInmueble();
            repositorioPropietario = new RepositorioPropietario();
        }

        public ActionResult Indice()
        {
            var inmuebles = repositorio.TraerTodos();
            return View(inmuebles);
        }

        public ActionResult Eliminar(int id)
        {

            if (repositorio.ContratoActivo(id) == -1)
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Inmueble eliminado exitosamente.";
                return RedirectToAction("Indice");
            }
            else
            {
                TempData["Mensaje"] = "Error al eliminar Inmueble,existe un contrato activo.";
                return RedirectToAction("Indice");
            }



        }

        public ActionResult Detalle(int id)
        {

            InmuebleModel inmueble = repositorio.TraerId(id);
            PropietarioModel propietario = repositorioPropietario.TraerId(inmueble.Id_propietario);
            ViewBag.Propietario = propietario;
            return View(inmueble);
        }


        public ActionResult NuevoEditar(int id)
        {
            ViewBag.propietarios = repositorioPropietario.TraerTodos();
            InmuebleModel inmueble = new InmuebleModel();
            if (id > 0)
            {
                inmueble = repositorio.TraerId(id);
                PropietarioModel p = repositorioPropietario.TraerId(inmueble.Id_propietario);
                TempData["id"] = p.Id_propietario;
                TempData["nombre"] = p.Nombre;
                TempData["apellido"] = p.Apellido;
                TempData["dni"] = p.Dni;
            }


            return View(inmueble);
        }

        [HttpPost]
        public ActionResult EditarInmueble(InmuebleModel inmueble)
        {
            var archivo = inmueble.ImagenArchivo;
            string imagenOriginal = Request.Form["ImagenOriginal"];
            if (!imagenOriginal.Equals(archivo))
            {
                this.EliminarArchivoServer(imagenOriginal);
            }



            if (archivo != null && archivo.Length > 0)
            {
                var carpetaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(carpetaDestino))
                    Directory.CreateDirectory(carpetaDestino);

                //Ticks para caracteres no válidos
                var nombreArchivo = DateTime.Now.Ticks + Path.GetExtension(archivo.FileName);
                var ruta = Path.Combine(carpetaDestino, nombreArchivo);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    archivo.CopyTo(stream);
                }
                inmueble.Imagen = "/uploads/" + nombreArchivo;
            }


            repositorio.Modificacion(inmueble);
            TempData["Mensaje"] = $"Inmueble nro {inmueble.Id_inmueble} editado";

            return RedirectToAction("Indice");
        }

        public ActionResult NuevoInmueble(InmuebleModel inmueble)
        {
            var ImagenArchivo = Request.Form["ImagenArchivo"];
            Console.WriteLine(ImagenArchivo);
            repositorio.Alta(inmueble);
            TempData["Mensaje"] = $"Inmueble creado";

            return RedirectToAction("Indice");
        }


        [HttpPost]
        public ActionResult EliminarImagen(InmuebleModel inmueble)
        {
            inmueble = repositorio.TraerId(inmueble.Id_inmueble);

            if (this.EliminarArchivobd(inmueble) == 0)
            {
                TempData["Mensaje"] = $"No se pudo eliminar la imagen";
            }


            return RedirectToAction("NuevoEditar", new { id = inmueble.Id_inmueble });
        }


        //elimina el archivo del servidor y de la base de datos
        private int EliminarArchivobd(InmuebleModel inmueble)
        {
            int funca = 0;
            if (!string.IsNullOrEmpty(inmueble.Imagen))
            {
                var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", inmueble.Imagen.TrimStart('/'));
                if (System.IO.File.Exists(ruta))
                {
                    System.IO.File.Delete(ruta);
                }
                inmueble.Imagen = "";
                funca = repositorio.EliminarImagen(inmueble);
            }
            return funca;
        }


        //elimina solo el archivo del servidor
        private int EliminarArchivoServer(string link)
        {
            int funca = 0;
            if (!string.IsNullOrEmpty(link))
            {
                var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", link.TrimStart('/'));
                if (System.IO.File.Exists(ruta))
                {
                    System.IO.File.Delete(ruta);
                }
            }
            return funca;
        }

    }
}

