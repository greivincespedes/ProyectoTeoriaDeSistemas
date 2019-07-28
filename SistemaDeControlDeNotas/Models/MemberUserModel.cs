using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.BaseModels;

namespace SistemaDeControlDeNotas.Models
{
    public class MemberUserModel : BaseUserModel
    {
        [Required(ErrorMessage = "Por favor ingrese su correo electronico")]
        [EmailAddress(ErrorMessage = "Verifique el formato de su correo correo")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una contraseña")]
        public override string Password { get; set; }


        public override byte UserProfile { get; set; }
    }
}