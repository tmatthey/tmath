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
using System.Linq;

namespace Math.Gps
{
    public class Analyzer
    {

        public Analyzer(IList<GpsPoint> reference, IList<GpsPoint> current, double radius)
        {
            var gpsTrackRef = new GpsTrack(reference);
            gpsTrackRef.CreateLookup(gpsTrackRef.Center, 50.0);
            var gpsTrackCur = new GpsTrack(current);
            var trackCur = new Transformer(gpsTrackCur.Track, gpsTrackRef.Center);
            var neighboursCur = gpsTrackRef.Grid.Find(trackCur.Track, radius);
            var neighboursRef = GridLookup.ReferenceOrdering(neighboursCur);

            Reference = new AnalyzerTrackWrapper(reference, gpsTrackRef.Grid.Track, neighboursRef);
            Current = new AnalyzerTrackWrapper(current, trackCur.Track, neighboursCur);

            var x = new List<double>();
            var y = new List<double>();
            foreach (var point in neighboursRef)
            {
                x.Add(point.First().Reference);
                y.Add(point.First().Current);
            }
            double a, b;
            Regression.Linear(x, y, out a, out b);
        }

        public AnalyzerTrackWrapper Current { get; private set; }
        public AnalyzerTrackWrapper Reference { get; private set; }
    }
}
