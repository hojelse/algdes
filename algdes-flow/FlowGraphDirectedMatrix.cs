using System;
using System.Collections.Generic;

public class FlowGraphDirectedMatrix
{
  int[,] graph;
  int[,] res;
  int N;
  int source;
  int sink;
  int capacity = 0;

  public FlowGraphDirectedMatrix(int source, int sink, int N)
  {
    this.source = source;
    this.sink = sink;
    this.N = N;
    graph = new int[N, N];
    res = new int[N, N];
  }

  public void AddEdge(int from, int to, int capacity)
  {
    this.capacity = Math.Max(this.capacity, capacity);
    graph[from, to] = capacity;
    graph[to, from] = 0;
    res[from, to] = capacity;
    res[to, from] = 0;
  }

  public void FordFulkerson()
  {
    while (capacity > 0)
    {
      while (TryFindPath(capacity, out int[] path))
        Augment(path, FindBottleneck(path));

      capacity /= 2;
    }
  }

  public int MaxFlow()
  {
    return GetMinCutCapacity();
  }

  public int GetMinCutCapacity()
  {
    var cutSet = FindCutSet();

    int minCutCapacity = 0;

    foreach (var pair in cutSet)
    {
      var from = pair.Item1;
      var to = pair.Item2;
      minCutCapacity += graph[from, to];
    }

    return minCutCapacity;
  }

  public IEnumerable<(int, int)> FindCutSet()
  {
    var sourceSet = GetSourceSet();

    foreach (var from in sourceSet)
      for (var to = 0; to < N; to++)
        if (graph[from, to] > 0 && !sourceSet.Contains(to))
          yield return (from, to);
  }

  private void Augment(int[] path, int b)
  {
    for (var from = sink; from != 0; from = path[from])
    {
      var to = path[from];
      res[from, to] += b;
      res[to, from] -= b;
    }
  }

  private int FindBottleneck(int[] path)
  {
    var bottleNeck = int.MaxValue;

    for (var to = sink; to != 0; to = path[to])
    {
      var from = path[to];
      bottleNeck = Math.Min(bottleNeck, res[from, to]);
    }

    return bottleNeck;
  }

  // Find path from source to sink using Depth first search
  private bool TryFindPath(int capacity, out int[] path)
  {
    var visited = new HashSet<int>();
    path = new int[N];

    var stack = new Stack<int>();
    stack.Push(source);
    visited.Add(source);

    while (stack.TryPop(out int from))
    {
      for (var to = 0; to < N; to++)
      {
        if (res[from, to] >= capacity && !visited.Contains(to))
        {
          stack.Push(to);
          visited.Add(to);
          path[to] = from;

          if (to == sink)
            return true;
        }
      }
    }

    return false;
  }

  // Find the set of nodes that is connected to the source node
  public HashSet<int> GetSourceSet()
  {
    var visited = new HashSet<int>();
    var stack = new Stack<int>();

    stack.Push(source);
    visited.Add(source);

    while (stack.TryPop(out int from))
    {
      for (var to = 0; to < N; to++)
      {
        if (res[from, to] > 0 && !visited.Contains(to))
        {
          stack.Push(to);
          visited.Add(to);
        }
      }
    }

    return visited;
  }

}
