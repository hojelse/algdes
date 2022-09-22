using System.Collections;
using System.Text.RegularExpressions;

class Program
{
  static string line;
  static Point[] Py;

  static void Main(string[] args)
  {
    SkipTSPHeader();
    var points = ParsePoints();
    (double minDist, Point p1, Point p2) = ClosestPair(points);

    Console.WriteLine($"minimum distance: {minDist}");
    Console.WriteLine($"point {p1.id} at ({p1.x}, {p1.y})");
    Console.WriteLine($"point {p2.id} at ({p2.x}, {p2.y})");

  }

  private static (double minDist, Point p1, Point p2) ClosestPair(List<Point> points)
  {
    Point[] pointsSortedX = SortPointsX(points).ToArray();
    BisectArray<Point> Px = new BisectArray<Point>(pointsSortedX);
    Py = SortPointsY(points).ToArray();

    return ClosestPairRec(Px);
  }

  private static (double minDist, Point p1, Point p2) ClosestPairRec(BisectArray<Point> P)
  {
    if (P.Count() <= 3)
      return ClosestPairQuadratic(P);

    (var left, var right) = P.Bisect();

    (double ldist, Point l1, Point l2) = ClosestPairRec(left);
    (double rdist, Point r1, Point r2) = ClosestPairRec(right);

    var delta = Math.Min(ldist, rdist);
    var mid = left.Last().x;

    (double sdist, Point s1, Point s2) = MinOfCutSet(left, right, mid);

    if      (sdist < delta) return (sdist, s1, s2);
    else if (ldist < rdist) return (ldist, l1, l2);
    else                    return (rdist, r1, r2);
  }

  private static (double sdist, Point s1, Point s2) MinOfCutSet(BisectArray<Point> left, BisectArray<Point> right, double mid)
  {
    HashSet<string> leftIds = GetIdsHashed(left);
    HashSet<string> rightIds = GetIdsHashed(right);

    List<Point> Sy = new();

    foreach (var point in Py)
      if (leftIds.Contains(point.id) || rightIds.Contains(point.id))
        Sy.Add(point);

    return ClosestPairQuadraticWithCap(Sy, 15);
  }

  private static HashSet<string> GetIdsHashed(BisectArray<Point> arr)
  {
    HashSet<string> set = new HashSet<string>();
    foreach (var item in arr) set.Add(item.id);
    return set;
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
    pointsSortedX.Sort((p1, p2) =>
    {
      if (p1.x == p2.x) return 0;
      else if (p1.x > p2.x) return 1;
      else return -1;
    });
    return pointsSortedX;
  }

  private static (double, Point, Point) ClosestPairQuadraticWithCap(IList<Point> points, int cap)
  {
    double minDist = double.PositiveInfinity;
    Point? minP1 = null;
    Point? minP2 = null;
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

  private static (double, Point, Point) ClosestPairQuadratic(BisectArray<Point> points)
  {
    double minDist = double.PositiveInfinity;
    Point? minP1 = null;
    Point? minP2 = null;
    int N = points.Count();

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

  private static void NewMethod()
  {
    Console.WriteLine("tsp");
  }

  static double parseDouble(string str) {
    return double.Parse(str, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
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