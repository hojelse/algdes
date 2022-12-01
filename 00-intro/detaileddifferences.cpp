#include <iostream>
#include <vector>
#include <string>
typedef long long LL;
typedef long double F;
int main(void) {
  LL numCases;
  std::string a;
  std::string b;
  std::cin >> numCases;
  for (LL i = 0; i < numCases; i++)
  {
    std::cin >> a >> b;
    std::cout << a << std::endl;
    std::cout << b << std::endl;
    for (LL i = 0; i < a.length(); i++)
      if(a[i] == b[i])
        std::cout << '.';
      else
        std::cout << '*';
    std::cout << std::endl;
  }
  
  return 0;
}