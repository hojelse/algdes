#include <iostream>
#include <vector>
#include <string>
#include <set>
#include <map>
#include <algorithm>
typedef long long LL;
typedef long double F;
int main(void) {
  int n;
  std::cin >> n;
  
  std::string name;
  std::string gradeStr;
  
  std::map<LL, std::vector<std::string>> grade_name;

  std::map<char, LL> char_val = {
    {'A', 100},
    {'B', 200},
    {'C', 300},
    {'D', 400},
    {'E', 500},
    {'F', 600},
    {'X', -50},
    {'-', 1},
    {'+', -1},
  };

  for (LL i = 0; i < n; i++)
  {
    std::cin >> name >> gradeStr;
    LL gradeNum = 0;

    for (char c : gradeStr)
      gradeNum += char_val[c];

    if (grade_name.find(gradeNum) == grade_name.end())
      grade_name[gradeNum] = std::vector<std::string>{};
    grade_name[gradeNum].push_back(name);
  }

  for (auto kv : grade_name) {
    std::sort(kv.second.begin(), kv.second.end());
    for (auto name : kv.second)
      std::cout << name << std::endl;
  }
  
  return 0;
}