# Graph
Connectivity                  |  BFS/DFS                            |  O(m+n)
Bipartiteness                 |  BFS/DFS                            |  O(m+n)
Minimum Spanning Tree         |  Kruskal's                          |  O(E log E) time  O(E) space
Minimum Spanning Tree         |  Lazy Prim's                        |  O(E log E) time  O(E) space
Minimum Spanning Tree         |  Red-rule Blue-rule                 |  
Single pair shortest path     |  Dijkstra's shortest path           |  O(E log V) time O(V) space
Single-source shortest paths  |  Dijkstra's shortest paths          |  O(E log V) time O(V) space
SS Longest paths in ew DAGs   |  Dijkstra's shortest path *         |  O(E log V) time O(V) space
* shortest path in this copy is the longest path
# Divide and Conquer
Sorting                       |  Mergesort                          |  O(N log N) time  O(log N) space
Sorting                       |  Randomized Quicksort               |  O(N log N) time  O(N) space
Integer Multiplication        |  Grade-school                       |  Theta(n^2) time
Integer Multiplication        |  Karatsuba multiplication           |  O(n^1.585) time
Closest pair of points        |  Kleinberg-Tardos Closest Pair      |  
# Greedy
Coin changing                 |  Cashier′s
Interval Scheduling           |  Earliest-finish-time-first
Interval Partitioning         |  Earliest-start-time-first
# Dynamic Programming
Weighted Interval Scheduling  |  top-down                           |  O(n log n) time
Subset sums                   |  bottom-up                          |  
Knapsack                      |  bottom-up                          |  O(nW) or O(n*2^m) time, where m is the number of bits in W
Sequence alignment            |  bottom-up                          |  Theta(m n) time
Single pair shortest path     |  Bellman–Ford                       |  O(E V) time  O(V) space
# Network Flows
Minimum-cut                   |  Ford-Fulkerson                     |  O(m n C) time
Maximum-flow                  |  Ford-Fulkerson                     |  O(m n C) time
Maximum Matching              |  Ford-Fulkerson                     |  O(m n C) time
Bipartite Matching            |  Ford-Fulkerson                     |  O(m n) time
Largest Bipartite Matching    |  ?                                  |  O(N^3) time
# NP
## Contraint Satisfaction
Circuit-SAT                   |  NP-Complete
3-SAT                         |  NP-Complete
## Packing and Covering
Independent set               |  NP-Complete
Vertex cover                  |  NP-Complete
Set cover                     |  NP-Complete
Set packing                   |  NP-Complete
## Sequencing
Directed Hamiltonian cycle    |  NP-Complete
Hamiltonian cycle             |  NP-Complete
Hamiltonian path              |  NP-Complete
Longest Path                  |  NP-Hard (decision NP-C exists)
Traveling Salesman Problem    |  NP-Hard (decision NP-C exists)
## Partitioning
Graph 3-color                 |  NP-Complete
Planar 3-color                |  NP-Complete
## Numerical
Subset sums                   |  NP-Complete
Scheduling release/deadline   |  NP-Complete

- Integer Factorization (NP and not P and not NP-Hard)
- Knapsack
- Maximum cut
- 3D matching  |  NP-Complete  |  3-SAT <=p
- Factoring
- Integer linear programming

X can be solved by Y  =>  X reduces to Y   =>  X <=p Y

Karp reduction, Cook-Levin reduction

P             |  Solvable in P time.
NP            |  Verifiable in P time.
NP-Hard       |  At least as hard as the hardest NP problem.
NP-Complete   |  Both NP and NP-hard.
