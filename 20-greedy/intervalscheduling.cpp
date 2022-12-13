// #include <bits/stdc++.h>
#include <iostream>
#include <vector>
#include <algorithm>

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

// Given a set of intervals. Find maximum cardinality subset of non overlapping intervals.
// 1. Sort on finish times
// 2. Greedily take the next that doesn't overlap
// Time complexity O(N log N) for sort, plus O(N) for greedy algorithm
int main(void) {
  ll N;
  cin >> N;

  vii intervals;

  ll s, f;
  for (ll i = 0; i < N; i++)
  {
    cin >> s >> f;
    intervals.push_back(ii{f, s});
  }

  sort(intervals.begin(), intervals.end());

  ll num = 0;
  ll frontier = 0;

  for (auto item : intervals)
  {
    bool noOverlap = frontier <= item.second;
    if (noOverlap) {
      frontier = item.first;
      num++;
    }
  }

  cout << num << endl;

  return 0;
}