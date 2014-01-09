using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using GopalanCinemasEntities;

namespace GopalanCinemasDL
{
    public partial class users_dal
    {
        public string connectionStr = System.Configuration.ConfigurationManager.AppSettings["GCCon"];
        SqlConnection con;
        string _strQuery;
        SqlCommand cmd;
        DataTable dt;
        string sqlquery = null;

        public string InsertUsers(users objenti)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_insert_users", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@uname",objenti.LastName),
                new SqlParameter("@emailid",objenti.Email),
                new SqlParameter("@mobile",objenti.MobPhone),
                new SqlParameter("@userid",objenti.StakeholderID)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.Parameters["@userid"].Direction = ParameterDirection.InputOutput;
                cmd.ExecuteNonQuery();
                string retval = cmd.Parameters["@userid"].Value.ToString();
                return retval;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
    }
}
