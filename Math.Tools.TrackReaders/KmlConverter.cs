/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2017 Thierry Matthey
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
using System.Text.RegularExpressions;
using Math.Tools.TrackReaders.Kml;

namespace Math.Tools.TrackReaders
{
    /// <summary>
    /// TCX Converter
    /// </summary>
    public static class KmlConverter
    {
        /// <summary>
        /// Converts TXC data into abstract Track definition
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Track Convert(kml data)
        {
            Track track = null;
            if (data?.Document?.Folder != null)
            {
                var trackPoints = new List<TrackPoint>();
                var time = DateTime.Now;
                foreach (var folder in data.Document.Folder)
                {
                    if (folder.Placemark != null)
                    {
                        foreach (var placemark in folder.Placemark)
                        {
                            if (placemark.LineString?.coordinates != null)
                            {
                                var lineString = Regex.Split(placemark.LineString.coordinates, @"[\s\n\t]+");
                                var useElevation = placemark.LineString.altitudeMode == altitudeModeEnumType.absolute;
                                var distance = 0.0;
                                TrackPoint lastPoint = null;
                                foreach (var str in lineString)
                                {
                                    var coordinates = str.Split(',');
                                    double lat, lng;
                                    if (coordinates.Length > 1 && double.TryParse(coordinates[0].Trim(), out lng) &&
                                        double.TryParse(coordinates[1].Trim(), out lat))
                                    {
                                        var elev = 0.0;
                                        if (useElevation && !double.TryParse(coordinates[2].Trim(), out elev))
                                        {
                                            elev = 0.0;
                                        }
                                        if (lastPoint != null)
                                        {
                                            distance += Gps.Geodesy.Distance.Haversine(lat, lng, lastPoint.Latitude,
                                                lastPoint.Longitude);
                                        }
                                        trackPoints.Add(new TrackPoint(lat, lng, elev, distance, 0, time));
                                        lastPoint = trackPoints.Last();
                                    }
                                }
                            }
                        }
                    }
                }
                if (trackPoints.Count > 0)
                {
                    track = new Track
                    {
                        Date = time,
                        SportType = SportType.Unknown,
                        TrackPoints = trackPoints.ToList(),
                        Name = data.Document.name
                    };
                }
            }
            return track;
        }
    }
}