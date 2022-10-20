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

    int maxFlow = graph.FordFulkerson();

    var solutionEdges = graph.flowEdges.Where(x => x.flow > 0).ToList();

    Console.WriteLine($"{N} {maxFlow} {solutionEdges.Count}");

    solutionEdges.Sort();

    foreach (var edge in solutionEdges)
      Console.WriteLine($"{edge.from} {edge.to} {edge.flow}");
  }

  private static FlowGraph ParseGraph(int N, int M, int source, int sink)
  {
    var graph = new FlowGraph(source, sink);

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

class FlowGraph
{
  public List<Edge> flowEdges = new List<Edge>();
  public Dictionary<int, List<Edge>> adj = new Dictionary<int, List<Edge>>();

  public int source;
  public int sink;

  public FlowGraph(int source, int sink)
  {
    this.source = source;
    this.sink = sink;
  }

  public int FordFulkerson()
  {
    while (TryFindPath(out var path))
      Augment(path);

    int valueOfFlow = 0;
    foreach (var edge in adj[source])
    {
      valueOfFlow += edge.flow;
    }

    return valueOfFlow;
  }

  private int GetMaxFlow()
  {
    int valueOfFlow = 0;

    foreach (var edge in GetCutSet())
      valueOfFlow += edge.flow;

    return valueOfFlow;
  }

  private List<Edge> GetCutSet()
  {
    HashSet<int> expanded = new HashSet<int>();
    Stack<int> stack = new Stack<int>();

    stack.Push(source);

    while (stack.Count != 0)
    {
      if(!stack.TryPop(out var currNode))
        break;

      if(expanded.Contains(currNode)) continue;

      if(!adj.TryGetValue(currNode, out var outgoingEdges) || outgoingEdges.Count == 0)
        continue;

      foreach (var edge in outgoingEdges)
        if(!expanded.Contains(edge.to) && edge.capacity > 0)
          stack.Push(edge.to);

      expanded.Add(currNode);
    }

    List<Edge> cutSet = new List<Edge>();

    foreach (var node in expanded)
    {
      if(!adj.TryGetValue(node, out var outgoingEdges) || outgoingEdges.Count == 0)
        continue;
      
      foreach (var edge in outgoingEdges)
        if(!expanded.Contains(edge.to))
          if(edge.flow != 0)
            cutSet.Add(edge);
    }

    return cutSet;
  }

  private void Augment(IEnumerable<Edge> path)
  {
    int b = bottleneck(path);

    foreach (var edge in path)
    {
      edge.capacity -= b;
      edge.reverse.capacity += b;

      if(edge.isForward) edge.flow += b;
      if(edge.reverse.isForward) edge.reverse.flow -= b;
    }
  }

  private int bottleneck(IEnumerable<Edge> path)
  {
    var minResCap = path.First().capacity;

    foreach (var re in path)
      if(re.capacity < minResCap)
        minResCap = re.capacity;

    return minResCap;
  }

  public void AddEdge(int from, int to, int capacity)
  {
    Edge forwardEdge = new Edge(from, to, capacity, true, 0);
    Edge backwardEdge = new Edge(to, from, 0, false, 0);

    forwardEdge.reverse = backwardEdge;
    backwardEdge.reverse = forwardEdge;

    if (!adj.ContainsKey(from))
      adj.Add(from, new List<Edge>());
    if (!adj.ContainsKey(to))
      adj.Add(to, new List<Edge>());

    adj[from].Add(forwardEdge);
    adj[to].Add(backwardEdge);

    flowEdges.Add(forwardEdge);
  }

  public bool TryFindPath(out IEnumerable<Edge> path1)
  {
    LinkedList<Edge> path = new LinkedList<Edge>();
    path1 = path;
    HashSet<int> expanded = new HashSet<int>();
    Stack<Edge> stack = new Stack<Edge>();

    stack.Push(new Edge(-1, source, -1, true, 0));

    while (true)
    {
      if(!stack.TryPop(out var currEdge))
        break;

      path.AddLast(currEdge);

      var currNode = currEdge.to;

      if (currNode == sink)
      {
        path.RemoveFirst();
        return true;
      }

      if(expanded.Contains(currNode)) continue;

      if(!adj.TryGetValue(currNode, out var outgoingEdges) || outgoingEdges.Count == 0)
      {
        path.RemoveLast();
        continue;
      }

      foreach (var edge in outgoingEdges)
      {
        if(!expanded.Contains(edge.to) && edge.capacity > 0)
          stack.Push(edge);
      }

      expanded.Add(currNode);
    }

    return false;
  }

  private void PrintPath(IEnumerable<Edge> path)
  {
    Console.Write(source);
    foreach (var edge in path)
    {
      Console.Write("->" + edge.to);
    }

    Console.WriteLine();
  }

  public void PrintEdges()
  {
    Console.WriteLine("edges");
    foreach (var entry in adj)
    {
      foreach (var edge in entry.Value)
        if(edge.capacity != 0)
          Console.WriteLine($"{entry.Key} --{edge.capacity}--> {edge.to}");
    }
  }
}

class Edge : IComparable<Edge>
{
  public Edge reverse { get; set; }
  public int from { get; set; }
  public int to { get; set; }
  public int capacity { get; set; }
  public bool isForward { get; set; }
  public int flow { get; set; }

  public Edge(int from, int to, int capacity, bool isForward, int flow)
  {
    this.from = from;
    this.to = to;
    this.capacity = capacity;
    this.isForward = isForward;
    this.flow = flow;
  }

  public int CompareTo(Edge other)
  {
    if (other is Edge)
    {
      var that = (Edge)other;

      if (this.from.CompareTo(that.from) == 0)
        return this.to.CompareTo(that.to);

      return this.from.CompareTo(that.from);
    }
    else
    {
      throw new ArgumentException("Object is not a Edge ");
    }
  }
}
