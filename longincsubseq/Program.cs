internal class Program
{
  static int N;
  static List<int> xs;

  private static void Main(string[] args)
  {
    while (Console.In.Peek() != -1)
    {
      RunCase();
    }

    static void RunCase()
    {
      N = int.Parse(Console.ReadLine());
      xs = Console.ReadLine().Split(" ").Select(int.Parse).ToList();

      int max = 0;
      List<int> maxs = new List<int>();

      for (int i = 0; i < N; i++)
      {
        var list = LISQ(i).ToList();

        Console.WriteLine($"[{i}]");
        foreach (var idx in list)
          Console.Write(xs[idx] + " ");
        Console.WriteLine();
      }
        Console.WriteLine();


      // Console.WriteLine(max);
      // foreach (var idx in maxs)
      //   Console.Write(idx + " ");
      // Console.WriteLine();
    }
  }

  private static LinkedList<int> LISQ(int idx)
  {
    
  }
}