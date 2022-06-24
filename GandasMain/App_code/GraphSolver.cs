using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GandasMain
{
    public class GraphSolver
    {
        private List<List<int>> Adjs; //Gretimumo struktūta, kurioje saugomas grafas
        public GraphSolver(List<List<int>> adjs)
        {
            Adjs = adjs;
        }
        //Pradinio grafo braižymui skirtas metodas
        public Graph GetStartingGraph()
        {
            Graph graph = new Graph();
            graph.Directed = false;
            graph.Attr.BackgroundColor = Color.LightGray;
            if (Adjs.Count == 0)
            {
                Node node = new Node("1");
                node.Attr.Shape = Shape.Circle;
                graph.AddNode(node);
            }
            else
            {
                for (int i = 0; i < Adjs.Count; i++)
                {
                    for (int j = 0; j < Adjs[i].Count; j++)
                    {
                        if (i + 1 < Adjs[i][j])
                        {
                            Edge one = graph.AddEdge((i + 1).ToString(), Adjs[i][j].ToString());
                            one.Attr.ArrowheadAtTarget = ArrowStyle.None;
                            if (one.SourceNode.Attr.Shape != Shape.Circle)
                            {
                                one.SourceNode.Attr.Shape = Shape.Circle;
                            }
                            one.TargetNode.Attr.Shape = Shape.Circle;
                        }
                    }
                }
            }
            return graph;
        }
        public List<Graph> SpreadRumors(int Start)
        {
            List<Graph> Solution = new List<Graph>();
            if (Adjs.Count == 0)
            {
                Graph one = GetStartingGraph();
                foreach (Node n in one.Nodes)
                {
                    n.Attr.FillColor = Color.Red;
                }
                Solution.Add(one);
                return Solution;
            }
            else
            {
                List<Path> Solve = new List<Path>();
                Path temp = new Path();
                temp.AddVertex(Start - 1);
                Solve.Add(temp);
                int i = 0;
                while (Solve[i].VerticesCount < Adjs.Count)
                {
                    List<Path> pathVariations = new List<Path>();
                    for (int j = 0; j < Solve[i].VerticesCount; j++)
                    {
                        GeneratePaths(Solve[i].GetVertex(j), ref pathVariations, Solve[i]);
                    }
                    AppendPaths(Solve[i], pathVariations, Solve);
                    i++;
                }
                List<Path> best = SelectBestPaths(i, Solve);
                return GenerateSolutionGraph(best);
            }
        }
        private void GeneratePaths(int from, ref List<Path> pathVariations, Path rest)
        {
            if (pathVariations.Count == 0)
            {
                for (int i = 0; i < Adjs[from].Count; i++)
                {
                    if (!rest.ContainsVertex(Adjs[from][i] - 1))
                    {
                        Path temp = new Path();
                        temp.AddVertex(Adjs[from][i] - 1);
                        temp.AddEdge(new SimpleEdge(from, Adjs[from][i] - 1, rest.Steps + 1));
                        pathVariations.Add(temp);
                    }
                }
            }
            else
            {
                List<Path> newVariations = new List<Path>();
                for (int i = 0; i < pathVariations.Count; i++)
                {
                    bool log = true;
                    for (int j = 0; j < Adjs[from].Count; j++)
                    {
                        if (!pathVariations[i].ContainsVertex(Adjs[from][j] - 1) && !rest.ContainsVertex(Adjs[from][j] - 1))
                        {
                            log = false;
                            Path pathCpy = new Path(pathVariations[i]);
                            pathCpy.AddVertex(Adjs[from][j] - 1);
                            pathCpy.AddEdge(new SimpleEdge(from, Adjs[from][j] - 1, rest.Steps + 1));
                            newVariations.Add(pathCpy);
                        }
                    }
                    if (log) newVariations.Add(pathVariations[i]);
                }
                pathVariations = newVariations;
            }
        }
        private void AppendPaths(Path rest, List<Path> pathVariations, List<Path> solve)
        {
            for (int i = 0; i < pathVariations.Count; i++)
            {
                Path Appended = new Path(rest, rest.Steps);
                Appended.AppendPath(pathVariations[i]);
                solve.Add(Appended);
            }
        }
        private List<Path> SelectBestPaths(int index, List<Path> solve)
        {
            int criteria = solve[index].Steps;
            List<Path> best = new List<Path>();
            for (int i = index; i < solve.Count; i++)
            {
                if (solve[i].Steps == criteria && solve[i].VerticesCount == Adjs.Count)
                {
                    best.Add(solve[i]);
                }
            }
            return best;
        }
        private List<Graph> GenerateSolutionGraph(List<Path> best)
        {
            List<Graph> solutions = new List<Graph>();
            for (int i = 0; i < best.Count; i++)
            {
                Graph solution = GetStartingGraph();
                for (int j = 0; j < best[i].EdgesCount; j++)
                {
                    foreach (var one in solution.Edges)
                    {
                        if (one.SourceNode.Id == (best[i].GetEdge(j).SourceNode + 1).ToString() && one.TargetNode.Id == (best[i].GetEdge(j).TargetNode + 1).ToString() || one.SourceNode.Id == (best[i].GetEdge(j).TargetNode + 1).ToString() && one.TargetNode.Id == (best[i].GetEdge(j).SourceNode + 1).ToString())
                        {
                            one.SourceNode.Attr.FillColor = Color.Blue;
                            one.TargetNode.Attr.FillColor = Color.Blue;
                            one.Attr.Color = Color.Blue;
                            one.LabelText = best[i].GetEdge(j).Depth.ToString();
                        }
                    }
                }
                solution.FindNode((best[i].GetEdge(0).SourceNode + 1).ToString()).Attr.FillColor = Color.Red;
                solutions.Add(solution);
            }
            return solutions;
        }
        ////Užduoties sprendimui skirtas metodas
        //public List<Graph> SpreadRumors(int StartingPoint)
        //{
        //    if (Adjs.Count == 0)
        //    {
        //        List<Graph> Solution = new List<Graph>();
        //        Graph one = GetStartingGraph();
        //        foreach (Node n in one.Nodes)
        //        {
        //            n.Attr.FillColor = Color.Red;
        //        }
        //        Solution.Add(one);
        //        return Solution;
        //    }
        //    else
        //    {
        //        List<int> Spath = new List<int>();
        //        List<int> Sdepth = new List<int>();
        //        List<List<List<int>>> Depth3D = new List<List<List<int>>>();
        //        List<SimpleEdge> EdgeMatrix = new List<SimpleEdge>();
        //        List<Graph> Solutions = new List<Graph>();
        //        bool back = false;
        //        int counter = 0;
        //        int MinDepth = 0;
        //        int Svertex = 0;
        //        Spath.Add(StartingPoint - 1);
        //        Sdepth.Add(Spath.Count);
        //        while (true)
        //        {
        //            if (Sdepth.Count == 1)
        //            {
        //                counter++;
        //                if (counter > Adjs[StartingPoint - 1].Count) break;
        //                List<List<int>> Layer = new List<List<int>>();
        //                for (int i = 0; i < Adjs.Count; i++)
        //                {
        //                    Layer.Add(new List<int>());
        //                }
        //                Layer[StartingPoint - 1].Add(Adjs[StartingPoint - 1][counter - 1] - 1);
        //                Depth3D.Add(Layer);
        //                EdgeMatrix.Add(new SimpleEdge(StartingPoint, Adjs[StartingPoint - 1][counter - 1], Sdepth.Count));
        //                Spath.Add(Adjs[StartingPoint - 1][counter - 1] - 1);
        //                Sdepth.Add(Spath.Count);
        //                back = false;
        //            }
        //            else if (!back)
        //            {
        //                List<List<int>> Layer = new List<List<int>>();
        //                for (int j = 0; j < Depth3D[Depth3D.Count - 1].Count; j++)
        //                {
        //                    Layer.Add(new List<int>());
        //                }
        //                Depth3D.Add(Layer);
        //            }
        //            if (Sdepth[Sdepth.Count - 1] != Spath.Count && Spath.Count != Adjs.Count)
        //            {
        //                bool IsPut2 = false;
        //                for (int i = Spath.IndexOf(Svertex - 1); i < Sdepth[Sdepth.Count - 1]; i++)
        //                {
        //                    bool IsPut1 = false;
        //                    for (int j = 0; j < Adjs[Spath[i]].Count; j++)
        //                    {
        //                        if (!Spath.Contains(Adjs[Spath[i]][j] - 1) && !Depth3D[Depth3D.Count - 1][Spath[i]].Contains(Adjs[Spath[i]][j] - 1))
        //                        {
        //                            Spath.Add(Adjs[Spath[i]][j] - 1);
        //                            Depth3D[Depth3D.Count - 1][Spath[i]].Add(Adjs[Spath[i]][j] - 1);
        //                            EdgeMatrix.Add(new SimpleEdge(Spath[i] + 1, Adjs[Spath[i]][j], Sdepth.Count));
        //                            for (int k = i + 1; k < Sdepth[Sdepth.Count - 1]; k++)
        //                            {
        //                                Depth3D[Depth3D.Count - 1][Spath[k]].Clear();
        //                            }
        //                            IsPut2 = true;
        //                            IsPut1 = true;
        //                            break;
        //                        }
        //                    }
        //                    if (!IsPut1)
        //                    {
        //                        bool IsValid = true;
        //                        for (int j = 0; j < Adjs[Spath[i]].Count; j++)
        //                        {
        //                            if (!Spath.Contains(Adjs[Spath[i]][j] - 1))
        //                            {
        //                                IsValid = false;
        //                            }
        //                        }
        //                        if (!IsValid) break;
        //                    }
        //                }
        //                if (IsPut2)
        //                {
        //                    Sdepth.Add(Spath.Count);
        //                    back = false;
        //                }
        //                else
        //                {
        //                    Svertex = BackTrack(Sdepth, Spath, EdgeMatrix);
        //                    back = true;
        //                }
        //            }
        //            else if (Spath.Count != Adjs.Count)
        //            {
        //                for (int i = 0; i < Sdepth[Sdepth.Count - 1]; i++)
        //                {
        //                    for (int j = 0; j < Adjs[Spath[i]].Count; j++)
        //                    {
        //                        if (!Spath.Contains(Adjs[Spath[i]][j] - 1) && !Depth3D[Depth3D.Count - 1][Spath[i]].Contains(Adjs[Spath[i]][j] - 1))
        //                        {
        //                            Spath.Add(Adjs[Spath[i]][j] - 1);
        //                            Depth3D[Depth3D.Count - 1][Spath[i]].Add(Adjs[Spath[i]][j] - 1);
        //                            EdgeMatrix.Add(new SimpleEdge(Spath[i] + 1, Adjs[Spath[i]][j], Sdepth.Count));
        //                            for (int k = i + 1; k < Sdepth[Sdepth.Count - 1]; k++)
        //                            {
        //                                Depth3D[Depth3D.Count - 1][Spath[k]].Clear();
        //                            }
        //                            break;
        //                        }
        //                    }
        //                    if (Sdepth[Sdepth.Count - 1] == Spath.Count)
        //                    {
        //                        bool IsValid = true;
        //                        for (int j = 0; j < Adjs[Spath[i]].Count; j++)
        //                        {
        //                            if (!Spath.Contains(Adjs[Spath[i]][j] - 1))
        //                            {
        //                                IsValid = false;
        //                            }
        //                        }
        //                        if (!IsValid) break;
        //                    }
        //                }
        //                if (Sdepth[Sdepth.Count - 1] != Spath.Count)
        //                {
        //                    Sdepth.Add(Spath.Count);
        //                    back = false;
        //                }
        //                else
        //                {
        //                    Svertex = BackTrack(Sdepth, Spath, EdgeMatrix);
        //                    Depth3D.RemoveAt(Depth3D.Count - 1);
        //                    back = true;
        //                }
        //            }
        //            if (Spath.Count == Adjs.Count)
        //            {
        //                if (Sdepth[Sdepth.Count - 1] != Spath.Count)
        //                {
        //                    Sdepth.Add(Spath.Count);
        //                }
        //                back = true;
        //                if (MinDepth == 0)
        //                {
        //                    MinDepth = Sdepth.Count;
        //                    Solutions.Add(GenerateSolutionGraph(EdgeMatrix));
        //                }
        //                else if (MinDepth > Sdepth.Count)
        //                {
        //                    MinDepth = Sdepth.Count;
        //                    Solutions.Clear();
        //                    Solutions.Add(GenerateSolutionGraph(EdgeMatrix));
        //                }
        //                else if (MinDepth == Sdepth.Count)
        //                {
        //                    Solutions.Add(GenerateSolutionGraph(EdgeMatrix));
        //                }
        //                Svertex = BackTrack(Sdepth, Spath, EdgeMatrix);
        //            }
        //        }
        //        return Solutions;
        //    }
        //}
        ////Užduoties sprendimui skirtas metodas
        //private int BackTrack(List<int> Sdepth, List<int> Spath, List<SimpleEdge> EdgeMatrix)
        //{
        //    if (Spath.Count == Sdepth[Sdepth.Count - 1])
        //    {
        //        Sdepth.RemoveAt(Sdepth.Count - 1);
        //    }
        //    int Svertex = EdgeMatrix[EdgeMatrix.Count - 1].SourceNode;
        //    EdgeMatrix.RemoveAt(EdgeMatrix.Count - 1);
        //    Spath.RemoveAt(Spath.Count - 1);
        //    return Svertex;
        //}
        //private Graph GenerateSolutionGraph(List<SimpleEdge> EdgeMatrix)
        //{
        //    Graph Solution = GetStartingGraph();
        //    for (int i = 0; i < EdgeMatrix.Count; i++)
        //    {
        //        foreach (var one in Solution.Edges)
        //        {
        //            if (one.SourceNode.Id == EdgeMatrix[i].SourceNode.ToString()
        //           && one.TargetNode.Id == EdgeMatrix[i].TargetNode.ToString() || one.SourceNode.Id
        //           == EdgeMatrix[i].TargetNode.ToString() && one.TargetNode.Id ==
        //           EdgeMatrix[i].SourceNode.ToString())
        //            {
        //                one.SourceNode.Attr.FillColor = Color.Blue;
        //                one.TargetNode.Attr.FillColor = Color.Blue;
        //                one.Attr.Color = Color.Blue;
        //                one.LabelText = EdgeMatrix[i].Depth.ToString();
        //            }
        //        }
        //    }

        //    Solution.FindNode(EdgeMatrix[0].SourceNode.ToString()).Attr.FillColor =
        //    Color.Red;
        //    return Solution;
        //}
    }
}
