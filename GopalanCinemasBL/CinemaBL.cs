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
    public class CinemaBL
    {
        CinemaDL dd = new CinemaDL();
        #region GetCinemaList
        /// <summary>
        /// Add New Category 
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public  DataTable GetCinemaList()
        {
            try
            {
               // GopalanCinemasDL.DL.CinemaDL.
                return dd.GetCinemaList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
