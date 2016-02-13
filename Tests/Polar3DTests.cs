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

using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class Polar3DTests
    {
        [Test]
        public void Polar3D_EmptyConstructor_ZeroElement()
        {
            var v = new Polar3D();
            v.R.ShouldBe(0);
            v.Phi.ShouldBe(0);
            v.Theta.ShouldBe(0);
        }

        [Test]
        public void Polar3D_ZeroElement_IsZero()
        {
            Polar3D.Zero.R.ShouldBe(0);
            Polar3D.Zero.Phi.ShouldBe(0);
            Polar3D.Zero.Theta.ShouldBe(0);
        }

        [TestCase(0, 0, 0, 0, 1)]
        [TestCase(0, 90, 0, 0, 1)]
        [TestCase(0, 180, 0, 0, 1)]
        [TestCase(0, 270, 0, 0, 1)]
        [TestCase(90, 0, 1, 0, 0)]
        [TestCase(90, 90, 0, 1, 0)]
        [TestCase(90, 180, -1, 0, 0)]
        [TestCase(90, 270, 0, -1, 0)]
        [TestCase(180, 0, 0, 0, -1)]
        [TestCase(180, 90, 0, 0, -1)]
        [TestCase(180, 180, 0, 0, -1)]
        [TestCase(180, 270, 0, 0, -1)]
        [TestCase(270, 0, -1, 0, 0)]
        [TestCase(270, 90, 0, -1, 0)]
        [TestCase(270, 180, 1, 0, 0)]
        [TestCase(270, 270, 0, 1, 0)]
        public void Polar3D_CastVector3D_ReturnsExpected(double theta, double phi, double x, double y, double z)
        {
            var v = new Vector3D(x, y, z).Normalized();
            var p = new Polar3D(Conversion.DegToRad(theta), Conversion.DegToRad(phi));
            Vector3D u = p;
            u.X.ShouldBe(v.X, 1e-13);
            u.Y.ShouldBe(v.Y, 1e-13);
            u.Z.ShouldBe(v.Z, 1e-13);
        }

        [TestCase(0, 0, 0, 0, 2)]
        [TestCase(90, 0, 3, 0, 0)]
        [TestCase(90, 90, 0, 4, 0)]
        [TestCase(90, 180, -5, 0, 0)]
        [TestCase(90, 270, 0, -6, 0)]
        [TestCase(180, 0, 0, 0, -7)]
        public void Polar3D_CastPolar3D_ReturnsExpected(double theta, double phi, double x, double y, double z)
        {
            var v = new Vector3D(x, y, z).Normalized();
            var p = new Polar3D(Conversion.DegToRad(theta), Conversion.DegToRad(phi));
            Polar3D q = v;
            Vector3D u = p;
            q.R.ShouldBe(p.R, 1e-13);
            q.Theta.ShouldBe(p.Theta, 1e-13);
            q.Phi.ShouldBe(p.Phi, 1e-13);
        }

        [TestCase(0, 0)]
        [TestCase(45, 0)]
        [TestCase(45, 45)]
        [TestCase(45, 90)]
        [TestCase(45, 135)]
        [TestCase(45, 180)]
        [TestCase(45, 225)]
        [TestCase(45, 270)]
        [TestCase(45, 315)]
        [TestCase(90, 0)]
        [TestCase(90, 45)]
        [TestCase(90, 90)]
        [TestCase(90, 135)]
        [TestCase(90, 180)]
        [TestCase(90, 225)]
        [TestCase(90, 270)]
        [TestCase(90, 315)]
        [TestCase(135, 0)]
        [TestCase(135, 45)]
        [TestCase(135, 90)]
        [TestCase(135, 135)]
        [TestCase(135, 180)]
        [TestCase(135, 225)]
        [TestCase(135, 270)]
        [TestCase(135, 315)]
        [TestCase(180, 0)]
        public void Polar3D_Scan_ConversionId(double theta, double phi)
        {
            var p = new Polar3D(Conversion.DegToRad(theta), Conversion.DegToRad(phi), 3.14);
            Vector3D v = p;
            Polar3D q = v;
            q.R.ShouldBe(p.R, 1e-13);
            q.Theta.ShouldBe(p.Theta, 1e-13);
            q.Phi.ShouldBe(p.Phi, 1e-13);
        }
    }
}