using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Models
{
    public class EvaluationModel
    {
        public int AssignmentTask { get; set; }
        public int Category { get; set; }
        public int status { get; set; }
        public int GroupID { get; set; }
        public int WorkID { get; set; }
        public int StudentID { get; set; }
        public int TaskID { get; set; }
        public string WorkSubject { get; set; }
        public string StudentName { get; set; }
        public string GroupName { get; set; }
        public string LeadershipScale { get; set; }
        public string PunctualityScale { get; set; }
        public string HonestyScale { get; set; }
        public string AttitudeScale { get; set; }
    }
}