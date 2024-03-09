using System.Drawing;

namespace OsuBeatmapParser;

public class Beatmap
{
    public const int LATEST_OSU_FILE_VERSION = 14;
    
    // General
    public int Version;
    public string AudioFileName;
    public int AudioLeadIn;
    public int PreviewTime;
    public bool Countdown;
    public SampleSet SampleSet;
    public double StackLeniency;
    public Ruleset Mode;
    public int ModeId;
    public bool LetterboxInBreaks;
    public bool WidescreenStoryboard;
    public bool StoryFireInFront;
    public bool SpecialStyle;
    public bool EpilepsyWarning;
    public bool UseSkinSprites;
    // public int CirclesCount;
    // public int SlidersCount;
    // public int SpinnersCount;
    public int Length;
    public int BPM;

    // Editor
    public List<int> Bookmarks = new List<int>();
    public string BookmarksString;
    public double DistanceSpacing;
    public int BeatDivisor;
    public int GridSize;
    public float TimelineZoom;
    
    // Metadata
    public string Title;
    public string TitleUnicode;
    public string Artist;
    public string ArtistUnicode;
    public string Creator;
    public string VersionDifficulty;
    public string Source;
    public List<String> Tags = new List<string>();

    public string TagsString
    {
        get => string.Join(",", Tags.ToArray());
        set => Tags = value.Split(",").ToList();
    }
    public int BeatmapID;
    public int BeatmapSetID;
    
    // Difficulty
    public float HPDrainRate;
    public float CircleSize;
    public float OverallDifficulty;
    public float ApproachRate;
    public double SliderMultiplier;
    public double SliderTickRate;
    
    // Events
    public string BackgroundImage;
    public string Video;
    public int VideoOffset;
    public List<BeatmapBreak> Breaks = new List<BeatmapBreak>();
    public Storyboard Storyboard;
    
    // Colors
    public List<Color> ComboColours = new List<Color>();
    public Color SliderTrackOverride;
    public Color SliderBorder;

    public List<TimingPoint> TimingPoints = new List<TimingPoint>();
    public List<HitObject> HitObjects = new List<HitObject>();

    public double BeatLengthAt(int offset)
    {
        if (TimingPoints.Count == 0) return 0;

        var timingPoint = 0;
        var samplePoint = 0;

        for (var i = 0; i < TimingPoints.Count; i++)
        {
            if (TimingPoints[i].Offset <= offset)
            {
                if (TimingPoints[i].Inherited) samplePoint = i; else timingPoint = i;
            }
        }
        
        double multiplier = 1;
        if (samplePoint <= timingPoint || !(TimingPoints[samplePoint].BeatLength < 0))
            return TimingPoints[timingPoint].BeatLength * multiplier;
        if (TimingPoints[samplePoint].BeatLength >= 0)
        {
            multiplier = 1;
        }
        else
        {
            multiplier = double.Clamp((float)-TimingPoints[samplePoint].BeatLength, 10, 100) / 100f;
        }

        return TimingPoints[timingPoint].BeatLength * multiplier;
    }
}