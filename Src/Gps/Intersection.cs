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

namespace Math.Gps
{
    public static class Intersection
    {
        public enum Result
        {
            Undefined,
            NotIntersecting,
            Same,
            Inside,
            Outside,
            Overlapping
        }

        public static Result MinCircle(GpsTrack one, GpsTrack two)
        {
            if (double.IsNaN(one.MinCircleAngle) || double.IsNaN(two.MinCircleAngle))
                return Result.Undefined;

            var r1 = one.MinCircleAngle*2.0*Geodesy.EarthRadius;
            var r2 = two.MinCircleAngle*2.0*Geodesy.EarthRadius;
            if (one.Center.Distance(two.Center) < 1.0 && Comparison.IsEqual(r1, r2, 1.0))
                return Result.Same;

            var r12 = System.Math.Abs(one.MinCircleCenter.Angle(two.MinCircleCenter))*2.0*Geodesy.EarthRadius;
            if (Comparison.IsLessEqual(r12 + r1, r2, 1.0))
                return Result.Outside;

            if (Comparison.IsLessEqual(r12 + r2, r1, 1.0))
                return Result.Inside;

            if (Comparison.IsLessEqual(r12, r1 + r2, 1.0))
                return Result.Overlapping;

            return Result.NotIntersecting;
        }

        public static Result Rect(GpsTrack one, GpsTrack two)
        {
            if (double.IsNaN(one.RotationAngle) || double.IsNaN(two.RotationAngle))
                return Result.Undefined;

            var t1 = one.CreateTransformedTrack();
            var t2 = two.CreateTransformedTrack();
            var r1 = t1.Size.Min.Distance(t1.Size.Max);
            var r2 = t2.Size.Min.Distance(t2.Size.Max);
            var r12 = System.Math.Abs(one.Center.Angle(two.Center))*2.0*Geodesy.EarthRadius;
            if (!Comparison.IsLessEqual(r12, r1 + r2, 1.0))
                return Result.NotIntersecting;

            t2 = two.CreateTransformedTrack(one.Center);
            if (t1.Size.Min.Distance(t2.Size.Min) < 1.0 && t1.Size.Max.Distance(t2.Size.Max) < 1.0)
                return Result.Same;

            var insideMin = t1.Size.IsInside(t2.Size.Min);
            var insideMax = t1.Size.IsInside(t2.Size.Max);
            var outsideMin = t2.Size.IsInside(t1.Size.Min);
            var outsideMax = t2.Size.IsInside(t1.Size.Max);

            if (insideMax && insideMin)
            {
                return Result.Inside;
            }
            if (outsideMin && outsideMax)
            {
                return Result.Outside;
            }

            if (insideMax || insideMin || outsideMin || outsideMax)
                return Result.Overlapping;

            return Result.NotIntersecting;
        }
    }
}