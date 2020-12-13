using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trip.Model;

namespace Trip.IBLL
{
    public interface IUserBLL
    {
        User GetUser(string userID);
        List<User> GetAllUser();
        List<User> GetByParam(string id, int start, int limit, string order, string sort, string search, ref int total);
        void AddUser(User user);
        void DeleteUser(User user);
        void DeleteUser(string userID);
        void UpdateUser(User user);
    }
}
