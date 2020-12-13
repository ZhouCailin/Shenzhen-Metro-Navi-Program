using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//sig
namespace Trip.Model
{
    internal class Node
    {
        //属性
        public int Name
        {
            get;
            set;
        }
        public Node Parent
        {
            get;
            set;
        }
        public double Weight
        {
            get;
            set;
        }

        //操作
        public double GetAllWeight()
        {
            var allWeight = 0d;
            var node = this;
            do
            {
                allWeight += node.Weight;
                node = node.Parent;
            } while (node != null);
            return allWeight;
        }
    }
    internal class Edge
    {
        //属性
        public int Start
        {
            get;
            set;
        }
        public int End
        {
            get;
            set;
        }
        public double Weight
        {
            get;
            set;
        }
    }
    public class Graph
    {
        //属性
        private List<Node> _nodes;
        private List<Edge> _edges;
        //操作
        public Graph(List<Tuple<int, int,double>>weights)
        {
            _nodes = new List<Node>();
            _edges = new List<Edge>();
            InitWeights(weights);
        }//构造函数
        private List<Edge> ConvertToEdge(List<Tuple<int, int, double>> weights)
        {
            var edges = new List<Edge>();
            weights.ForEach(weight =>
            {
                edges.Add(new Edge()
                {
                    Start = weight.Item1,
                    End = weight.Item2,
                    Weight = weight.Item3
                });
            });
            return edges;
        }//List<Tupple>->List<Edge>
        private void InitNodes()
        {
            _edges.ForEach(weight =>
            {
                if (_nodes.All(x => x.Name != weight.Start))
                {
                    _nodes.Add(new Node()
                    {
                        Name = weight.Start
                    });
                }

                if (_nodes.All(x => x.Name != weight.End))
                {
                    _nodes.Add(new Node()
                    {
                        Name = weight.End
                    });
                }
            });
        }//List<Edge>->List<Node>
        public void InitWeights(List<Tuple<int, int, double>> weights)
        {
            _edges = ConvertToEdge(weights);
            InitNodes();
        }//Interface to CreateGraph：List<Tupple>->List<Node>,List<Edges>
        private Edge GetEdgeByTwoNode(Node start, Node end)
        {
            var edge = _edges.FirstOrDefault(x => x.Start == start.Name && x.End == end.Name);
            //如果找不到，尝试起末点调换找——因此支持无向图同一边起末点相反无需重复输入
            //如果要支持有向图，把下面代码删除
            if (edge == null)
            {
                edge = _edges.FirstOrDefault(x => x.Start == end.Name && x.End == start.Name);
            }
            return edge;
        }
        private Node GetNodeByName(int nodeName)
        {
            return _nodes.FirstOrDefault(x => x.Name == nodeName);
        }
        public Stack Find(int start, int end, ref double pathDis)
        {
            var startNode = GetNodeByName(start);
            var endNode = GetNodeByName(end);

            //排除异常
            if (startNode == null || endNode == null)
            {
                return new Stack();
            }

            //距离初始化
            //队列s目前只有起始点，接下来会装入确认好最短路径的点
            //队列u为未确认的点
            var s = _nodes.Where(x => x.Name == start).ToList();
            var u = _nodes.Where(x => x.Name != start).ToList();
            s.ForEach(x =>
            {
                x.Weight = 0;
            });
            u.ForEach(x =>
            {
                //u中所有点的weight都是记录他与起点的目前距离，现在置无穷
                x.Weight = double.PositiveInfinity;
            });

            //开始DJ算法
            while (u.Any())
            {
                //环节1：更新
                //上一个确认好最短路径的点，第一个是起始点
                var node = s.Last();
                //更新"node"到"x"的距离
                u.ForEach(x =>
                {
                    var edge = GetEdgeByTwoNode(node, x);
                    //两点之间要有边（哪个作起点都行）
                    if (edge != null)
                    {
                        //新距离=出发点-该点
                        //Node中的操作GetAllWeight——已构建好的的最短路径路程距离                   
                        var weights = node.GetAllWeight() + edge.Weight;
                        //更新距离<目前距离？
                        if (weights < x.Weight)
                        {
                            x.Weight = weights;
                            x.Parent = node;
                        }
                    }
                });

                //环节2：找出最短距离点
                var minNode = u.OrderBy(x => x.Weight).FirstOrDefault();
                //最短距离确认点入队列S
                s.Add(minNode);
                u.Remove(minNode);
                if (minNode.Name == end)
                {
                    pathDis = minNode.Weight;
                }
            }
            //结束DJ算法

            //开始路程回溯
            var paths = new Stack();
            while (endNode != null)
            {
                paths.Push(endNode.Name);
                //通过父结点属性逐一回溯
                endNode = endNode.Parent;
            }

            //排除异常：如果路径包含起点则正确，否则为无最短路径，输出空stack
            if (paths.Contains(start))
            {
                return paths;
            }
            else
            {
                return new Stack();
            }
        }//DJ核心函数——找寻两点最短路径
        }
}
