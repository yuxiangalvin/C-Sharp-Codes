using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PathPlanner
{
    public class WeightedGraph
    {
        public class Node
        {
            public string Name;
            public Point2D Point;
            public Dictionary<string, Node> AdjNodes;
            public double Cost;
            public double Distance;
            public bool Visit;
            public Node Pre;


            public Node(string name, int x, int y)
            {
                Name = name;
                Point = new Point2D(x,y);
                AdjNodes = new Dictionary<string, Node>();
                Cost = 0;
                Visit = false;
                Pre = null;
            }

            public Node()
            {
                AdjNodes = new Dictionary<string, Node>();
                Visit = false;
                Pre = null;
            }
        }

        public class PriorityQueue
        {
            public List<Node> PQ = new List<Node>();

            public int Parent(int index)
            {
                return (index - 1) / 2;
            }

            public int Left(int index)
            {
                return index * 2 + 1;
            }

            public int Right(int index)
            {
                return index * 2 + 2;
            }

            public void Heapify(int index)
            {
                int l = Left(index);
                int r = Right(index);
                int smallest;


                if (l < PQ.Count && PQ[l].Cost < PQ[index].Cost)
                {
                    smallest = l;
                }
                else
                {
                    smallest = index;
                }

                if (r < PQ.Count && PQ[r].Cost < PQ[smallest].Cost)
                {
                    smallest = r;
                }

                if (smallest != index)
                {
                    Node temp = PQ[index];
                    PQ[index] = PQ[smallest];
                    PQ[smallest] = temp;
                    this.Heapify(smallest);
                }
            }

            public void Insert(Node node, double num)
            {
                PQ.Add(new Node());
                int i = PQ.Count - 1;
                while (i > 0 && PQ[Parent(i)].Cost > num)
                {
                    PQ[i] = PQ[Parent(i)];
                    i = Parent(i);
                }

                PQ[i] = node;
            }

            public Node ExtractMin()
            {
                Node min_node = PQ[0];
                PQ[0] = PQ[PQ.Count - 1];
                PQ.RemoveAt(PQ.Count - 1);
                if (PQ.Count == 0)
                {
                    return min_node;
                }
                this.Heapify(0);
                return min_node;
            }

            public int FindIndex(Node node)
            {
                for (int i = 0; i < PQ.Count; i++)
                {
                    if (PQ[i] == node)
                    {
                        return i;
                    }
                }

                throw new ArgumentException("Can not find the node in priority queue");
            }


            public void DecreaseKey(Node node, double newcost)
            {
                int i = FindIndex(node);
                while (i > 0 && PQ[Parent(i)].Cost > newcost)
                {
                    PQ[i] = PQ[Parent(i)];
                    i = Parent(i);
                }
                PQ[i] = node;
            }
        }

        public Dictionary<string, Node> Dict = new Dictionary<string, Node>();

        public WeightedGraph(){ }

        public double Weight(Node node1, Node node2)
        {
            return Math.Sqrt(
                Math.Pow((node1.Point.X - node2.Point.X), 2) + Math.Pow((node1.Point.Y - node2.Point.Y), 2));
        }
  

        public void AddNode(string name, int x, int y)
        {
            if (Dict.ContainsKey(name))
            {
                throw new ArgumentException("Node already exists");
            }

            Node newpoint = new Node(name, x, y);
            Dict.Add(name, newpoint);
        }


        public Point2D NodeLocation(string name)
        {
            if (!Dict.ContainsKey(name))
            {
                return null;
            }

            return Dict[name].Point;
        }


        public IEnumerable<string> Nodes()
        {
            foreach (string name in Dict.Keys)
            {
                yield return name;
            }
        }


        public void AddEdge(string node1, string node2)
        {
            if (!Dict.ContainsKey(node1) || !Dict.ContainsKey(node2))
            {
                throw new ArgumentException("no such node");
            }

            if (node1 == node2)
            {
                return;
            }

            if (Dict[node1].AdjNodes.ContainsKey(node2) || Dict[node2].AdjNodes.ContainsKey(node1))
            {
                return;
            }

            Dict[node1].AdjNodes.Add(node2, Dict[node2]);
            Dict[node2].AdjNodes.Add(node1, Dict[node1]);
        }


        public IEnumerable<string> Neighbors(string node)
        {
            foreach (string name in Dict[node].AdjNodes.Keys)
            {
                yield return name;
            }
        }


        public void ReadFile(string path)
        {
            string[] data;
            data = File.ReadAllLines(path);
            int pos = 0;
            if (data[0] == "NODES")
            {
                pos++;

                if (pos >= data.Length)
                {
                    return;
                }

                for (int i = 1; data[i] != "EDGES"; i++)
                {
                    string[] three_data = data[i].Split('\t');
                    if (three_data.Length != 3)
                    {
                        throw new ArgumentException("Invalid format");
                    }

                    string newnode = three_data[0];

                    if (newnode == "")
                    {
                        throw new ArgumentException("Invalid format");
                    }

                    int x_pos = 0;
                    int y_pos = 0;

                    if (!(Int32.TryParse(three_data[1], out x_pos)))
                    {
                        throw new ArgumentException("Invalid format");
                    }

                    if (!(Int32.TryParse(three_data[2], out y_pos)))
                    {
                        throw new ArgumentException("Invalid format");
                    }

                    this.AddNode(newnode, x_pos, y_pos);
                    pos++;

                    if (pos >= data.Length)
                    {
                        return;
                    }
                }
            }

            if (data[pos] == "EDGES")
            {
                pos++;

                if (pos >= data.Length)
                {
                    return;
                }

                for (int i = pos; i < data.Length; i++)
                {
                    string[] two_nodes = data[i].Split('\t');
                    if (two_nodes.Length != 2)
                    {
                        throw new ArgumentException("Invalid format");
                    }

                    string node1 = two_nodes[0];
                    string node2 = two_nodes[1];

                    if (node1 == "" || node2 == "")
                    {
                        throw new ArgumentException("Invalid format");
                    }

                    if (!Dict.ContainsKey(node1) || !Dict.ContainsKey(node2))
                    {
                        throw new ArgumentException("no such node");
                    }

                    if (node1 != node2)
                    {
                        if (!Dict[node1].AdjNodes.ContainsKey(node2) && !Dict[node2].AdjNodes.ContainsKey(node1))
                        {
                            this.AddEdge(node1, node2);
                        }
                    }
                }
            }
        }

        public List<string> ShortestPathDijkstra(string node1, string node2)
        {
            if (!Dict.ContainsKey(node1) || !Dict.ContainsKey(node2))
            {
                throw new ArgumentException("no such node");
            }

            foreach (Node node in Dict.Values)
            {
                node.Visit = false;
                node.Pre = null;
            }

            List<string> result = new List<string>();
            PriorityQueue HPQ = new PriorityQueue();

            if (node1 == node2)
            {
                result.Add(node1);
                return result;
            }

            foreach (string node in this.Nodes())
            {
                Dict[node].Cost = int.MaxValue;
                Dict[node].Distance = int.MaxValue;
            }

            Dict[node1].Cost = 0;
            Dict[node1].Distance = 0;

            foreach (Node node in Dict.Values)
            {
                HPQ.Insert(node, node.Cost);
            }

            while (HPQ.PQ.Count != 0)
            {
                Node extract_min = HPQ.ExtractMin();

                if (extract_min == Dict[node2])
                {
                    Node current = Dict[node2];
                    while (current != null)
                    {
                        result.Insert(0, current.Name);
                        current = current.Pre;
                    }

                    if (result[0] != node1)
                    {
                        return null;
                    }
                    return result;
                }

                foreach (string neighbor in this.Neighbors(extract_min.Name))
                {
                    double w = Weight(extract_min, Dict[neighbor]);
                    double newCost = extract_min.Distance + w;
                    double newDistance = extract_min.Distance + w;
                    if (newDistance < Dict[neighbor].Distance)
                    {
                        HPQ.DecreaseKey(Dict[neighbor], newCost);
                        Dict[neighbor].Distance = newDistance;
                        Dict[neighbor].Cost = newCost;
                        Dict[neighbor].Pre = extract_min;
                    }
                }
            }
            return null;
        }


        public List<string> ShortestPathAStar(string node1, string node2)
        {
            if (!Dict.ContainsKey(node1) || !Dict.ContainsKey(node2))
            {
                throw new ArgumentException("no such node");
            }

            foreach (Node node in Dict.Values)
            {
                node.Visit = false;
                node.Pre = null;
            }

            List<string> result = new List<string>();
            PriorityQueue HPQ = new PriorityQueue();

            if (node1 == node2)
            {
                result.Add(node1);
                return result;
            }

            foreach (string node in this.Nodes())
            {
                Dict[node].Cost = int.MaxValue;
                Dict[node].Distance = int.MaxValue;
            }

            Dict[node1].Cost = 0;
            Dict[node1].Distance = 0;

            foreach (Node node in Dict.Values)
            {
                HPQ.Insert(node, node.Cost);
            }

            while (HPQ.PQ.Count != 0)
            {
                Node extract_min = HPQ.ExtractMin();

                if (extract_min == Dict[node2])
                {
                    Node current = Dict[node2];
                    while (current != null)
                    {
                        result.Insert(0, current.Name);
                        current = current.Pre;
                    }

                    if (result[0] != node1)
                    {
                        return null;
                    }
                    return result;
                }

                foreach (string neighbor in this.Neighbors(extract_min.Name))
                {
                    double w = Weight(extract_min, Dict[neighbor]) + Weight(Dict[neighbor], Dict[node2]);
                    double newCost = extract_min.Distance + w;
                    double newDistance = extract_min.Distance + Weight(extract_min, Dict[neighbor]);
                    if (newDistance < Dict[neighbor].Distance)
                    {
                        HPQ.DecreaseKey(Dict[neighbor], newCost);
                        Dict[neighbor].Distance = newDistance;
                        Dict[neighbor].Cost = newCost;
                        Dict[neighbor].Pre = extract_min;
                    }
                }
            }
            return null;
        }
    }
}
