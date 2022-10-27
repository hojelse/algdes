using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class maxflow
{
  static void Main(string[] args)
  {
    var tokens = Console.ReadLine().Split();
    int N = int.Parse(tokens[0]);
    int M = int.Parse(tokens[1]);
    int S = int.Parse(tokens[2]);
    int T = int.Parse(tokens[3]);

    var graph = ParseGraph(N, M, S, T);

    int maxFlow = graph.MaxFlow();

    var solutionEdges = graph.GetSolutionEdges();

    Console.WriteLine($"{N} {maxFlow} {solutionEdges.Count}");

    foreach (var flowedge in solutionEdges)
      Console.WriteLine($"{flowedge.from} {flowedge.to} {flowedge.flow}");
  }

  private static Flow ParseGraph(int N, int M, int source, int sink)
  {
    var graph = new Flow(source, sink, N);

    for (int i = 0; i < M; i++)
    {
      var tokens = Console.ReadLine().Split();
      var a = int.Parse(tokens[0]);
      var b = int.Parse(tokens[1]);
      var cap = int.Parse(tokens[2]);

      graph.AddEdge(a, b, cap);
    }

    return graph;
  }
}
