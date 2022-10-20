// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;

// class FlowGraph
// {
//   public Dictionary<int, List<Edge>> adj = new Dictionary<int, List<Edge>>();

//   public int source;
//   public int sink;

//   public FlowGraph(int source, int sink)
//   {
//     this.source = source;
//     this.sink = sink;
//   }

//   public int FordFulkerson()
//   {
    
//     while (TryFindPath(out var path))
//     {
//       Augment(path);
//     }

//     int valueOfFlow = 0;

//     foreach (var edge in this.adj[sink])
//     {
//       valueOfFlow += edge.residualCapacity;
//     }

//     return valueOfFlow;
//   }

//   private void Augment(IEnumerable<Edge> path)
//   {
//     int b = bottleneck(path);

//     foreach (var edge in path)
//     {
//       edge.residualCapacity -= b;
//       edge.reverse.residualCapacity += b;
//     }
//   }

//   private int bottleneck(IEnumerable<Edge> path)
//   {
//     var minResCap = path.First().residualCapacity;

//     foreach (var re in path)
//       if(re.residualCapacity < minResCap)
//         minResCap = re.residualCapacity;

//     return minResCap;
//   }

//   public void AddEdge(int from, int to, int capacity)
//   {
//     Edge forwardEdge = new Edge(from, to, capacity);
//     Edge backwardEdge = new Edge(to, from, 0);

//     forwardEdge.reverse = backwardEdge;
//     backwardEdge.reverse = forwardEdge;

//     if (!adj.ContainsKey(from))
//       adj.Add(from, new List<Edge>());
//     if (!adj.ContainsKey(to))
//       adj.Add(to, new List<Edge>());

//     adj[from].Add(forwardEdge);
//     adj[to].Add(backwardEdge);
//   }

//   public bool TryFindPath(out IEnumerable<Edge> path1)
//   {
//     LinkedList<Edge> path = new LinkedList<Edge>();
//     path1 = path;
//     HashSet<int> expanded = new HashSet<int>();
//     Stack<Edge> stack = new Stack<Edge>();

//     stack.Push(new Edge(-1, source, -1));

//     while (true)
//     {
//       if(!stack.TryPop(out var currEdge))
//         break;

//       path.AddLast(currEdge);

//       var currNode = currEdge.to;

//       if (currNode == sink)
//       {
//         path.RemoveFirst();
//         return true;
//       }

//       if(expanded.Contains(currNode)) continue;

//       if(!adj.TryGetValue(currNode, out var outgoingEdges) || outgoingEdges.Count == 0)
//       {
//         path.RemoveLast();
//         continue;
//       }

//       foreach (var edge in outgoingEdges)
//       {
//         if(!expanded.Contains(edge.to) && edge.residualCapacity > 0)
//           stack.Push(edge);
//       }

//       expanded.Add(currNode);
//     }

//     return false;
//   }

//   private void PrintPath(IEnumerable<Edge> path)
//   {
//     Console.Write(source);
//     foreach (var edge in path)
//     {
//       Console.Write("->" + edge.to);
//     }

//     Console.WriteLine();
//   }

//   public void PrintEdges()
//   {
//     Console.WriteLine("edges");
//     foreach (var entry in adj)
//     {
//       foreach (var edge in entry.Value)
//         if(edge.residualCapacity != 0)
//           Console.WriteLine($"{entry.Key} --{edge.residualCapacity}--> {edge.to}");
//     }
//   }
// }

// class Edge
// {
//   public Edge reverse { get; set; }
//   public int from { get; set; }
//   public int to { get; set; }
//   public int residualCapacity { get; set; }

//   public Edge(int from, int to, int residualCapacity)
//   {
//     this.from = from;
//     this.to = to;
//     this.residualCapacity = residualCapacity;
//   }
// }
