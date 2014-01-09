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
    public partial class MovieDL
    {
        #region Constructor

        public MovieDL()
        {
        }

        #endregion    

        #region Database Connection / Connection String

        //private static string ConStr =  ConfigurationManager.ConnectionStrings["GCCon"].ConnectionString;
        //private static SqlConnection sqlCon;
        public string _strQuery;
        SqlConnection con;
        string connectionStr = System.Configuration.ConfigurationManager.AppSettings["GCCon"];
        SqlCommand cmd;
        DataTable dtResult;

        #endregion

        #region GetMovieListByCinemaID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetMovieList(string CinemaCode)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_Gopalan_ViewAllMoviesByCinemaID";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cinemaid", CinemaCode));
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetShowDateListByCinemaIDMovieID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetShowDateList(string CinemaCode, string MovieID, int Seats)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getShowDateByCinemaIDMovieID";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cinemaid", CinemaCode));
                cmd.Parameters.Add(new SqlParameter("@movieid", MovieID));
                cmd.Parameters.Add(new SqlParameter("@seats", Seats));
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetShowTimeListByCinemaIDMovieIDAndDate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetShowTimeList(string CinemaCode, string MovieID, int Seats, DateTime dateid)
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getShowTimeByCinemaIDMovieIDDate";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cinemaid", CinemaCode));
                cmd.Parameters.Add(new SqlParameter("@movieid", MovieID));
                cmd.Parameters.Add(new SqlParameter("@seats", Seats));
                cmd.Parameters.Add(new SqlParameter("@dateid", dateid));
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
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region getLicenseType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable getLicenseType()
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getLicenseType";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetClassListByCinemaIDMovieIDSessionId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetClassList(string CinemaCode, string MovieID, string SessionID)
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getClassByCinemaIDMovieIDSessionID";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cinemaid", CinemaCode));
                cmd.Parameters.Add(new SqlParameter("@movieid", MovieID));
                cmd.Parameters.Add(new SqlParameter("@sessionid", SessionID));
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetNowShowingMovies
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetNowShowingMoviesList()
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "NowShowingMoviesList";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetComingSoonMovies
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetComingSoonMoviesList()
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "ComingSoonMoviesList";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetShowDateListByCinemaID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetShowDateListByCinema(string CinemaCode, int Seats)
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getShowDateByCinemaID";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cinemaid", CinemaCode));
                cmd.Parameters.Add(new SqlParameter("@seats", Seats));
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetShowDateListByCinemaID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetAllMoviesByCinemaIDDate(string CinemaCode, int Seats, DateTime dtDate)
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getAllMoviesByCinemaIDDate";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cinemaid", CinemaCode));
                cmd.Parameters.Add(new SqlParameter("@dateid", dtDate));
                cmd.Parameters.Add(new SqlParameter("@seats", Seats));
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetLatestNews
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetLatestNewsList(string cdn)
        {
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_getLatestNews";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cdn", cdn));
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetShowDateListByCinemaID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable InsertNewsletter(string EmailID)
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_Gopalan_InsertNewsletter";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmailID", EmailID));
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetShowDateListByCinemaID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetChinemaByFilmID(string FilmID)
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "usp_Gopalan_ViewAllCinemasByMovieID";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FilmID", FilmID));
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion

        #region GetFilmDetailsByFilmID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmID"></param>
        /// <returns></returns>

        public DataTable GetFilmDetailsByFilmID(string FilmID)
        {
            //string Condition;
            //Condition = "ID=" + "'" + prmID + "'" + " AND IsActive='1'";
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                _strQuery = "getFilmDetailsByFilmID";
                cmd = new SqlCommand(_strQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@filmcode", FilmID));
                SqlDataReader sdr = cmd.ExecuteReader();
                dtResult = new DataTable();
                dtResult.Load(sdr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        #endregion
    }
}
