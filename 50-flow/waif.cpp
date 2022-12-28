#include "ford_fulkerson.cpp"
int main() {
  ll N, M, P;
  cin >> N >> M >> P;
  
  ll S = N+M+P;
  ll T = S+1;
  Flow G = Flow(S+2, S, T);

  for (ll kid = 0; kid < N; kid++)
  {
    ll k;
    cin >> k;
    ll toy;
    for (ll j = 0; j < k; j++) {
      cin >> toy;
      G.add_edge(kid, N+toy-1, 1);
    }
    G.add_edge(S, kid, 1);
  }
  
  vi seen_toy(M, 0);
  for (ll cat = 0; cat < P; cat++)
  {
    ll l;
    cin >> l;
    ll toy;
    for (ll j = 0; j < l; j++) {
      cin >> toy;
      seen_toy[toy-1] = 1;
      G.add_edge(N+toy-1, N+M+cat, 1);
    }
    ll r;
    cin >> r;
    G.add_edge(N+M+cat, T, r);
  }

  for (ll i = 0; i < seen_toy.size(); i++)
    if (!seen_toy[i])
      G.add_edge(N+i, T, 1);


  cout << G.max_flow() << endl;

  return 0;
}