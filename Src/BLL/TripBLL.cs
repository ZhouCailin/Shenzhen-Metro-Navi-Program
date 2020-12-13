using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trip.Common;
using Trip.IBLL;
using Trip.IDAL;
using Trip.DAL;
using Trip.Model;

namespace Trip.BLL
{
    public class TripBLL : ITripBLL
    {
        ITripRepository m_tripRepository;

        public TripBLL(ITripRepository tripRepository)
        {
            m_tripRepository = tripRepository;    
        }
    
        public List<TripRecord> GetAllTrip()
        {
            try
            {
                List<TripRecord> trips = m_tripRepository.GetTrips(null, null, null);
                return trips;
            }
            catch(Common.TripDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }
        
        public void AddTrip(TripRecord trip)
        {
            try
            {
                m_tripRepository.Add(trip);
                ;
            }
            catch (Common.TripDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }

        public void DeleteTrip(TripRecord trip)
        {
            try
            {
                m_tripRepository.Delete(trip);
            }
            catch (Common.TripDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }

        public void DeleteTrip(string userID)
        {
            try
            {
                m_tripRepository.Delete(userID);
            }
            catch (Common.TripDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }

        public void UpdateTrip(TripRecord b_trip, TripRecord m_trip)//SIGN
        {
            try
            {
                m_tripRepository.Update(b_trip, m_trip);
            }
            catch (Common.TripDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }

        public TripRecord GetTrip(string userID, string cityNo)
        {
            try
            {
                return m_tripRepository.GetTrip(userID, cityNo);
            }
            catch (Common.TripDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }

        public List<TripRecord> GetTrips(string userID)
        {
            try
            {
                return m_tripRepository.GetTrips(null, null , "userID = '" + userID + "'");
            }
            catch (Common.TripDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }

        }
        
       public void DeleteTrips(string userID)
        {

        }


        public List<TripRecord> GetByParam(string id, int start, int limit, string order, string sort, string search, ref int total)
        {
            try
            {
                IQueryable<TripRecord> trips = m_tripRepository.GetTrips(order, sort, search).AsQueryable<TripRecord>();

                total = trips.Count();
                if (start == -1)                     // 不分页
                    return trips.ToList();
                else
                {
                    trips = trips.Skip(start).Take(limit);
                    return trips.ToList();
                }
            }
            catch (Common.TripDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }
    }
}
