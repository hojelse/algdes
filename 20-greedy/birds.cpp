#include "../header.hpp"
int main() {
  ll L, D, N;
  cin >> L >> D >> N;
  vi birds(N, 0);
   ll b;
  for (ll i = 0; i < N; i++) {
    cin >> b;
    birds[i] = b;
  }

  if (N == 0) {
    cout << (D + L - 12)/D << endl;
    return 0;
  }

  sort(birds.begin(), birds.end());

  ll sum = (birds[0]-6)/D;
  for (ll i = 0; i < N-1; i++) {
    sum += ((birds[i+1]-D)-birds[i])/D;
  }
  sum += (L-6-birds[N-1])/D;

  cout << sum << endl;
  return 0;
}