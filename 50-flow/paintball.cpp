#include "ford_fulkerson.cpp"
// Given an undirected graph.
// Find a directed subgraph such that every node has exactly 1 in-degree,
// and exactly 1 out-degree.

// 1. Transform graph into bipartite graph, by duplicating every node,
//    yielding a 'From' set and a 'To' set.
// 2. Solve bipartite perfect matching with ford fulkerson.

// Time complexity ??
int main() {
  ll N, M;
  cin >> N >> M;
  
  Flow G = Flow(N+N+2, N+N, N+N+1);

  ll A, B;
  for (ll i = 0; i < M; i++)
  {
    cin >> A >> B;
    A--; B--;
    G.add_edge(B, N+A, 1);
    G.add_edge(A, N+B, 1);
  }

  for (ll i = 0; i < N; i++)
  {
    G.add_edge(N+N, i, 1);
    G.add_edge(N+i, N+N+1, 1);
  }
  
  ll maxflow = G.max_flow();

  if (maxflow != N) {
    cout << "Impossible" << endl;
    return 0;
  }

  for (ll i = 0; i < N; i++)
    for (auto id : G.g[i]) {
      auto e = G.es[id];
      if (e.f > 0 && e.v != N+N)
        cout << 1+e.v-N << endl;
    }

  return 0;
}