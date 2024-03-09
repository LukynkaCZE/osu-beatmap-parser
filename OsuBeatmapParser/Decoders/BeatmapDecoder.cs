namespace OsuBeatmapParser.Decoders;

public static class BeatmapDecoder
{

    public static Sections section = Sections.None;
    
    public static Beatmap Decode(string path)
    {
        if (File.Exists(path))
        {
            return Decode(File.ReadAllLines(path));
        }
        else
        {
            throw new FileNotFoundException();
        }
    }

    public static Beatmap Decode(string[] lines)
    {
        var beatmap = new Beatmap();
        section = Sections.Format;
        
        foreach (var line in lines)
        {
            section = line switch
            {
                "[General]" => Sections.General,
                "[Editor]" => Sections.Editor,
                "[Metadata]" => Sections.Metadata,
                "[Difficulty]" => Sections.Difficulty,
                "[Events]" => Sections.Events,
                "[TimingPoints]" => Sections.TimingPoints,
                "[Colours]" => Sections.Colours,
                "[HitObjects]" => Sections.HitObjects,
                _ => section
            };

            if(line.StartsWith("[") && line.EndsWith("]")) continue;
            if(line.StartsWith("//")) continue;
            if(line == "") continue;

            var currentSection = section;
            
            switch (section)
            {
                // File Format
                case Sections.Format:
                {
                    var split = line.Split("osu file format v");
                    var stringVer = split[1];
                    beatmap.Version = int.Parse(stringVer);
                    continue;
                }
                case Sections.General:
                    beatmap = SectionDecoder.ParseGeneralSection(beatmap, line);
                    break;
                case Sections.Editor:
                    beatmap = SectionDecoder.ParseEditorSection(beatmap, line);
                    break;
                case Sections.Metadata:
                    beatmap = SectionDecoder.ParseMetadataSection(beatmap, line);
                    break;
                case Sections.Difficulty:
                    beatmap = SectionDecoder.ParseDifficulty(beatmap, line);
                    break;
                case Sections.Events:
                    beatmap = SectionDecoder.ParseEvents(beatmap, line);
                    break;
                case Sections.TimingPoints:
                    beatmap = SectionDecoder.ParseTimingPoint(beatmap, line);
                    break;
                case Sections.HitObjects:
                    beatmap = SectionDecoder.ParseHitobjects(beatmap, line);
                    break;
                case Sections.Colours:
                    beatmap = SectionDecoder.ParseColours(beatmap, line);
                    break;
            }
        }
        return beatmap;
    }
}