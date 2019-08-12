using SistemaDeControlDeNotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Helpers
{
    public class SelectTaskHelpers
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }

        public Dictionary<int, string> Tasks { get; set; }

        public SelectTaskHelpers(int group)
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            Tasks = new Dictionary<int, string>();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            DataRow dr1 = dbModel.dtParametros.NewRow();
            dr1["Nombre"] = "@estudiante";
            dr1["TipoDato"] = "10";
            dr1["Valor"] = group;

            sNombreTabla = "T_TAREA";
            sNombreSP = "spListarTareasPorEstudiante";

            dbModel.dtParametros.Rows.Add(dr1);

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                Tasks.Add(0, "No hay tareas disponibles");
            }
            else if (dbModel.DS.Tables[sNombreTabla].Rows.Count == 0)
            {
                Tasks.Add(0, "No hay tareas disponibles");
            }
            else
            {
                for (int i = 0; i < dbModel.DS.Tables[sNombreTabla].Rows.Count; i++)
                {
                    TaskID = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[i][0].ToString());
                    TaskName = dbModel.DS.Tables[sNombreTabla].Rows[i][1].ToString();
                    Tasks.Add(TaskID, TaskName);
                }

            }
        }
    }
}