// fails with Time Limit https://open.kattis.com/submissions/10220053

// (inverse) Subset Sums
#include "../header.hpp"
#include <map>
#include <list>
// Subset sums O(nW) time O(nW) space
void subset_sums(mi& cache, vi& w, ll N, ll W) {
  for (ll i = 1; i <= N; i++) {
    for (ll j = 0; j <= W; j++) {
      cache[i][j] =
        (j < w[i])
        ? cache[i-1][j]
        : max(cache[i-1][j], w[i] + cache[i-1][j-w[i]]);
    }
  }
}
// Retrieve idxs O(n) time
void retrieve_idxs(mi& cache, vi& w, list<ll>& idxs, ll& sum, ll i, ll j) {
  if (i == 0) {}
  else {
    if(cache[i][j] > cache[i-1][j]) {
      // idxs.push_front(i);
      retrieve_idxs(cache, w, idxs, sum, i-1, j-w[i]);
    } else {
      retrieve_idxs(cache, w, idxs, sum, i-1, j);
      // the idxs not in the subset
      idxs.push_front(i);
      sum += w[i];
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
  ll K; cin >> K;
  ll N; cin >> N;

  if (N <= 0) {
    cout << "0 0" << endl;
    return;
  }

  ll n;
  vi dems(N+1);
  dems[0] = 0;
  for (ll i = 1; i <= N; i++) {
    cin >> n;
    dems[i] = n;
  }
  sort(dems.begin(), dems.end());

  ll sum = 0;
  for (ll dem: dems)
    sum += dem;

  // cout << K << " " << N << endl;
  // for (ll dem : dems)
  //   cout << dem << " ";
  // cout << endl;

  ll W = (sum-K);

  mi cache(N+1, vi(W+1, 0));
  subset_sums(cache, dems, N, W);

  print_cache(cache, N, W);

  ll sum1 = 0;
  list<ll> idxs;
  retrieve_idxs(cache, dems, idxs, sum1, N, W);

  cout << sum1 << " " << idxs.size() << endl;

}
int main() {
  ll T; cin >> T;
  REP(i, 1, T) solve();
  return 0;
}