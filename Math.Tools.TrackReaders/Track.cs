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
using System.Collections.Generic;
using System.Linq;
using Math.Gps;

namespace Math.Tools.TrackReaders
{
    /// <summary>
    /// Abstract GPS track definition including common data from GPX and TCX
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Start time and date of activity
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Type of activity
        /// </summary>
        public SportType SportType { get; set; }

        /// <summary>
        /// List of track points 
        /// </summary>
        public IList<TrackPoint> TrackPoints { get; set; }

        /// <summary>
        /// Name of the activity
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns list of GpsPoints
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GpsPoint> GpsPoints()
        {
            return TrackPoints.Select(trackPoint => trackPoint.Gps);
        }

        /// <summary>
        /// Returns list of heart beats
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> HeartRates()
        {
            return TrackPoints.Select(trackPoint => (int) trackPoint.HeartRate);
        }

        /// <summary>
        /// Returns time and date of each track point
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DateTime> Times()
        {
            return TrackPoints.Select(trackPoint => trackPoint.Time);
        }

        /// <summary>
        /// Returns list of date and time in seconds
        /// </summary>
        /// <returns></returns>
        public IEnumerable<double> Seconds()
        {
            return TrackPoints.Select(trackPoint => trackPoint.Second);
        }

        /// <summary>
        /// Returns list of elapsed seconds since start
        /// </summary>
        /// <returns></returns>
        public IEnumerable<double> ElapsedSeconds()
        {
            return TrackPoints?.FirstOrDefault() == null
                ? new List<double>()
                : TrackPoints.Select(trackPoint => trackPoint.Second - TrackPoints.FirstOrDefault().Second);
        }
    }
}