using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GandasMain
{
    public class SimpleEdge
    {
        public int SourceNode { get; set; }
        public int TargetNode { get; set; }
        public SimpleEdge(int S, int T)
        {
            SourceNode = S;
            TargetNode = T;
        }
    }
}
