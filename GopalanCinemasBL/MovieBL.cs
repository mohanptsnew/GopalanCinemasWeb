using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using GopalanCinemasDL;
using GopalanCinemasDL.DL;
using GopalanCinemasEntities;

namespace GopalanCinemasBL
{
    public partial class MovieBL
    {
        #region Constructor
        string sqlquery = null;
        public MovieBL()
        {
        }
    
        #endregion

        #region GetMovieList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        MovieDL mdl = new MovieDL();
        public  DataTable GetMovieList(string CinemaCode)
        {
            try
            {
                return mdl.GetMovieList(CinemaCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetShowDateList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetShowDateList(string CinemaCode, string MovieID, int Seats)
        {
            try
            {
                return mdl.GetShowDateList(CinemaCode, MovieID, Seats);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetShowTimeList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetShowTimeList(string CinemaCode, string MovieID, int Seats, DateTime dateid)
        {
            try
            {
                return mdl.GetShowTimeList(CinemaCode, MovieID, Seats, dateid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetLicenseType
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetLicenseType()
        {
            try
            {
                return mdl.getLicenseType();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetClassList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetClassList(string CinemaCode, string MovieID, string SessionID)
        {
            try
            {
                return mdl.GetClassList(CinemaCode, MovieID, SessionID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetNowShowingList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetNowShowingMoviesList()
        {
            try
            {
                return mdl.GetNowShowingMoviesList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetComingSoonMoviesList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetComingSoonMoviesList()
        {
            try
            {
                return mdl.GetComingSoonMoviesList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetShowDateList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetShowDateByCinemaList(string CinemaCode, int Seats)
        {
            try
            {
                return mdl.GetShowDateListByCinema(CinemaCode, Seats);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetAllMoviesByCinemaIDDateList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetAllMoviesByCinemaIDDate(string CinemaCode, int Seats, DateTime dtDate)
        {
            try
            {
                return mdl.GetAllMoviesByCinemaIDDate(CinemaCode, Seats, dtDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetLatestNewsList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetLatestNewsList(string cdn)
        {
            try
            {
                return mdl.GetLatestNewsList(cdn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region InsertNewsletter
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable InsertNewsletter(string EmailID)
        {
            try
            {
                return mdl.InsertNewsletter(EmailID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetCinemaListByFilmID
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetChinemaByFilmID(string FilmID)
        {
            try
            {
                return mdl.GetChinemaByFilmID(FilmID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetFilmDetailsByFilmID
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataTable GetFilmDetailsByFilmID(string FilmID)
        {
            try
            {
                return mdl.GetFilmDetailsByFilmID(FilmID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
