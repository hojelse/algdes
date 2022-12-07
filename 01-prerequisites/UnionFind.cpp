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

// Weighted quick-union with path compression
// contruction: O(N)
// find: Not quite amortized O(1)
// union: Not quite amortized O(1)
class union_find
{
  private:
    vi p;
  public:
    ll count;

    union_find(ll N) {
      count = N;
      for (ll i = 0; i < N; i++) {
        p.push_back(-1); // size of trees encoded in root nodes as negative inverse
      }
    }

    ll find(ll x) {
      return p[x] < 0 ? x : p[x] = find(p[x]); // implicit path compression
    }

    void uni(ll x, ll y) {
      ll i = find(x);
      ll j = find(y);
      if (i == j) return;
      if (p[i] > p[j]) swap(i, j);
      p[i] = j; p[j] += p[i];
      count--;
    }

    int size(int x) {
      return -p[find(x)];
    }
};

// Test for https://itu.kattis.com/problems/itu.islandinfection
int main(void) {
  ll R, C;
  cin >> R >> C;

  mi matrix;

  ll hr, hc, vr, vc;

  for (ll r = 0; r < R; r++)
  {
    vi v;
    string line;
    cin >> line;

    for (ll c = 0; c < C; c++)
    {
      ll i = line[c]-'0';
      // treat virus and human as land, but remember coordinates
      if      (i == 2) { vr = r; vc = c; v.push_back(1); }
      else if (i == 3) { hr = r; hc = c; v.push_back(1); }
      else             v.push_back(i);
    }

    matrix.push_back(v);
  }

  union_find sets = union_find(R*C);

  for (ll r = 0; r < R; r++)
  {
    for (ll c = 0; c < C; c++)
    {
      bool currIsLand =          matrix[r][c]   == 1;
      bool upIsLand   = r > 0 && matrix[r-1][c] == 1;
      bool leftIsLand = c > 0 && matrix[r][c-1] == 1;

      if (currIsLand && upIsLand)
        sets.uni((r*C)+c, ((r-1)*C)+c);

      if (currIsLand && leftIsLand)
        sets.uni((r*C)+c, (r*C)+(c-1));
    }
  }

  if (sets.find((hr*C)+hc) == sets.find((vr*C)+vc))
    cout << 1 << endl;
  else
    cout << 0 << endl;

  return 0;
}