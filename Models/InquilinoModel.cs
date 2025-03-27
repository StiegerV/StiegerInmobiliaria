using System.ComponentModel.DataAnnotations;
using Name;

namespace StiegerModels
{
    class InquilinoModel:personaModel
    {
        [Key]
        public int Id_inquilino { get; set; }


    }


}