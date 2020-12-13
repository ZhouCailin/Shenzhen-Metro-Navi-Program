using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trip.IDAL;
using Trip.Model;

namespace Trip.DAL
{
    public class  AccessTripFactory : ITripFactory
    {
        public IUserRepository CreateUserRepository()
        {
            IUserRepository pUserRepository = new AccessUserRepository();

            return pUserRepository;
        }

        public ITripRepository CreateTripRepository()
        {
            ITripRepository pTripRepository = new AccessTripRepository();
            return pTripRepository;
        }

        public ICityRepository CreateCityRepository()
        {
            ICityRepository pCityRepository = new AccessCityRepository();
            return pCityRepository;
        }
        public IDistRepository CreateDistRepository()
        {
            IDistRepository pDistRepository = new AccessDistRepository();
            return pDistRepository;
        }

    }
}
