using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GopalanCinemasEntities;

namespace GopalanCinemasDL
{
    public class booking_dal
    {
        string connectionStr = System.Configuration.ConfigurationManager.AppSettings["GCCon"];
        SqlConnection con;
        string _strQuery;
        SqlCommand cmd;
        DataTable dt;
        public int insertBookingDetails(booking objEntities)
        {
            try
            {
                int retval = 0;
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "sp_InsertBookingDetails";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@cinemaID", objEntities.Booking_CinemaID) ,
                new SqlParameter("@sessionLngID",objEntities.Booking_SessionID),
                new SqlParameter("@sessionMovieID",objEntities.Booking_MovieID),
                new SqlParameter("@stakeHolderID",objEntities.UserID),
                new SqlParameter("@transID",objEntities.TransID),
                new SqlParameter("@sessionBookingID",objEntities.BookingID),
                new SqlParameter("@bookingdate",objEntities.Booking_BookingDate),
                new SqlParameter("@sessionShowDate",objEntities.Booking_ShowDate),
                new SqlParameter("@sessionShowTime",objEntities.ShowTime),
                new SqlParameter("@sessionShowClass",objEntities.ShowClass),
                new SqlParameter("@noofseats",objEntities.NoofSeat),
                new SqlParameter("@sessionSeatDetails",objEntities.SeatDetails),
                new SqlParameter("@sessionFoodTotal",objEntities.FoodTotal),
                new SqlParameter("@sessionTicketAmount",objEntities.TicketAmount),
                new SqlParameter("@uip",objEntities.UIP),
                new SqlParameter("@cityid",objEntities.intCityID),
                new SqlParameter("@screenname",objEntities.ScreenName),
                new SqlParameter("@bookid",objEntities.bookid),
                new SqlParameter("@bookingdetailid",objEntities.BookingDetailID)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.Parameters["@bookingdetailid"].Direction = ParameterDirection.InputOutput;
                cmd.ExecuteNonQuery();
                retval = int.Parse(cmd.Parameters["@bookingdetailid"].Value.ToString());
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

        public void updatePGStatusbybookingdetailid(bool pgstatus, int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_update_pgstatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@paymentStatus",pgstatus),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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
        public void updatePGRecheckStatusbybookingdetailid(int pgstatus, int bdid, string msg)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_update_pgrecheck_status", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@paymentStatus",pgstatus),
                new SqlParameter("@msg",msg),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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
        public string UpdateOverAllAmountByBookingDetailID(decimal amt, int bid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_update_overallamount";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param1 = new SqlParameter[] { 
                new SqlParameter("@amt",amt),
                new SqlParameter("@bid",bid)
               
                };
                foreach (SqlParameter para in param1)
                {
                    cmd.Parameters.Add(para);
                }
                SqlDataReader sdr = cmd.ExecuteReader();
                return "updates";
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
        public DataTable getBookingDetailByBDID(int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getBookingDetailByBDID";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { new SqlParameter("@bdid", bdid) };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                return dt;
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
        public void insertResponseDetails(hdf_response objEntities)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_hdfc_response";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@BookingInfoID", objEntities.BookingInfoID) ,
                new SqlParameter("@TrackId",objEntities.TrackId),
                new SqlParameter("@PaymentId",objEntities.PaymentId),
                new SqlParameter("@Result",objEntities.Result),
                new SqlParameter("@TransactionID",objEntities.TransactionID),
                new SqlParameter("@AuthCode",objEntities.AuthCode),
                new SqlParameter("@Reference",objEntities.Reference),
                new SqlParameter("@TransactionStatus",objEntities.TransactionStatus),
                new SqlParameter("@DualVerification",objEntities.TransactionStatus)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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
        public void updateErrorStatusbybookingdetailid(string errorstatus, int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_update_errorstatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@errorStatus",errorstatus),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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

        public void updateRefundStatus(int refundstatus, string paymentid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_UpdateRefundStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@RefundStatus",refundstatus),
                new SqlParameter("@BookingInfoId",paymentid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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
        public void updateTxnStatusbybookingdetailid(bool pgstatus, int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_update_transactionstatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@transactionStatus",pgstatus),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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
        public void updateVistaStatusbybookingdetailid(bool vistastatus, int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_update_vistastatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@VistaStatus",vistastatus),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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
        public void updateSMSStatusbybookingdetailid(bool smsStatus, int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_updateSMSStatusbybdid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@smsStatus",smsStatus),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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

        public void updateMAILStatusbybookingdetailid(bool mailStatus, int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_updatemailStatusbybdid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@mailStatus",mailStatus),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();

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
        public DataTable getRefundDetails(string strfdate, string strtdate)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_RefundDetails";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { new SqlParameter("@fdate", strfdate), new SqlParameter("@tdate", strtdate) };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                return dt;
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

        public DataTable getRefundDetailsAll()
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_RefundDetailsAll";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                return dt;
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

        public DataTable getRefundedDetails()
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_RefundRecheckDetails";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                return dt;
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

        public void updateRefundStatusbybookingdetailid(bool refundstatus, int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_update_refundstatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@RefundStatus",refundstatus),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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
        public DataTable getBDIDbyfullbookingid(string fullid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getbookingdetailidbyfullid";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { new SqlParameter("@fullid", fullid) };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                return dt;
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
        public DataTable getPaymentData(string payid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getResponseDetailsbypayid";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { new SqlParameter("@payid", payid) };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                return dt;
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
        public DataTable getTransactionDetails(string strfdate, string strtdate,int pgstatus,int vistastatus,int transactionstatus)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_TransactionDetails";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                    new SqlParameter("@fdate", strfdate), 
                    new SqlParameter("@tdate", strtdate), 
                      new SqlParameter("@pg", pgstatus),
                        new SqlParameter("@vista", vistastatus), 
                          new SqlParameter("@trans", transactionstatus) 
                
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                return dt;
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
        public void updatePayIdbybookingdetailid(string payid, int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_update_payid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@payid",payid),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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
        public DataTable getBDIDbypaytrackid(string payid, string trackid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getbookingdetailidbypaytrackid";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { new SqlParameter("@payid", payid), new SqlParameter("@trackid", trackid) };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                return dt;
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
        public void updatekioskbybookingdetailid(string kiosk, int bdid)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                cmd = new SqlCommand("usp_update_kiosk", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@kioskid",kiosk),
                new SqlParameter("@BookingID",bdid)
                };
                foreach (SqlParameter para in param)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
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
        public DataTable getRecheckPaymentDetails()
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_gc_get_recheck_payment";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                return dt;
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
