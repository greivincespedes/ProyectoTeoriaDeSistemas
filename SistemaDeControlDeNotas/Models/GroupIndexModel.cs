using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Models
{
    public class GroupIndexModel
    {
        public MemberUserModel CurrentUser { get; set; }
        public GroupModel NewGroup { get; set; }
        public String OperationResult { get; set; }
    }
}