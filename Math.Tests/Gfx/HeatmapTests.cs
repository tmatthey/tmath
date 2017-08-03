/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2017 Thierry Matthey
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
 * BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
 * ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * ***** END LICENSE BLOCK *****
 */

using Math.Gfx;
using Math.Gps;
using Math.Tests.Gps;
using NUnit.Framework;
using Shouldly;
using BitmapFileWriter = Math.Gfx.BitmapFileWriter;

namespace Math.Tests.Gfx
{
    [TestFixture]
    public class HeatMapTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void HeatMap_Log_GeneratesHeatmap()
        {
            var heatMap = new HeatMap();
            heatMap.Add(_gpsTrackExamples.TrackOne());
            heatMap.Add(_gpsTrackExamples.TrackTwo());
            heatMap.Add(_gpsTrackExamples.TrackThree());
            heatMap.Add(_gpsTrackExamples.TrackFour());
            heatMap.Add(new GpsTrack(_gpsTrackExamples.TrackFive()));
            var bitmap = heatMap.Log(2.5);
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "heatMapCenter.log.png", bitmap, HeatColorMapping.Default);
        }

        [Test]
        public void HeatMap_Median_GeneratesHeatmap()
        {
            var heatMap = new HeatMap();
            heatMap.Add(_gpsTrackExamples.TrackOne());
            heatMap.Add(_gpsTrackExamples.TrackTwo());
            heatMap.Add(_gpsTrackExamples.TrackThree());
            heatMap.Add(_gpsTrackExamples.TrackFour());
            heatMap.Add(new GpsTrack(_gpsTrackExamples.TrackFive()));
            var bitmap = heatMap.Median(2.5);
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "heatMapCenter.median.png", bitmap, HeatColorMapping.Default);
        }

        [Test]
        public void HeatMap_Normalized_GeneratesHeatmap()
        {
            var heatMap = new HeatMap();
            heatMap.Add(_gpsTrackExamples.TrackOne());
            heatMap.Add(_gpsTrackExamples.TrackTwo());
            heatMap.Add(_gpsTrackExamples.TrackThree());
            heatMap.Add(_gpsTrackExamples.TrackFour());
            heatMap.Add(new GpsTrack(_gpsTrackExamples.TrackFive()));
            var bitmap = heatMap.Normalized(2.5);
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "heatMapCenter.normalized.png", bitmap,
                HeatColorMapping.Default);
        }

        [Test]
        public void HeatMap_NoTracks_ReturnsNull()
        {
            var heatMap = new HeatMap();
            heatMap.Normalized(2.5).ShouldBe(null);
            heatMap.Log(2.5).ShouldBe(null);
            heatMap.Median(2.5).ShouldBe(null);
        }
    }
}