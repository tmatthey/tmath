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
    public class GpsPointTests
    {
        [Test]
        public void GpsPoint_NorthPole_Vector3D()
        {
            var g = new GpsPoint {Latitude = 90, Longitude = 0, Elevation = 0.0};
            Vector3D v = g;
            v.X.ShouldBe(0.0);
            v.Y.ShouldBe(0.0);
            v.Z.ShouldBe(Geodesy.EarthRadius);
        }

        [Test]
        public void GpsPoint_Polar3DConversion_ReturnsId()
        {
            var p = new Polar3D(1, 1);
            GpsPoint g = p;
            Polar3D q = g;
            p.Theta.ShouldBe(q.Theta);
            p.Phi.ShouldBe(q.Phi);
            p.R.ShouldBe(q.R);
        }

        [Test]
        public void GpsPoint_SouthPole_Vector3D()
        {
            var g = new GpsPoint {Latitude = -90, Longitude = 0, Elevation = 0.0};
            Vector3D v = g;
            v.X.ShouldBe(0.0);
            v.Y.ShouldBe(0.0);
            v.Z.ShouldBe(-Geodesy.EarthRadius);
        }

        [Test]
        public void GpsPoint_Vector3DConversion_ReturnsId()
        {
            var v = new Vector3D(1, 1, 1);
            GpsPoint g = v;
            Vector3D w = g;
            v.X.ShouldBe(w.X, 1e-7);
            v.Y.ShouldBe(w.Y, 1e-7);
            v.Z.ShouldBe(w.Z, 1e-7);
        }
    }
}