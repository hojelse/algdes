using System;
using System.Collections.Generic;

public class FlowGraphDirectedAdj
{
  Dictionary<int, List<Edge>> graph;
  Dictionary<int, List<Edge>> res;
  int N;
  int source;
  int sink;
  int scaler = 0;

  public FlowGraphDirectedAdj(int source, int sink, int N)
  {
    this.source = source;
    this.sink = sink;
    this.N = N;
    this.graph = new Dictionary<int, List<Edge>>();
    this.res = new Dictionary<int, List<Edge>>();
  }

  public void AddEdge(int from, int to, int w)
  {
    this.scaler = Math.Max(this.scaler, w);

    graph.TryAdd(from, new List<Edge>());
    res.TryAdd(from, new List<Edge>());
    res.TryAdd(to, new List<Edge>());

    var origEdge = new Edge(from, to, w);
    graph[from].Add(origEdge);

    (Edge forward, Edge backward) = Edge.BuildEdges(from, to, w);
    res[from].Add(forward);
    res[to].Add(backward);
  }

  public int MaxFlow()
  {
    FordFulkerson();
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

class Edge
{
  public int from;
  public int to;
  public int w;
  public Edge reverse;

  public Edge(int from, int to, int w)
  {
    this.from = from;
    this.to = to;
    this.w = w;
  }

  public static (Edge, Edge) BuildEdges(int from, int to, int w)
  {
    var forward = new Edge(from, to, w);
    var backward = new Edge(to, from, 0);

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
