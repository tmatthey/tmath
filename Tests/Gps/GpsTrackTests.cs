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

using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class GpsTrackTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void GpsTrack_Constructor_CorrectCenter()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            var sum = new Vector3D();
            var d = 0.0;
            foreach (var g in gpsTrack.Track)
            {
                Vector3D v = g;
                sum += v.Normalized();
                d += v.Norm();
            }
            sum.Normalize();
            sum *= d/_gpsTrackExamples.TrackOne().Count;
            sum.X.ShouldBe(gpsTrack.Center.X, 1e-7);
            sum.Y.ShouldBe(gpsTrack.Center.Y, 1e-7);
            sum.Z.ShouldBe(gpsTrack.Center.Z, 1e-7);
        }

        [Test]
        public void GpsTrack_Constructor_CorrectTrack()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            gpsTrack.Track.Count.ShouldBe(_gpsTrackExamples.TrackOne().Count);
        }
    }
}