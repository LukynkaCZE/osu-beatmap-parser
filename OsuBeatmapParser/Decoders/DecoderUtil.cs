using System.Drawing;
using System.Globalization;
using System.Numerics;

namespace OsuBeatmapParser;

public static class DecoderUtil
{
    public static bool ToBool(string value)
    {
        return value switch
        {
            "1" => true,
            _ => false
        };
    }

    public static SliderCurveType GetSliderCurveType(char c)
    {
        switch (c)
        {
            case 'C':
                return SliderCurveType.Catmull;
            case 'B':
                return SliderCurveType.Bezier;
            case 'L':
                return SliderCurveType.Linear;
            case 'P':
                return SliderCurveType.PerfectCurve;
            default:
                return SliderCurveType.PerfectCurve;
        }
    }

    public static List<Vector2> GetSliderPoints(string[] split)
    {
        var sliderPoints = new List<Vector2>();
        foreach (var pos in split)
        {
            var positionSplit = pos.Split(":");
            if (positionSplit.Length == 2)
            {
                var x = int.Parse(positionSplit[0], CultureInfo.InvariantCulture);
                var y = int.Parse(positionSplit[1], CultureInfo.InvariantCulture);
                sliderPoints.Add(new Vector2(x, y));
            }
        }
        return sliderPoints;
    }

    public static int CalculateEndTime(Beatmap beatmap, int starTime, int repeats, double pixelLength)
    {
        var duration = (int)(pixelLength / (100.0 * beatmap.SliderMultiplier) * repeats * beatmap.BeatLengthAt(starTime));
        return starTime + duration;
    }

    public static Color ParseColour(string value)
    {
        var colour = value.Split(",").Select(c => int.Parse(c)).ToArray();
        return Color.FromArgb(colour.Length == 4 ? colour[3] : 255, colour[0], colour[1], colour[2]);
    }
}