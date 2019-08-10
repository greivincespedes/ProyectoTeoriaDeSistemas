using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.Helpers;

namespace SistemaDeControlDeNotas.Models
{
    public class WorkIndexModel
    {
        public MemberUserModel CurrentUser { get; set; }
        public WorkModel NewWork { get; set; }
        public SelectGroupHelpers GroupsList { get; set; }
        public String OperationResult { get; set; }
    }
}