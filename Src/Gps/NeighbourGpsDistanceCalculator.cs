﻿/*
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

namespace Math.Gps
{
    //
    // Aggregates all points, which are at least as close (perpendicular distance) 
    // as given radius to the given reference track.
    //
    public class NeighbourGpsDistanceCalculator : ANeighbourDistanceCalculator
    {
        private readonly Vector3D _center;

        public NeighbourGpsDistanceCalculator(IList<GpsPoint> reference, double gridSize)
        {
            var gpsTrack = new GpsTrack(reference);
            _flatTrack = gpsTrack.CreateFlatTrack();
            _gridLookup = new GridLookup(_flatTrack, gridSize);
            _center = gpsTrack.Center;
        }

        public NeighbourGpsDistanceCalculator(IList<GpsPoint> reference)
            : this(reference, 50.0)
        {
        }


        public NeighbourDistance Analyze(IList<GpsPoint> current, double radius)
        {
            return Analyze(new FlatTrack(new GpsTrack(current).Track, _center), radius);
        }
    }
}