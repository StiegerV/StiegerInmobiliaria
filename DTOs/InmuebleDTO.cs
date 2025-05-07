namespace StiegerInmobiliaria.DTOs
{
    public class InmuebleDTO
    {
        public int Id_inmueble{get;set;}
        public string? Tipo{get;set;}

        public string? Direccion{get;set;}


         public override string ToString()
        {
            return $"{Tipo}"+" "+$"{Direccion}";
        }
    }
}