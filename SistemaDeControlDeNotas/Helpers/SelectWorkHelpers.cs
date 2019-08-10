using SistemaDeControlDeNotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Helpers
{
    public class SelectWorkHelpers
    {
        public int WorkID { get; set; }
        public string WorkName { get; set; }

        public Dictionary<int, string> Works { get; set; }

        public SelectWorkHelpers(int group)
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            Works = new Dictionary<int, string>();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            DataRow dr1 = dbModel.dtParametros.NewRow();
            dr1["Nombre"] = "@group";
            dr1["TipoDato"] = "1";
            dr1["Valor"] = group;

            sNombreTabla = "T_TRABAJO";
            sNombreSP = "spListarTrabajosDisponibles";

            dbModel.dtParametros.Rows.Add(dr1);

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                Works.Add(0, "No hay trabajos disponibles");
            }
            else if (dbModel.DS.Tables[sNombreTabla].Rows.Count == 0)
            {
                Works.Add(0, "No hay trabajos disponibles");
            }
            else
            {
                for (int i = 0; i < dbModel.DS.Tables[sNombreTabla].Rows.Count; i++)
                {
                    WorkID = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[i][0].ToString());
                    WorkName = dbModel.DS.Tables[sNombreTabla].Rows[i][1].ToString();
                    Works.Add(WorkID, WorkName);
                }

            }
        }
    }
}