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
    public class UserBLL : IUserBLL
    {
        IUserRepository m_userRepository;

        public UserBLL(IUserRepository userRepository)
        {
            m_userRepository = userRepository;    
        }
    
        public List<User> GetAllUser()
        {
            try
            {
                List<User> users = m_userRepository.GetUsers(null, null, null);
                return users;
            }
            catch(Common.UserDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }
        
        public void AddUser(User user)
        {
            try
            {
                m_userRepository.Add(user);
                ;
            }
            catch (Common.UserDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }

        public void DeleteUser(User user)
        {
            try
            {
                m_userRepository.Delete(user.userID);
            }
            catch (Common.UserDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }

        public void DeleteUser(string userID)
        {
            try
            {
                m_userRepository.Delete(userID);
            }
            catch (Common.UserDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                m_userRepository.Update(user);
            }
            catch (Common.UserDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }

        public User GetUser(string userID)
        {
            try
            {
                return m_userRepository.GetUser(userID);
            }
            catch (Common.UserDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }


        public List<User> GetByParam(string id, int start, int limit, string order, string sort, string search, ref int total)
        {
            try
            {
                IQueryable<User> users = m_userRepository.GetUsers(order, sort, search).AsQueryable<User>();

                total = users.Count();
/*                if (page == -1)                     // 不分页
                    return users.ToList();
                else
                {
                    users = users.Skip((page - 1) * rows).Take(rows);
                    return users.ToList();
                }*/

                if (start == -1)                     // 不分页
                    return users.ToList();
                else
                {
                    users = users.Skip(start).Take(limit);
                    return users.ToList();
                }
            }
            catch (Common.UserDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }
    }
}
