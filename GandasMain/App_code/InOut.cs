using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GandasMain
{
    public static class InOut
    {
        //Metodas skirtas grafo nuskaitymui iš failo
        public static GraphSolver ReadGraph(string FileName)
        {
            List<List<int>> Adjs = new List<List<int>>();
            string[] lines = File.ReadAllLines(FileName);
            string message = "";
            int ErrorCount = 0;
            for(int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                List<int> vertex = new List<int>();
                for(int j = 0; j < parts.Length;j++)
                {
                    try
                    {
                        int value = int.Parse(parts[j]);
                        if (value > 0)
                        {
                            vertex.Add(value);
                        }
                        else
                        {
                            ErrorCount++;
                            int line = i + 1;
                            message = message + line + ", ";
                            break;
                        }
                    }
                    catch(FormatException)
                    {
                        ErrorCount++;
                        int line = i + 1;
                        message = message + line + ", ";
                        break;
                    }
                }
                Adjs.Add(vertex);
            }
            if(!String.IsNullOrEmpty(message))
            {
                if(ErrorCount == 1)
                {
                    message = "Eilutės nr: " + message.Remove(message.Length - 2);
                    throw new FormatException(message);
                }
                else
                {
                    message = "Eilučių nr: " + message.Remove(message.Length - 2);
                    throw new FormatException(message);
                }
            }
            GraphSolver solver = new GraphSolver(Adjs);
            return solver;
        }
    }
}
