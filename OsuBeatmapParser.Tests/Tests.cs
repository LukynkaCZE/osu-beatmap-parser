using OsuBeatmapParser.Decoders;

namespace OsuBeatmapParser.Tests;

public class Tests
{
    private Beatmap _beatmap;
    
    [SetUp]
    public void Setup()
    {
        _beatmap = BeatmapDecoder.Decode("../../../test.osu");
    }

    [Test]
    public void BeatmapTest()
    {
        Assert.Multiple(() =>
        {
            Assert.AreEqual("audio.mp3", _beatmap.AudioFileName);
            Assert.AreEqual(99664, _beatmap.PreviewTime);
            Assert.AreEqual(SampleSet.Normal, _beatmap.SampleSet);
            
            Assert.AreEqual(10, _beatmap.Bookmarks.Count);
            Assert.AreEqual(16, _beatmap.GridSize);
            Assert.AreEqual(0.8f, _beatmap.TimelineZoom);
            
            Assert.AreEqual("new beginnings", _beatmap.Title);
            Assert.AreEqual("nekodex", _beatmap.Artist);
            Assert.AreEqual("pishifat", _beatmap.Creator);
            Assert.AreEqual(1011011, _beatmap.BeatmapSetID);
            
            Assert.AreEqual(0, _beatmap.HPDrainRate);
            Assert.AreEqual(2, _beatmap.CircleSize);
            Assert.AreEqual(0.7, _beatmap.SliderMultiplier);

            Assert.AreEqual("new-beginnings.jpg", _beatmap.BackgroundImage);
            
            Assert.AreEqual(5, _beatmap.ComboColours.Count);

            var circleCount = 0;
            var sliderCount = 0;
            var spinnerCount = 0;
            
            foreach (var hit in _beatmap.HitObjects)
            {
                switch (hit.Type)
                {
                    case HitObjectType.Circle:
                        circleCount++; break;
                    case HitObjectType.Slider:
                        sliderCount++; break;
                    case HitObjectType.Spinner:
                        spinnerCount++;
                        break;
                }
            }
            
            Assert.AreEqual(20, circleCount);
            Assert.AreEqual(12, sliderCount);
            Assert.AreEqual(2, spinnerCount);
            Assert.AreEqual(34, _beatmap.HitObjects.Count);
        });
    }
}