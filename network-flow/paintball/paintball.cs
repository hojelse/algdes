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
      foreach (var edge in g.res[i])
        if (edge.to != 0 && edge.reverse.w > 0)
          Console.WriteLine(edge.to-t);

  }
}

public class FlowGraphDirectedAdj
{
  public Dictionary<int, List<Edge>> res;
  public int N;
  public int source;
  public int sink;
  int scaler = 0;

  public FlowGraphDirectedAdj(int source, int sink, int N)
  {
    this.source = source;
    this.sink = sink;
    this.N = N;
    this.res = new Dictionary<int, List<Edge>>();
  }

  public void AddEdge(int from, int to, int w)
  {
    this.scaler = Math.Max(this.scaler, w);

    res.TryAdd(from, new List<Edge>());
    res.TryAdd(to, new List<Edge>());

    (Edge forward, Edge backward) = Edge.BuildEdges(from, to, w);
    res[from].Add(forward);
    res[to].Add(backward);
  }

  public int MaxFlow()
  {
    FordFulkerson();

    int sum = 0;
    foreach (var edge in res[source])
      if(edge.isForward)
        sum += edge.reverse.w;
    return sum;
  }

  public void FordFulkerson()
  {
    while (scaler > 0)
    {
      while (TryFindPath(scaler, out Edge[] path))
      {
        int b = FindBottleneck(path);
        Augment(path, b);
      }

      scaler /= 2;
    }
  }

  private void Augment(Edge[] path, int b)
  {
    for (Edge curr = path[this.sink]; curr != null; curr = path[curr.from])
    {
      curr.IncreaseFlow(b);
    }
  }

  private int FindBottleneck(Edge[] path)
  {
    var bottleNeck = int.MaxValue;

    for (Edge curr = path[this.sink]; curr != null; curr = path[curr.from])
    {
      bottleNeck = Math.Min(bottleNeck, curr.w);
    }

    return bottleNeck;
  }

  private bool TryFindPath(int scaler, out Edge[] path)
  {
    var visited = new HashSet<int>();
    path = new Edge[N];

    var stack = new Stack<int>();
    stack.Push(this.source);
    visited.Add(this.source);

    while (stack.TryPop(out int curr))
    {
      foreach (Edge outEdge in res[curr])
      {
        int neighbor = outEdge.to;

        if (outEdge.w >= this.scaler && !visited.Contains(neighbor))
        {
          stack.Push(neighbor);
          visited.Add(neighbor);
          path[neighbor] = outEdge;

          if (neighbor == sink)
            return true;
        }
      }
    }

    return false;
  }
}

public class Edge
{
  public int from;
  public int to;
  public int w;
  public bool isForward;
  public Edge reverse;

  public Edge(int from, int to, int w, bool isForward)
  {
    this.from = from;
    this.to = to;
    this.w = w;
    this.isForward = isForward;
  }

  public static (Edge, Edge) BuildEdges(int from, int to, int w)
  {
    var forward = new Edge(from, to, w, true);
    var backward = new Edge(to, from, 0, false);

    forward.reverse = backward;
    backward.reverse = forward;

    return (forward, backward);
  }

  public void IncreaseFlow(int b)
  {
    this.w -= b;
    this.reverse.w += b;
  }
}
