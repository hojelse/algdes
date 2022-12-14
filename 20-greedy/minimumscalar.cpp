// #include <bits/stdc++.h>
#include <iostream>
#include <algorithm>
#include <vector>
using namespace std;
typedef long long ll;
typedef unsigned long long ull;
typedef long double lf;
typedef vector<ll> vi;
typedef vector<vi> mi;
typedef pair<ll, ll> ii;
typedef vector<ii> vii;
typedef vector<vii> mii;
#define REP(i,a,b) for (int i = a; i <= b; i++)
template <class T> T smod(T a, T b) { return (a % b + b) % b; }

// Minimize scalar product by permuting coordinates
// 1. sort vectors
// 2. pair up smallest scalar with largest scalar
// Scalar product can get too large for ints, 10^5 * 10^5 * 800 = 8*10^12
ll solve() {
  ll N;
  cin >> N;

  vi xs;
  vi ys;
  ll x;
  REP(i, 1, N) {
    cin >> x;
    xs.push_back(x);
  }
  REP(i, 1, N) {
    cin >> x;
    ys.push_back(x);
  }

  sort(xs.begin(), xs.end());
  sort(ys.begin(), ys.end());

  ll scalar_prod = 0;
  REP(i, 0, N-1)
    scalar_prod += xs[i] * ys[N-1-i];

  return scalar_prod;
}

int main(void) {
  ll T;
  cin >> T;
  REP(i, 1, T) {
    cout << "Case #" << i << ": " << solve() << endl;
  }
  
  return 0;
}