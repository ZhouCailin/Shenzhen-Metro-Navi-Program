using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Trip.Common
{

    public class TripDBException : ApplicationException
    {
        public TripDBException(string info)
            : base(info)
        {

        }
    }

    public class UserDBException : ApplicationException
    {
        public UserDBException(string info)
            : base(info)
        {

        }
    }

}
