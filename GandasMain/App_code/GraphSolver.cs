using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Layout.Layered;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GandasMain
{
    public class GraphSolver
    {
        private List<List<int>> Adjs;
        public GraphSolver(List<List<int>> adjs)
        {
            Adjs = adjs;
        }
        public Graph GetStartingGraph()
        {
            Graph graph = new Graph();
            graph.Directed = false;
            var settings = new SugiyamaLayoutSettings();
            settings.NodeSeparation = 50;
            settings.LayerSeparation = 50;
            settings.EdgeRoutingSettings.EdgeRoutingMode = Microsoft.Msagl.Core.Routing.EdgeRoutingMode.StraightLine;
            graph.LayoutAlgorithmSettings = settings;
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
        public List<GraphInfo> SpreadRumors(int Start)
        {
            if (Adjs.Count == 0)
            {
                List<GraphInfo> Solution = new List<GraphInfo>();
                Path path = new Path();
                path.AddVertex(Start - 1);
                GraphInfo one = new GraphInfo(GetStartingGraph(), path);
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
                        temp.AddEdge(new SimpleEdge(from, Adjs[from][i] - 1));
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
                            pathCpy.AddEdge(new SimpleEdge(from, Adjs[from][j] - 1));
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
                Path Appended = new Path(rest);
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
        private List<GraphInfo> GenerateSolutionGraph(List<Path> best)
        {
            List<GraphInfo> solutions = new List<GraphInfo>();
            for (int i = 0; i < best.Count; i++)
            {
                GraphInfo solution = new GraphInfo(GetStartingGraph(), best[i]);
                solutions.Add(solution);
            }
            return solutions;
        }
    }
}
