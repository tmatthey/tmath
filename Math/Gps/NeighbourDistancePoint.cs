/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2024 Thierry Matthey
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
    public class NeighbourDistancePoint
    {
        public NeighbourDistancePoint(int reference, int current, double minDistance)
        {
            Reference = reference;
            Current = current;
            MinDistance = minDistance;
            Fraction = double.NaN;
            RefDistance = double.NaN;
        }

        public NeighbourDistancePoint(int reference, int current, double minDistance, double fraction,
            double refDistance)
        {
            // Index of reference track
            Reference = reference;
            // Index of current / this track
            Current = current;
            // Perpendicular distance from current point to reference track (segment of point [Reference, Reference+1])
            MinDistance = minDistance;
            // Perpendicular parameter of point on segment [Reference, Reference+1] 
            Fraction = fraction;
            // Distance along reference track of perpendicular point
            RefDistance = refDistance;
        }

        public NeighbourDistancePoint(NeighbourDistancePoint d)
        {
            Reference = d.Reference;
            Current = d.Current;
            MinDistance = d.MinDistance;
            Fraction = d.Fraction;
            RefDistance = d.RefDistance;
        }

        public int Reference { get; }
        public int Current { get; }
        public double MinDistance { get; }
        public double Fraction { get; }
        public double RefDistance { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((NeighbourDistancePoint) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Reference.GetHashCode();
                hashCode = (hashCode * 397) ^ Current.GetHashCode();
                return hashCode;
            }
        }

        public bool IsEqual(NeighbourDistancePoint d)
        {
            return d != null && Reference == d.Reference && Current == d.Current;
        }

        public int CompareTo(NeighbourDistancePoint d)
        {
            if (Comparison.IsEqual(MinDistance, d.MinDistance))
            {
                if (Current == d.Current && Reference == d.Reference)
                    return 0;
                return Current == d.Current ? (Reference < d.Reference ? -1 : 1) : (Current < d.Current ? -1 : 1);
            }

            return MinDistance < d.MinDistance ? -1 : 1;
        }
    }
}