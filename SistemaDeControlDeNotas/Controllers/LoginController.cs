using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeControlDeNotas.Models;
using SistemaDeControlDeNotas.Helpers;
using System.Data;

namespace SistemaDeControlDeNotas.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberUserModel user)
        {
            if (ModelState.IsValid)
            {
                #region Local variables

                string sNombreTabla, sNombreSP, sMsjError = string.Empty, sPass = user.Password;

                DatabaseModel dbModel = new DatabaseModel();
                DatabaseHelpers dbHelper = new DatabaseHelpers();
                EncryptionHelpers encryption = new EncryptionHelpers();
                #endregion

                dbHelper.GenerarDataTableParametros(ref dbModel);

                DataRow dr1 = dbModel.dtParametros.NewRow();
                dr1["Nombre"] = "@username";
                dr1["TipoDato"] = "4";
                dr1["Valor"] = user.Email;

                DataRow dr2 = dbModel.dtParametros.NewRow();
                dr2["Nombre"] = "@passwrod";
                dr2["TipoDato"] = "4";
                dr2["Valor"] = user.Password;

                dbModel.dtParametros.Rows.Add(dr1);
                dbModel.dtParametros.Rows.Add(dr2);

                sNombreTabla = "T_USUARIO";
                sNombreSP = "spAutenticarUsuario";

                dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

                encryption.EncryptString(ref sPass);

                user.Password = sPass;

                if (dbModel.sMsjError != string.Empty)
                {
                    sMsjError = dbModel.sMsjError;
                    user.UserProfile = 0;
                    return View();
                }
                else
                {
                    user.UserProfile = Convert.ToByte(dbModel.DS.Tables[sNombreTabla].Rows[0][0].ToString());

                    if (user.UserProfile == 0)
                    {
                        return View();
                    }
                    else
                    {
                        return View("Home", user);
                    }
                }
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public ActionResult PasswordRecovery()
        {
            return View();
        }
    }
}