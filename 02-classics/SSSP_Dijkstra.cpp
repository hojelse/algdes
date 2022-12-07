#include <iostream>
#include <vector>
#include <queue>
#include <climits>
using namespace std;
#define inf LLONG_MAX
typedef long long ll;
typedef vector<ll> vi;
typedef vector<vi> mi;
typedef pair<ll, ll> ii;
typedef vector<ii> vii;
typedef vector<vii> mii;

vi sssp_dijkstra(mii& adj, ll& s) {
  vi distTo;
  priority_queue<ii, vii, greater<ii> > pq;

  for (ll i = 0; i < adj.size(); i++)
    distTo.push_back(inf);

  pq.push(ii{0, s});
  distTo[s] = 0;

  while (!pq.empty()) {
    auto [ prio, v1 ] = pq.top();
    pq.pop();
    // relax vertex
    for (ii edge : adj[v1]) {
      auto [v2, w] = edge;
      if (prio > distTo[v2]) continue;
      if (distTo[v2] > distTo[v1] + w) {
        distTo[v2] = distTo[v1] + w;
        pq.push(ii{distTo[v2], v2});
      }
    }
  }
  return distTo;
}

// Test for https://open.kattis.com/problems/shortestpath1
int main(void) {
  ll n, m, q, s;
  while (true)
  {
    cin >> n >> m >> q >> s;
    if (n==0 && m==0 && q==0 && s==0) break;

    // Create adj
    mii adj;
    ll v1, v2, w;
    for (ll i = 0; i < n; i++)
      adj.push_back(vii{});

    for (ll i = 0; i < m; i++) {
      cin >> v1 >> v2 >> w;
      adj[v1].push_back(ii{v2, w});
    }
    
    // Run single source shortest-paths dijkstra
    vector<ll> distTo = sssp_dijkstra(adj, s);

    // Run queries on distTo
    ll queryVertex;
    for (ll i = 0; i < q; i++) {
      cin >> queryVertex;
      if (distTo[queryVertex] == inf)
        cout << "Impossible" << endl;
      else
        cout << distTo[queryVertex] << endl;
    }
    cout << endl;
  }
  return 0;
}