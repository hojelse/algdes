# Graph (Greedy)
Minimum Spanning Tree         |  Kruskal's                          |  O(E log E) time  O(E) space
Minimum Spanning Tree         |  Lazy Prim's                        |  O(E log E) time  O(E) space
Minimum Spanning Tree         |  Red-rule Blue-rule                 |  
Single pair shortest path     |  Bellman–Ford                       |  O(E V) time  O(V) space
Single pair shortest path     |  Dijkstra's shortest path           |  O(E log V) time O(V) space
Single-source shortest paths  |  Dijkstra's shortest paths          |  O(E log V) time O(V) space
SS Longest paths in ew DAGs   |  Dijkstra's shortest path *         |  O(E log V) time O(V) space
* shortest path in this copy is the longest path

# Greedy
Coin changing                 |  Cashier′s
Interval Scheduling           |  Earliest-finish-time-first
Interval Partitioning         |  Earliest-start-time-first

# Stable matching
Stable Roommate *             |  No algorithm
Stable Marriage **            |  Gale-Shapley propose-reject        |  O(N^2)
* 1 set, 1-to-1 matching, fully ranked
** 2 sets, 1-to-1 matching, fully ranked

# Divide and Conquer
Sorting                       |  Mergesort                          |  O(N log N) time  O(log N) space
Sorting                       |  Randomized Quicksort               |  O(N log N) time  O(N) space
Integer Multiplication        |  Grade-school                       |  Theta(n^2) time
Integer Multiplication        |  Karatsuba multiplication           |  O(n^1.585) time
Closest pair of points        |  Kleinberg-Tardos Closest Pair      |  

# Dynamic Programming
Weighted Interval Scheduling  |  top-down                           |  O(n log n) time
Sequence alignment            |  bottom-up                          |  Theta(m n) time
Knapsack                      |  bottom-up                          |  

# Network Flows
Minimum-cut                   |  Ford-Fulkerson                     |  O(m n C) time
Maximum-flow                  |  Ford-Fulkerson                     |  O(m n C) time
Maximum Matching              |  Ford-Fulkerson                     |  
Bipartite Matching            |  Ford-Fulkerson                     |  O(m n) time

Largest Bipartite Matching    |  ?                                  |  O(N^3) time

# NP
- Karp reduction
- Cook-Levin reduction

X can be solved by Y
X reduces to Y
X <=p Y

P             |  Solvable in P time.
NP            |  Verifiable in P time.
NP-Hard       |  At least as hard as the hardest NP problem.
NP-Complete   |  Both NP and NP-hard.

Co-NP

Circuit-SAT                 |  
3-SAT                       |  NP-Complete
Independent set             |  
Vertex cover                |  
Set cover                   |  O(2^N)
Directed Hamiltonian cycle  |  
Hamiltonian cycle           |  
Hamiltonian path            |  
Traveling Salesman          |  NP-Complete
Graph 3-color               |  
Planar 3-color              |  
Subset-sum                  |  
Scheduling                  |  
Knapsack                    |  NP-Complete

Maximum cut, 3D matching, Factoring, Integer linear programming

