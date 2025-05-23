﻿/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2025 Thierry Matthey
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
using Math.Tools.TrackReaders.Tcx;

namespace Math.Tools.TrackReaders
{
    /// <summary>
    /// TCX Converter
    /// </summary>
    public static class TcxConverter
    {
        /// <summary>
        /// Converts TXC data into abstract Track definition
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Track Convert(TrainingCenterDatabase_t data)
        {
            IEnumerable<TrackPoint> trackPoints = null;
            var sport = SportType.Unknown;
            var date = DateTime.Now;
            if (data?.Activities?.Activity != null)
            {


                var activities = data.Activities.Activity;
                trackPoints = activities.SelectMany(a =>
                {
                    return a.Lap
                        .SelectMany(l => l.Track)
                        .Where(t => t?.Position != null)
                        .Select(t => new TrackPoint(t.Position.LatitudeDegrees, t.Position.LongitudeDegrees,
                            t.AltitudeMeters,
                            t.DistanceMeters,
                            t.HeartRateBpm?.Value ?? 0,
                            t.Time));
                });
                var activity = activities.FirstOrDefault();
                if (activity != null)
                {
                    date = activity.Id;
                    sport = FindSport(activity.Sport);
                }
            }
            else if (data?.Courses != null)
            {
                var courses = data.Courses;
                trackPoints = courses.SelectMany(a =>
                {
                    return a.Track
                        .Where(t => t?.Position != null)
                        .Select(t => new TrackPoint(t.Position.LatitudeDegrees, t.Position.LongitudeDegrees,
                            t.AltitudeMeters,
                            t.DistanceMeters,
                            t.HeartRateBpm?.Value ?? 0,
                            t.Time));
                });
                if (trackPoints.Any())
                    date = trackPoints.First().Time;
            }


            return new Track { Date = date, SportType = sport, TrackPoints = trackPoints.ToList() };
        }

        private static SportType FindSport(Sport_t sport)
        {
            switch (sport)
            {
                case Sport_t.Running:
                    return SportType.Running;
                case Sport_t.Biking:
                    return SportType.Cycling;
                case Sport_t.Other:
                    return SportType.Unknown;
                default:
                    return SportType.Unknown;
            }
        }
    }
}