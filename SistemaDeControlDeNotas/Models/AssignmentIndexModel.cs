using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.Helpers;

namespace SistemaDeControlDeNotas.Models
{
    public class AssignmentIndexModel
    {
        public MemberUserModel CurrentUser { get; set; }
        public TaskModel NewTask { get; set; }
        public SelectGroupHelpers GroupsList { get; set; }
        public SelectWorkHelpers WorksList { get; set; }
        public String OperationResult { get; set; }
    }
}