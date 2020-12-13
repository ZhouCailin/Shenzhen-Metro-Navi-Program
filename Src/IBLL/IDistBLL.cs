using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trip.Model;

namespace Trip.IBLL
{
    public interface IDistBLL
    {
        List<DistRecord> GetAllWeight();
    }
}
