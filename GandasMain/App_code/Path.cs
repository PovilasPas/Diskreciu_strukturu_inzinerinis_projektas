using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GandasMain
{
    public class Path
    {
        private List<int> Vertices;
        private List<SimpleEdge> Edges;
        public int VerticesCount
        {
            get
            {
                return Vertices.Count;
            }
        }
        public int EdgesCount
        {
            get
            {
                return Edges.Count;
            }
        }
        public int Steps { get; private set; }
        public Path(Path path, int steps)
        {
            Vertices = new List<int>();
            Edges = new List<SimpleEdge>();
            Steps = steps;
            for (int i = 0; i < path.Vertices.Count; i++)
            {
                Vertices.Add(path.GetVertex(i));
            }
            for (int i = 0; i < path.Edges.Count; i++)
            {
                Edges.Add(path.GetEdge(i));
            }
        }
        public Path(Path path)
        {
            Vertices = new List<int>();
            Edges = new List<SimpleEdge>();
            Steps = 0;
            for (int i = 0; i < path.Vertices.Count; i++)
            {
                Vertices.Add(path.GetVertex(i));
            }
            for (int i = 0; i < path.Edges.Count; i++)
            {
                Edges.Add(path.GetEdge(i));
            }
        }
        public Path()
        {
            Vertices = new List<int>();
            Edges = new List<SimpleEdge>();
            Steps = 0;
        }
        public bool ContainsVertex(int element)
        {
            return Vertices.Contains(element);
        }
        public void AddEdge(SimpleEdge edge)
        {
            Edges.Add(edge);
        }
        public SimpleEdge GetEdge(int index)
        {
            return Edges[index];
        }
        public void AddVertex(int vertex)
        {
            Vertices.Add(vertex);
        }
        public int GetVertex(int index)
        {
            return Vertices[index];
        }
        public void AppendPath(Path other)
        {
            Steps++;
            for (int i = 0; i < other.VerticesCount; i++)
            {
                Vertices.Add(other.GetVertex(i));
            }
            for (int i = 0; i < other.EdgesCount; i++)
            {
                Edges.Add(other.GetEdge(i));
            }
        }
    }
}
