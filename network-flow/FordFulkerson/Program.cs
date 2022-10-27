using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  public static void Main()
  {
    Flow g = ParseThoreInput();

    g.FordFulkerson();
    int maxFlow = g.MaxFlow();

    Console.WriteLine(maxFlow);
  }

  private static Flow ParseThoreInput()
  {
    var N = int.Parse(Console.ReadLine()!);

    for (var i = 0; i < N; i++)
      Console.ReadLine();

    int source = 0;
    int sink = N-1;
    var g = new Flow(source, sink, N);

    var M = int.Parse(Console.ReadLine()!);
    for (var i = 0; i < M; i++)
    {
      var line = Console.ReadLine()!.Split(" ").Select(int.Parse).ToArray();
      var from = line[0];
      var to = line[1];
      var capacity = (line[2] == -1) ? int.MaxValue : line[2];

      g.AddEdge(from, to, capacity);
      g.AddEdge(to, from, capacity);
    }

    return g;
  }
}
