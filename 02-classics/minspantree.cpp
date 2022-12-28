#include "../header.hpp"
struct Edge { ll u, v, w; };
struct UndirectedEdgeWeigtedGraph {
  ll V;
  vector<vector<Edge>> g;
  UndirectedEdgeWeigtedGraph(ll V)
    : V(V), g(V) {}
  void add_edge(ll u, ll v, ll w) {
    g[u].push_back(Edge{u, v, w});
    g[u].push_back(Edge{v, u, w});
  }
};
struct union_find
{
  const int N;
  vi p;

  union_find(int n) : N(n), p(n, -1) { }

  int find(int x) {
    return p[x] < 0 ? x : p[x] = find(p[x]);
  }

  void uni(int x, int y) {
    int i = find(x);
    int j = find(y);
    if (i == j) return;
    if (p[i] < p[j]) swap(i, j);
    p[j] += p[i];
    p[i] = j;
  }

  int size(int x) {
    return -p[find(x)];
  }
};

ll kruskal(UndirectedEdgeWeigtedGraph& G, vector<Edge>& mst, ll& W) {
  if (G.V==1) return true;
  vector<Edge> es;
  for (auto l : G.g)
    for (auto e : l)
      es.push_back(e);
  sort(es.begin(), es.end(),
    [](Edge& a, Edge& b){return a.w < b.w;});
  union_find uf = union_find(G.V);
  for (auto edge : es) {
    if (mst.size() == G.V-1) return true;
    if (uf.find(edge.u) == uf.find(edge.v)) continue;
    uf.uni(edge.u, edge.v);
    mst.push_back(edge);
    W += edge.w;
  }
  return false;
}

bool cmp_by_u_then_v(const Edge& a, const Edge& b) {
  if (a.u == b.u) return a.v < b.v;
  return a.u < b.u;
}
int main() {
  ll n, m;
  while (true) {
    cin >> n >> m;
    if (n == 0 && m == 0) return 0;

    UndirectedEdgeWeigtedGraph G = UndirectedEdgeWeigtedGraph(n);

    ll u, v, w;
    for (ll i = 0; i < m; i++) {
      cin >> u >> v >> w;
      G.add_edge(u, v, w);
    }

    vector<Edge> mst;
    ll W = 0;
    if (kruskal(G, mst, W)) {
      cout << W << endl;
      // Transform edges such that u < v
      for (ll i = 0; i < mst.size(); i++) {
        Edge e = mst[i];
        if (e.u > e.v) mst[i] = Edge{e.v, e.u, e.w};
      }
      // sort edges in lexicographic order
      sort(mst.begin(), mst.end(), cmp_by_u_then_v);
      // print edges
      for (auto [u, v, w] : mst) cout << u << " " << v << endl;
    } else {
      cout << "Impossible" << endl;
    }
  }
  return 0;
}