using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  public static void Main()
  {
    // RunThore();
    // RunKattisMinCut();
  }

  // private static void RunKattisMinCut()
  // {
  //   FlowGraphDirectedMatrix g = ParseKattisMinCut();

  //   g.FordFulkerson();
  //   var sourceSet = g.GetSourceSet();

  //   var list = sourceSet.ToList();
  //   list.Sort();
  //   list.Reverse();

  //   Console.WriteLine(list.Count);
  //   foreach (var v in list)
  //     Console.WriteLine(v);

  // }

  // private static FlowGraphDirectedMatrix ParseKattisMinCut()
  // {
  //   var firstline = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
  //   var n = firstline[0];
  //   var m = firstline[1];
  //   var s = firstline[2];
  //   var t = firstline[3];

  //   var g = new FlowGraphDirectedMatrix(s, t, n);

  //   for (int i = 0; i < m; i++)
  //   {
  //     var line = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
  //     var from = line[0];
  //     var to = line[1];
  //     var w = line[2];

  //     g.AddEdge(from, to, w);
  //   }

  //   return g;
  // }

  private static void RunThore()
  {
    FlowGraphDirectedAdj g = ParseThoreInput();

    g.FordFulkerson();
    int maxFlow = g.MaxFlow();

    Console.WriteLine(maxFlow);
  }

  private static FlowGraphDirectedAdj ParseThoreInput()
  {
    var N = int.Parse(Console.ReadLine()!);

    for (var i = 0; i < N; i++)
      Console.ReadLine();

    int source = 0;
    int sink = N-1;
    var g = new FlowGraphDirectedAdj(source, sink, N);

    var M = int.Parse(Console.ReadLine()!);
    for (var i = 0; i < M; i++)
    {
      var line = Console.ReadLine()!.Split(" ").Select(int.Parse).ToArray();
      var from = line[0];
      var to = line[1];
      var capacity = (line[2] == -1) ? int.MaxValue : line[2];

      g.AddEdge(from, to, capacity);
    }

    return g;
  }
}
