/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2021 Thierry Matthey
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
    public static class Tools
    {
        public static void Variance<T>(IList<T> track, IList<double> time, out double velocity, out double acceleration)
            where T : GpsPoint
        {
            var v = new List<double>();
            var a = new List<double>();
            var t = new List<double>();
            for (var i = 0; i + 1 < track.Count; i++)
            {
                var dt = time[i + 1] - time[i];
                var l = track[i + 1].HaversineDistance(track[i]);
                v.Add(l / dt);
                t.Add(dt);
                if (i > 0)
                {
                    a.Add((v[i] - v[i - 1]) / (t[i - 1] + t[i]) * 2.0);
                }
            }

            var varianceVel = Statistics.Arithmetic.Variance(v, t);
            velocity = Comparison.IsZero(varianceVel) ? 0 : varianceVel;
            var varianceAcc = Statistics.Arithmetic.Variance(a);
            acceleration = Comparison.IsZero(varianceAcc) ? 0 : varianceAcc;
        }

        public static void DistanceVelocityAcceleration<T>(IList<T> track, out List<double> distance,
            out List<double> velocity, out List<double> acceleration) where T : GpsPoint
        {
            var time = new List<double>();
            for (var i = 0; i < track.Count; i++)
                time.Add(i);
            DistanceVelocityAcceleration(track, time, out distance, out velocity, out acceleration);
        }

        public static void DistanceVelocityAcceleration<T>(IList<T> track, IList<double> time,
            out List<double> distance,
            out List<double> velocity, out List<double> acceleration) where T : GpsPoint
        {
            distance = new List<double>();
            velocity = new List<double>();
            acceleration = new List<double>();

            if (track.Count == 0)
                return;
            if (track.Count == 1)
            {
                distance.Add(0.0);
                velocity.Add(0.0);
                acceleration.Add(0.0);
                return;
            }

            var t = new List<double> {0.0};
            var v = new List<double> {0.0};
            var s = 0.0;
            distance.Add(s);
            for (var i = 1; i < track.Count; i++)
            {
                var dt = time[i] - time[i - 1];
                var l = track[i].HaversineDistance(track[i - 1]);
                t.Add(dt);
                v.Add(l / dt);
                s += l;
                distance.Add(s);
            }

            velocity.Add(0.0);
            acceleration.Add(0.0);
            for (var i = 1; i + 1 < track.Count; i++)
            {
                velocity.Add((v[i] * t[i] + v[i + 1] * t[i + 1]) / (t[i] + t[i + 1]));
                acceleration.Add((v[i + 1] * t[i + 1] - v[i] * t[i]) / (t[i] + t[i + 1]) * 2.0);
            }

            velocity.Add(0.0);
            acceleration.Add(0.0);
        }
    }
}