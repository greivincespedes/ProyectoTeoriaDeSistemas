using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.Helpers;

namespace SistemaDeControlDeNotas.Models
{
    public class EvaluationIndexModel
    {
        public MemberUserModel CurrentUser { get; set; }
        public EvaluationModel Evaluation { get; set; }
        public SelectTaskHelpers TasksList { get; set; }
        public String OperationResult { get; set; }
    }
}