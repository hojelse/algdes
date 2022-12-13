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

// The cheapest item when buying at least 3 items is 100% discounted.
// Given a set of items to buy, find the maximum discount.
// 1. Sort the prices.
// 2. Greedily buy the 3 most expensive items to get the 3rd as the discount. Repeat until empty.
int main(void) {
  ll N, p;
  cin >> N;

  vi prices;
  for (ll i = 0; i < N; i++)
  {
    cin >> p;
    prices.push_back(p);
  }

  sort(prices.begin(), prices.end());

  ll sum = 0;
  for (ll i = prices.size()-3; i >= 0; i -= 3)
  {
    sum += prices[i];
  }

  cout << sum << endl;

  return 0;
}