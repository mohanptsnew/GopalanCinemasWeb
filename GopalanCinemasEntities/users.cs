using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GopalanCinemasEntities
{
    public partial class users
    {
        private int _StakeholderID;
        private string _Email;
        private string _Password;
        private string _FirstName;
        private string _LastName;
        private string _MobPhone;
        private int _City;

        public int StakeholderID
        {
            get
            {
                return _StakeholderID;

            }
            set
            {
                _StakeholderID = value;
            }
        }
        public string Email
        {
            get
            {
                return _Email;

            }
            set
            {
                _Email = value;
            }

        }
        public string Password
        {
            get
            {
                return _Password;

            }
            set
            {
                _Password = value;
            }
        }
        public string FirstName
        {
            get
            {
                return _FirstName;

            }
            set
            {
                _FirstName = value;
            }

        }
        public string LastName
        {
            get
            {
                return _LastName;

            }
            set
            {
                _LastName = value;
            }

        }
        public string MobPhone
        {
            get
            {
                return _MobPhone;
            }
            set
            {
                _MobPhone = value;
            }
        }
        public int City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
            }
        }
    }
}
