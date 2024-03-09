using System.Numerics;

namespace OsuBeatmapParser;

public class Slider : HitObject
{
    public int EndTime;
    public SliderCurveType CurveType;
    public List<Vector2> SliderPoints = new List<Vector2>();
    public int Repeats;
    public double PixelLength;
    public List<HitSound> EdgeHitSounds = new List<HitSound>();
    public List<Tuple<SampleSet, SampleSet>> EdgeAdditions = new List<Tuple<SampleSet, SampleSet>>();
}