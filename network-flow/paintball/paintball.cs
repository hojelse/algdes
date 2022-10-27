using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  public static void Main()
  {
    var firstline = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    var n = firstline[0];
    var m = firstline[1];
    var s = 0;
    var t = n+1;

    var g = new Flow(s, t, n*2+2);

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
}
