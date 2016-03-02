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

using System.Collections.Generic;
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class AnalyzerTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void TotalDistance_ReturnsExpeced()
        {
            var analyzer = new Analyzer(_gpsTrackExamples.TrackOne(), _gpsTrackExamples.TrackTwo(), 50.0);
            analyzer.Reference.TotalDistance.ShouldBe(Distance(analyzer.Reference.Track), 1e-1);
            analyzer.Current.TotalDistance.ShouldBe(Distance(analyzer.Current.Track), 1e-1);
        }

        [Test]
        public void ListOfNeighbours_InRange()
        {
            var analyzer = new Analyzer(_gpsTrackExamples.TrackOne(), _gpsTrackExamples.TrackTwo(), 50.0);
            foreach (var point in analyzer.Reference.Neighbours)
            {
                foreach (var p in point)
                {
                    var r = p.Reference;
                    var c = p.Current;
                    r.ShouldBeGreaterThanOrEqualTo(0);
                    r.ShouldBeLessThan(analyzer.Reference.Track.Count);
                    c.ShouldBeGreaterThanOrEqualTo(0);
                    c.ShouldBeLessThan(analyzer.Current.Track.Count);
                }
            }
        }

        private double Distance(IList<GpsPoint> track)
        {
            var d = 0.0;
            for (var i = 0; i + 1 < track.Count; i++)
            {
                d += track[i].HaversineDistance(track[i + 1]);
            }
            return d;
        }
    }
}
