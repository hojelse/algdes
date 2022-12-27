#include "ford_fulkerson.cpp"
#include <map>
// Given N pairs of integers (a,b) and 3 binary operations {a+b, a-b, a*b}.
// Find N distinct results.

// 1. Create bipartite graph,
//    such that a pair of integers (a,b) is a node in the left set,
//    and a distinct result, r, is a node in the right set,
//    with crossing edges iff. a op b = r
// 2. Solve bipartite matching with Ford-Fulkerson.

// Time complexity ??
int main() {
  ll N;
  cin >> N;

  ll S = N+(3*N);
  ll T = S+1;
  Flow G = Flow(N+(3*N)+2, S, T);

  ll nextId = N;
  map<ll, ll> res_to_id;
  map<ll, ll> id_to_res;
  map<ll, string> eid_to_name;

  ll a, b;
  for (ll i = 0; i < N; i++)
  {
    cin >> a >> b;

    G.add_edge(S, i, 1);

    // handle addition
    ll res = a+b;
    ll rid = -1;
    if (res_to_id.find(res) != res_to_id.end()) {
      rid = res_to_id[res];
    } else {
      rid = nextId;
      res_to_id[res] = nextId;
      id_to_res[nextId] = res;
      G.add_edge(nextId, T, 1);
      nextId++;
    }
    string name = to_string(a) + " + " + to_string(b) + " = " + to_string(res);

    ll eid = G.add_edge(i, rid, 1);

    eid_to_name[eid] = name;

    // handle subtraction
    res = a-b;
    rid = -1;
    if (res_to_id.find(res) != res_to_id.end()) {
      rid = res_to_id[res];
    } else {
      rid = nextId;
      res_to_id[res] = nextId;
      id_to_res[nextId] = res;
      G.add_edge(nextId, T, 1);
      nextId++;
    }
    name = to_string(a) + " - " + to_string(b) + " = " + to_string(res);

    eid = G.add_edge(i, rid, 1);

    eid_to_name[eid] = name;

    // handle multiplication
    res = a*b;
    rid = -1;
    if (res_to_id.find(res) != res_to_id.end()) {
      rid = res_to_id[res];
    } else {
      rid = nextId;
      res_to_id[res] = nextId;
      id_to_res[nextId] = res;
      G.add_edge(nextId, T, 1);
      nextId++;
    }
    name = to_string(a) + " * " + to_string(b) + " = " + to_string(res);

    eid = G.add_edge(i, rid, 1);

    eid_to_name[eid] = name;
  }

  ll maxflow = G.max_flow();
  if (maxflow != N) {
    cout << "impossible" << endl;
    return 0;
  }

  for (ll i = 0; i < N; i++) {
    for (auto eid : G.g[i]) {
      Edge e = G.es[eid];
      if (e.f > 0 && e.v != S) {
        cout << eid_to_name[eid] << endl;
      }
    }
  }

}