namespace StiegerInmobiliaria.DTOs
{
    public class UsuarioDTO
    {
        public int Id_usuario{get;set;}

        public string Nombre{get;set;}

        public string Rol{get; set;}

         public override string ToString()
        {
            return $"{Nombre}"+" "+$"{Rol}";
        }
    }
}