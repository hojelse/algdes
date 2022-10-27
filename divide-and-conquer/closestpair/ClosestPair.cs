using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class ClosestPairs
{
  public static (double minDist, Point p1, Point p2) ClosestPair(List<Point> points)
  {
    Point[] pointsSortedX = SortPointsX(points).ToArray();
    Span<Point> Px = new Span<Point>(pointsSortedX);
    var Py = SortPointsY(points);

    return ClosestPairRec(Px, Py);
  }

  private static (double minDist, Point p1, Point p2) ClosestPairRec(Span<Point> Px, List<Point> Py)
  {
    if (Px.Length == 2)
      return (dist(Px[0], Px[1]), Px[0], Px[1]);

    if (Px.Length <= 3)
      return ClosestPairQuadratic(Px);

    var leftX = Px.LeftHalf();
    var rightX = Px.RightHalf();

    var mid = leftX[leftX.Length-1].x;

    (var leftY, var rightY) = BisectYs(Py, mid, leftX.Length, rightX.Length);

    (double ldist, Point l1, Point l2) = ClosestPairRec(leftX, leftY);
    (double rdist, Point r1, Point r2) = ClosestPairRec(rightX, rightY);

    var delta = Math.Min(ldist, rdist);

    (double sdist, Point s1, Point s2) = MinOfCutSet(delta, mid, Py);

    if      (sdist < delta) return (sdist, s1, s2);
    else if (ldist < rdist) return (ldist, l1, l2);
    else                    return (rdist, r1, r2);
  }

  private static (List<Point> leftY, List<Point> rightY) BisectYs(List<Point> Py, double mid, int lL, int rL)
  {
    List<Point> leftY = new List<Point>();
    List<Point> rightY = new List<Point>();
    
    foreach (var p in Py)
    {
      if (p.x <= mid) leftY.Add(p);
      else            rightY.Add(p);
    }

    return (leftY, rightY);
  }

  private static (double sdist, Point s1, Point s2) MinOfCutSet(double delta, double mid, List<Point> Py)
  {
    List<Point> Sy = new List<Point>();

    foreach (var point in Py)
      if (Math.Abs(mid - point.x) < delta)
        Sy.Add(point);

    if (Sy.Count < 2)
      return (double.MaxValue, null, null);

    return ClosestPairQuadraticWithCap(Sy, 15);
  }

  private static List<Point> SortPointsY(List<Point> points)
  {
    var pointsSortedX = points;
    pointsSortedX.Sort((p1, p2) =>
    {
      if (p1.y == p2.y) return 0;
      else if (p1.y > p2.y) return 1;
      else return -1;
    });
    return pointsSortedX;
  }

  private static List<Point> SortPointsX(List<Point> points)
  {
    var pointsSortedX = points;
    pointsSortedX.Sort((p1, p2) => {
      if (p1.x == p2.x) return 0;
      else if (p1.x > p2.x) return 1;
      else return -1;
    });
    return pointsSortedX;
  }

  private static (double, Point, Point) ClosestPairQuadraticWithCap(IList<Point> points, int cap = int.MaxValue)
  {
    double minDist = double.PositiveInfinity;
    Point minP1 = null;
    Point minP2 = null;
    int N = points.Count;

    for (int i = 0; i < N; i++)
    {
      for (int j = i; j < Math.Min(cap, N); j++)
      {
        if (i == j) continue;
        Point p1 = points[i];
        Point p2 = points[j];

        double d = dist(p1, p2);
        if (d < minDist) {
          minDist = d;
          minP1 = p1;
          minP2 = p2;
        }
      }
    }

    if (minP1 == null || minP2 == null)
      throw new Exception("No pairs found");

    return (minDist, minP1, minP2);
  }

  private static (double, Point, Point) ClosestPairQuadratic(Span<Point> points)
  {
    double minDist = double.PositiveInfinity;
    Point minP1 = null;
    Point minP2 = null;
    int N = points.Length;

    for (int i = 0; i < N; i++)
    {
      for (int j = i; j < N; j++)
      {
        if (i == j) continue;
        Point p1 = points[i];
        Point p2 = points[j];

        double d = dist(p1, p2);
        if (d < minDist) {
          minDist = d;
          minP1 = p1;
          minP2 = p2;
        }
      }
    }

    if (minP1 == null || minP2 == null)
      throw new Exception("No pairs found");

    return (minDist, minP1, minP2);
  }

  // Standard Euclidean distance
  private static double dist(Point p1, Point p2)
  {
    double dx = p1.x - p2.x;
    double dy = p1.y - p2.y;
    return Math.Sqrt(dx*dx + dy*dy);
  }
}

class Point
{
  public string id { get; }
  public double x { get; }
  public double y { get; }

  public Point(string id, double x, double y)
  {
    this.id = id; this.x = x; this.y = y;
  }

}

public static class ExtensionMethods
{
  public static Span<T> LeftHalf<T>(this Span<T> s)
  {
    var left = s.Slice(0, s.Length/2);
    return left;
  }
  public static Span<T> RightHalf<T>(this Span<T> s)
  {
    var right = s.Slice(s.Length/2-1, s.Length/2);
    return right;
  }
}
