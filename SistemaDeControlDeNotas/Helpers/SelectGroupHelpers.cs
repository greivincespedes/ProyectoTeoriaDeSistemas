using SistemaDeControlDeNotas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Helpers
{
    public class SelectGroupHelpers
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }

        public Dictionary<int, string> Groups { get; set; }

        public SelectGroupHelpers()
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            Groups = new Dictionary<int, string>();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            sNombreTabla = "T_GRUPO";
            sNombreSP = "spListarGruposDisponibles";

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                Groups.Add(0, "No hay grupos disponibles");
            }
            else if (dbModel.DS.Tables[sNombreTabla].Rows.Count == 0)
            {
                Groups.Add(0, "No hay grupos disponibles");
            }
            else
            {
                for (int i = 0; i < dbModel.DS.Tables[sNombreTabla].Rows.Count; i++)
                {
                    GroupID = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[i][0].ToString());
                    GroupName = dbModel.DS.Tables[sNombreTabla].Rows[i][1].ToString();
                    Groups.Add(GroupID, GroupName);
                }

            }
        }
    }
}