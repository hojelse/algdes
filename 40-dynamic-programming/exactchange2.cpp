// failing with time limit: https://open.kattis.com/submissions/10163740
#include "../header.hpp"
#include <map>
ll p(ll j) {
  return j-1;
}
ii best(ii a, ii b, ll K) {
  auto [a_total, a_count] = a;
  auto [b_total, b_count] = b;
  ll delta_a = a_total - K;
  ll delta_b = b_total - K;
  if (a_total < K) return b;
  if (b_total < K) return a;
  if (delta_a < delta_b) return a;
  if (delta_a > delta_b) return b;
  if (a_count < b_count) return a;
  return b;
}
ii rec(map<string, ii>& cache, vi& running_sum, vi& dems, ll K, ll j) {
  if (j < 0) return ii{0, 0};
  if (running_sum[j] < K) return ii{inf, inf};
  string key = to_string(K);
  if (cache.find(key) != cache.end()) return cache[key];

  ll J = dems[j];
  auto [total, count] = rec(cache, running_sum, dems, K-J, p(j));
  ii a = ii{J + total, 1 + count};

  ii b = rec(cache, running_sum, dems, K, j-1);

  ii opt = best(a, b, K);
  cache[key] = opt;
  return opt;
}
void solve() {
  ll K; cin >> K;
  ll N; cin >> N;

  if (N <= 0) {
    cout << "0 0" << endl;
    return;
  }

  ll n;
  vi dems(N);
  for (ll i = 0; i < N; i++) {
    cin >> n;
    dems[i] = n;
  }
  map<string, ii> cache;
  sort(dems.begin(), dems.end());

  vi running_sum(N);
  running_sum[0] = dems[0];
  for (ll i = 1; i < N; i++) {
    running_sum[i] = running_sum[i-1] + dems[i];
  }

  auto [total, count] = rec(cache, running_sum, dems, K, N-1);
  cout << total << " " << count << endl;
}
int main() {
  ll T; cin >> T;
  REP(i, 1, T) solve();
  return 0;
}