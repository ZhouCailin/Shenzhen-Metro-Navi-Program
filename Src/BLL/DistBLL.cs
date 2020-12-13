using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Trip.Common;
using Trip.IBLL;
using Trip.IDAL;
using Trip.DAL;
using Trip.Model;

//sig
namespace Trip.BLL
{
    public class DistBLL : IDistBLL
    {
        IDistRepository m_distRepository;
        private Graph m_graph; 
        private List<DistRecord> m_disrec;

        public DistBLL(IDistRepository distRepository)
        {
            m_distRepository = distRepository;
            InitGraph();
        }
        public List<DistRecord> GetAllWeight()
        {
            try
            {
                List<DistRecord> weights = m_distRepository.GetAllWeight();
                return weights;
            }
            catch (Common.TripDBException ex)
            {
                throw new Common.TripDBException(ex.Message);
            }
        }
        public void InitGraph()
        {
            var weights = new List<Tuple<int, int, double>>();
            m_disrec = GetAllWeight();
            m_disrec.ForEach(singleRec =>
            {
                var singleWeight = new Tuple<int, int, double>(Convert.ToInt32(singleRec.fid1), Convert.ToInt32(singleRec.fid2), singleRec.dist);
                weights.Add(singleWeight);
            });
            m_graph=new Graph(weights);
        }

        public string id2string (int id)
        {
            string s="";
            m_disrec.ForEach(x =>
            {
                if (id == Convert.ToInt32(x.fid1)) s = x.name1;
            });
            return s;
        }
        public Stack getPath(string sp, string ep, ref double pathDis)
        {
            var outputList = new List<string>();
            var stk = new Stack();
            int spid = 0; int epid = 0;
            m_disrec.ForEach(x =>
            {
                if (x.name1.Equals(sp)) spid = Convert.ToInt32(x.fid1);
                if (x.name1.Equals(ep)) epid = Convert.ToInt32(x.fid1);
            });
            stk = m_graph.Find(spid, epid, ref pathDis);
            return stk;
        }
        /*
        public List<String> getPath(string sp, string ep, ref double pathDis)
        {
            var outputList=new List<string>();
            int pid;
            var stk=new Stack();
            int spid = 0; int epid = 0;
            m_disrec.ForEach(x=>{
                if(x.name1.Equals(sp)) spid=Convert.ToInt32(x.fid1); 
                if(x.name1.Equals(ep)) epid=Convert.ToInt32(x.fid1); 
            });
            stk= m_graph.Find(spid, epid, ref pathDis);
            while (stk.Count!=0)
            {
                pid = Convert.ToInt32(stk.Pop().ToString());
                outputList.Add(id2string(pid));
            }
            return outputList;
        }
         * */
    }
}
