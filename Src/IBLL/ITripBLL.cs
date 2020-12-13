using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trip.Model;

namespace Trip.IBLL
{
    public interface ITripBLL
    {
        List<TripRecord> GetTrips(string userID);
        List<TripRecord> GetByParam(string id, int start, int limit, string order, string sort, string search, ref int total);
        void AddTrip(TripRecord tripRecord);
        void DeleteTrip(TripRecord tripRecord);
        void DeleteTrips(string userID);
        void UpdateTrip(TripRecord b_trip, TripRecord m_trip);
    }
}
