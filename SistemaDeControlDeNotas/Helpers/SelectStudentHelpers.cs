using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.Models;
using SistemaDeControlDeNotas.Helpers;
using System.Data;

namespace SistemaDeControlDeNotas.Helpers
{
    public class SelectStudentHelpers
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }

        public Dictionary<int, string> Students { get; set; }

        public SelectStudentHelpers(int studentID)
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            Students = new Dictionary<int, string>();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            DataRow dr1 = dbModel.dtParametros.NewRow();
            dr1["Nombre"] = "@grupo";
            dr1["TipoDato"] = "1";
            dr1["Valor"] = studentID;

            sNombreTabla = "T_USUARIO";
            sNombreSP = "spListarEstudiantesPorGrupo";

            dbModel.dtParametros.Rows.Add(dr1);

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                Students.Add(0, "No hay estudiantes disponibles");
            }
            else
            {
                for (int i = 0; i < dbModel.DS.Tables[sNombreTabla].Rows.Count; i++)
                {
                    UserID = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[i][0].ToString());
                    UserEmail = dbModel.DS.Tables[sNombreTabla].Rows[i][1].ToString();
                    Students.Add(UserID, UserEmail);
                }

            }
        }
    }
}