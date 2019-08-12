using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SistemaDeControlDeNotas.Models;

namespace SistemaDeControlDeNotas.Helpers
{
    public class DatabaseHelpers
    {
        /// <summary>
        /// Metodo para crear el connection string y usarlo para crear
        /// un objeto SqlConnection a partir de dicho connection string
        /// </summary>
        /// <param name="dbModel">Objeto de tipo DB_Model</param>
        public void GetConnection(ref DatabaseModel dbModel)
        {
            try
            {
                dbModel.sCadenaCNX = "SERVER = localhost; database=DB_CONTROL_EVALUACIONES;  USER ID = sa; PASSWORD = N3xx0n";
                dbModel.CNX = new SqlConnection(dbModel.sCadenaCNX);

                dbModel.bBanderaError = false;
                dbModel.sMsjError = string.Empty;
            }
            catch (SqlException ex)
            {
                dbModel.sCadenaCNX = string.Empty;
                dbModel.CNX = null;

                dbModel.bBanderaError = true;
                dbModel.sMsjError = ex.Message.ToString();
            }
        }

        /// <summary>
        /// Metodo para abrir la conexion a la base de datos
        /// </summary>
        /// <param name="dbModel">Objeto de tipo DB_Model</param>
        public void OpenConnection(ref DatabaseModel dbModel)
        {
            try
            {
                if (dbModel.CNX != null)
                {
                    if (dbModel.CNX.State == ConnectionState.Closed)
                    {
                        dbModel.CNX.Open();
                        dbModel.bBanderaError = false;
                        dbModel.sMsjError = string.Empty;
                    }
                    else
                    {
                        dbModel.bBanderaError = true;
                        dbModel.sMsjError = "La conexion ya se encuentra abierta";
                    }
                }
            }
            catch (SqlException ex)
            {
                dbModel.bBanderaError = true;
                dbModel.sMsjError = ex.Message.ToString();
            }
        }

        /// <summary>
        /// Metodo para cerrar la conexion a la base de datos
        /// </summary>
        /// <param name="dbModel">Objeto de tipo DB_Model</param>
        public void CloseConnection(ref DatabaseModel dbModel)
        {
            try
            {
                if (dbModel.CNX != null)
                {
                    if (dbModel.CNX.State == ConnectionState.Open)
                    {
                        dbModel.CNX.Close();
                        dbModel.CNX.Dispose();
                        dbModel.bBanderaError = false;
                        dbModel.sMsjError = string.Empty;
                    }
                    else
                    {
                        dbModel.bBanderaError = true;
                        dbModel.sMsjError = "La conexion ya se encuentra cerrada";
                    }
                }
            }
            catch (SqlException ex)
            {
                dbModel.bBanderaError = true;
                dbModel.sMsjError = ex.Message.ToString();
            }
        }

        /// <summary>
        /// Metodo para Ejecutar los Listar y Filtrar de las Tablas
        /// </summary>
        /// <param name="sNombreTabla">Tabla a la cual se va a hacer la consulta</param>
        /// <param name="sNombreSP">Stored procedure que se llamara</param>
        /// <param name="dbModel">Objeto de tipo DB_Model</param>
        public void ExecuteFill(string sTableName, string sSPName, ref DatabaseModel dbModel)
        {
            try
            {
                GetConnection(ref dbModel);
                OpenConnection(ref dbModel);

                dbModel.DAP = new SqlDataAdapter(sSPName, dbModel.CNX);
                dbModel.DAP.SelectCommand.CommandType = CommandType.StoredProcedure;

                if (!(dbModel.dtParametros is null))
                {
                    foreach (DataRow dr in dbModel.dtParametros.Rows)
                    {
                        switch (dr["TipoDato"].ToString())
                        {
                            case "1":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.Int).Value = dr["Valor"].ToString();
                                }
                                break;
                            case "2":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.Char).Value = dr["Valor"].ToString();
                                }
                                break;
                            case "3":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.NChar).Value = dr["Valor"].ToString();
                                }
                                break;
                            case "4":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.VarChar).Value = dr["Valor"].ToString();
                                }
                                break;
                            case "5":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.NVarChar).Value = dr["Valor"].ToString();
                                }
                                break;
                            case "6":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.DateTime).Value = dr["Valor"].ToString();
                                }
                                break;
                            case "7":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.Bit).Value = dr["Valor"].ToString();
                                }
                                break;
                            case "8":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.Decimal).Value = dr["Valor"].ToString();
                                }
                                break;
                            case "9":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.TinyInt).Value = dr["Valor"].ToString();
                                }
                                break;
                            case "10":
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.SmallInt).Value = dr["Valor"].ToString();
                                }
                                break;
                            default:
                                {
                                    dbModel.DAP.SelectCommand.Parameters.Add(dr["Nombre"].ToString(), SqlDbType.VarChar).Value = dr["Valor"].ToString();
                                }
                                break;
                        }
                    }
                }

                dbModel.DAP.Fill(dbModel.DS, sTableName);

                dbModel.bBanderaError = false;
                dbModel.sMsjError = string.Empty;
            }
            catch (SqlException ex)
            {
                dbModel.bBanderaError = true;
                dbModel.sMsjError = ex.Message.ToString();
            }
            finally
            {
                if (dbModel.CNX.State == ConnectionState.Open)
                {
                    dbModel.CNX.Close();
                    dbModel.CNX.Dispose();
                }

            }
        }

        /// <summary>
        /// Metodo para llenar el DT de parametros
        /// </summary>
        /// <param name="dbModel">DT de parametros</param>
        public void GenerarDataTableParametros(ref DatabaseModel dbModel)
        {
            dbModel.dtParametros = new DataTable("Parametros");

            DataColumn dcNombre = new DataColumn("Nombre");
            DataColumn dcTipoDato = new DataColumn("TipoDato");
            DataColumn dcValor = new DataColumn("Valor");

            dbModel.dtParametros.Columns.Add(dcNombre);
            dbModel.dtParametros.Columns.Add(dcTipoDato);
            dbModel.dtParametros.Columns.Add(dcValor);
        }
    }
}