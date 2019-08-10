using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Models
{
    public class WorkModel
    {
        public int WorkID { get; set; }

        [Required(ErrorMessage = "Por favor seleccione un grupo")]
        public int GroupID { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un asunto")]
        public String Subject { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una descripcion")]
        public String Description { get; set; }

        public byte WorkStatus { get; set; }
    }
}