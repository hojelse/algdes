
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
  static string line;

  static void Main(string[] args)
  {
    SkipTSPHeader();
    var points = ParsePoints();

    (double minDist, Point p1, Point p2) = KleinbergTardosClosestPair.ClosestPair(points);

    Console.WriteLine($"minimum distance: {minDist}");
    Console.WriteLine($"point {p1.id} at ({p1.x}, {p1.y})");
    Console.WriteLine($"point {p2.id} at ({p2.x}, {p2.y})");
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