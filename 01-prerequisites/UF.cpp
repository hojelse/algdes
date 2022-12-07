#include <iostream>
#include <set>
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

// Weighted quick-union with path compresson
// contruction: O(N)
// find: Not quite amortized O(1)
// union: Not quite amortized O(1)
class UF
{
  private:
    vi id;
    vi size;
  public:
    ll count;

    UF(ll N) {
      count = N;
      for (ll i = 0; i < N; i++) {
        id.push_back(i);
        size.push_back(1);
      }
    };

    ll find(ll p) {
      set<ll> seen;
      while (p != id[p]) { p = id[p]; seen.insert(p); }
      for (ll s : seen) id[s] = p; // path compression
      return p;
    };

    void _union(ll p, ll q) {
      ll i = find(p);
      ll j = find(q);
      if (i == j) return;
      // set smaller tree under root of bigger tree
      if (size[i] < size[j]) { id[i] = j; size[j] += size[i]; }
      else                   { id[j] = i; size[i] += size[j]; }
      count--;
    }
};