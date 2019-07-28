using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.Models;
using SistemaDeControlDeNotas.Helpers;

namespace SistemaDeControlDeNotas.Models
{
    public class UserIndexModel
    {
        public MemberUserModel CurrentUser { get; set; }
        public NewUserModel NewUser { get; set; }
        public string OperationResult { get; set; }
    }
}