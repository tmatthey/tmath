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
using System.Linq;

namespace Math.Gps
{
    public class AnalyzerTrackWrapper
    {
        public AnalyzerTrackWrapper(IList<GpsPoint> track, IList<Vector2D> transformed, IList<List<Distance>> neighbours, IList<double> distance, IList<double> displacement, bool refTrack)
        {
            Track = track;
            Transformed = transformed;
            Neighbours = neighbours;
            Distance = distance;
            Displacement = displacement;
            CommonDistance = 0.0;
            for (var i = 1; i < neighbours.Count; i++)
            {
                if (refTrack)
                {
                    if (neighbours[i - 1][0].Reference + 1 == neighbours[i][0].Reference)
                        CommonDistance += displacement[i];
                    
                }
                else
                {
                    if (neighbours[i - 1][0].Current + 1 == neighbours[i][0].Current)
                        CommonDistance += displacement[i];
                }
            }
        }
        public IList<GpsPoint> Track { get; private set; }
        public IList<Vector2D> Transformed { get; private set; }
        public IList<List<Distance>> Neighbours { get; private set; }
        public IList<double> Distance { get; private set; }
        public IList<double> Displacement { get; private set; }
        public double TotalDistance { get { return Distance.LastOrDefault(); } }
        public double CommonDistance { get; private set; }
    }
}