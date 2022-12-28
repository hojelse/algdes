#include "ford_fulkerson.cpp"
#include <cmath>
// Bipartite matching on 3 networks
void solve(ll N, ll M, ll SEC, ll V) {
  ll R = N;
  ll H = M;
  vector<pair<lf,lf>> robs(R);
  lf x, y;
  for (ll r = 0; r < R; r++)
  {
    cin >> x >> y;
    robs[r] = pair<lf, lf>{x, y};
  }

  ll S = R+H;
  ll T = R+H+1;

  Flow g1 = Flow(S+2, S, T);

  for (ll h = 0; h < H; h++)
  {
    cin >> x >> y;
    for (ll r = 0; r < R; r++)
    {
      auto [x1, y1] = robs[r];
      lf blah = abs(x1-x)*abs(x1-x) + abs(y1-y)*abs(y1-y);
      lf dist = 0.0;
      if (blah > 0.0) dist = sqrt(blah);
      if (dist <= SEC*V ) g1.add_edge(r, R+h, 1);
    }
  }

  for (ll r = 0; r < R; r++) {
    g1.add_edge(S, r, 1);
  }

  for (ll h = 0; h < H; h++) {
    g1.add_edge(R+h, T, 1);
  }

  cout << N - g1.max_flow() << endl;
}

int main() {
  ll N, M, S, V;
  while (cin >> N >> M >> S >> V)
  {
    solve(N, M, S, V);
  }
  return 0;
}