using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.Helpers;

namespace SistemaDeControlDeNotas.Models
{
    public class ModifyCategoryIndexModel
    {
        public string OperationResult { get; set; }
        public CategoryModel ModifyCategory { get; set; }
        public SelectCategoryHelpers CategoriesList { get; set; }
        public MemberUserModel CurrentUser { get; set; }
    }
}