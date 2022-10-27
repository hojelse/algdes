using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
  public static string leftPattern = "-oo";
  public static string rightPattern = "oo-";
  public static int minPebbleCount = int.MaxValue;
  public static Dictionary<string,int> dict = new Dictionary<string, int>();
  static void Main(string[] args)
  {
    string state = Console.ReadLine();
    int n = int.Parse(state);

    for (int i = 0; i < n; i++)
    {
      state = Console.ReadLine();
      minPebbleCount = int.MaxValue;
      NewMethod(state);
      Console.WriteLine(minPebbleCount);
    }

  }

  private static void NewMethod(string state)
  {
    if(dict.TryGetValue(state, out int pebbleCountFromDict)){
      if(minPebbleCount > pebbleCountFromDict) minPebbleCount = pebbleCountFromDict;
      return;
    }

    bool stateHasLegalMove = GetStateHasLegalMove(state);
    int pebbleCount = state.Replace("-", "").Length;
    if (!stateHasLegalMove) {
      if(minPebbleCount > pebbleCount) minPebbleCount = pebbleCount;
      return;
    }

    dict.Add(state, pebbleCount);

    foreach (Match match in Regex.Matches(state, leftPattern))
    {
      int index = match.Index;
      string newState = Jump(true, index, state);
      NewMethod(newState);
    }

    foreach (Match match in Regex.Matches(state, rightPattern))
    {
      int index = match.Index;
      string newState = Jump(false, index, state);
      NewMethod(newState);
    }

  }

  private static string Jump(bool jumpLeft, int index, string state)
  {
    char[] chars = state.ToCharArray();
    if (jumpLeft)
    {
      chars[index] = 'o';
      chars[index + 1] = '-';
      chars[index + 2] = '-';
    }
    else
    {
      chars[index] = '-';
      chars[index + 1] = '-';
      chars[index + 2] = 'o';
    }
    string after = new string(chars);
    return after;
  }

  private static bool GetStateHasLegalMove(string state)
  {
    return state.Contains(leftPattern) || state.Contains(rightPattern);
  }
}
