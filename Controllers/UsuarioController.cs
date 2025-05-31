using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using StiegerInmobiliaria.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace StiegerInmobiliaria.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IrepositorioUsuario repositorio;
        private readonly IConfiguration config;

        public UsuarioController(IrepositorioUsuario repositorio, IConfiguration config)
        {
            this.repositorio = repositorio;
            this.config = config;

        }

        [AllowAnonymous]
        public ActionResult LoginView()
        {

            return View("Login");
        }


        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioModel usuario)
        {
            try
            {
                string hasheo = Hash(usuario);
                var comparo = repositorio.obtenerXNombre(usuario.Nombre);
                if (comparo.Id_usuario == -1 || comparo.Contraseña != hasheo)
                {
                    TempData["Mensaje"] = "El nombre o la contraseña no son validos.";
                    TempData["Alerta"] = "alert alert-danger";
                    return RedirectToAction("LoginView");
                }

                var claims = new List<Claim>{
                new Claim(ClaimTypes.Name,comparo.Nombre),
                new Claim(ClaimTypes.NameIdentifier,comparo.Id_usuario+""),
                new Claim(ClaimTypes.Role,comparo.Rol)
                };

                //donde se guardan las claims
                var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                                            CookieAuthenticationDefaults.AuthenticationScheme,
                                            new ClaimsPrincipal(claimsIdentity));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return RedirectToAction("LoginView");
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult NuevoEditar(int id)
        {
            var usr = new UsuarioModel();
            if (id > 0)
            {
                int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (User.IsInRole("administrador") || id == idUser)
                {
                    usr = repositorio.TraerId(id);
                    usr.Contraseña = "";
                }
                else
                {
                    return RedirectToAction("Restringido", "Home");
                }
            }

            return View(usr);
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Perfil(int id)
        {
            var usr = repositorio.TraerId(id);
            usr.Contraseña = "";
            return View(usr);
        }

        [Authorize(Policy = "administrador")]
        public ActionResult Indice(int id)
        {
            var usrs = repositorio.TraerTodosDTO();
            return View(usrs);
        }

        [Authorize(Policy = "administrador")]
        public ActionResult Nuevo(UsuarioModel usr)
        {
            if (ModelState.IsValid)
            {
                //hashear ps
                var hash = Hash(usr);
                usr.Contraseña = hash;
                //crear link imagen
                usr.Imagen = GuardarArchivo(usr);
                repositorio.Alta(usr);

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
                return View("NuevoEditar", usr);
            }

        }

        [Authorize(Policy = "administrador")]
        public ActionResult Eliminar(int id)
        {
            try
            {
                repositorio.Baja(id);
                return Json(new { success = true, mensaje = "Usuario eliminado exitosamente." });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, mensaje = "Ah ocurrido un error al eliminar." });
                throw;
            }
        }
        public ActionResult Editar(UsuarioModel usr)
        {
            int idUser = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var og = repositorio.TraerId(usr.Id_usuario);
            if (User.IsInRole("administrador") || usr.Id_usuario == idUser)
            {

                //hashear ps solo si escribio algo
                if (!string.IsNullOrEmpty(usr.Contraseña))
                {
                    var hash = Hash(usr);
                    //modifico la contraseña?
                    if (og.Contraseña != hash)
                    {
                        og.Contraseña = hash;
                    }

                }

                //crear link imagen
                var archivo = usr.ImgArchivo;
                if (archivo != null)
                {
                    if (!string.IsNullOrEmpty(og.Imagen) && archivo.FileName != Path.GetFileName(og.Imagen))
                    {
                        EliminarArchivoServer(og.Imagen);
                    }

                    string url = GuardarArchivo(usr);
                    og.Imagen = url;
                }
                repositorio.Modificacion(og);
            }
            else
            {
                return RedirectToAction("Restringido", "Home");
            }

            return RedirectToAction("Perfil", new { id = og.Id_usuario });
        }

        /*public ActionResult EliminarImagen(){

            return RedirectToAction();
        }*/
        
        private string Hash(UsuarioModel usuario)
        {
            string hasheo = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: usuario.Contraseña,
                    salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                        numBytesRequested: 256 / 8));
            return hasheo;
        }


        private string GuardarArchivo(UsuarioModel usr)
        {
            var archivo = usr.ImgArchivo;
            var url = "";
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
                url = "/uploads/" + nombreArchivo;
            }

            return url;
        }



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
        //---------------------------------------------------------------------------------------------------------------------------
    }

}