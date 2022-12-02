#include <iostream>
#include <vector>
#include <string>
#include <set>
#include <map>
#include <algorithm>
typedef long long LL;
typedef long double F;

LL binarySearch(std::vector<LL>& A, LL x) {
  LL lo = 0;
  LL hi = A.size()-1;
  while (lo <= hi) {
    LL mid = (lo+hi)/2;
    if (A[mid] == x) return mid;
    if (A[mid] > x) hi = mid-1;
    else lo = mid+1;
  }
  return -1;
}

std::string pairExists(std::vector<LL>& A) {
  sort(A.begin(), A.end());
  for (LL i = 0; i < A.size(); i++)
    if (binarySearch(A, 7777-A[i]) != -1)
      return "Yes";
  return "No";
}

std::string unique(std::vector<LL>& A) {
  std::set<LL> seen;
  for (LL i : A) {
    if (seen.find(i) != seen.end())
      return "Contains duplicate";
    seen.insert(i);
  }
  return "Unique";
}

LL findMajor(std::vector<LL>& A) {
  std::map<LL, LL> counts;
  for (LL i : A)
    if (counts.find(i) != counts.end())
      counts[i]++;
    else
      counts[i] = 1;
  for (auto kv : counts)
    if (kv.second > A.size()/2)
      return kv.first;
  return -1;
}

void medians(std::vector<LL>& A) {
  sort(A.begin(), A.end());
  LL half = (A.size()-1)/2;
  if (A.size()%2 == 0)
    std::cout << A[half] << " " << A[half+1];
  else
    std::cout << A[half];
}

void sortedHundreds(std::vector<LL>& A) {
  sort(A.begin(), A.end());
  for (LL i : A)
    if (100 <= i && i <= 999)
      std::cout << i << " ";
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

  if      (t == 1) std::cout << pairExists(A);
  else if (t == 2) std::cout << unique(A);
  else if (t == 3) std::cout << findMajor(A);
  else if (t == 4) medians(A);
  else if (t == 5) sortedHundreds(A);

  std::cout << std::endl;

  return 0;
}