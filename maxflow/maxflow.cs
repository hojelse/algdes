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

  private static FlowGraphDirectedAdj ParseGraph(int N, int M, int source, int sink)
  {
    var graph = new FlowGraphDirectedAdj(source, sink, N);

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

  private void Augment(Edge[] pathTree, int b)
  {
    foreach (Edge curr in GetPath(pathTree))
      curr.IncreaseFlow(b);
  }

  private int FindBottleneck(Edge[] pathTree)
  {
    var bottleNeck = int.MaxValue;

    foreach (Edge curr in GetPath(pathTree))
      bottleNeck = Math.Min(bottleNeck, curr.w);

    return bottleNeck;
  }

  private bool TryFindPath(int minEdgeWeight, out Edge[] pathTree)
  {
    pathTree = new Edge[N];
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
  private IEnumerable<Edge> GetPath(Edge[] pathTree)
  {
    for (Edge curr = pathTree[this.sink]; curr != null; curr = pathTree[curr.from])
      yield return curr;
  }

  public HashSet<int> GetSourceComponent()
  {
    var visited = new HashSet<int>();
    var stack = new Stack<int>();

    stack.Push(this.source);
    visited.Add(this.source);

    while (stack.TryPop(out int curr))
    {
      foreach (Edge outEdge in adj[curr])
      {
        if (outEdge.w < 1) continue;
        if (visited.Contains(outEdge.to)) continue;

        visited.Add(outEdge.to);
        stack.Push(outEdge.to);
      }
    }

    return visited;
  }

  public HashSet<FlowEdge> GetSolutionEdges()
  {
    var set = new HashSet<FlowEdge>();
    foreach (var kv in adj)
      foreach (var edge in kv.Value)
        if (edge.isForward && edge.reverse.w > 0)
          set.Add(new FlowEdge(edge.from, edge.to, edge.reverse.w));
    return set;
  }
}

public class FlowEdge
{
  public int from;
  public int to;
  public int flow;

  public FlowEdge(int from, int to, int flow)
  {
    this.from = from;
    this.to = to;
    this.flow = flow;
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
