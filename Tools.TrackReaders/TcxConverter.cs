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
using System.Linq;
using Tools.TrackReaders.Tcx;

namespace Tools.TrackReaders
{
    public static class TcxConverter
    {
        public static Track Convert(TrainingCenterDatabase_t data)
        {
            if (data == null || data.Activities == null || data.Activities.Activity == null)
            {
                return null;
            }
            var activities = data.Activities.Activity;
            var trackPoints = activities.SelectMany(a =>
            {
                return a.Lap
                    .SelectMany(l => l.Track)
                    .Where(t => t != null && t.Position != null)
                    .Select(t => new TrackPoint(t.Position.LatitudeDegrees, t.Position.LongitudeDegrees,
                        t.AltitudeMeters,
                        t.DistanceMeters,
                        t.HeartRateBpm != null ? t.HeartRateBpm.Value : (byte) 0,
                        t.Time));
            });

            var activity = activities.FirstOrDefault();
            var date = DateTime.Now;
            var sport = SportType.Unknown;
            if (activity != null)
            {
                date = activity.Id;
                sport = FindSport(activity.Sport);
            }
            return new Track {Date = date, SportType = sport, TrackPoints = trackPoints.ToList()};
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