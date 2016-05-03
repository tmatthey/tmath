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
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class GridLookupTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void CurrentOrdering_ReturnsCorretOrderedList()
        {
            var gpsTrackRef = new GpsTrack(_gpsTrackExamples.TrackOne());
            gpsTrackRef.SetupLookup(gpsTrackRef.Center, 50.0);
            var gpsTrackCur = new GpsTrack(_gpsTrackExamples.TrackTwo());
            var trackCur = new Transformer(gpsTrackCur.Track, gpsTrackRef.Center);
            var neighboursCur = gpsTrackRef.Lookup.Find(trackCur.Track, trackCur.Displacement, gpsTrackRef.Lookup.Size);
            var i = -1;
            foreach (var point in neighboursCur)
            {
                var j = -1;
                foreach (var distance in point)
                {
                    if (j < 0)
                    {
                        j = distance.Current;
                    }
                    else
                    {
                        distance.Current.ShouldBe(j);
                    }
                }
                j.ShouldBeGreaterThan(i);
                i = j;
            }
        }

        [Test]
        public void Find_WithDifferentGridSize_ReturnsSameList()
        {
            const double radius = 50.0;
            var gpsTrackRef = new GpsTrack(_gpsTrackExamples.TrackOne());
            gpsTrackRef.SetupLookup(gpsTrackRef.Center, 0.5);
            var gpsTrackCur = new GpsTrack(_gpsTrackExamples.TrackTwo());
            var trackCur = new Transformer(gpsTrackCur.Track, gpsTrackRef.Center);
            var neighboursCur1 = gpsTrackRef.Lookup.Find(trackCur.Track, trackCur.Displacement, radius);

            gpsTrackRef.SetupLookup(gpsTrackRef.Center, radius);
            var neighboursCur2 = gpsTrackRef.Lookup.Find(trackCur.Track, trackCur.Displacement, radius);

            neighboursCur1 =
                neighboursCur1.Select(
                    points => (from d in points where d.Dist <= radius select new Distance(d)).ToList())
                    .Where(newPoints => newPoints.Count > 0)
                    .ToList();
            neighboursCur2 =
                neighboursCur2.Select(
                    points => (from d in points where d.Dist <= radius select new Distance(d)).ToList())
                    .Where(newPoints => newPoints.Count > 0)
                    .ToList();

            neighboursCur1.Count.ShouldBe(neighboursCur2.Count);
            for (var i = 0; i < neighboursCur1.Count; i++)
            {
                neighboursCur1[i].Count.ShouldBe(neighboursCur2[i].Count);
                for (var j = 0; j < neighboursCur1[i].Count; j++)
                {
                    neighboursCur1[i][j].IsEqual(neighboursCur2[i][j]).ShouldBe(true);
                }
            }
        }

        [Test]
        public void Find_WithPointFarAway_ReturnsEmptyList()
        {
            var gpsTrackRef = new GpsTrack(_gpsTrackExamples.TrackOne());
            gpsTrackRef.SetupLookup(gpsTrackRef.Center, 50.0);
            gpsTrackRef.Lookup.Find(new Vector2D(-10000.0, -10000.0), 10.0).Count.ShouldBe(0);
        }

        [Test]
        public void ReferenceOrdering_ReturnsCorretOrderedList()
        {
            var gpsTrackRef = new GpsTrack(_gpsTrackExamples.TrackOne());
            gpsTrackRef.SetupLookup(gpsTrackRef.Center, 50.0);
            var gpsTrackCur = new GpsTrack(_gpsTrackExamples.TrackTwo());
            var trackCur = new Transformer(gpsTrackCur.Track, gpsTrackRef.Center);
            var neighboursCur = gpsTrackRef.Lookup.Find(trackCur.Track, trackCur.Displacement, gpsTrackRef.Lookup.Size);
            var neighboursRef = GridLookup.ReferenceOrdering(neighboursCur);
            var i = -1;
            foreach (var point in neighboursRef)
            {
                var j = -1;
                foreach (var distance in point)
                {
                    if (j < 0)
                    {
                        j = distance.Reference;
                    }
                    else
                    {
                        distance.Reference.ShouldBe(j);
                    }
                }
                j.ShouldBeGreaterThan(i);
                i = j;
            }
        }
    }
}