using System;
using System.Collections.Generic;
using System.Linq;

class shopaholic
{
  static void Main(string[] args) {
    int size = int.Parse(Console.ReadLine());

    List<long> items = Console.ReadLine()
      .Split(" ")
      .Select(x => long.Parse(x))
      .ToList();

    items.Sort((a, b) => ((int)b - (int)a));

    long sum = items.Where((x, idx) => (idx % 3 == 2)).Sum();

    Console.WriteLine(sum);
  }
}