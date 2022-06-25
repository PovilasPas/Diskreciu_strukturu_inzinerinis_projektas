using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Msagl.Core;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Layout;
using System.IO;

namespace GandasMain
{
    public partial class Form1 : Form
    {
        const string Dfv = "../../App_data/Grafas.txt";
        private GraphSolver Solver;
        private int StartPoint;
        private List<GraphInfo> SolutionGraphs;
        private int CurrentGraph;
        public Form1()
        {
            InitializeComponent();
        }

        private void Read_Click(object sender, EventArgs e)
        {
            try
            {
                if(Directory.Exists(System.IO.Path.GetDirectoryName(Dfv)))
                {
                    if(!File.Exists(Dfv))
                    {
                        throw new IOException("Failas Grafas.txt neegzistuoja");
                    }
                }
                else
                {
                    throw new IOException("Katalogas \"App_data\" neegzistuoja");
                }
                Solver = InOut.ReadGraph(Dfv);
                Graph StartingGraph = Solver.GetStartingGraph();
                GView.Graph = new Graph();
                GView.Graph = StartingGraph;
                SVertices.Items.Clear();
                for (int i = 1; i <= StartingGraph.NodeCount; i++)
                {
                    SVertices.Items.Add(i);
                }
                SVertices.Enabled = true;
                Read.Enabled = false;
            }
            catch(FormatException except)
            {
                string message = "Blogas failo Grafas.txt formatas. " + except.Message;
                MessageBoxButtons Buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, "Klaida", Buttons, MessageBoxIcon.Error);
                SVertices.Items.Clear();
                if (GView.Graph != null)
                {
                    GView.Graph = null;
                }
            }
            catch(IOException except)
            {
                MessageBoxButtons Buttons = MessageBoxButtons.OK;
                MessageBox.Show(except.Message, "Klaida", Buttons, MessageBoxIcon.Error);
                SVertices.Items.Clear();
                if (GView.Graph != null)
                {
                    GView.Graph = null;
                }
            }
            finally
            {
                StartPoint = 0;
                CurrentGraph = 0;
                SolutionGraphs = new List<GraphInfo>();
                Previous.Enabled = false;
                Next.Enabled = false;
                Steps.Text = "";
            }
        }

        private void SVertices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Execute.Enabled == false)
            {
                Execute.Enabled = true;
            }
            if(StartPoint != 0)
            {
                GView.Graph.FindNode(StartPoint.ToString()).Attr.FillColor = Color.LightGray;
                StartPoint = (int)SVertices.SelectedItem;
                GView.Graph.FindNode(StartPoint.ToString()).Attr.FillColor = Color.Red;
            }
            else
            {
                StartPoint = (int)SVertices.SelectedItem;
                GView.Graph.FindNode(StartPoint.ToString()).Attr.FillColor = Color.Red;
            }
            GView.Refresh();
        }

        private void Execute_Click(object sender, EventArgs e)
        {
            SVertices.Items.Clear();
            SVertices.Items.Add(StartPoint);
            SolutionGraphs = Solver.SpreadRumors(StartPoint);
            GView.Graph = SolutionGraphs[CurrentGraph].solution;
            Steps.Text = SolutionGraphs[CurrentGraph].steps;
            Execute.Enabled = false;
            SVertices.Enabled = false;
            Read.Enabled = true;
            if (CurrentGraph + 1 < SolutionGraphs.Count)
            {
                Next.Enabled = true;
            }
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            if(CurrentGraph - 1 >= 0)
            {
                CurrentGraph--;
                GView.Graph = SolutionGraphs[CurrentGraph].solution;
                Steps.Text = SolutionGraphs[CurrentGraph].steps;
                Next.Enabled = true;
                if(CurrentGraph - 1 < 0)
                {
                    Previous.Enabled = false;
                }
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if(CurrentGraph + 1 < SolutionGraphs.Count)
            {
                CurrentGraph++;
                GView.Graph = SolutionGraphs[CurrentGraph].solution;
                Steps.Text = SolutionGraphs[CurrentGraph].steps;
                Previous.Enabled = true;
                if(CurrentGraph + 1 >= SolutionGraphs.Count)
                {
                    Next.Enabled = false;
                }
            }
        }
    }
}
