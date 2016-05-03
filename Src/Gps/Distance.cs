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
    public class Distance
    {
        public Distance(int reference, int current, double dist)
        {
            Reference = reference;
            Current = current;
            Dist = dist;
            Fraction = 0.0;
        }

        public Distance(int reference, int current, double dist, double fraction)
        {
            Reference = reference;
            Current = current;
            Dist = dist;
            Fraction = fraction;
        }

        public Distance(Distance d)
        {
            Reference = d.Reference;
            Current = d.Current;
            Dist = d.Dist;
            Fraction = d.Fraction;
        }

        public int Reference { get; private set; }
        public int Current { get; private set; }
        public double Dist { get; private set; }
        public double Fraction { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((Distance) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Reference.GetHashCode();
                hashCode = (hashCode*397) ^ Current.GetHashCode();
                return hashCode;
            }
        }

        public bool IsEqual(Distance d)
        {
            return d != null && Reference == d.Reference && Current == d.Current;
        }

        public int CompareTo(Distance d)
        {
            if (Comparison.IsEqual(Dist, d.Dist))
            {
                if (Current == d.Current && Reference == d.Reference)
                    return 0;
                return (Current == d.Current ? (Reference < d.Reference ? -1 : 1) : (Current < d.Current ? -1 : 1));
            }
            return Dist < d.Dist ? -1 : 1;
        }
    }
}