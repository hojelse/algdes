# 1. Introduction: Some Representative Problems
Matching: set of ordered pair where each item occurs in at most one pair.
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

# 4. Greedy Alorithms
# 5. Divide and Conquer
Applications tends to reduce polynomial algorithms to better polynomial algorithms

# 6. Dynamic Programming
Applications tends to reduce exponential brute-force search down to polynomial time

## 6.1 Weighted Interval Scheduling: A Recursive Procedure
```
OPT(0) = 0
OPT(j) = max(vj + OPT(p(j)), OPT(j-1))
```
Recursive solution top-down
```
M-Compute-Opt(j):
  if j == 0:      return 0
  else if j in M: return M[j]
  else:
    M[j] = max(
      vj + M-Compute-Opt(p(j)),
      M-Compute-Opt(j-1)
    )
    return M[j]
```
Retriving solution from cache
```
Find-Solution(j)
  if j=0:
    return nothing
  else:
    if vj + M[p(j)] >= M[j-1]:
      return j :: Find-Solution(p(j))
    else:
      return P(j-1)
```
## 6.2. Principles of Dynamic Programming: Memoization or Iteration over Subproblems
Iterative solution bottom-up
```
Iterative-Compute-Opt:
  M[0] = 0
  For j = 1,2,...,n:
    M[j] = max(vj + M[p(j)], M[j-1])
```

Subproblem properties
1. P number of subproblems
2. Solution to original problem can be easily computed from solutions to the subproblems
3. There is a natural ordering on subproblems, from "smallest" to "largest". Easy computable current solution from sub-solutions.

## 6.4. Subset Sums and Knapsacks: Adding a Variable
Subset sums. Maximize sum of WEIGHTS of subset S such that sum of WEIGHTS <= W

Knapsack Problem. Maximize sum of VALUES of subset S while sum of WEIGHTS <= W

OPT(i,w) is the optimal solution for subset {1,...,i} and w remaining weight, where 0 <= i <= n and 0 <= w <= W

```c++
void subset_sums(mi& cache, vi& ns, ll N, ll W) {
  for (ll i = 1; i <= N; i++) {
    for (ll w = 0; w <= W; w++) {
      ll wi = ns[i];
      cache[i][w] =
        (w < wi)
        ? cache[i-1][w]
        : max(cache[i-1][w], wi + cache[i][w-wi]);
    }
  }
}
```

Knapsack pseudo-polynomial,
O(nW) time or
O(n*2^m) where m is the number of bits in W

## 6.6. Sequence Alignment
```
OPT(i,j) = min[
    a[xi,yj] + OPT(i-1, j-1),
    d + OPT(i-1, j),
    d + OPT(i, j-1)
  ]
```
```
Alignment(X,Y):
  Array A[0..m][0..n]
  for i in 0..m: A[i,0] = i*d
  for j in 0..n: A[0,j] = j*d
  for j in 1..n:
    for i in 1..m:
      A[i][j] = OPT(i,j)
  return A[m][n]
```
O(mn)

## 6.8. Bellman-ford?

# 8. NP and Computational Intractability
## 8.1. Polynomial-Time Reductions
Problem X is at least as hard as problem Y

Suppose Y <=P X. If X can be solved in polynomial time, then Y can be solved in polynomial time.

Suppose Y <=P X. If Y cannot be solved in polynomial time, then X cannot be solved in polynomial time.

- Max Independent Set
  - Maximum vertex subset S
  - for no edge (u,v), both u and v are in S
- Min Vertex Cover
  - Minimum vertex subset S
  - for all edges (u,v), u or v is in S

Independent Set <=p Vertex Cover
Vertex Cover <=p Independent Set

Given set U of n vertices, and some subsets...
- Min Set Cover
  - Minimize number of subsets
  - Contraint, collectively include every vertex in U
- Max Set Packing
  - Maximize number of subsets
  - Contraint, no overlaps

Independent Set <=p Set Packing
Vertex Cover <=p Set Cover

## 8.2. Reductions via “Gadgets”: The Satisfiability Problem
- 3-SAT
  - SAT but 3 terms in every clause

3-SAT <=p Independent Set

## 8.3. Efficient Certification and the Definition of NP
- finite solution string, s
- size of s, |s|
- decision problem, X, set of true solution strings
- algorithm, A: s -> yes/no, where A(s)=yes iff s in X
- polynomial function, p
- an efficient certifier, B: s,t -> yes/no, polytime 

Certificate t and Certifier B examples
- 3-SAT 
  - t: An assignment, e.g. x1=true, x2=false, x3=false
  - B: Evaluates CNF, (true or false or false) and ... => true/false
- Independent Set
  - t: a set of at least k vertices
  - B: verify no edges between vertices pairwise, O(N^2)
- Set Cover
  - t: a set of at least k sets
  - B: union(the k sets) = U

## 8.4. NP-Complete Problems
X is NP-Complete
X in NP and for all Y in NP, Y <=p X

Circuit SAT
- Given a DAG (with 'input' and 'output' nodes) of boolean operators, does an assigment of inputs exist where the output is true.

Circuit SAT is NP-Complete

Circuit SAT <=p 3-SAT

### 8.4.x. General Strategy for Proving New Problems NP-Complete
Given new problem X
