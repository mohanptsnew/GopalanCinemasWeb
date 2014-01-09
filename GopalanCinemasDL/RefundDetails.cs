using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GopalanCinemasBL;
namespace GopalanCinemasDL
{
    public class RefundDetails
    {
        public RefundDetails()
        {
        }
        public static string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GCCon"].ConnectionString.ToString();
            return connectionString;
        }
        public static List<RefundBean> getRefundDetails()
        {
            SqlCommand myCommand = new SqlCommand("sp_gc_getResendVista", new SqlConnection(GetConnectionString()));
            List<RefundBean> RefundList = new List<RefundBean>();
            try
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Connection.Open();
                SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    RefundBean RefundBean = new RefundBean();
                    RefundBean.FirstName = Convert.ToString(dr["FirstName"]);
                    RefundBean.LastName = Convert.ToString(dr["LastName"]);
                    RefundBean.EmailId = Convert.ToString(dr["Email"]);
                    RefundBean.MobileNo = Convert.ToString(dr["Mobile"]);
                    RefundBean.FilmTitle = Convert.ToString(dr["Film_strTitle"]);
                    RefundBean.CinemaName = Convert.ToString(dr["Cinema_strName"]);
                    RefundList.Add(RefundBean);
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                myCommand.Connection.Close();
            }
            return RefundList;
        }


    }
}
