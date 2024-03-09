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
}