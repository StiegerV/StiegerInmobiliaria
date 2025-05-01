namespace StiegerInmobiliaria.DTOs
{
    public class PropietarioDTO
    {
        public int Id_propietario{get; set;}

        public string Nombre{get; set;}

        public string Apellido{get; set;}

         public override string ToString()
        {
            return $"{Nombre}"+" "+$"{Apellido}";
        }
    }
}