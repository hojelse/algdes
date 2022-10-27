using System;
using System.Collections.Generic;

public class Flow
{
  public Dictionary<int, List<FlowEdge>> adj;
  public int N;
  public int source;
  public int sink;
  int maxEdgeCapacity = 0;

  public Flow(int source, int sink, int N)
  {
    this.source = source;
    this.sink = sink;
    this.N = N;
    this.adj = new Dictionary<int, List<FlowEdge>>();
  }

  public void AddEdge(int from, int to, int w)
  {
    this.maxEdgeCapacity = Math.Max(this.maxEdgeCapacity, w);

    adj.TryAdd(from, new List<FlowEdge>());
    adj.TryAdd(to, new List<FlowEdge>());

    (FlowEdge forward, FlowEdge backward) = FlowEdge.BuildEdges(from, to, w);
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
      while (TryFindPath(minEdgeWeight, out FlowEdge[] path))
        Augment(path, FindBottleneck(path));
  }

  private void Augment(FlowEdge[] pathTree, int b)
  {
    foreach (FlowEdge curr in GetPath(pathTree))
      curr.IncreaseFlow(b);
  }

  private int FindBottleneck(FlowEdge[] pathTree)
  {
    var bottleNeck = int.MaxValue;

    foreach (FlowEdge curr in GetPath(pathTree))
      bottleNeck = Math.Min(bottleNeck, curr.w);

    return bottleNeck;
  }

  private bool TryFindPath(int minEdgeWeight, out FlowEdge[] pathTree)
  {
    pathTree = new FlowEdge[N];
    var visited = new HashSet<int>();
    var stack = new Stack<int>();

    stack.Push(this.source);
    visited.Add(this.source);

    while (stack.TryPop(out int curr))
    {
      foreach (FlowEdge outEdge in adj[curr])
      {
        if (outEdge.w < minEdgeWeight) continue;
        if (visited.Contains(outEdge.to)) continue;

        pathTree[outEdge.to] = outEdge;
        visited.Add(outEdge.to);
        stack.Push(outEdge.to);

        if (outEdge.to == sink)
          return true;
      }
    }

    return false;
  }

  // Traverse a tree of edges from sink (leaf) to source (root)
  private IEnumerable<FlowEdge> GetPath(FlowEdge[] pathTree)
  {
    for (FlowEdge curr = pathTree[this.sink]; curr != null; curr = pathTree[curr.from])
      yield return curr;
  }

  public HashSet<OriginalEdge> GetSolutionEdges()
  {
    var set = new HashSet<OriginalEdge>();
    foreach (var kv in adj)
      foreach (var edge in kv.Value)
        if (edge.isForward && edge.reverse.w > 0)
          set.Add(new OriginalEdge(edge.from, edge.to, edge.reverse.w));
    return set;
  }

  public HashSet<int> GetSourceComponent()
  {
    var visited = new HashSet<int>();
    var stack = new Stack<int>();

    stack.Push(this.source);
    visited.Add(this.source);

    while (stack.TryPop(out int curr))
    {
      foreach (var outEdge in adj[curr])
      {
        if (outEdge.w < 1) continue;
        if (visited.Contains(outEdge.to)) continue;

        visited.Add(outEdge.to);
        stack.Push(outEdge.to);
      }
    }

    return visited;
  }
}

public class OriginalEdge
{
  public int from;
  public int to;
  public int flow;

  public OriginalEdge(int from, int to, int flow)
  {
    this.from = from;
    this.to = to;
    this.flow = flow;
  }
}

public class FlowEdge
{
  public int from;
  public int to;
  public int w;
  public bool isForward;
  public FlowEdge reverse;

  public FlowEdge(int from, int to, int w, bool isForward)
  {
    this.from = from;
    this.to = to;
    this.w = w;
    this.isForward = isForward;
  }

  public static (FlowEdge, FlowEdge) BuildEdges(int from, int to, int w)
  {
    var forward = new FlowEdge(from, to, w, true);
    var backward = new FlowEdge(to, from, 0, false);

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
