namespace StiegerInmobiliaria.DTOs
{
    public class InquilinoDTO
    {
        public int Id_inquilino{get;set;}

        public string? Nombre{get;set;}

        public string? Apellido{get;set;}


         public override string ToString()
        {
            return $"{Nombre}"+" "+$"{Apellido}";
        }
    }
}