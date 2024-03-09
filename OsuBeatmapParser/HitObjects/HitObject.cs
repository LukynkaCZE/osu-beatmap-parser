using System.Numerics;

namespace OsuBeatmapParser;

public class HitObject
{
    public Vector2 Position;
    public int StartTime;
    public HitObjectType Type;
    public HitSound HitSound;
    public bool IsNewCombo;
    public ObjectParams ObjectParams;
    public int ComboOffset;
}