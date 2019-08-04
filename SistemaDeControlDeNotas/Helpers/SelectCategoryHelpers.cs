using SistemaDeControlDeNotas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Helpers
{
    public class SelectCategoryHelpers
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public Dictionary<int, string> Categories { get; set; }

        public SelectCategoryHelpers()
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            Categories = new Dictionary<int, string>();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            sNombreTabla = "T_CATEGORIA";
            sNombreSP = "spListCategoryName";

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                Categories.Add(0, "No hay Categorias disponibles");
            }
            else if (dbModel.DS.Tables[sNombreTabla].Rows.Count == 0)
            {
                Categories.Add(0, "No hay Categorias disponibles");
            }
            else
            {
                for (int i = 0; i < dbModel.DS.Tables[sNombreTabla].Rows.Count; i++)
                {
                    CategoryID = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[i][0].ToString());
                    CategoryName = dbModel.DS.Tables[sNombreTabla].Rows[i][1].ToString();
                    Categories.Add(CategoryID, CategoryName);
                }

            }
        }
    }
}