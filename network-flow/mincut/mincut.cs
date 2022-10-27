using System;
using System.Collections.Generic;
using System.Linq;

class mincut
{
  public static void Main(string[] args)
  {
    Flow g = ParseInput();

    int maxflow = g.MaxFlow();
    var sourceComponent = g.GetSourceComponent();

    Console.WriteLine(sourceComponent.Count);

    foreach (var v in sourceComponent)
      Console.WriteLine(v);
  }

  private static Flow ParseInput()
  {
    var l = Console.ReadLine().Split(" ").Select(int.Parse).ToList();
    int n = l[0];
    int m = l[1];
    int s = l[2];
    int t = l[3];

    Flow g = new Flow(s, t, n);

    for (int i = 0; i < m; i++)
    {
      var line = Console.ReadLine().Split(" ").Select(int.Parse).ToList();
      int u = line[0];
      int v = line[1];
      int w = line[2];

      g.AddEdge(u, v, w);
    }

    return g;
  }
}
