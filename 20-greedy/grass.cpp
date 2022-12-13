// #include <bits/stdc++.h>
#include <iostream>
#include <vector>
#include <algorithm>
#include <math.h>
using namespace std;
typedef long long ll;
typedef unsigned long long ull;
typedef long double lf;
typedef vector<ll> vi;
typedef vector<vi> mi;
typedef pair<ll, ll> ii;
typedef vector<ii> vii;
typedef vector<vii> mii;
const int INF = ~(1<<31);
template <class T> T smod(T a, T b) { return (a % b + b) % b; }

// 1. Transform problem to 1 dimension using pythagoras
// 2. Sort on left
// 3. keep watered
// 4. Find largest current right while curr left <= watered

pair<lf, lf> best_between(ll& i, lf left, lf right, vector<pair<lf,lf>>& intervals) {
  pair<lf, lf> best{-1, -1};
  for (; i < intervals.size() && intervals[i].first <= left; i++)
  {
    best = (best.second > intervals[i].second) ? best : intervals[i];
  }
  return best;
}

ll solve(vector<pair<lf,lf>>& intervals, ll L) {
  ll idx = 0;
  lf left = 0;
  lf right = L;

  ll count = 0;
  while (left < right) {
    auto best = best_between(idx, left, right, intervals);
    if (best.first == -1 && best.second == -1) return -1;
    count++;
    left = best.second;
  }

  return count;
}

int main(void) {
  ll N, L, W;
  while (cin >> N >> L >> W) {

    vector<pair<lf, lf>> intervals;

    ll x, r;
    for (ll i = 0; i < N; i++)
    {
      cin >> x >> r;
      lf side_length = sqrt((r*r) - ((W/2.0)*(W/2.0)));
      lf left = x-side_length;
      lf right = x+side_length;
      cout << left << " " << right << endl;
      intervals.push_back(pair<lf, lf>{left, right});
    }

    sort(intervals.begin(), intervals.end());

    cout << solve(intervals, L) << endl;
  }
  return 0;
}