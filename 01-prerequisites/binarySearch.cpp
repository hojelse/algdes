#include <iostream>
#include <vector>
#include <climits>
using namespace std;
#define inf LLONG_MAX
typedef long long ll;
typedef vector<ll> vi;
typedef vector<vi> mi;
typedef pair<ll, ll> ii;
typedef vector<ii> vii;
typedef vector<vii> mii;

// Assumes A is sorted
ll BinarySearch(vector<ll>& A, ll x) {
  ll lo = 0;
  ll hi = A.size()-1;
  while (lo <= hi) {
    ll mid = (lo+hi)/2;
    if (A[mid] == x) return mid;
    if (A[mid] > x) hi = mid-1;
    else lo = mid+1;
  }
  return -1;
}