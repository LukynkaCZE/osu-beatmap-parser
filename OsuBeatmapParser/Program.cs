// See https://aka.ms/new-console-template for more information

using OsuBeatmapParser.Decoders;

var lines = File.ReadAllLines("../../../test.osu");
var beatmap = BeatmapDecoder.Decode(lines);
Console.WriteLine(beatmap.TimingPoints.Count);
Console.WriteLine(beatmap.TimingPoints[0].SampleSet);
Console.WriteLine(beatmap.TimingPoints[0].Volume);
Console.WriteLine(beatmap.TimingPoints[0].TimeSignature);
// Console.WriteLine(beatmap.ApproachRate);