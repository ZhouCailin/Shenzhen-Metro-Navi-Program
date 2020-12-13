using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trip.Model;

namespace Trip.IDAL
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(string userID);
        void Update(User user);
        User GetUser(string userID);
        List<User> GetUsers(string order, string sort, string search);
        //public List<User> GetUsers();
    }
}
