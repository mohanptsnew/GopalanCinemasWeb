using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GopalanCinemasEntities
{
    public partial class hdf_response
    {
        private int _BookingInfoID;
        private string _TrackId;
        private string _PaymentId;
        private string _Result;
        private string _TransactionID;
        private string _AuthCode;
        private string _Reference;
        private bool _DualVerification;
        private bool _TransactionStatus;

        public int BookingInfoID
        {
            get
            {
                return _BookingInfoID;
            }
            set
            {
                _BookingInfoID = value;

            }
        }
        public string TrackId
        {
            get
            {
                return _TrackId;
            }
            set
            {
                _TrackId = value;

            }
        }
        public string PaymentId
        {
            get
            {
                return _PaymentId;
            }
            set
            {
                _PaymentId = value;

            }
        }
        public string Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;

            }
        }
        public string TransactionID
        {
            get
            {
                return _TransactionID;
            }
            set
            {
                _TransactionID = value;

            }
        }
        public string AuthCode
        {
            get
            {
                return _AuthCode;
            }
            set
            {
                _AuthCode = value;

            }
        }
        public string Reference
        {
            get
            {
                return _Reference;
            }
            set
            {
                _Reference = value;

            }
        }
        public bool DualVerification
        {
            get
            {
                return _DualVerification;
            }
            set
            {
                _DualVerification = value;

            }
        }
        public bool TransactionStatus
        {
            get
            {
                return _TransactionStatus;
            }
            set
            {
                _TransactionStatus = value;

            }
        }
        
    }
}
