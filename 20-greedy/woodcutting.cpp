// not working, probably precision error
#include "../header.hpp"
int main(void) {
  ll T, N, W, x;
  cin >> T;

  for (ll i = 0; i < T; i++) {
    cin >> N;
    vi peop;

    for (ll j = 0; j < N; j++) {
      cin >> W;
      ll sum = 0;

      for (ll k = 0; k < W; k++) {
        cin >> x;
        sum += x;
      }

      peop.push_back(sum);
    }

    sort(peop.begin(), peop.end());

    vi wait;
    ll time = 0;
    for (auto p : peop) {
      wait.push_back(time + p);
      time += p;
    }

    ll sum = 0;
    for (auto w : wait)
      sum += w;

    lf avg = ((lf)sum) / ((lf)wait.size());

    cout << avg << endl;
  }
  
  return 0;
}