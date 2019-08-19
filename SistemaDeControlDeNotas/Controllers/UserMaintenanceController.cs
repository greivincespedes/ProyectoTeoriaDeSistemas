using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeControlDeNotas.Models;
using SistemaDeControlDeNotas.Helpers;

namespace SistemaDeControlDeNotas.Controllers
{
    public class UserMaintenanceController : Controller
    {
        [HttpGet]
        public ActionResult Index(MemberUserModel user)
        {
            return View(user);
        }

        [HttpGet]
        public ActionResult Help()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Back(MemberUserModel user)
        {
            return View("~/Views/Login/Home.cshtml", user);
        }

        [HttpGet]
        public ActionResult NewUser(MemberUserModel user)
        {
            UserIndexModel userIndexModel = new UserIndexModel();
            userIndexModel.NewUser = new NewUserModel();
            userIndexModel.CurrentUser = user;
            userIndexModel.NewUser.GroupsList = new SelectGroupHelpers();

            return View(userIndexModel);
        }

        [HttpPost]
        public ActionResult NewUser(UserIndexModel userIndexModel)
        {
            if (ModelState.IsValid)
            {
                userIndexModel.NewUser.UserId = 0;
                userIndexModel.NewUser.Status = 1;

                #region Local variables

                string sNombreTabla, sNombreSP, sMsjError = string.Empty;

                DatabaseModel dbModel = new DatabaseModel();
                DatabaseHelpers dbHelper = new DatabaseHelpers();

                #endregion

                dbHelper.GenerarDataTableParametros(ref dbModel);

                DataRow dr1 = dbModel.dtParametros.NewRow();
                dr1["Nombre"] = "@bandera";
                dr1["TipoDato"] = "1";
                dr1["Valor"] = 2;

                DataRow dr2 = dbModel.dtParametros.NewRow();
                dr2["Nombre"] = "@id_usuario";
                dr2["TipoDato"] = "1";
                dr2["Valor"] = userIndexModel.NewUser.UserId;

                DataRow dr3 = dbModel.dtParametros.NewRow();
                dr3["Nombre"] = "@grupo_usuario";
                dr3["TipoDato"] = "1";
                dr3["Valor"] = userIndexModel.NewUser.UserGroup;

                DataRow dr4 = dbModel.dtParametros.NewRow();
                dr4["Nombre"] = "@perfil_usuario";
                dr4["TipoDato"] = "1";
                dr4["Valor"] = userIndexModel.NewUser.UserProfile;

                DataRow dr5 = dbModel.dtParametros.NewRow();
                dr5["Nombre"] = "@nombre_usuario";
                dr5["TipoDato"] = "4";
                dr5["Valor"] = userIndexModel.NewUser.Name;

                DataRow dr6 = dbModel.dtParametros.NewRow();
                dr6["Nombre"] = "@apellido1";
                dr6["TipoDato"] = "4";
                dr6["Valor"] = userIndexModel.NewUser.FirstLastName;

                DataRow dr7 = dbModel.dtParametros.NewRow();
                dr7["Nombre"] = "@apellido2";
                dr7["TipoDato"] = "4";
                dr7["Valor"] = userIndexModel.NewUser.SecondLastName;

                DataRow dr8 = dbModel.dtParametros.NewRow();
                dr8["Nombre"] = "@cedula";
                dr8["TipoDato"] = "4";
                dr8["Valor"] = userIndexModel.NewUser.IdNumber;

                DataRow dr9 = dbModel.dtParametros.NewRow();
                dr9["Nombre"] = "@email";
                dr9["TipoDato"] = "4";
                dr9["Valor"] = userIndexModel.NewUser.Email;

                DataRow dr10 = dbModel.dtParametros.NewRow();
                dr10["Nombre"] = "@usuario";
                dr10["TipoDato"] = "4";
                dr10["Valor"] = userIndexModel.NewUser.User;

                DataRow dr11 = dbModel.dtParametros.NewRow();
                dr11["Nombre"] = "@contrasenna";
                dr11["TipoDato"] = "4";
                dr11["Valor"] = userIndexModel.NewUser.Password;

                DataRow dr12 = dbModel.dtParametros.NewRow();
                dr12["Nombre"] = "@estatus";
                dr12["TipoDato"] = "1";
                dr12["Valor"] = userIndexModel.NewUser.Status;

                dbModel.dtParametros.Rows.Add(dr1);
                dbModel.dtParametros.Rows.Add(dr2);
                dbModel.dtParametros.Rows.Add(dr3);
                dbModel.dtParametros.Rows.Add(dr4);
                dbModel.dtParametros.Rows.Add(dr5);
                dbModel.dtParametros.Rows.Add(dr6);
                dbModel.dtParametros.Rows.Add(dr7);
                dbModel.dtParametros.Rows.Add(dr8);
                dbModel.dtParametros.Rows.Add(dr9);
                dbModel.dtParametros.Rows.Add(dr10);
                dbModel.dtParametros.Rows.Add(dr11);
                dbModel.dtParametros.Rows.Add(dr12);


                sNombreTabla = "T_USUARIO";
                sNombreSP = "spAdministrarUsuario";

                dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

                if (dbModel.sMsjError != string.Empty)
                {
                    userIndexModel.OperationResult = "Se produjo un error al crear el usuario, por favor intentelo de nuevo mas tarde";
                    return View("OperationResult", userIndexModel);
                }
                else
                {
                    userIndexModel.OperationResult = "El usuario se ha creado de manera exitosa";
                    return View("OperationResult", userIndexModel);
                }
            }
            else
            {
                userIndexModel.NewUser.GroupsList = new SelectGroupHelpers();
                return View(userIndexModel);
            }
        }

