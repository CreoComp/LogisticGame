using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const float EpsilonDistance = 0.01f;
    public const float FuelCost = 2.57f;
    public const float MapScale = 1f / 100f;
    public const float TimeAtTheStation = 5f;
    public const float MinutesBeforeStartRoute = 60f;

    public static List<Color> basicColors = new List<Color>()
    {
        new Color(205 / 255f, 133 / 255f, 63 / 255f),
        new Color(255 / 255f, 192 / 255f, 203 / 255f),
        new Color(255 / 255f, 160 / 255f, 122 / 255f),
        new Color(240 / 255f, 230 / 255f, 140 / 255f),
        new Color(152 / 255f, 251 / 255f, 152 / 255f),
        new Color(221 / 255f, 160 / 255f, 221 / 255f)
    };
}