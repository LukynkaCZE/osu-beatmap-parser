namespace OsuBeatmapParser.Decoders;

public static class SectionDecoder
{

    public static Beatmap ParseTimingPoint(Beatmap beatmap, string line)
    {
        var tokens = line.Split(",");

        var offset = (int)float.Parse(tokens[0]);
        var beatLength = double.Parse(tokens[1]);
        var timeSignature = TimeSignature.Quadruple;
        var sampleSet = SampleSet.None;
        var customSampleSet = 0;
        var volume = 100;
        var inherited = true;
        var effects = Effects.None;

        if (tokens.Length >= 3)
            timeSignature = (TimeSignature)int.Parse(tokens[2]);
        if (tokens.Length >= 4)
            sampleSet = (SampleSet)int.Parse(tokens[3]);
        if (tokens.Length >= 6)
            volume = int.Parse(tokens[4]);
        if (tokens.Length >= 7)
            inherited = !DecoderUtil.ToBool(tokens[6]);
        if (tokens.Length >= 8)
            effects = (Effects)int.Parse(tokens[7]);
        
        beatmap.TimingPoints.Add(new TimingPoint
        {
            Offset = offset,
            BeatLength = beatLength,
            TimeSignature = timeSignature,
            SampleSet = sampleSet,
            CustomSampleSet = customSampleSet,
            Volume = volume,
            Inherited = inherited,
            Effects = effects
        });
        
        return beatmap;
    }

    
    public static Beatmap ParseEvents(Beatmap beatmap, string line)
    {

        string[] tokens = line.Split(",");
        EventType eventType = EventType.None;

        if (Enum.TryParse(tokens[0], out EventType e))
        {
            eventType = (EventType)Enum.Parse(typeof(EventType), tokens[0]);
        } else if (line.StartsWith(" ") || line.StartsWith("_"))
        {
            eventType = EventType.StoryboardCommand;
        }
        
        switch (eventType)
        {
            case EventType.Background:
                beatmap.BackgroundImage = tokens[2].Trim('"');
                break;
            case EventType.Video:
                beatmap.Video = tokens[2].Trim('"');
                beatmap.VideoOffset = int.Parse(tokens[1]);
                break;
            case EventType.Break:
                beatmap.Breaks.Add(new BeatmapBreak(int.Parse(tokens[1]), int.Parse(tokens[2])));
                break;
            case EventType.Sprite:
            case EventType.Animation:
            case EventType.Sample:
            case EventType.StoryboardCommand:
                break;
            //TODO: Storyboard Parser Implementation
        }
        return beatmap;
    }

    public static Beatmap ParseDifficulty(Beatmap beatmap, string line)
    {
        var index = line.IndexOf(":", StringComparison.Ordinal);
        var variable = line.Remove(index).Trim();
        var value = line.Remove(0, index + 1).Trim();

        switch (variable)
        {
            case "HPDrainRate":
                beatmap.HPDrainRate = float.Parse(value);
                break;
            case "CircleSize":
                beatmap.CircleSize = float.Parse(value);
                break;
            case "OverallDifficulty":
                beatmap.OverallDifficulty = float.Parse(value);
                break;
            case "ApproachRate":
                beatmap.ApproachRate = float.Parse(value);
                break;
            case "SliderMultiplier":
                beatmap.SliderMultiplier = double.Parse(value);
                break;
            case "SliderTickRate":
                beatmap.SliderTickRate = double.Parse(value);
                break;
        }

        return beatmap;
    }

    public static Beatmap ParseMetadataSection(Beatmap beatmap, string line)
    {
        var index = line.IndexOf(":", StringComparison.Ordinal);
        var variable = line.Remove(index).Trim();
        var value = line.Remove(0, index + 1).Trim();

        switch (variable)
        {
            case "Title":
                beatmap.Title = value;
                break;
            case "TitleUnicode":
                beatmap.Title = value;
                break;
            case "Artist":
                beatmap.Artist = value;
                break;
            case "ArtistUnicode":
                beatmap.ArtistUnicode = value;
                break;
            case "Creator":
                beatmap.Creator = value;
                break;
            case "Version":
                beatmap.BeatmapVersion = value;
                break;
            case "Source":
                beatmap.Source = value;
                break;
            case "Tags":
                beatmap.Tags = value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                break;
            case "BeatmapID":
                beatmap.BeatmapID = int.Parse(value);
                break;
            case "BeatmapSetID":
                beatmap.BeatmapSetID = int.Parse(value);
                break;
        }
        return beatmap;
    }
    
    public static Beatmap ParseEditorSection(Beatmap beatmap, string line)
    {
        var index = line.IndexOf(":", StringComparison.Ordinal);
        var variable = line.Remove(index).Trim();
        var value = line.Remove(0, index + 1).Trim();

        switch (variable)
        {
            case "Bookmarks":
                beatmap.Bookmarks = value.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                break;
            case "DistanceSpacing":
                beatmap.DistanceSpacing = double.Parse(value);
                break;
            case "BeatDivisor":
                beatmap.BeatDivisor = int.Parse(value);
                break;
            case "GridSize":
                beatmap.GridSize = int.Parse(value);
                break;
            case "TimelineZoom":
                beatmap.TimelineZoom = float.Parse(value);
                break;
        }
        
        return beatmap;
    }
    
    public static Beatmap ParseGeneralSection(Beatmap beatmap, string line)
    {
        var newBeatmap = beatmap;
        
        var index = line.IndexOf(":", StringComparison.Ordinal);
        var variable = line.Remove(index).Trim();
        var value = line.Remove(0, index + 1).Trim();

        switch (variable)
        {
            case "AudioFilename":
                newBeatmap.AudioFileName = value;
                break;
            case "AudioLeadIn":
                newBeatmap.AudioLeadIn = int.Parse(value);
                break;
            case "PreviewTime":
                newBeatmap.PreviewTime = int.Parse(value);
                break;
            case "Countdown":
                newBeatmap.Countdown = DecoderUtil.ToBool(value);
                break;
            case "SampleSet":
                newBeatmap.SampleSet = (SampleSet)Enum.Parse(typeof(SampleSet), value);
                break;
            case "StackLeniency":
                newBeatmap.StackLeniency = double.Parse(value);
                break;
            case "Mode":
                newBeatmap.Mode = (Ruleset)Enum.Parse(typeof(Ruleset), value);
                newBeatmap.ModeId = int.Parse(value);
                break;
            case "LetterboxInBreaks":
                newBeatmap.LetterboxInBreaks = DecoderUtil.ToBool(value);
                break;
            case "WidescreenStoryboard":
                newBeatmap.WidescreenStoryboard = DecoderUtil.ToBool(value);
                break;
            case "StoryFireInFront":
                newBeatmap.StoryFireInFront = DecoderUtil.ToBool(value);
                break;
            case "SpecialStyle":
                newBeatmap.SpecialStyle = DecoderUtil.ToBool(value);
                break;
            case "EpilepsyWarning":
                newBeatmap.EpilepsyWarning = DecoderUtil.ToBool(value);
                break;
            case "UseSkinSprites":
                newBeatmap.UseSkinSprites = DecoderUtil.ToBool(value);
                break;
        }

        return newBeatmap;
    }
}