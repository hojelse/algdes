#include <iostream>
#include <vector>
#include <algorithm>
#include <climits>
#include <queue>
using namespace std;
typedef long long ll;
typedef unsigned long long ull;
typedef long double lf;
typedef vector<ll> vi;
typedef vector<vi> mi;
typedef pair<ll, ll> ii;
typedef vector<ii> vii;
typedef vector<vii> mii;
#define inf LLONG_MAX
#define REP(i,a,b) for (int i = a; i <= b; i++)
template <class T> T smod(T a, T b) { return (a % b + b) % b; }

struct Edge {
  ll from, to, c, f;
};

struct Flow {
  ll V, E, source, sink;
  vector<Edge> edges;
  vector<vector<ll>> graph;
  ll scalar;

  Flow(ll V, ll E, ll source, ll sink) : V(V), E(E), source(source), sink(sink), graph(V), scalar(0) {}

  void add_edge(ll v1, ll v2, ll c) {
    scalar = max(scalar, c);
    edges.push_back(Edge{v1, v2, c, 0});
    graph[v1].push_back(edges.size()-1);
    edges.push_back(Edge{v2, v1, 0, 0});
    graph[v2].push_back(edges.size()-1);
  }

  bool try_find_path(ll m, vi& path) {
    path = vi(V, -1);
    vi seen(V, false);
    vi stack;

    stack.push_back(source);
    seen[source] = true;

    while (stack.size() > 0) {
      ll currV = stack.back();
      stack.pop_back();
      for (ll edgeId : graph[currV]) {
        Edge outEdge = edges[edgeId];
        if (outEdge.c - outEdge.f < m) continue;
        if (seen[outEdge.to]) continue;

        path[outEdge.to] = edgeId;
        seen[outEdge.to] = true;
        stack.push_back(outEdge.to);

        if (outEdge.to == sink) return true;
      }
    }

    return false;
  }

  void ford_fulkerson() {
    for (ll m = scalar; m > 0; m >>= 1) {
      vi path;
      while (try_find_path(m, path)) {
        ll b = inf;
        for (ll edgeId = path[sink]; edgeId != -1; edgeId = path[edges[edgeId].from])
          b = min(b, edges[edgeId].c - edges[edgeId].f);
        for (ll edgeId = path[sink]; edgeId != -1; edgeId = path[edges[edgeId].from]) {
          edges[edgeId].f += b;
          edges[edgeId^1].f -= b;
        }
      }
    }
  }

  ll max_flow() {
    ford_fulkerson();

    ll sum = 0;
    for (ll idx : graph[source]) {
      if (idx % 2 == 0) {
        sum += edges[idx].f;
      }
    }
    return sum;
  }
};

int main() {
  ll V, E, src, sin;
  cin >> V >> E >> src >> sin;

  Flow G = Flow(V, E, src, sin);
  ll v1, v2, c;
  for (ll i = 0; i < E; i++) {
    cin >> v1 >> v2 >> c;
    G.add_edge(v1, v2, c);
  }

  ll maxflow = G.max_flow();

  vector<Edge> ve;
  for (auto e : G.edges)
    if (e.f > 0)
      ve.push_back(e);

  cout << V << " " << maxflow << " " << ve.size() << endl;

  for (auto e : ve)
    cout << e.from << " " << e.to << " " << e.f << endl;

  return 0;
}