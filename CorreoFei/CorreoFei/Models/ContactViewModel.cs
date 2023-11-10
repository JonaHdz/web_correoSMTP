using System.ComponentModel.DataAnnotations;

namespace CorreoFei.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nombre del contacto")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una direccion de correo electronica valida.")]
        [Display(Name = "Correo elctronico")]
        public string Correo { get; set; }


    }
}
