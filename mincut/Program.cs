using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  static List<string> idToName;
  static Dictionary<string, int> nameToId;

  public static void Main(string[] args)
  {
    MaxFlow g = ParseThoreInput();

    // MinCut(g);
    // PrintCutSet(g);
    int max = MaxFlow(g);
    Console.WriteLine($"max: {max}");
  }

  private static MaxFlow ParseThoreInput()
  {
    int N = int.Parse(Console.ReadLine());

    idToName = new List<string>();
    nameToId = new Dictionary<string, int>();

    for (int i = 0; i < N; i++)
      Console.ReadLine();

    int M = int.Parse(Console.ReadLine());

    MaxFlow g = new MaxFlow(0, 54, N);

    for (int i = 0; i < M; i++)
    {
      var l = Console.ReadLine().Split(" ").Select(int.Parse).ToList();
      int u = l[0];
      int v = l[1];
      int c = l[2];

      g.AddEdge(u, v, c);
      g.AddEdge(v, u, c);
    }

    return g;
  }

  private static MaxFlow ParseInput()
  {
    var l = Console.ReadLine().Split(" ").Select(int.Parse).ToList();
    int n = l[0];
    int m = l[1];
    int s = l[2];
    int t = l[3];

    MaxFlow g = new MaxFlow(s, t, n);

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

  private static int MaxFlow(MaxFlow g)
  {
    int maxflow = g.FordFulkerson();
    var edges = g.GetSolutionEdges();
    Console.WriteLine($"{g.n} {maxflow} {edges.Count}");
    foreach (var edge in edges)
    {
      Console.WriteLine($"{edge.from} {edge.to} {edge.flow}");
    }

    return maxflow;
  }

  private static void PrintCutSet(MaxFlow g)
  {
    int maxflow = g.FordFulkerson();
    foreach (var edge in g.GetCutSet())
    {
      Console.WriteLine($"{edge.from} --> {edge.to} w:{edge.flow}");
    }
  }

  private static void MinCut(MaxFlow g)
  {
    int maxflow = g.FordFulkerson();
    var set = g.GetSourceComponent();
    Console.WriteLine(set.Count);
    foreach (var v in set)
    {
      Console.WriteLine(v);
    }
  }
}

class MaxFlow
{
  List<List<Edge>> orig;
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
      this.reverse.flow -= b;
      this.flow += b;
    } else {
      this.w += b;
      this.reverse.w -= b;
      this.flow += b;
      this.reverse.flow -= b;
    }
  }
}
