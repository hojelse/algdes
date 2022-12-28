#include "ford_fulkerson.cpp"
#include <cmath>
// Bipartite matching on 3 networks
void solve(ll R, ll scen) {
  vector<pair<lf,lf>> robs(R);
  lf x, y;
  for (ll r = 0; r < R; r++)
  {
    cin >> x >> y;
    robs[r] = pair<lf, lf>{x, y};
  }

  ll H;
  cin >> H;

  ll S = R+H;
  ll T = R+H+1;

  Flow g1 = Flow(S+2, S, T);
  Flow g2 = Flow(S+2, S, T);
  Flow g3 = Flow(S+2, S, T);

  for (ll h = 0; h < H; h++)
  {
    cin >> x >> y;
    for (ll r = 0; r < R; r++)
    {
      auto [x1, y1] = robs[r];
      lf blah = abs(x1-x)*abs(x1-x) + abs(y1-y)*abs(y1-y);
      lf dist = 0.0;
      if (blah > 0.0) dist = sqrt(blah);
      if (dist > 200.0) continue;
      if (dist <= 200.0) g3.add_edge(r, R+h, 1);
      if (dist <= 100.0) g2.add_edge(r, R+h, 1);
      if (dist <= 50.0 ) g1.add_edge(r, R+h, 1);
    }
  }

  for (ll r = 0; r < R; r++) {
    g1.add_edge(S, r, 1);
    g2.add_edge(S, r, 1);
    g3.add_edge(S, r, 1);
  }

  for (ll h = 0; h < H; h++) {
    g1.add_edge(R+h, T, 1);
    g2.add_edge(R+h, T, 1);
    g3.add_edge(R+h, T, 1);
  }

  cout << "Scenario " << scen << endl;
  cout << "In 5 seconds "  << g1.max_flow() << " robot(s) can escape" << endl;
  cout << "In 10 seconds " << g2.max_flow() << " robot(s) can escape" << endl;
  cout << "In 20 seconds " << g3.max_flow() << " robot(s) can escape" << endl;
  cout << endl;
}

int main() {
  ll R;
  cin >> R;
  ll h = 1;
  while (R != 0)
  {
    solve(R, h++);
    cin >> R;
  }
  return 0;
}