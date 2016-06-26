/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016 Thierry Matthey
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

using System.Linq;
using Math.Gfx;
using Math.Tests.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gfx
{
    [TestFixture]
    public class HeatmapTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void HeatMapCenter_GeneratesHeatmap()
        {
            var heatMap = new HeatMap(HeatMap.CalculateCenter);
            heatMap.Add(_gpsTrackExamples.TrackOne());
            heatMap.Add(_gpsTrackExamples.TrackTwo());
            heatMap.Add(_gpsTrackExamples.TrackThree());
            heatMap.Add(_gpsTrackExamples.TrackFour());
            heatMap.Add(_gpsTrackExamples.TrackFive());
            var bitmap = heatMap.Normalized(2.5, 0.05, 1.0);
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "heatMapCenter.png", bitmap);
        }

        [Test]
        public void HeatMapDiff_GeneratesHeatmap()
        {
            var heatMapMinCircle = new HeatMap(HeatMap.CalculateMinCircle);
            heatMapMinCircle.Add(_gpsTrackExamples.TrackOne());
            heatMapMinCircle.Add(_gpsTrackExamples.TrackTwo());
            heatMapMinCircle.Add(_gpsTrackExamples.TrackThree());
            heatMapMinCircle.Add(_gpsTrackExamples.TrackFour());
            heatMapMinCircle.Add(_gpsTrackExamples.TrackFive());
            var bitmapMinCircle = heatMapMinCircle.Normalized(2.5, 0.05, 1.0);

            var heatMapCenter = new HeatMap(HeatMap.CalculateCenter);
            heatMapCenter.Add(_gpsTrackExamples.TrackOne());
            heatMapCenter.Add(_gpsTrackExamples.TrackTwo());
            heatMapCenter.Add(_gpsTrackExamples.TrackThree());
            heatMapCenter.Add(_gpsTrackExamples.TrackFour());
            heatMapCenter.Add(_gpsTrackExamples.TrackFive());
            var bitmapCenter = heatMapCenter.Normalized(2.5, 0.05, 1.0);

            var cMax = 0.0;
            foreach (var i in Enumerable.Range(0, bitmapCenter.GetLength(0)))
            {
                foreach (var j in Enumerable.Range(0, bitmapCenter.GetLength(1)))
                {
                    var c = System.Math.Abs(bitmapCenter[i, j] - bitmapMinCircle[i, j]);
                    bitmapCenter[i, j] = c;
                    cMax = System.Math.Max(c, cMax);
                }
            }
            if (Comparison.IsPositive(cMax))
            {
                foreach (var i in Enumerable.Range(0, bitmapCenter.GetLength(0)))
                {
                    foreach (var j in Enumerable.Range(0, bitmapCenter.GetLength(1)))
                    {
                        bitmapCenter[i, j] /= cMax;
                    }
                }
            }
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "heatMapDiff.png", bitmapCenter);
            cMax.ShouldBeGreaterThan(0.0);
        }

        [Test]
        public void HeatMapMinCricle_GeneratesHeatmap()
        {
            var heatMap = new HeatMap(HeatMap.CalculateMinCircle);
            heatMap.Add(_gpsTrackExamples.TrackOne());
            heatMap.Add(_gpsTrackExamples.TrackTwo());
            heatMap.Add(_gpsTrackExamples.TrackThree());
            heatMap.Add(_gpsTrackExamples.TrackFour());
            heatMap.Add(_gpsTrackExamples.TrackFive());
            var bitmap = heatMap.Normalized(2.5, 0.05, 1.0);
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "heatMapMinCircle.png", bitmap);
        }
    }
}