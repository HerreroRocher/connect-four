using UnityEngine;
public static class GameData
{
    public static string[] Players = new string[] { "Daniel", "Natasja" };
    public static Color[] Colors = new Color[] { Color.red, Color.yellow };
    public static string[][] SpecialPowers = { new[] { "DoubleMove", "TakeOver" }, new[] { "DoubleMove", "TakeOver" } };
    public static int Rows = 7;
    public static int Columns = 7;
    public static int InARowRequirements = 4;
}
