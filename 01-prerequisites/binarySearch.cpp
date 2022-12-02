#include <vector>
typedef long long LL;
// Assumes A is sorted
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