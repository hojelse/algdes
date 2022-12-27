#include "../header.hpp"
#include <map>
ll sum(vi& nums, ll patt) {
  ll sum = 0;
  for (ll i = 0; i < nums.size(); i++) {
    if (patt & 1)
      sum += nums[i];
    patt >>= 1;
  }
  return sum;
}
void print_nums(vi& nums, ll patt) {
  for (ll i = 0; i < nums.size(); i++) {
    if (patt & 1)
      cout << nums[i] << " ";
    patt >>= 1;
  }
  cout << endl;
}
bool solve(vi& nums, map<ll, ll>& sum_to_patt) {
  for (ll patt = 1; patt <= 0b11111111111111111111; patt++)
  {
    ll s = sum(nums, patt);
    if (sum_to_patt.find(s) != sum_to_patt.end()) {
      ll other_patt = sum_to_patt[s];
      print_nums(nums, patt);
      print_nums(nums, other_patt);
      return true;
    }
    sum_to_patt[s] = patt;
  }
  return false;
}
int main() {
  ll T, N, num;
  cin >> T;
  for (ll i = 0; i < T; i++) {
    cin >> N;
    vi nums(N);
    for (ll i = 0; i < N; i++) {
      cin >> num;
      nums[i] = num;
    }
    cout << "Case #" << i+1 << ":" << endl;
    map<ll, ll> sum_to_patt;
    if (!solve(nums, sum_to_patt)) {
      cout << "Impossible" << endl;
    }
  }
  return 0;
}