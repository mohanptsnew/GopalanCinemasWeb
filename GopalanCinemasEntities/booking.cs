using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GopalanCinemasEntities
{
    public partial class booking
    {
        private string _strCity;
        private string _strCinemaID;
        private string _strMovieID;
        private string _strShowdate;
        private long _lngSessionID;
        private string _strPGroup;
        private string _strLicense;
        private string _strBFeeCode;
        private string _strTTypeCode;
        private string _strTransID;
        private string _strBookingID;
        private string _strBookingDate;
        private string _strShowtime;
        private string _strShowClass;
        private string _strUserID;
        private int _intSeats;
        private string _strSeatDetails;
        private string _strBookingFee;
        private string _strFoodtotal;
        private string _strTicketAmount;
        private string _strIP;
        private string _strNameOnCard;
        private string _strCardNo;
        private int _BookingDetailID;
        private string _AreaCat_strCode;
        private int _intCityID;
        private decimal _DiscountAmount;
        private string _ScreenName;
        private decimal _OverAllAmount;
        private string _cardtype;
        private string _pageurl;
        private string _ccavenue;
        private string _fcardno;
        private string _nettype;
        private string _bank;
        private string _cityname;
        private int _flag;
        private long _bookid;
        // List of Property  


       


        public long bookid
        {
            get
            {
                return _bookid;
            }
            set
            {
                _bookid = value;

            }
        }
      
        public int flag
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;

            }
        }
       

        

        public string nettype
        {
            get
            {
                return _nettype;
            }
            set
            {
                _nettype = value;

            }
        }

        public string bank
        {
            get
            {
                return _bank;
            }
            set
            {
                _bank = value;

            }
        }

        public string cityname
        {
            get
            {
                return _cityname;
            }
            set
            {
                _cityname = value;

            }
        }


   

        public string fcardno
        {
            get
            {
                return _fcardno;
            }
            set
            {
                _fcardno = value;

            }
        }



     


       
        public string ccavenue
        {
            get
            {
                return _ccavenue;
            }
            set
            {
                _ccavenue = value;

            }
        }
        public string pageurl
        {
            get
            {
                return _pageurl;
            }
            set
            {
                _pageurl = value;

            }
        }
        public string cardtype
        {
            get
            {
                return _cardtype;
            }
            set
            {
                _cardtype = value;

            }
        }

      
   
        public decimal OverAllAmount
        {
            get
            {
                return _OverAllAmount;
            }
            set
            {
                _OverAllAmount = value;

            }
        }
        public string ScreenName
        {
            get
            {
                return _ScreenName;
            }
            set
            {
                _ScreenName = value;

            }
        }
       
    
        public decimal DiscountAmount
        {
            get
            {
                return _DiscountAmount;
            }
            set
            {
                _DiscountAmount = value;

            }
        }
        public int intCityID
        {
            get
            {
                return _intCityID;
            }
            set
            {
                _intCityID = value;

            }
        }

        public string Booking_City
        {
            get
            {
                return _strCity;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("City cannot be empty");
                }
                else
                {
                    _strCity = value;
                }
            }
        }
        public string Booking_CinemaID
        {
            get
            {
                return _strCinemaID;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("Cinema cannot be empty");
                }
                else
                {
                    _strCinemaID = value;
                }
            }
        }


        public string Booking_MovieID
        {
            get
            {
                return _strMovieID;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("Movie cannot be empty");
                }
                else
                {
                    _strMovieID = value;
                }
            }
        }

        public DateTime Booking_ShowDate
        {
            get
            {
                return DateTime.Parse(_strShowdate);
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("Showdate cannot be empty");
                }
                else
                {
                    //if (commonfunctions.IsDate(_strShowdate))
                    //{
                    _strShowdate = value.ToString();
                    //}
                    //else
                    //{
                    //    throw new Exception("Invalid showdate");
                    //}
                }
            }
        }

        public long Booking_SessionID
        {
            get
            {
                return _lngSessionID;
            }
            set
            {
                if (value.ToString() == null)
                {
                    throw new Exception("SessionID cannot be empty");
                }
                else
                {
                    _lngSessionID = value;
                }
            }
        }

        public string Booking_PGroupID
        {
            get
            {
                return _strPGroup;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("PGroup cannot be empty");
                }
                else
                {
                    _strPGroup = value;
                }
            }
        }
        public string Booking_License
        {
            get
            {
                return _strLicense;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("License cannot be empty");
                }
                else
                {
                    _strLicense = value;
                }
            }
        }
        public string Booking_BFeeCode
        {
            get
            {
                return _strBFeeCode;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("BookingFee code cannot be empty");
                }
                else
                {
                    _strBFeeCode = value;
                }
            }
        }
        public string Booking_TTypeCode
        {
            get
            {
                return _strTTypeCode;
            }
            set
            {
                //if (value == null || value == "")
                //{
                //    throw new Exception("TType code cannot be empty");
                //}
                //else
                //{
                _strTTypeCode = value;
                //}
            }
        }
        public string TransID
        {
            get
            {
                return _strTransID;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("TransID cannot be empty");
                }
                else
                {
                    _strTransID = value;
                }
            }
        }
        public string BookingID
        {
            get
            {
                return _strBookingID;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("BookingIDID cannot be empty");
                }
                else
                {
                    _strBookingID = value;
                }
            }
        }
        public DateTime Booking_BookingDate
        {
            get
            {
                return DateTime.Parse(_strBookingDate);
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("Bookingdate cannot be empty");
                }
                else
                {
                    //if (commonfunctions.IsDate(_strShowdate))
                    //{
                    _strBookingDate = value.ToString();
                    //}
                    //else
                    //{
                    //    throw new Exception("Invalid showdate");
                    //}
                }
            }
        }
        public string ShowTime
        {
            get
            {
                return _strShowtime;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("Showtime cannot be empty");
                }
                else
                {
                    _strShowtime = value;
                }
            }
        }
        public string ShowClass
        {
            get
            {
                return _strShowClass;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("Show class cannot be empty");
                }
                else
                {
                    _strShowClass = value;
                }
            }
        }
        public string UserID
        {
            get
            {
                return _strUserID;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("UserID cannot be empty");
                }
                else
                {
                    _strUserID = value;
                }
            }
        }
        public int NoofSeat
        {
            get
            {
                return _intSeats;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("No of seat cannot be empty");
                }
                else
                {
                    _intSeats = value;
                }
            }
        }
        public string SeatDetails
        {
            get
            {
                return _strSeatDetails;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("No of seat cannot be empty");
                }
                else
                {
                    _strSeatDetails = value;
                }
            }
        }
        public string FoodTotal
        {
            get
            {
                return _strFoodtotal;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("Foodtotal cannot be empty");
                }
                else
                {
                    _strFoodtotal = value;
                }
            }
        }
        public string BookingFee
        {
            get
            {
                return _strBookingFee;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("Bookingfee cannot be empty");
                }
                else
                {
                    _strBookingFee = value;
                }
            }
        }
        public string TicketAmount
        {
            get
            {
                return _strTicketAmount;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("TicketAmount cannot be empty");
                }
                else
                {
                    _strTicketAmount = value;
                }
            }
        }
        public string UIP
        {
            get
            {
                return _strIP;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("IP cannot be empty");
                }
                else
                {
                    _strIP = value;
                }
            }
        }
        public string Booking_NameOnCard
        {
            get
            {
                return _strNameOnCard;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("NameOnCard cannot be empty");
                }
                else
                {
                    _strNameOnCard = value;
                }
            }
        }
        public string Booking_CardNo
        {
            get
            {
                return _strCardNo;
            }
            set
            {
                _strCardNo = value;
            }
        }
        public int BookingDetailID
        {
            get
            {
                return _BookingDetailID;
            }
            set
            {
                _BookingDetailID = value;
            }
        }

        public string AreaCat_strCode
        {
            get
            {
                return _AreaCat_strCode;
            }
            set
            {
                _AreaCat_strCode = value;
            }
        }
    }
}
