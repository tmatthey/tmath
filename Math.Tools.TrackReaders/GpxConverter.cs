/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2024 Thierry Matthey
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

using System;
using System.Linq;
using Math.Tools.TrackReaders.Gpx;

namespace Math.Tools.TrackReaders
{
    /// <summary>
    /// GPX converter
    /// </summary>
    public static class GpxConverter
    {
        /// <summary>
        /// Converts GPX data into abstract Track definition
        /// </summary>
        /// <param name="data">GPX data</param>
        /// <returns></returns>
        public static Track Convert(gpx data)
        {
            if (data.trk?.trkseg == null)
            {
                return null;
            }

            var time = DateTime.Now;
            var first = true;
            var trackPoints = data.trk.trkseg.Select(pkt =>
            {
                byte hearRate = 0;
                if (pkt.extensions?.TrackPointExtension != null && pkt.extensions.TrackPointExtension.hrSpecified)
                {
                    hearRate = pkt.extensions.TrackPointExtension.hr;
                }

                if (first)
                {
                    time = pkt.time;
                    first = false;
                }

                return new TrackPoint((double) pkt.lat, (double) pkt.lon, (double) pkt.ele, double.NaN, hearRate,
                    pkt.time);
            });

            var sport = FindSport(data.trk.type);

            return new Track
            {
                Date = data.metadata?.time ?? time,
                SportType = sport,
                TrackPoints = trackPoints.ToList()
            };
        }


        private static SportType FindSport(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return SportType.Unknown;
            }

            if (type.Equals("running", StringComparison.CurrentCultureIgnoreCase))
            {
                return SportType.Running;
            }

            return SportType.Unknown;
        }
    }
}