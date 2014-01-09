#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

///<summary>
/// Name          : Karthik T
/// Created Date  : June 6,2011
/// Description   : Data Definitions
/// Modified Date : 
///</summary>
///


namespace GopalanCinemasDL.DBUtils
{
    public class DataDefinition
    {

        #region Cinemas

        public enum Cinemas
        {
            usp_Gopalan_ViewAllCinemas
        }

        #endregion
        #region Movies

        public enum Movies
        {
            usp_Gopalan_ViewAllMoviesByCinemaID,
            usp_getShowDateByCinemaIDMovieID,
            usp_getShowTimeByCinemaIDMovieIDDate,
            usp_getLicenseType,
            usp_getClassByCinemaIDMovieIDSessionID,
            ComingSoonMoviesList,
            NowShowingMoviesList,
            usp_getShowDateByCinemaID,
            usp_getAllMoviesByCinemaIDDate,
            usp_getLatestNews,
            usp_Gopalan_InsertNewsletter,
            usp_Gopalan_ViewAllCinemasByMovieID,
            getFilmDetailsByFilmID
        }

        #endregion
    }
}
