using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Surya
{
    class DataBaseClass
    {
        OdbcConnection conn;
        public OdbcConnection openconn()
        {
            #region
            string CS = "DSN=bckup; UID=sa; Pwd=1234";
            //string CS = "DSN=raideit; UID=sa; Pwd=1234";
            conn = new OdbcConnection(CS);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    return conn;
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Failed to Connect with Database.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return conn;
            #endregion
        }

    }
}
