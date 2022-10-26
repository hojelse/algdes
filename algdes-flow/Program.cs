using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  public static void Main()
  {
    RunThore();
    // RunKattisMinCut();
    // RunKattisPaintball();
  }

  private static void RunKattisPaintball()
  {
    var firstline = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    var n = firstline[0];
    var m = firstline[1];
    var s = 0;
    var t = n+1;

    var g = new FlowGraphDirectedAdj(s, t, n*2+2);

    for (int i = 0; i < m; i++)
    {
      var line = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
      var from = line[0]; // make zero indexed
      var to = line[1]; // make zero indexed
      var w = 1;

      g.AddEdge(from, t+to, w);
      g.AddEdge(to, t+from, w);
    }

    for (int i = 1; i <= n; i++)
    {
      g.AddEdge(s, i, 1);
      g.AddEdge(t+i, t, 1);
    }

    int maxflow = g.MaxFlow();

    if (maxflow != n)
    {
      Console.WriteLine("Impossible");
      return;
    }

    for (int i = 1; i <= n; i++)
      foreach (var edge in g.adj[i])
        if (edge.to != 0 && edge.reverse.w > 0)
          Console.WriteLine(edge.to-t);
  }

  private static void RunKattisMinCut()
  {
    FlowGraphDirectedMatrix g = ParseKattisMinCut();

    g.FordFulkerson();
    var sourceSet = g.GetSourceSet();

    var list = sourceSet.ToList();
    list.Sort();
    list.Reverse();

    Console.WriteLine(list.Count);
    foreach (var v in list)
      Console.WriteLine(v);

  }

  private static FlowGraphDirectedMatrix ParseKattisMinCut()
  {
    var firstline = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    var n = firstline[0];
    var m = firstline[1];
    var s = firstline[2];
    var t = firstline[3];

    var g = new FlowGraphDirectedMatrix(s, t, n);

    for (int i = 0; i < m; i++)
    {
      var line = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
      var from = line[0];
      var to = line[1];
      var w = line[2];

      g.AddEdge(from, to, w);
    }

    return g;
  }

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
      g.AddEdge(to, from, capacity);
    }

    return g;
  }
}
