using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GandasMain
{
    public class GraphInfo
    {
        public Graph solution { get; private set; }
        public string steps { get; private set; }
        public GraphInfo(Graph startingGraph, Path path)
        {
            solution = startingGraph;
            ColorGraph(path);
            FormSteps(path);
        }
        private void ColorGraph(Path path)
        {
            for(int i = 0; i < path.EdgesCount; i++)
            {
                foreach(var one in solution.Edges)
                {
                    if(one.SourceNode.Id == (path.GetEdge(i).SourceNode + 1).ToString() && one.TargetNode.Id == (path.GetEdge(i).TargetNode + 1).ToString() || one.SourceNode.Id == (path.GetEdge(i).TargetNode + 1).ToString() && one.TargetNode.Id == (path.GetEdge(i).SourceNode + 1).ToString())
                    {
                        one.SourceNode.Attr.FillColor = Color.Blue;
                        one.TargetNode.Attr.FillColor = Color.Blue;
                        one.Attr.Color = Color.Blue;
                        break;
                    }
                }
            }
            solution.FindNode((path.GetVertex(0) + 1).ToString()).Attr.FillColor = Color.Red;
        }
        private void FormSteps(Path path)
        {
            int start = 0;
            for(int i = 0; i < path.Steps; i++)
            {
                steps += (i + 1) + " žingsnis:\n";
                for(int j = start; j < path.GetHistory(i); j++)
                {
                    steps += (path.GetEdge(j).SourceNode + 1) + " -> " + (path.GetEdge(j).TargetNode + 1) + "\n";
                }
                start = path.GetHistory(i);
            }
            steps = steps.Trim();
        }
    }
}
