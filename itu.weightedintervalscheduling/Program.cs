using System.Collections.Generic;
using System;
using System.Linq;

class weightedintervalscheduling
{
  static Interval[] intervals;
  static long[] ps;
  static Dictionary<int, long> optimalCache;

  public static void Main(string[] args)
  {
    optimalCache = new Dictionary<int, long>();

    var intervalsList = parseInput();

    // Sort by finish times
    intervalsList.Sort((i, j) => (int)(i.f - j.f));

    intervals = intervalsList.ToArray();

    // PrintIntervals();

    int N = intervals.Length;

    ps = CreatePs();

    // PrintPs();

    var t = Optimal(N-1);

    Console.WriteLine(t);

    optimalCache = null;
  }

  private static long[] CreatePs()
  {
    var p = new long[intervals.Length];

    for (int j = 0; j < p.Length; j++)
    {
      p[j] = BinarySearch(intervals, intervals[j].s, j-1);
    }

    return p;
  }

  private static long BinarySearch(Interval[] intervals, long key, int r) {
    int l = 0;

    int mid = -1;
    while (l <= r) {
      mid = (l + r) / 2;
      if (key < intervals[mid].f) {
        r = mid - 1;
      } else {
        l = mid + 1;
      }
    }

    if (r > -1)
      return r;

    return -1;
  }

  private static long Optimal(int j)
  {
    if (j == -1) return 0;
    if (optimalCache.ContainsKey(j))
      return optimalCache[j];

    var curr = intervals[j];

    long isIn = curr.w + Optimal((int)ps[j]);
    long isOut = Optimal(j-1);

    long max = Math.Max(isIn, isOut);

    optimalCache.Add(j, max);

    return max;
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
      long w = tokens[2];

      intervals.Add(new Interval(s, f, w));
    }

    return intervals;
  }

  private static void PrintPs()
  {
    Console.WriteLine();
    for (int i = 0; i < intervals.Length; i++)
    {
      Console.WriteLine($"p[{i}] = {ps[i]}");
    }
  }

  private static void PrintIntervals()
  {
    int idx = 0;
    foreach (var interval in intervals)
    {
      for (int i = 0; i < interval.s; i++)
        Console.Write(" ");

      for (int j = (int)interval.s; j <= interval.f; j++)
        Console.Write("-");

      Console.Write("  idx" + (idx++) + " ");
      Console.Write(interval.s + " ");
      Console.Write(interval.f + " ");
      Console.Write(interval.w + " ");
      Console.WriteLine();
    }
  }
}

public class Interval
{
  public long s { get; }
  public long f { get; }
  public long w { get; }

  public Interval(long s, long f, long w) {
    this.s = s;
    this.f = f;
    this.w = w;
  }
}
