using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Models
{
    public class GroupModel
    {
        public int GroupID { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre para el grupo")]
        public String GroupName { get; set; }

        public int GroupStatus { get; set; }
    }
}