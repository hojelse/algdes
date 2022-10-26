using System;
using System.Collections.Generic;

public class FlowGraphDirectedAdj
{
  public Dictionary<int, List<Edge>> adj;
  public int N;
  public int source;
  public int sink;
  int maxEdgeCapacity = 0;

  public FlowGraphDirectedAdj(int source, int sink, int N)
  {
    this.source = source;
    this.sink = sink;
    this.N = N;
    this.adj = new Dictionary<int, List<Edge>>();
  }

  public void AddEdge(int from, int to, int w)
  {
    this.maxEdgeCapacity = Math.Max(this.maxEdgeCapacity, w);

    adj.TryAdd(from, new List<Edge>());
    adj.TryAdd(to, new List<Edge>());

    (Edge forward, Edge backward) = Edge.BuildEdges(from, to, w);
    adj[from].Add(forward);
    adj[to].Add(backward);
  }

  public int MaxFlow()
  {
    FordFulkerson();

    int sum = 0;
    foreach (var edge in adj[source])
      if(edge.isForward)
        sum += edge.reverse.w;

    return sum;
  }

  public void FordFulkerson()
  {
    for (int minEdgeWeight = this.maxEdgeCapacity; minEdgeWeight > 0; minEdgeWeight /= 2)
      while (TryFindPath(minEdgeWeight, out Edge[] path))
        Augment(path, FindBottleneck(path));
  }

  private void Augment(Edge[] path, int b)
  {
    foreach (Edge curr in GetPath(path))
      curr.IncreaseFlow(b);
  }

  private int FindBottleneck(Edge[] path)
  {
    var bottleNeck = int.MaxValue;

    foreach (Edge curr in GetPath(path))
      bottleNeck = Math.Min(bottleNeck, curr.w);

    return bottleNeck;
  }

  private bool TryFindPath(int minEdgeWeight, out Edge[] path)
  {
    path = new Edge[N];
    var visited = new HashSet<int>();
    var stack = new Stack<int>();

    stack.Push(this.source);
    visited.Add(this.source);

    while (stack.TryPop(out int curr))
    {
      foreach (Edge outEdge in adj[curr])
      {
        if (outEdge.w < minEdgeWeight) continue;
        if (visited.Contains(outEdge.to)) continue;

        path[outEdge.to] = outEdge;
        visited.Add(outEdge.to);
        stack.Push(outEdge.to);

        if (outEdge.to == sink)
          return true;
      }
    }

    return false;
  }

  // Traverse a tree of edges from sink (leaf) to source (root)
  private IEnumerable<Edge> GetPath(Edge[] tree)
  {
    for (Edge curr = tree[this.sink]; curr != null; curr = tree[curr.from])
      yield return curr;
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
