using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trip.Model;

namespace Trip.IDAL
{
    public interface ITripRepository
    {
        void GetID();//SIGN:获取ID填入新数据行
        void Add(TripRecord trip);
        void Delete(TripRecord trip);
        void Delete(string userID);
        void Update(TripRecord b_trip, TripRecord m_trip);//SIGN
        TripRecord GetTrip(string userID, string cityNo);
//        List<TripRecord> GetTrips(string userID);
        List<TripRecord> GetTrips(string order, string sort, string search);

    }
}
