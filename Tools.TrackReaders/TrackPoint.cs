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

using System;
using Math.Gps;

namespace Tools.TrackReaders
{
    public class TrackPoint
    {
        public TrackPoint(double latitude, double longitude, double elevation, double distance, byte heartRate,
            DateTime time)
        {
            Gps = new GpsPoint(latitude, longitude, elevation);
            HeartRate = heartRate;
            Distance = distance;
            Time = time;
        }


        public GpsPoint Gps { get; private set; }

        public double Latitude
        {
            get { return Gps.Latitude; }
        }

        public double Longitude
        {
            get { return Gps.Longitude; }
        }

        public double Elevation
        {
            get { return Gps.Elevation; }
        }

        public double Distance { get; private set; }
        public byte HeartRate { get; private set; }
        public DateTime Time { get; private set; }

        public double Second
        {
            get { return Time.Ticks*1.0e-7; }
        }
    }
}