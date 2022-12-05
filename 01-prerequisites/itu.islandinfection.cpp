#include <iostream>
#include <vector>
#include <string>
#include <set>
#include <map>
#include <algorithm>
typedef long long LL;
typedef long double F;

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

      if (size[i] < size[j]) { id[i] = j; size[j] += size[i]; }
      else                   { id[j] = i; size[i] += size[j]; }
      count--;
    }
};

int main(void) {
  LL R, C;
  std::cin >> R >> C;

  std::vector<std::vector<LL> > matrix;

  LL hr, hc, vr, vc;

  for (LL r = 0; r < R; r++)
  {
    std::vector<LL> v;
    std::string line;
    std::cin >> line;

    for (LL c = 0; c < C; c++)
    {
      LL i = line[c]-'0';
      // treat virus and human as land, but remember coordinates
      if      (i == 2) { vr = r; vc = c; v.push_back(1); }
      else if (i == 3) { hr = r; hc = c; v.push_back(1); }
      else             v.push_back(i);
    }

    matrix.push_back(v);
  }

  UF sets = UF(R*C);

  for (LL r = 0; r < R; r++)
  {
    for (LL c = 0; c < C; c++)
    {
      bool currIsLand =          matrix[r][c]   == 1;
      bool upIsLand   = r > 0 && matrix[r-1][c] == 1;
      bool leftIsLand = c > 0 && matrix[r][c-1] == 1;

      if (currIsLand && upIsLand)
        sets._union((r*C)+c, ((r-1)*C)+c);

      if (currIsLand && leftIsLand)
        sets._union((r*C)+c, (r*C)+(c-1));
    }
  }

  if (sets.find((hr*C)+hc) == sets.find((vr*C)+vc))
    std::cout << 1 << std::endl;
  else
    std::cout << 0 << std::endl;

  return 0;
}