using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Models
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public int WorkID { get; set; }
        public int GroupID { get; set; }    
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un titulo")]
        public String Subject { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una descripcion")]
        public String Description { get; set; }
        public byte TaskStatus { get; set; }

        public string GroupName { get; set; }
        public string WorkSubject { get; set; }
    }
}