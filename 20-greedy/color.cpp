#include "../header.hpp"
// Given a set of intervals.
// Find maximum cardinality subset of non overlapping intervals.

// Earliest start time first
// 1. Sort on finish times ascending
// 2. Greedily take the next that doesn't overlap

// Time complexity O(N log N) for sort, plus O(N) for greedy algorithm
ll machines(vi& D, ll from, ll C, ll K) {
  ll d = from;
  ll socks = 0;
  ll diff = 0;

  ll n = from;
  while (socks < C && diff <= K) {
    if (from >= D.size()) return 0;
    diff = D[++n] - D[d];
    socks++;
  }

  return 1 + machines(D, n, C, K);
}
int main() {
  ll S, C, K;
  cin >> S >> C >> K;

  ll d0;
  vi D(S);
  for (ll i = 0; i < S; i++) {
    cin >> d0;
    D[i] = d0;
  }

  sort(D.begin(), D.end());

  cout << machines(D, 0, C, K) << endl;

  return 0;
}