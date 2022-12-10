// not working: https://open.kattis.com/submissions/10098587
// #include <bits/stdc++.h>
#include <iostream>
#include <vector>
#include <map>
#include <list>
#include <algorithm>
#include <set>
using namespace std;
typedef long long ll;
typedef unsigned long long ull;
typedef long double lf;
typedef vector<ll> vi;
typedef vector<vi> mi;
typedef pair<ll, ll> ii;
typedef vector<ii> vii;
typedef vector<vii> mii;
template <class T> T smod(T a, T b) { return (a % b + b) % b; }
int main(void) {
  ll X, Y;
  cin >> X >> Y;

  if (Y < X) {
    cout << "impossible" << endl;
    return 0;
  }

  list<ll> singles;            // initially all xs
  for (ll x = 0; x < X; x++)
    singles.push_back(x);
  vi c(X, 0);                  // initially all |Y|
  mi next(X, vi(Y, 0));        // next[x] ys from low to high
  mi rank(Y, vi(X, 0));        // rank[y][x] gives rank higher is better
  vi y_x(Y, -1);
  vi x_y(X, -1);

  ll D, E;
  cin >> D >> E;

  set<ll> all_ys;
  for (ll y = 0; y < Y; y++) all_ys.insert(y);

  vector<set<ll>> not_seen;
  for (ll i = 0; i < X; i++)
    not_seen.push_back(all_ys);

  map<ll, ll> play_state;

  ll s, x, y;
  for (ll i = 0; i < E; i++)
  {
    cin >> s >> x >> y;
    x--; y--;

    if (y != -1) {
      next[x].push_back(y);
      not_seen[x].erase(y);
    }

    ll rem = D-s;
    if (play_state.find(x) != play_state.end()) {
      rank[play_state[x]][x] -= rem;
    }

    if (y == -1) {
      rank[play_state[x]][x] -= rem;
      play_state.erase(x);
    } else {
      play_state[x] = y;
      rank[y][x] += rem;
    }
  }

  for (ll x = 0; x < X; x++)
    for (auto y : not_seen[x])
      next[x].push_back(y);

  for (ll x = 0; x < X; x++)
  {
    vii sorted;
    for (ll y = 0; y < Y; y++)
      sorted.push_back(ii{next[x][y], y});
    sort(sorted.begin(), sorted.end());

    for (ll y = 0; y < Y; y++)
      next[x][y] = sorted[y].second;
  }

  while (!singles.empty())
  {
    ll x = singles.front();
    ll curr = c[x]++;
    if (curr == Y) {
      singles.pop_front();
      continue;
    }
    ll y = next[x][curr];
    if(y_x[y] == -1) {
      y_x[y] = x;
      x_y[x] = y;
      singles.pop_front();
    } else {
      ll currX = y_x[y];
      if (rank[currX] < rank[x]) continue;
      singles.pop_front(); singles.push_back(currX);
      x_y[currX] = -1;
      x_y[x] = y;
      y_x[y] = x;
    }
  }

  for (ll x = 0; x < X; x++) {
    if (x_y[x] == -1) {
      cout << "impossible" << endl;
      return 0;
    }
  }

  for (ll x = 0; x < X; x++)
    cout << x_y[x]+1 << " ";
  cout << endl;

  return 0;
}