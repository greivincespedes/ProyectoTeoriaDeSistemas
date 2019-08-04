using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Models
{
    public class CategoryIndexModel
    {
        public MemberUserModel CurrentUser { get; set; }
        public CategoryModel Category { get; set; }
        public String OperationResult { get; set; }
    }
}