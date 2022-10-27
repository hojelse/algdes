using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
  static string line;

  static void Main(string[] args)
  {
    RunKattis();
    // RunThore();
  }

  private static void RunKattis()
  {
    int N = -1;
    while ((N = int.Parse(Console.ReadLine())) != 0)
    {
      var points = ParsePointsKattis(N);
      (double minDist, Point p1, Point p2) = ClosestPairs.ClosestPair(points);
      Console.WriteLine($"{p1.x} {p1.y} {p2.x} {p2.y}");
    }
  }

  private static void RunThore()
  {
    SkipTSPHeader();
    var points = ParsePoints();

    (double minDist, Point p1, Point p2) = ClosestPairs.ClosestPair(points);

    Console.WriteLine($"minimum distance: {minDist}");
    Console.WriteLine($"point {p1.id} at ({p1.x}, {p1.y})");
    Console.WriteLine($"point {p2.id} at ({p2.x}, {p2.y})");
  }

  // Parsing

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

  private static List<Point> ParsePoints()
  {
    List<Point> list = new List<Point>();

    while ((line = Console.ReadLine()) != null)
    {
      line = line.Trim();
      if (line == "EOF") break;
      if (line == "") break;
      string[] tokens = Regex.Split(line, @"\s+");
      string id = tokens[0];
      double x = parseDouble(tokens[1]);
      double y = parseDouble(tokens[2]);

      list.Add(new Point(id, x, y));
    }

    return list;
  }

  private static void SkipTSPHeader()
  {
    char firstChar = (char)Console.In.Peek();

    if (firstChar == 'N') while (Console.ReadLine().Trim() != "NODE_COORD_SECTION") { }
  }

  static double parseDouble(string str) {
    return double.Parse(str, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
  }
}

