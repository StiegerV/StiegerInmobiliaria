namespace StiegerInmobiliaria.Models
{

    public class UsuarioModel
    {

        public int Id_usuario { get; set; }

        public string? Nombre { get; set; }

        public string? Contraseña { get; set; }

        public string? Rol { get; set; }

        public string? Imagen { get; set; }
        public IFormFile? ImgArchivo { get; set; }


    }
}