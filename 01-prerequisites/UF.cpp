#include <vector>
#include <set>
typedef long long LL;

// Weighted quick-union with path compresson
class UF
{
  private:
    std::vector<LL> id;
    std::vector<LL> size;
  public:
    LL count;

    UF(LL N) {
      count = N;
      for (LL i = 0; i < N; i++) {
        id.push_back(i);
        size.push_back(1);
      }
    };

    LL find(LL p) {
      std::set<LL> seen;
      while (p != id[p]) { p = id[p]; seen.insert(p); }
      for (LL s : seen) id[s] = p; // path compression
      return p;
    };

    void _union(LL p, LL q) {
      LL i = find(p);
      LL j = find(q);
      if (i == j) return;
      // set smaller tree under root of bigger tree
      if (size[i] < size[j]) { id[i] = j; size[j] += size[i]; }
      else                   { id[j] = i; size[i] += size[j]; }
      count--;
    }
};