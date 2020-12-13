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
    public class CityBLL : ICityBLL
    {
        ICityRepository m_cityRepository;

        public CityBLL(ICityRepository cityRepository)
        {
            m_cityRepository = cityRepository;    
        }
    
        public List<City> GetAllCity()
        {
            try
            {
                List<City> citys = m_cityRepository.GetAllCity();
                return citys;
            }
            catch(Common.UserDBException ex)
            {
                throw new Common.UserDBException(ex.Message);
            }
        }
        
    }
}
