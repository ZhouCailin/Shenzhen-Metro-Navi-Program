using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip.IDAL
{
    public interface ITripFactory
    {
        IUserRepository CreateUserRepository();
        ITripRepository CreateTripRepository();
        ICityRepository CreateCityRepository();
        IDistRepository CreateDistRepository();
    }
}
