# 1. Introduction: Some Representative Problems
## 1.1. A First Problem: Stable matching
General stable matching problem:
- match 1 company to K companies
- each with S available slots
- for N companies and M applicants

Stable Marriage problem:
- perfect bipartite matching
- N proposers to N rejecters
- fully ranked / no ties

Gale-Shapley algorithm
- GS terminates after at most n^2 iterations of the While loop
- The sequence of people to whom a rejector is engaged gets better and better.
- The sequence of people to whom a proposer proposes gets worse and worse.
- But proposers have unfair favoring for some instances, proposers will end up happy and rejectors unhappy.
- Finding multiple stable matchings? Not achievable by any natural algorithm
- How we choose the next free proposer doesn't affect the matching

## 1.2. Five Representative Problems
Overview
1. Interval Scheduling can be solved greedily
2. Weighted Intervals Scheduling can be solved with dynamic programming
3. Bipartite Matching can be solved by network flow
4. Independent Set is NP-complete
  - Interval Scheduling reduces to Independent Set, where intervals are nodes and edges are if they overlap
  - Bipartite Matching reduces to Independent Set, where edges in BM is edges in ISET, and edges in BM with a common end is nodes in ISET.
5. Competitive Facility Location is PSPACE-complete (meaning, no short certificate, i think (page 8))

# 2. Basics of Algorithm Analysis
## 2.1. Computational Tractability
For a processor performing a million high-level instructions per second
|   size|       n | n log n |     n^2 |     n^3 |   1.5^n |     2^n |      n! |
|:-:    |:-:      |:-:      |:-:      |:-:      |:-:      |:-:      |:-:      |
|     10| < 1 sec | < 1 sec | < 1 sec | < 1 sec | < 1 sec | < 1 sec |   4 sec |
|     30| < 1 sec | < 1 sec | < 1 sec | < 1 sec | < 1 sec |  18 min |       x |
|     50| < 1 sec | < 1 sec | < 1 sec | < 1 sec |  11 min |
|   10^2| < 1 sec | < 1 sec | < 1 sec |   1 sec |
|   10^3| < 1 sec | < 1 sec | < 1 sec |  18 min |
|   10^4| < 1 sec | < 1 sec |   2 min |
|   10^5| < 1 sec |   2 sec |
|   10^6|   1 sec |  20 sec |

## 2.2. Asymptotic Order of Growth
- Big O : asymptotic upper bound
- Omega : asymptotic lower bound
- Theta : asymptotic tight bound

log2 n = logb n / logb a


# 3. Graphs
## 3.1. Basic Definitions and Applications
- path        : sequence other nodes connected by edges v0, v1, ..., vk
- s-t path    : path from node s to node t
- simple path : path with distinct nodes
- connected   : exist path between every pair of nodes, 1 connected component
- tree        : connected, no cycles, V = N-1

## 3.2. Graph Connectivity and Graph Traversal
The s-t connectivity or the Maze-Solving problem
- Breadth-first-search
  - generates BFS tree
  - also a connected component R of graph G containing s
- Depth-first-search
  - DFS tree
  - same R

Set of All Connected Components
- repeated BFS or DFS, with new yet-not-seen node, until every node is seen

## 3.3. Implementing Graph Traversal Using Queues and Stacks
Running time for connected graphs O(m+n) = O(m) since m >= n-1

- Sparse graphs    : m much smaller than n^2
- Adjacency matrix : O(n^2) space
- Adjacency list   : O(m+n) space, great when sparse graph

- BFS : Stack
- DFS : Queue

## 3.4. Testing biparititeness: An Application of BFS
- A bipartite graph cannot contain an odd cycle.
- Bipartite is the same as 2-colorability

In BFS color odd layers red and even layers blue. Then afterwards check if all edges have different colored nodes. O(m+n) + O(m)

## 3.5. Connectivity in Directed Graphs
- BFS computes a tree, encoding whether there is a path from s to t (not whether theres a path from t to s).

Given s which nodes has a path to s? Reverse all edges in G, then run BFS.

- Strongly connected : if there exists a path from u to v, and v to u, for all pairs (u, v)
- Strong component : Subset of G which is strongly connected

Run BFS on G and G-reverse if both searches reaches all nodes, then G is strongly connected, if not then the intersection of both searches is a strong component of G.

For any two nodes s and t in a directed graph, their strong components are either identical or disjoint.

Compute the strong components for all nodes is possible in O(m+n)

## 3.6. DAGs and Topological Ordering
- If G has a topological ordering, then G is a DAG.
- DAGS can encode happens-before-relations or dependency relations

Topological Ordering in O(m+n)
- keep indegrees of all nodes
- find vertex v with indegree=0 
- remove v from G
- decrement all indegree of all neighbors of v
- repeat until no active nodes

# Greedy Alorithms
## 4.1. 


