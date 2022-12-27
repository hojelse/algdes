#include "../header.hpp"
void mul(ll d1, vi& cs1, ll d2, vi& cs2) {
  ll d0 = d1 + d2;
  cout << d0 << endl;

  vi cs0(d0+1);

  for (ll i = 0; i < cs1.size(); i++) {
    for (ll j = 0; j < cs2.size(); j++) {
      ll d = i+j;
      cs0[d] += cs1[i]*cs2[j];
    }
  }
  
  for (ll i : cs0)
    cout << i << " ";
  cout << endl;
}

void read(vi& v) {
  ll c;
  for (ll i = 0; i < v.size(); i++) {
    cin >> c;
    v[i] = c;
  }
}

int main() {
  ll T;
  cin >> T;
  for (ll i = 0; i < T; i++) {
    ll d1, d2;

    cin >> d1;
    vi cs1(d1+1);
    read(cs1);

    cin >> d2;
    vi cs2(d2+1);
    read(cs2);

    mul(d1, cs1, d2, cs2);
  }
  
  return 0;
}