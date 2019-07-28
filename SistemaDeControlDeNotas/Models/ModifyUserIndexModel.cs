using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.Helpers;

namespace SistemaDeControlDeNotas.Models
{
    public class ModifyUserIndexModel
    {
        public string OperationResult { get; set; }
        public NewUserModel ModifyUser { get; set; }
        public SelectUserHelpers UsersList { get; set; }
        public MemberUserModel CurrentUser { get; set; }
    }
}