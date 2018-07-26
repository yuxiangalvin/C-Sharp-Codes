using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    public class Graph
    {
        public class Node
        {
            public string Name;
            public Dictionary<string, Node> AdjNodes;
            public bool Visit;
            public Node Pre;

            public Node()
            {
                AdjNodes = new Dictionary<string, Node>();
                Visit = false;
                Pre = null;
            }

            public Node(string name)
            {
                Name = name;
                AdjNodes = new Dictionary<string, Node>();
                Visit = false;
                Pre = null;
            }
        }

        public Dictionary<string, Node> Dict = new Dictionary<string, Node>();

        public Graph()
        { }

        public void AddNode(string name)
        {
            if (Dict.ContainsKey(name))
            {
                throw new ArgumentException("Node already exists");
            }

            Node newnode = new Node(name);
            Dict.Add(name, newnode);
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

            if (Dict[node1].AdjNodes.ContainsKey(node2) || Dict[node2].AdjNodes.ContainsKey(node1))
            {
                throw new ArgumentException("there is an existing edge between two nodes");
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
            foreach (string command in data)
            {
                string[] two_nodes = command.Split('\t');
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

                if (!Dict.ContainsKey(node1))
                {
                    this.AddNode(node1);
                }
                if (!Dict.ContainsKey(node2))
                {
                    this.AddNode(node2);
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

        public List<string> ShortestPath(string node1, string node2)
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
            Node n = new Node();
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(Dict[node1]);
            Dict[node1].Visit = true;

            if (node1 == node2)
            {
                result.Add(node1);
                return result;
            }

            while (q.Count != 0)
            {
                n = q.Dequeue();
                foreach (string neighbour in Neighbors(n.Name))
                {
                    if (Dict[neighbour].Visit == false)
                    {
                        q.Enqueue(Dict[neighbour]);
                        Dict[neighbour].Pre = n;
                        Dict[neighbour].Visit = true;
                        if (neighbour == node2)
                        {
                            Node current = Dict[neighbour];
                            while (current != null)
                            {
                                result.Insert(0, current.Name);
                                current = current.Pre;
                            }
                            return result;
                        }
                    }
                }
            }
            return null;
        }

        public int Distance(string node1, string node2)
        {
            if (!Dict.ContainsKey(node1) || !Dict.ContainsKey(node2))
            {
                throw new ArgumentException("no such node");
            }

            List<string> shortest_path = ShortestPath(node1, node2);

            if (shortest_path == null)
            {
                return -1;
            }

            return shortest_path.Count - 1;
        }




    }

}
