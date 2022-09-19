using System.Collections.Generic;
using System;
using System.Linq;

class intervalscheduling
{
  static void Main(string[] args)
  {
    List<Interval> intervals = parseInput();

    intervals.Sort((i, j) => (int)(i.f - j.f));

    long num = 0;
    long frontier = 0;

    foreach (var item in intervals)
    {
      bool noOverlap = frontier <= item.s;
      if (noOverlap) {
        frontier = item.f;
        num++;
      }
    }

    Console.WriteLine(num);
  }

  private static List<Interval> parseInput()
  {
    long N = long.Parse(Console.ReadLine());

    List<Interval> intervals = new List<Interval>();

    for (long i = 0; i < N; i++)
    {
      var tokens = Console.ReadLine().Split(" ").Select(x => long.Parse(x)).ToArray();
      long s = tokens[0];
      long f = tokens[1];

      intervals.Add(new Interval(s, f));
    }

    return intervals;
  }
}

public class Interval
{
  public long s { get; }
  public long f { get; }

  public Interval(long s, long f) {
    this.s = s;
    this.f = f;
  }
}