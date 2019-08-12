using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.BaseModels;
using SistemaDeControlDeNotas.Helpers;

namespace SistemaDeControlDeNotas.Models
{
    public class NewUserModel : BaseUserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un numero de grupo")]
        [RegularExpression(@"[0-9]+")]
        public int UserGroup { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un correo")]
        public override byte UserProfile { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el primer apellido")]
        public string FirstLastName { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el segundo apellido")]
        public string SecondLastName { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el numero de identificacion")]
        [RegularExpression(@"^[0-9]-[0-9]{4}-[0-9]{4}$", ErrorMessage = "La identificacion debe tener el formato 1-1111-1111")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un correo")]
        [EmailAddress(ErrorMessage = "Verifique el formato del correo")]
        public override string Email { get; set; }

        public string User { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una contraseña")]
        [MinLength(8, ErrorMessage = "La contraseña debe contar con al menos 8 caracteres")]
        public override string Password { get; set; }

        [Required]
        public int Status { get; set; }

        public SelectGroupHelpers GroupsList { get; set; }
    }
}