using System;
using System.Collections.Generic;
using System.Linq;

class paintball
{
  static void Main(string[] args)
  {
    var tokens = Console.ReadLine().Split();
    int N = int.Parse(tokens[0]);
    int M = int.Parse(tokens[1]);

    var graph = ParseGraph(N, M);

    int maxFlow = graph.FordFulkerson();

    foreach (var edge in graph.GetSolutionEdges())
    {
      Console.WriteLine($"{edge.from} --> {edge.to} w:{edge.flow}");
    }

    Console.WriteLine("maxflow" + maxFlow);

    if(maxFlow == N)
    {
      for (int i = 1; i <= N; i++)
        Console.WriteLine(graph.orig[i].Find(x => x.to != 0 && x.flow != 0)?.to - N);
    }
    else
      Console.WriteLine("Impossible");
  }

  private static MaxFlow ParseGraph(int N, int M)
  {
    int source = 0;
    int sink = N*2+1;

    var graph = new MaxFlow(source, sink, N*2+2);

    for (int i = 0; i < M; i++)
    {
      var tokens = Console.ReadLine().Split();
      var a = int.Parse(tokens[0]);
      var b = int.Parse(tokens[1]);

      graph.AddEdge(a, b+N, 1);
      graph.AddEdge(b, a+N, 1);
    }

    for (int i = 1; i <= N; i++)
      graph.AddEdge(source, i, 1);

    for (int i = 1; i <= N; i++)
      graph.AddEdge(i+N, sink, 1);

    return graph;
  }
}

class MaxFlow
{
  public List<List<Edge>> orig;
  List<List<Edge>> res;
  int s;
  int t;
  public int n;

  public MaxFlow(int s, int t, int n)
  {
    this.s = s;
    this.t = t;
    this.n = n;
    res = new List<List<Edge>>();
    for (int i = 0; i < n; i++) res.Add(new List<Edge>());
    orig = new List<List<Edge>>();
    for (int i = 0; i < n; i++) orig.Add(new List<Edge>());
  }

  public void AddEdge(int u, int v, int w)
  {
    (Edge forward, Edge backward) = Edge.BuildEdges(u, v, w);

    res[u].Add(forward);
    orig[u].Add(forward);
    res[v].Add(backward);
  }

  public int FordFulkerson()
  {
    while(TryFindPath(out LinkedList<Edge> path))
    {
      int b = bottleneck(path);

      foreach (var edge in path)
        edge.IncFlow(b);
    }

    return orig[s].Sum(x => x.flow);
  }

  public List<Edge> GetSolutionEdges()
  {
    List<Edge> solutionEdges = new List<Edge>();

    foreach (var neighbors in orig)
      foreach (var edge in neighbors)
        if (edge.flow > 0)
          solutionEdges.Add(edge);

    return solutionEdges;
  }

  public List<Edge> GetCutSet()
  {
    var visited = GetSourceComponent();

    List<Edge> cutset = new List<Edge>();

    foreach (int v in visited)
      foreach (Edge e in orig[v])
        if(!visited.Contains(e.to))
          cutset.Add(e);

    return cutset;
  }

  public HashSet<int> GetSourceComponent()
  {
    HashSet<int> visited = new HashSet<int>();
    Stack<Edge> stack = new Stack<Edge>();

    var e = new Edge(-1, s, -1, true);
    var v = s;
    stack.Push(e);

    while (stack.Count > 0)
    {
      e = stack.Pop();
      v = e.to;

      if (visited.Contains(v)) continue;

      var neighbors = res[v];
      foreach (var edge in neighbors)
      {
        if (edge.w == 0) continue;
        if (visited.Contains(edge.to)) continue;
        stack.Push(edge);
      }

      visited.Add(v);
    }

    return visited;
  }

  private int bottleneck(LinkedList<Edge> path)
  {
    var minW = path.First().w;

    foreach (var edge in path)
      if(edge.w < minW)
        minW = edge.w;

    return minW;
  }

  private bool TryFindPath(out LinkedList<Edge> path)
  {
    HashSet<int> visited = new HashSet<int>();
    Stack<Edge> stack = new Stack<Edge>();
    path = new LinkedList<Edge>();

    var e = new Edge(-1, s, -1, true);
    var v = s;
    stack.Push(e);

    while (stack.Count > 0)
    {
      e = stack.Pop();
      v = e.to;

      if (this.s != v) path.AddLast(e);

      if (v == this.t) return true;

      if (visited.Contains(v)) continue;

      var neighbors = res[v];
      foreach (var edge in neighbors)
      {
        if (edge.w == 0) continue;
        if (visited.Contains(edge.to)) continue;
        stack.Push(edge);
      }

      visited.Add(v);
    }

    return false;
  }
}

class Edge
{
  public int from { get; private set; }
  public int to { get; private set; }
  public int w { get; private set; }
  public Edge reverse { get; private set; }
  public bool forward { get; private set; }
  public int flow = 0;

  public Edge(int u, int v, int w, bool forward)
  {
    this.from = u;
    this.to = v;
    this.w = w;
    this.forward = forward;
  }

  public static (Edge, Edge) BuildEdges(int u, int v, int w)
  {
    var forward = new Edge(u, v, w, true);
    var backward = new Edge(v, u, 0, false);

    forward.reverse = backward;
    backward.reverse = forward;

    return (forward, backward);
  }

  internal void IncFlow(int b)
  {
    if(forward)
    {
      this.w -= b;
      this.reverse.w += b;
      this.flow += b;
    } else {
      this.w += b;
      this.reverse.w -= b;
      this.reverse.flow -= b;
    }
  }
}

