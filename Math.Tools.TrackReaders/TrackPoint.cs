/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2018 Thierry Matthey
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
using Math.Gps;

namespace Math.Tools.TrackReaders
{
    /// <summary>
    /// Abstract definition of a track point of an activity: GPS, elevation, distance, heart rate and time
    /// </summary>
    public class TrackPoint
    {
        /// <summary>
        /// A track point
        /// </summary>
        /// <param name="latitude">Latitude [deg]</param>
        /// <param name="longitude">Longitude [deg]</param>
        /// <param name="elevation">Elevation [m]</param>
        /// <param name="distance">Distance [m]</param>
        /// <param name="heartRate">Heart rate [byte]</param>
        /// <param name="time">Time and date</param>
        public TrackPoint(double latitude, double longitude, double elevation, double distance, byte heartRate,
            DateTime time)
        {
            Gps = new GpsPoint(latitude, longitude, elevation);
            HeartRate = heartRate;
            Distance = distance;
            Time = time;
        }


        /// <summary>
        /// Returns GpsPoint
        /// </summary>
        public GpsPoint Gps { get; }

        /// <summary>
        /// Returns latitude [deg]
        /// </summary>
        public double Latitude => Gps.Latitude;

        /// <summary>
        /// Returns Longitude [deg]
        /// </summary>
        public double Longitude => Gps.Longitude;

        /// <summary>
        /// Returns elevation [m]
        /// </summary>
        public double Elevation => Gps.Elevation;

        /// <summary>
        /// Returns distance [m]
        /// </summary>
        public double Distance { get; }

        /// <summary>
        /// Returns heart beat
        /// </summary>
        public byte HeartRate { get; }

        /// <summary>
        /// Returns date and time of current point
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Returns seconds of date and time of current point
        /// </summary>
        public double Second => Conversion.DateTimeToSeconds(Time);
    }
}