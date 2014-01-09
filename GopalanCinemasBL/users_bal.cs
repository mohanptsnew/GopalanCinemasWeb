using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GopalanCinemasEntities;
using GopalanCinemasDL;

namespace GopalanCinemasBL
{
    public partial class users_bal
    {
        DataTable dt;
        users_dal objdal = new users_dal();

        public string InsertUsers(users objent)
        {
            try
            {
                string result = null;
                result = objdal.InsertUsers(objent);
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
