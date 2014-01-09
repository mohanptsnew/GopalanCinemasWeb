using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Resources;


namespace GopalanCinemasDL.DL
{
    public class CinemaDL
    {
        #region Database Connection / Connection String

        public string _strQuery;
        SqlConnection con;
        string connectionStr = System.Configuration.ConfigurationManager.AppSettings["GCCon"];
        #endregion

        public DataTable GetCinemaList()
        {
            DataTable dtResult;
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                SqlCommand cmd;
                _strQuery = "usp_Gopalan_ViewAllCinemas";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                cmd.Dispose();
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