        [HttpGet]
        public ActionResult SelectUserToModify(MemberUserModel user)
        {
            ModifyUserIndexModel modifyUserIndexModel = new ModifyUserIndexModel();
            modifyUserIndexModel.CurrentUser = user;

            modifyUserIndexModel.UsersList = new SelectUserHelpers();

            ViewBag.HtmlStr = "action=\"ModifyUser\"";

            return View("SelectUser", modifyUserIndexModel);
        }

        [HttpGet]
        public ActionResult ModifyUser(ModifyUserIndexModel modifyUserIndexModel)
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            DataRow dr1 = dbModel.dtParametros.NewRow();
            dr1["Nombre"] = "@idUsuario";
            dr1["TipoDato"] = "10";
            dr1["Valor"] = modifyUserIndexModel.ModifyUser.UserId;

            dbModel.dtParametros.Rows.Add(dr1);

            sNombreTabla = "T_USUARIO";
            sNombreSP = "spListarUsuario";

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                sMsjError = dbModel.sMsjError;

                return View("FailedToCreateUser", modifyUserIndexModel);
            }
            else
            {
                modifyUserIndexModel.ModifyUser.UserId = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[0][0].ToString());
                modifyUserIndexModel.ModifyUser.UserGroup = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[0][1].ToString());
                modifyUserIndexModel.ModifyUser.UserProfile = Convert.ToByte(dbModel.DS.Tables[sNombreTabla].Rows[0][2].ToString());
                modifyUserIndexModel.ModifyUser.Name = dbModel.DS.Tables[sNombreTabla].Rows[0][3].ToString();
                modifyUserIndexModel.ModifyUser.FirstLastName = dbModel.DS.Tables[sNombreTabla].Rows[0][4].ToString();
                modifyUserIndexModel.ModifyUser.SecondLastName = dbModel.DS.Tables[sNombreTabla].Rows[0][5].ToString();
                modifyUserIndexModel.ModifyUser.IdNumber = dbModel.DS.Tables[sNombreTabla].Rows[0][6].ToString();
                modifyUserIndexModel.ModifyUser.Email = dbModel.DS.Tables[sNombreTabla].Rows[0][7].ToString();
                modifyUserIndexModel.ModifyUser.User = dbModel.DS.Tables[sNombreTabla].Rows[0][8].ToString();
                modifyUserIndexModel.ModifyUser.Password = dbModel.DS.Tables[sNombreTabla].Rows[0][9].ToString();
                modifyUserIndexModel.ModifyUser.Status = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[0][10].ToString());

                return View(modifyUserIndexModel);
            }
        }

        [HttpPost]
        public ActionResult EditUser(ModifyUserIndexModel modifyUserIndexModel)
        {
            if (ModelState.IsValid)
            {
                modifyUserIndexModel.ModifyUser.Status = 1;

                #region Local variables

                string sNombreTabla, sNombreSP, sMsjError = string.Empty;

                DatabaseModel dbModel = new DatabaseModel();
                DatabaseHelpers dbViewModel = new DatabaseHelpers();

                #endregion

                dbViewModel.GenerarDataTableParametros(ref dbModel);

                DataRow dr1 = dbModel.dtParametros.NewRow();
                dr1["Nombre"] = "@bandera";
                dr1["TipoDato"] = "1";
                dr1["Valor"] = 3;

                DataRow dr2 = dbModel.dtParametros.NewRow();
                dr2["Nombre"] = "@id_usuario";
                dr2["TipoDato"] = "1";
                dr2["Valor"] = modifyUserIndexModel.ModifyUser.UserId;

                DataRow dr3 = dbModel.dtParametros.NewRow();
                dr3["Nombre"] = "@grupo_usuario";
                dr3["TipoDato"] = "1";
                dr3["Valor"] = modifyUserIndexModel.ModifyUser.UserGroup;

                DataRow dr4 = dbModel.dtParametros.NewRow();
                dr4["Nombre"] = "@perfil_usuario";
                dr4["TipoDato"] = "1";
                dr4["Valor"] = modifyUserIndexModel.ModifyUser.UserProfile;

                DataRow dr5 = dbModel.dtParametros.NewRow();
                dr5["Nombre"] = "@nombre_usuario";
                dr5["TipoDato"] = "4";
                dr5["Valor"] = modifyUserIndexModel.ModifyUser.Name;

                DataRow dr6 = dbModel.dtParametros.NewRow();
                dr6["Nombre"] = "@apellido1";
                dr6["TipoDato"] = "4";
                dr6["Valor"] = modifyUserIndexModel.ModifyUser.FirstLastName;

                DataRow dr7 = dbModel.dtParametros.NewRow();
                dr7["Nombre"] = "@apellido2";
                dr7["TipoDato"] = "4";
                dr7["Valor"] = modifyUserIndexModel.ModifyUser.SecondLastName;

                DataRow dr8 = dbModel.dtParametros.NewRow();
                dr8["Nombre"] = "@cedula";
                dr8["TipoDato"] = "4";
                dr8["Valor"] = modifyUserIndexModel.ModifyUser.IdNumber;

                DataRow dr9 = dbModel.dtParametros.NewRow();
                dr9["Nombre"] = "@email";
                dr9["TipoDato"] = "4";
                dr9["Valor"] = modifyUserIndexModel.ModifyUser.Email;

                DataRow dr10 = dbModel.dtParametros.NewRow();
                dr10["Nombre"] = "@usuario";
                dr10["TipoDato"] = "4";
                dr10["Valor"] = modifyUserIndexModel.ModifyUser.User;

                DataRow dr11 = dbModel.dtParametros.NewRow();
                dr11["Nombre"] = "@contrasenna";
                dr11["TipoDato"] = "4";
                dr11["Valor"] = modifyUserIndexModel.ModifyUser.Password;

                DataRow dr12 = dbModel.dtParametros.NewRow();
                dr12["Nombre"] = "@estatus";
                dr12["TipoDato"] = "1";
                dr12["Valor"] = modifyUserIndexModel.ModifyUser.Status;

                dbModel.dtParametros.Rows.Add(dr1);
                dbModel.dtParametros.Rows.Add(dr2);
                dbModel.dtParametros.Rows.Add(dr3);
                dbModel.dtParametros.Rows.Add(dr4);
                dbModel.dtParametros.Rows.Add(dr5);
                dbModel.dtParametros.Rows.Add(dr6);
                dbModel.dtParametros.Rows.Add(dr7);
                dbModel.dtParametros.Rows.Add(dr8);
                dbModel.dtParametros.Rows.Add(dr9);
                dbModel.dtParametros.Rows.Add(dr10);
                dbModel.dtParametros.Rows.Add(dr11);
                dbModel.dtParametros.Rows.Add(dr12);


                sNombreTabla = "T_USUARIO";
                sNombreSP = "spAdministrarUsuario";

                dbViewModel.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

                if (dbModel.sMsjError != string.Empty)
                {
                    UserIndexModel userIndexModel = new UserIndexModel();
                    userIndexModel.CurrentUser = modifyUserIndexModel.CurrentUser;

                    userIndexModel.OperationResult = "Se produjo un error al modificar el usuario, por favor intentelo de nuevo mas tarde";
                    return View("OperationResult", userIndexModel);
                }
                else
                {
                    UserIndexModel userIndexModel = new UserIndexModel();
                    userIndexModel.CurrentUser = modifyUserIndexModel.CurrentUser;

                    userIndexModel.OperationResult = "El usuario se ha modificado de manera exitosa";
                    return View("OperationResult", userIndexModel);
                }
            }
            else
            {
                return View("ModifyUser", modifyUserIndexModel);
            }
        }

        [HttpGet]
        public ActionResult SelectUserToDelete(MemberUserModel user)
        {
            ModifyUserIndexModel modifyUserIndexModel = new ModifyUserIndexModel();
            modifyUserIndexModel.CurrentUser = user;

            modifyUserIndexModel.UsersList = new SelectUserHelpers();

            ViewBag.HtmlStr = "action=\"DeleteUser\"";

            return View("SelectUser", modifyUserIndexModel);
        }

        [HttpGet]
        public ActionResult DeleteUser(ModifyUserIndexModel modifyUserIndexModel)
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbHelper = new DatabaseHelpers();

            #endregion

            dbHelper.GenerarDataTableParametros(ref dbModel);

            DataRow dr1 = dbModel.dtParametros.NewRow();
            dr1["Nombre"] = "@idUsuario";
            dr1["TipoDato"] = "10";
            dr1["Valor"] = modifyUserIndexModel.ModifyUser.UserId;

            dbModel.dtParametros.Rows.Add(dr1);

            sNombreTabla = "T_USUARIO";
            sNombreSP = "spListarUsuario";

            dbHelper.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                sMsjError = dbModel.sMsjError;

                return View("FailedToCreateUser", modifyUserIndexModel);
            }
            else
            {
                modifyUserIndexModel.ModifyUser.UserId = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[0][0].ToString());
                modifyUserIndexModel.ModifyUser.UserGroup = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[0][1].ToString());
                modifyUserIndexModel.ModifyUser.UserProfile = Convert.ToByte(dbModel.DS.Tables[sNombreTabla].Rows[0][2].ToString());
                modifyUserIndexModel.ModifyUser.Name = dbModel.DS.Tables[sNombreTabla].Rows[0][3].ToString();
                modifyUserIndexModel.ModifyUser.FirstLastName = dbModel.DS.Tables[sNombreTabla].Rows[0][4].ToString();
                modifyUserIndexModel.ModifyUser.SecondLastName = dbModel.DS.Tables[sNombreTabla].Rows[0][5].ToString();
                modifyUserIndexModel.ModifyUser.IdNumber = dbModel.DS.Tables[sNombreTabla].Rows[0][6].ToString();
                modifyUserIndexModel.ModifyUser.Email = dbModel.DS.Tables[sNombreTabla].Rows[0][7].ToString();
                modifyUserIndexModel.ModifyUser.User = dbModel.DS.Tables[sNombreTabla].Rows[0][8].ToString();
                modifyUserIndexModel.ModifyUser.Password = dbModel.DS.Tables[sNombreTabla].Rows[0][9].ToString();
                modifyUserIndexModel.ModifyUser.Status = Convert.ToInt32(dbModel.DS.Tables[sNombreTabla].Rows[0][10].ToString());

                return View(modifyUserIndexModel);
            }
        }

        [HttpPost]
        public ActionResult RemoveUser(ModifyUserIndexModel modifyUserIndexModel)
        {
            #region Local variables

            string sNombreTabla, sNombreSP, sMsjError = string.Empty;

            DatabaseModel dbModel = new DatabaseModel();
            DatabaseHelpers dbViewModel = new DatabaseHelpers();

            #endregion

            dbViewModel.GenerarDataTableParametros(ref dbModel);

            DataRow dr1 = dbModel.dtParametros.NewRow();
            dr1["Nombre"] = "@ID_USUARIO";
            dr1["TipoDato"] = "1";
            dr1["Valor"] = modifyUserIndexModel.ModifyUser.UserId;

            dbModel.dtParametros.Rows.Add(dr1);

            sNombreTabla = "T_USUARIO";
            sNombreSP = "spDisableUser";

            dbViewModel.ExecuteFill(sNombreTabla, sNombreSP, ref dbModel);

            if (dbModel.sMsjError != string.Empty)
            {
                UserIndexModel userIndexModel = new UserIndexModel();
                userIndexModel.CurrentUser = modifyUserIndexModel.CurrentUser;

                userIndexModel.OperationResult = "Se produjo un error al eliminar el usuario, por favor intentelo de nuevo mas tarde";
                return View("OperationResult", userIndexModel);
            }
            else
            {
                UserIndexModel userIndexModel = new UserIndexModel();
                userIndexModel.CurrentUser = modifyUserIndexModel.CurrentUser;

                userIndexModel.OperationResult = "El usuario se ha eliminado de manera exitosa";
                return View("OperationResult", userIndexModel);
            }
        }
    }
}