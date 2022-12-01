#include <iostream>
typedef long long LL;
typedef long double F;
int main(void) {
  LL x, y;
  while (std::cin >> x >> y)
    if (x > y)
      std::cout << x-y << std::endl;
    else
      std::cout << y-x << std::endl;
  return 0;
}