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
    public class booking_bal
    {
        DataTable dt;
        booking_dal objBooking = new booking_dal();

        public int insertBookingDetails(booking objEntities)
        {
            try
            {
                int retval;
                retval = objBooking.insertBookingDetails(objEntities);
                return retval;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updatePGStatusbybookingdetailid(bool pgstatus, int bdid)
        {
            try
            {
                objBooking.updatePGStatusbybookingdetailid(pgstatus, bdid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UpdateOverAllAmountByBookingDetailID(decimal amt, int bid)
        {
            try
            {
                string result;

                result = objBooking.UpdateOverAllAmountByBookingDetailID(amt, bid);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getBookingDetailByBDID(int bdid)
        {
            try
            {
                dt = new DataTable();
                dt = objBooking.getBookingDetailByBDID(bdid);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void insertResponseDetails(hdf_response objEntities)
        {
            try
            {
                objBooking.insertResponseDetails(objEntities);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updateErrorStatusbybookingdetailid(string errorstatus, int bdid)
        {
            try
            {
                objBooking.updateErrorStatusbybookingdetailid(errorstatus, bdid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updateTxnStatusbybookingdetailid(bool pgstatus, int bdid)
        {
            try
            {
                objBooking.updateTxnStatusbybookingdetailid(pgstatus, bdid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updateVistaStatusbybookingdetailid(bool vistastatus, int bdid)
        {
            try
            {
                objBooking.updateVistaStatusbybookingdetailid(vistastatus, bdid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updateSMSStatusbybookingdetailid(bool smsStatus, int bdid)
        {
            try
            {
                objBooking.updateSMSStatusbybookingdetailid(smsStatus, bdid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateMAILStatusbybookingdetailid(bool mailStatus, int bdid)
        {
            try
            {
                objBooking.updateMAILStatusbybookingdetailid(mailStatus, bdid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updateRefundStatus(int refundstatus, string paymentid)
        {
            try
            {
                objBooking.updateRefundStatus(refundstatus, paymentid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getRefundDetails(string strfdate, string strtdate)
        {
            try
            {
                dt = new DataTable();
                dt = objBooking.getRefundDetails(strfdate, strtdate);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable getRefundDetailsAll()
        {
            try
            {
                dt = new DataTable();
                dt = objBooking.getRefundDetailsAll();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable getRefundedDetails()
        {
            try
            {
                dt = new DataTable();
                dt = objBooking.getRefundedDetails();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateRefundStatusbybookingdetailid(bool refundstatus, int bdid)
        {
            try
            {
                objBooking.updateRefundStatusbybookingdetailid(refundstatus, bdid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getBDIDbyfullbookingid(string fullid)
        {
            try
            {
                dt = new DataTable();
                dt = objBooking.getBDIDbyfullbookingid(fullid);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getPaymentData(string payid)
        {
            try
            {
                dt = new DataTable();
                dt = objBooking.getPaymentData(payid);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getTransactionDetails(string strfdate, string strtdate, int pgstatus, int vistastatus, int transactionstatus)
        {
            try
            {
                dt = new DataTable();
                dt = objBooking.getTransactionDetails(strfdate, strtdate,pgstatus,vistastatus,transactionstatus);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updatePayIdbybookingdetailid(string payid, int bdid)
        {
            try
            {
                objBooking.updatePayIdbybookingdetailid(payid, bdid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getBDIDbypaytrackid(string payid, string trackid)
        {
            try
            {
                dt = new DataTable();
                dt = objBooking.getBDIDbypaytrackid(payid, trackid);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updatekioskbybookingdetailid(string kiosk, int bdid)
        {
            try
            {
                objBooking.updatekioskbybookingdetailid(kiosk, bdid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getRecheckPaymentDetails()
        {
            try
            {
                dt = new DataTable();
                dt = objBooking.getRecheckPaymentDetails();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updatePGRecheckStatusbybookingdetailid(int pgstatus, int bdid, string msg)
        {
            try
            {
                objBooking.updatePGRecheckStatusbybookingdetailid(pgstatus, bdid, msg);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
