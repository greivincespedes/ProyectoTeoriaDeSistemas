using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Models
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una descripcion")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Por favor seleccione un estado")]
        [Range(1, 2, ErrorMessage = "Por favor seleccione un estado")]
        public byte State { get; set; }
    }
}