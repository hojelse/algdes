#include <iostream>
#include <vector>
#include <set>
#include <map>
#include <list>
#include <queue>
#include <limits>
#include <climits>
using namespace std;

#define inf LLONG_MAX

typedef long long LL;
typedef pair<LL, LL> pq_item;
typedef pair<LL, LL> weighted_edge;
typedef vector<vector<weighted_edge> > digraph;

digraph createAdj(LL n, LL m) {
  digraph adj;
  vector<weighted_edge> empty_neigbors;
  for (LL i = 0; i < n; i++)
    adj.push_back(empty_neigbors);

  LL u, v, w;
  for (LL i = 0; i < m; i++)
  {
    cin >> u >> v >> w;
    weighted_edge e;
    e.first = v;
    e.second = w;
    adj[u].push_back(e);
  }

  return adj;
}

vector<LL> dijkstra_sssp(digraph& adj, LL& s) {
  vector<LL> distTo;
  priority_queue<pq_item, vector<pq_item>, greater<pq_item> > pq;

  for (LL i = 0; i < adj.size(); i++)
    distTo.push_back(inf);

  pq_item pqi;
  pqi.first = 0;
  pqi.second = s;
  pq.push(pqi);
  distTo[s] = 0;

  while (!pq.empty()) {
    pq_item i = pq.top();
    LL pqw = i.first;
    LL u = i.second;
    pq.pop();

    // relax vertex
    for (weighted_edge e : adj[u])
    {
      LL v = e.first;
      LL weight = e.second;
      if (pqw > distTo[v]) continue;
      if (distTo[v] > distTo[u] + weight)
      {
        distTo[v] = distTo[u] + weight;
        pqi.first = distTo[v];
        pqi.second = v;
        pq.push(pqi);
      }
    }
  }
  return distTo;
}

int main(void) {
  LL n, m, q, s;
  while (true)
  {
    cin >> n >> m >> q >> s;
    if (n==0 && m==0 && q==0 && s==0) break;

    digraph adj = createAdj(n, m);
    
    // Run single source shortest-paths dijkstra
    vector<LL> distTo = dijkstra_sssp(adj, s);

    // Run queries on distTo
    LL queryVertex;
    for (LL i = 0; i < q; i++) {
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