using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaDeControlDeNotas.Models
{
    public class DatabaseModel
    {
        #region Private Variables

        private string _sCadenaCNX, _sMsjError;
        private bool _bBanderaError;

        #endregion

        #region Getters and Setters

        public string sCadenaCNX
        {
            get { return _sCadenaCNX; }
            set { _sCadenaCNX = value; }
        }

        public string sMsjError
        {
            get { return _sMsjError; }
            set { _sMsjError = value; }
        }

        public bool bBanderaError
        {
            get { return _bBanderaError; }
            set { _bBanderaError = value; }
        }

        #endregion

        #region Objetos Publicos

        public SqlConnection CNX;
        public SqlCommand CMD;
        public SqlDataAdapter DAP;
        public DataSet DS = new DataSet();
        public DataTable DT;
        public DataTable dtParametros;

        #endregion
    }
}