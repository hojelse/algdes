// fails with Wrong Answer https://open.kattis.com/submissions/10114021

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
  ll K, T;
  cin >> K >> T;

  if (T < K) {
    cout << "impossible" << endl;
    return 0;
  }

  ll D, E;
  cin >> D >> E;

  map<ll, ll> play_state;
  mi earliest(K, vi(T, D));
  mi least_dur(T, vi(K, 0));

  ll s, k, t;
  for (ll i = 0; i < E; i++)
  {
    cin >> s >> k >> t;
    k--; t--;

    if (t != -1)
      earliest[k][t] = s;

    ll rem = D-s;
    if (play_state.find(k) != play_state.end()) {
      least_dur[play_state[k]][k] -= rem;
    }

    if (t == -1) {
      least_dur[play_state[k]][k] -= rem;
      play_state.erase(k);
    } else {
      play_state[k] = t;
      least_dur[t][k] += rem;
    }
  }


  ll X = T;
  ll Y = K;
  list<ll> singles;
  for (ll x = 0; x < X; x++)
    singles.push_back(x);
  vi c(X, 0);
  mi next(X, vi(Y, 0));
  mi rank = earliest;
  vi y_x(Y, -1);
  vi x_y(X, -1);

  for (ll t = 0; t < T; t++)
  {
    vii sorted;
    for (ll k = 0; k < K; k++)
      sorted.push_back(ii{least_dur[t][k], k});

    sort(sorted.begin(), sorted.end());

    for (ll k = 0; k < K; k++)
      next[t][k] = sorted[k].second;
  }

  while (!singles.empty())
  {
    ll x = singles.front();
    ll curr = c[x]++;
    if (curr == K) {
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
      if (rank[y][currX] <= rank[y][x]) continue;
      singles.pop_front(); singles.push_back(currX);
      x_y[currX] = -1;
      x_y[x] = y;
      y_x[y] = x;
    }
  }

  for (ll y = 0; y < Y; y++) {
    if (y_x[y] == -1) {
      cout << "impossible" << endl;
      return 0;
    }
  }

  for (ll y = 0; y < Y; y++)
    cout << y_x[y]+1 << " ";
  cout << endl;

  return 0;
}