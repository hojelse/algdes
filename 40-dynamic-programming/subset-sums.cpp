#include "../header.hpp"
#include <map>
#include <list>
// Subset sums O(nW) time O(nW) space
void subset_sums(mi& cache, vi& ns, ll N, ll W) {
  for (ll i = 1; i <= N; i++) {
    for (ll w = 0; w <= W; w++) {
      ll wi = ns[i-1];
      cache[i][w] =
        (w < wi)
        ? cache[i-1][w]
        : max(cache[i-1][w], wi + cache[i-1][w-wi]);
    }
  }
}
// Retrieve idxs O(n) time
void retrieve_idxs(mi& cache, vi& ns, list<ll>& idxs, ll i, ll j) {
  ll v = cache[i][j];
  if (v == 0) {}
  else {
    if(v < cache[i-1][j]) retrieve_idxs(cache, ns, idxs, i-1, j);
    else {
      idxs.push_front(i-1);
      retrieve_idxs(cache, ns, idxs, i-1, j-ns[i-1]);
    }
  }
}
void print_cache(mi& cache, ll N, ll W) {
  for (ll i = N; i > -1; i--) {
    for (ll w = 0; w <= W; w++) {
      cout << cache[i][w] << " ";
    }
    cout << endl;
  }
}
void solve() {
  ll W; cin >> W;
  ll N; cin >> N;

  if (N <= 0) {
    cout << "0 0" << endl;
    return;
  }

  ll n;
  vi ns(N);
  for (ll i = 0; i < N; i++) {
    cin >> n;
    ns[i] = n;
  }
  sort(ns.begin(), ns.end());

  mi cache(N+1, vi(W+1, 0));
  subset_sums(cache, ns, N, W);

  print_cache(cache, N, W);

  list<ll> idxs;
  retrieve_idxs(cache, ns, idxs, N, W);

  for (auto idx : idxs)
    cout << ns[idx] << " ";
  cout << endl;
}
int main() {
  ll T; cin >> T;
  REP(i, 1, T) solve();
  return 0;
}