#include <iostream>
#include <vector>
#include <string>
#include <set>
#include <algorithm>
typedef long long LL;
typedef long double F;

LL seven() {
  return 7;
}

std::string compare(std::vector<LL>& A) {
  LL a0 = A[0];
  LL a1 = A[1];
  if (a0 > a1) return "Bigger";
  if (a0 == a1) return "Equal";
  return "Smaller";
}

LL median(std::vector<LL>& A) {
  sort(A.begin(), A.begin()+3);
  return A[1];
}

LL sum(std::vector<LL>& A) {
  LL sum = 0;
  for (auto i : A)
    sum += i;
  return sum;
}

LL sumEvens(std::vector<LL>& A) {
  LL sum = 0;
  for (auto i : A)
    if (i % 2 == 0)
      sum += i;
  return sum;
}

void toChars(std::vector<LL>& A) {
  for (LL i : A)
    std::cout << char(97 + (i % 26));
}

std::string jumps(std::vector<LL>& A) {
  std::set<LL> seen;
  LL i = 0;
  while(true) {
    if (i == A.size()-1) return "Done";
    if (i >  A.size()-1) return "Out";
    seen.insert(i);
    i = A[i];
    if (seen.find(i) != seen.end()) return "Cyclic";
  }
}

int main(void) {
  LL N, t, c;
  std::vector<LL> A;

  std::cin >> N >> t;

  for (LL i = 0; i < N; i++)
  {
    std::cin >> c;
    A.push_back(c);
  }

  if      (t == 1) std::cout << seven();
  else if (t == 2) std::cout << compare(A);
  else if (t == 3) std::cout << median(A);
  else if (t == 4) std::cout << sum(A);
  else if (t == 5) std::cout << sumEvens(A);
  else if (t == 6) toChars(A);
  else if (t == 7) std::cout << jumps(A);

  std::cout << std::endl;

  return 0;
}