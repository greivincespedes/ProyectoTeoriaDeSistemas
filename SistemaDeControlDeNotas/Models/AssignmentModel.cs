using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Models
{
    public class AssignmentModel
    {
        public int UserID { get; set; }
        public int TaskID { get; set; }
        public int AssignmentStatus { get; set; }
    }
}