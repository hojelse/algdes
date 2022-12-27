#include "../header.hpp"
// Ford Fulkerson with DFS and bottleneck scaling
struct Edge { ll u, v, c, f; };
struct Flow {
  ll V, E, S, T, M;
  vector<Edge> es;
  mi g;
  Flow(ll V, ll S, ll T)
    : V(V), S(S), T(T), M(0), g(V) {}
  ll add_edge(ll u, ll v, ll c) {
    M = max(M, c);
    ll id = es.size();
    es.push_back(Edge{u, v, c, 0});
    g[u].push_back(id);
    es.push_back(Edge{v, u, 0, 0});
    g[v].push_back(id+1);
    return id;
  }
  ll augment(ll u, ll b, ll m, vi& seen) {
    if (u == T) return b;
    seen[u] = 1;
    for (ll i : g[u]) {
      Edge e = es[i];
      ll d = e.c - e.f;
      if (seen[e.v] || d < m) continue;
      if (ll r = augment(e.v, min(b, d), m, seen)) {
        es[i].f += r;
        es[i^1].f -= r;
        return r;
      }
    }
    return 0;
  }
  ll max_flow() {
    ll flow = 0;
    vi seen;
    for (ll m = M; m > 0; m >>= 1)
      while (ll b = augment(S, inf, m, seen = vi(V, 0)))
        flow += b;
    return flow;
  }
};