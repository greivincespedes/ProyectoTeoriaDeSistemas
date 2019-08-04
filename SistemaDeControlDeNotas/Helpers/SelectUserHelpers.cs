using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.Models;
using SistemaDeControlDeNotas.Helpers;

namespace SistemaDeControlDeNotas.Helpers
{
    public class SelectUserHelpers
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }

        public Dictionary<int, string> Users { get; set; }

        public SelectUserHelpers()
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            Users = new Dictionary<int, string>();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            sNombreTabla = "T_USUARIO";
            sNombreSP = "spListEmailUsers";

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                Users.Add(0, "No hay usuarios disponibles");
            }
            else
            {

                for (int i = 0; i < dbModel.DS.Tables[sNombreTabla].Rows.Count; i++)
                {
                    UserID = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[i][0].ToString());
                    UserEmail = dbModel.DS.Tables[sNombreTabla].Rows[i][1].ToString();
                    Users.Add(UserID, UserEmail);
                }

            }
        }
    }
}