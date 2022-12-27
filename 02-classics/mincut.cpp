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
void dfs(Flow G, ll u, vi& seen) {
  seen[u] = 1;
  for (ll i : G.g[u]) {
    Edge e = G.es[i];
    if (e.c - e.f <= 0 || seen[e.v]) continue;
    dfs(G, e.v, seen);
  }
}
int main() {
  ll N, M, S, T;
  cin >> N >> M >> S >> T;

  Flow G = Flow(N, S, T);

  ll u, v, w;
  for (ll i = 0; i < M; i++) {
    cin >> u >> v >> w;
    G.add_edge(u, v, w);
  }
  G.max_flow();

  // for (Edge e : G.es) {
  //   cout << e.u << "->" << e.v << " c:" << e.c << " f:" << e.f << endl;
  // }

  vi seen(N, false);
  dfs(G, S, seen);

  ll c = 0;
  for (ll i = 0; i < seen.size(); i++)
    if (seen[i])
      c++;
  cout << c << endl;
  for (ll i = 0; i < seen.size(); i++)
    if (seen[i])
      cout << i << endl;

  return 0;
}