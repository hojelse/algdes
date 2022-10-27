using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
  static string line;

  static void Main(string[] args)
  {
    int N = -1;
    while ((N = int.Parse(Console.ReadLine())) != 0)
    {
      var points = ParsePointsKattis(N);
      (double minDist, Point p1, Point p2) = KleinbergTardosClosestPair.ClosestPair(points);
      Console.WriteLine($"{p1.x} {p1.y} {p2.x} {p2.y}");
    }
  }

  private static List<Point> ParsePointsKattis(int N)
  {
    List<Point> list = new List<Point>();

    for (int i = 0; i < N; i++)
    {
      line = Console.ReadLine();
      line = line.Trim();
      string[] tokens = Regex.Split(line, @"\s+");
      double x = parseDouble(tokens[0]);
      double y = parseDouble(tokens[1]);

      list.Add(new Point($"{x}-{y}", x, y));
    }

    return list;
  }

  static double parseDouble(string str) {
    return double.Parse(str, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
  }
}

