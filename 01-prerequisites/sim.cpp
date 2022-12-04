// Use std::list as a linked list

#include <iostream>
#include <sstream>
#include <vector>
#include <string>
#include <set>
#include <map>
#include <algorithm>
#include <list>
typedef long long LL;
typedef long double F;

void runCase() {
  std::list<char> chars{};
  std::list<char>::iterator it = chars.begin();

  std::string line;
  std::getline(std::cin, line);

  for (char c : line) {
    if      (c == ']') it = chars.end();
    else if (c == '[') it = chars.begin();
    else if (c == '<') {
      if (it == chars.begin()) continue;
      std::advance(it, -1);
      it = chars.erase(it);
    }
    else {
      it = chars.insert(it, c);
      std::advance(it, 1);
    }
  }

  for (char c : chars) std::cout << c;

  std::cout << std::endl;
}

int main(void) {
  int T;
  std::cin >> T;
  std::string line;
  std::getline(std::cin, line);

  for (LL i = 0; i < T; i++)
    runCase();

  return 0;
}
