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

using System.Linq;
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class FunctionTests
    {
        [TestCase(27.1)]
        [TestCase(0.13)]
        [TestCase(0.0)]
        [TestCase(-27.1)]
        [TestCase(-0.13)]
        public void Cbrt_WithNumber_ReturnsExpectedResult(double x)
        {
            var a = Function.Cbrt(x);
            var a3 = a * a * a;
            a3.ShouldBe(x, 1e-13);
        }

        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        public void Cbrt_WithNotNumber_ReturnsInput(double x)
        {
            var a = Function.Cbrt(x);
            var a3 = a * a * a;
            a3.ShouldBe(x);
        }

        [TestCase(27.1)]
        [TestCase(0.13)]
        [TestCase(0.0)]
        [TestCase(-27.1)]
        [TestCase(-0.13)]
        public void Qnrt_WithNumber_ReturnsExpectedResult(double x)
        {
            var a = Function.Qnrt(x);
            var a5 = a * a * a * a * a;
            a5.ShouldBe(x, 1e-13);
        }

        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        public void Qnrt_WithNotNumber_ReturnsInput(double x)
        {
            var a = Function.Qnrt(x);
            var a3 = a * a * a * a * a;
            a3.ShouldBe(x);
        }

        [TestCase(-System.Math.PI / 2.0)]
        [TestCase(-0.1)]
        [TestCase(0.0)]
        [TestCase(0.1)]
        [TestCase(System.Math.PI / 2.0)]
        public void FastSin_WithInRange_ReturnsExpected(double a)
        {
            var y = Function.FastSin(a);
            y.ShouldBe(System.Math.Sin(a), 0.0205);
        }

        [TestCase(0, 0, 1)]
        [TestCase(90, 1, 0)]
        [TestCase(180, 0, -1)]
        [TestCase(270, -1, 0)]
        [TestCase(360, 0, 1)]
        public void SinCos_Angle_ReturnsExpected(double a, double s, double c)
        {
            Function.SinCos(Conversion.DegToRad(a), out var sin, out var cos);
            sin.ShouldBe(s);
            cos.ShouldBe(c);
        }

        [TestCase(7, 7 - System.Math.PI * 2)]
        [TestCase(-1, -1 + System.Math.PI * 2)]
        [TestCase(3.1, 3.1)]
        [TestCase(0.0, 0.0)]
        [TestCase(System.Math.PI * 2, 0.0)]
        public void NormalizeAngle(double a, double expected)
        {
            var y = Function.NormalizeAngle(a);
            y.ShouldBe(expected);
        }

        [TestCase(0, 0)]
        [TestCase(179, 179)]
        [TestCase(180, 180)]
        [TestCase(181, -179)]
        [TestCase(-179, -179)]
        [TestCase(-180, 180)]
        [TestCase(-181, 179)]
        public void NormalizeAnglePi(double a, double expected)
        {
            a = Conversion.DegToRad(a);
            var y = Conversion.RadToDeg(Function.NormalizeAnglePi(a));
            y.ShouldBe(expected);
        }

        [TestCase(0, 0)]
        [TestCase(179, 179)]
        [TestCase(180, 180)]
        [TestCase(181, -179)]
        [TestCase(-179, -179)]
        [TestCase(-180, 180)]
        [TestCase(-181, 179)]
        public void NormalizeAngle180(double a, double expected)
        {
            var y = Function.NormalizeAngle180(a);
            y.ShouldBe(expected);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 5)]
        [TestCase(6, 8)]
        [TestCase(7, 13)]
        [TestCase(8, 21)]
        [TestCase(9, 34)]
        [TestCase(10, 55)]
        [TestCase(11, 89)]
        [TestCase(12, 144)]
        [TestCase(60, 1548008755920)]
        public void FibonacciInt(int n, long f)
        {
            Function.FibonacciInt(n).ShouldBe((ulong)f);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 5)]
        [TestCase(6, 8)]
        [TestCase(7, 13)]
        [TestCase(8, 21)]
        [TestCase(9, 34)]
        [TestCase(10, 55)]
        [TestCase(11, 89)]
        [TestCase(12, 144)]
        [TestCase(60, 1548008755920)]
        public void FibonacciBinet(int n, double f)
        {
            Function.FibonacciBinet(n).ShouldBe(f);
        }

        [TestCase(100)]
        [TestCase(200)]
        [TestCase(300)]
        public void FibonacciBinet_Equal_Fibonacci(int n)
        {
            var f = Function.Fibonacci(n);
            Function.FibonacciBinet(n).ShouldBe(f, f * 1e-13);
        }

        [TestCase(0, 1ul)]
        [TestCase(1, 1ul)]
        [TestCase(2, 2ul)]
        [TestCase(3, 6ul)]
        [TestCase(4, 24ul)]
        [TestCase(5, 120ul)]
        [TestCase(6, 720ul)]
        [TestCase(7, 5040ul)]
        [TestCase(8, 40320ul)]
        [TestCase(9, 362880ul)]
        [TestCase(10, 3628800ul)]
        [TestCase(11, 39916800ul)]
        [TestCase(12, 479001600ul)]
        [TestCase(20, 2432902008176640000ul)]
        [TestCase(21, 0ul)] // Overflow
        public void FactorialInt(int n, ulong f)
        {
            Function.FactorialInt(n).ShouldBe(f);
        }

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(4, 24)]
        [TestCase(5, 120)]
        [TestCase(6, 720)]
        [TestCase(7, 5040)]
        [TestCase(8, 40320)]
        [TestCase(9, 362880)]
        [TestCase(10, 3628800)]
        [TestCase(11, 39916800)]
        [TestCase(12, 479001600)]
        [TestCase(20, 2432902008176640000)]
        [TestCase(21, 2432902008176640000.0 * 21.0)]
        [TestCase(25, 2432902008176640000.0 * (21.0 * 22.0 * 23.0 * 24.0 * 25.0))]
        public void Factorial(int n, double f)
        {
            Function.Factorial(n).ShouldBe(f);
        }

        [TestCase(36, 8, 4)]
        [TestCase(36, 1, 1)]
        [TestCase(51, 3, 3)]
        [TestCase(81, 54, 27)]
        public void GCD(int a, int b, int gcd)
        {
            Function.GCD(a, b).ShouldBe(gcd);
        }

        [TestCase(1, false)]
        [TestCase(94439461, true)]
        [TestCase(94439460, false)]
        [TestCase(94439430, false)]
        [TestCase(94439420, false)]
        [TestCase(94439987, true)]
        [TestCase(94439431, false)]
        [TestCase(94439421, false)]
        [TestCase(98764321261, true)]
        [TestCase(103226621521, false)]
        [TestCase(922337203685477647L, true)]
        [TestCase(888888877777777L, true)]
        public void IsPrime(long n, bool prime)
        {
            Function.IsPrime(n).ShouldBe(prime);
        }

        [TestCase(0.0, 1, 2, 1)]
        [TestCase(0.25, 1, 2, 1.25)]
        [TestCase(0.5, 1, 2, 1.5)]
        [TestCase(1.0, 1, 2, 2)]
        public void InterpolateAsLinearCombination(double a, double x0, double x1, double x)
        {
            Function.Interpolate(a, x0, x1).ShouldBe(x);
        }

        [TestCase(0, 1, 2, 4, 6, 2)]
        [TestCase(0, 1, 2, 6, 4, 8)]
        [TestCase(1, 1, 2, 4, 6, 4)]
        [TestCase(1.25, 1, 2, 4, 6, 4.5)]
        [TestCase(1.5, 1, 2, 4, 6, 5)]
        [TestCase(2, 1, 2, 4, 6, 6)]
        [TestCase(3, 1, 2, 6, 4, 2)]
        [TestCase(0, 1, 1, 4, 6, 5)]
        [TestCase(1, 1, 1, 4, 6, 5)]
        [TestCase(2, 1, 1, 4, 6, 5)]
        public void Interpolate(double x, double x0, double x1, double y0, double y1, double y)
        {
            Function.Interpolate(x, x0, x1, y0, y1).ShouldBe(y);
        }

        [TestCase(-.01)]
        [TestCase(0)]
        [TestCase(0.23)]
        [TestCase(1)]
        [TestCase(2.34)]
        public void Interpolate_Vector2D(double x)
        {
            var v0 = new Vector2D(1, 2);
            var v1 = new Vector2D(3, 4);
            var expected = v0.Interpolate(v1, x);
            var res = Function.Interpolate(x, v0, v1);
            res.ShouldBe(expected);
        }

        [TestCase(-.01)]
        [TestCase(0)]
        [TestCase(0.23)]
        [TestCase(1)]
        [TestCase(2.34)]
        public void Interpolate_Vector3D(double x)
        {
            var v0 = new Vector3D(1, 2, 19);
            var v1 = new Vector3D(3, 4, 17);
            var expected = v0.Interpolate(v1, x);
            var res = Function.Interpolate(x, v0, v1);
            res.ShouldBe(expected);
        }

        [TestCase(-.01)]
        [TestCase(0)]
        [TestCase(0.23)]
        [TestCase(1)]
        [TestCase(2.34)]
        public void Interpolate_GpsPoint(double x)
        {
            var g0 = new GpsPoint(12, 34, 19);
            var g1 = new GpsPoint(-1.2, 17, -119);
            var expected = g0.Interpolate(g1, x);
            var res = Function.Interpolate(x, g0, g1);
            res.ShouldBe(expected);
        }

        [TestCase(0, 3.389064903)]
        [TestCase(0.5, 21.3730459481406d)]
        [TestCase(-0.5, 4.23789082143744d)]
        public void Minetti(double x, double e)
        {
            Function.Minetti(x).ShouldBe(e, 1e-10);
        }

        [TestCase(0, 3.389064903)]
        [TestCase(0.5, 21.3730459481406d)]
        [TestCase(-0.5, 4.23789082143744d)]
        public void MinettiFactor(double x, double e)
        {
            Function.MinettiFactor(x).ShouldBe(e / 3.389064903, 1e-10);
        }


        [Test]
        public void FactorialInt_Max()
        {
            var fmax = Function.FactorialInt(Function.MaxFactorialInt);
            var fdmax = Function.Factorial(Function.MaxFactorialInt);
            var fmaxp1 = Function.FactorialInt(Function.MaxFactorialInt + 1);
            var fdmaxp1 = Function.Factorial(Function.MaxFactorialInt + 1);
            fmax.ShouldBeGreaterThan(fmaxp1);
            ((double)fmax).ShouldBe(fdmax);
            fmaxp1.ShouldBe(0ul);
            fdmaxp1.ShouldBe(fdmax * (Function.MaxFactorialInt + 1));
        }

        [Test]
        public void FibonacciInt_Max()
        {
            var fdmaxm1 = Function.Fibonacci(Function.MaxFibonacciInt - 1);
            var fmax = Function.FibonacciInt(Function.MaxFibonacciInt);
            var fdmax = Function.Fibonacci(Function.MaxFibonacciInt);
            var fmaxp1 = Function.FibonacciInt(Function.MaxFibonacciInt + 1);
            var fdmaxp1 = Function.Fibonacci(Function.MaxFibonacciInt + 1);
            fmax.ShouldBeGreaterThan(fmaxp1);
            ((double)fmax).ShouldBe(fdmax);
            fmaxp1.ShouldBe(0ul);
            fdmaxp1.ShouldBe(fdmaxm1 + fdmax);
        }

        private readonly long[] Primes = new long[]
         {
                2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
                103,
                107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173,
                179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283,
                293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373,
                379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499,
                503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593,
                599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727,
                733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821,
                823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967,
                971, 977, 983, 991, 997, 1009, 1013, 1019, 1021, 1031, 1033, 1039,
                1049, 1051, 1061, 1063, 1069, 1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163,
                1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223, 1229, 1231, 1237,
                1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373,
                1381, 1399, 1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453,
                1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511, 1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571,
                1579, 1583, 1597, 1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637,
                1657, 1663, 1667, 1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759, 1777, 1783,
                1787, 1789, 1801, 1811, 1823, 1831, 1847, 1861, 1867, 1871, 1873,
                1877, 1879, 1889, 1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987, 1993, 1997, 1999, 2003,
                2011, 2017, 2027, 2029, 2039, 2053, 2063, 2069, 2081, 2083, 2087,
                2089, 2099, 2111, 2113, 2129, 2131, 2137, 2141, 2143, 2153, 2161, 2179, 2203, 2207, 2213, 2221, 2237,
                2239, 2243, 2251, 2267, 2269, 2273, 2281, 2287, 2293, 2297, 2309,
                2311, 2333, 2339, 2341, 2347, 2351, 2357, 2371, 2377, 2381, 2383, 2389, 2393, 2399, 2411, 2417, 2423,
                2437, 2441, 2447, 2459, 2467, 2473, 2477, 2503, 2521, 2531, 2539,
                2543, 2549, 2551, 2557, 2579, 2591, 2593, 2609, 2617, 2621, 2633, 2647, 2657, 2659, 2663, 2671, 2677,
                2683, 2687, 2689, 2693, 2699, 2707, 2711, 2713, 2719, 2729, 2731,
                2741, 2749, 2753, 2767, 2777, 2789, 2791, 2797, 2801, 2803, 2819, 2833, 2837, 2843, 2851, 2857, 2861,
                2879, 2887, 2897, 2903, 2909, 2917, 2927, 2939, 2953, 2957, 2963,
                2969, 2971, 2999, 3001, 3011, 3019, 3023, 3037, 3041, 3049, 3061, 3067, 3079, 3083, 3089, 3109, 3119,
                3121, 3137, 3163, 3167, 3169, 3181, 3187, 3191, 3203, 3209, 3217,
                3221, 3229, 3251, 3253, 3257, 3259, 3271, 3299, 3301, 3307, 3313, 3319, 3323, 3329, 3331, 3343, 3347,
                3359, 3361, 3371, 3373, 3389, 3391, 3407, 3413, 3433, 3449, 3457,
                3461, 3463, 3467, 3469, 3491, 3499, 3511, 3517, 3527, 3529, 3533, 3539, 3541, 3547, 3557, 3559, 3571,
                3581, 3583, 3593, 3607, 3613, 3617, 3623, 3631, 3637, 3643, 3659,
                3671, 3673, 3677, 3691, 3697, 3701, 3709, 3719, 3727, 3733, 3739, 3761, 3767, 3769, 3779, 3793, 3797,
                3803, 3821, 3823, 3833, 3847, 3851, 3853, 3863, 3877, 3881, 3889,
                3907, 3911, 3917, 3919, 3923, 3929, 3931, 3943, 3947, 3967, 3989, 4001, 4003, 4007, 4013, 4019, 4021,
                4027, 4049, 4051, 4057, 4073, 4079, 4091, 4093, 4099, 4111, 4127,
                4129, 4133, 4139, 4153, 4157, 4159, 4177, 4201, 4211, 4217, 4219, 4229, 4231, 4241, 4243, 4253, 4259,
                4261, 4271, 4273, 4283, 4289, 4297, 4327, 4337, 4339, 4349, 4357,
                4363, 4373, 4391, 4397, 4409, 4421, 4423, 4441, 4447, 4451, 4457, 4463, 4481, 4483, 4493, 4507, 4513,
                4517, 4519, 4523, 4547, 4549, 4561, 4567, 4583, 4591, 4597, 4603,
                4621, 4637, 4639, 4643, 4649, 4651, 4657, 4663, 4673, 4679, 4691, 4703, 4721, 4723, 4729, 4733, 4751,
                4759, 4783, 4787, 4789, 4793, 4799, 4801, 4813, 4817, 4831, 4861,
                4871, 4877, 4889, 4903, 4909, 4919, 4931, 4933, 4937, 4943, 4951, 4957, 4967, 4969, 4973, 4987, 4993,
                4999, 5003, 5009, 5011, 5021, 5023, 5039, 5051, 5059, 5077, 5081,
                5087, 5099, 5101, 5107, 5113, 5119, 5147, 5153, 5167, 5171, 5179, 5189, 5197, 5209, 5227, 5231, 5233,
                5237, 5261, 5273, 5279, 5281, 5297, 5303, 5309, 5323, 5333, 5347,
                5351, 5381, 5387, 5393, 5399, 5407, 5413, 5417, 5419, 5431, 5437, 5441, 5443, 5449, 5471, 5477, 5479,
                5483, 5501, 5503, 5507, 5519, 5521, 5527, 5531, 5557, 5563, 5569,
                5573, 5581, 5591, 5623, 5639, 5641, 5647, 5651, 5653, 5657, 5659, 5669, 5683, 5689, 5693, 5701, 5711,
                5717, 5737, 5741, 5743, 5749, 5779, 5783, 5791, 5801, 5807, 5813,
                5821, 5827, 5839, 5843, 5849, 5851, 5857, 5861, 5867, 5869, 5879, 5881, 5897, 5903, 5923, 5927, 5939,
                5953, 5981, 5987, 6007, 6011, 6029, 6037, 6043, 6047, 6053, 6067,
                6073, 6079, 6089, 6091, 6101, 6113, 6121, 6131, 6133, 6143, 6151, 6163, 6173, 6197, 6199, 6203, 6211,
                6217, 6221, 6229, 6247, 6257, 6263, 6269, 6271, 6277, 6287, 6299,
                6301, 6311, 6317, 6323, 6329, 6337, 6343, 6353, 6359, 6361, 6367, 6373, 6379, 6389, 6397, 6421, 6427,
                6449, 6451, 6469, 6473, 6481, 6491, 6521, 6529, 6547, 6551, 6553,
                6563, 6569, 6571, 6577, 6581, 6599, 6607, 6619, 6637, 6653, 6659, 6661, 6673, 6679, 6689, 6691, 6701,
                6703, 6709, 6719, 6733, 6737, 6761, 6763, 6779, 6781, 6791, 6793,
                6803, 6823, 6827, 6829, 6833, 6841, 6857, 6863, 6869, 6871, 6883, 6899, 6907, 6911, 6917, 6947, 6949,
                6959, 6961, 6967, 6971, 6977, 6983, 6991, 6997, 7001, 7013, 7019,
                7027, 7039, 7043, 7057, 7069, 7079, 7103, 7109, 7121, 7127, 7129, 7151, 7159, 7177, 7187, 7193, 7207,
                7211, 7213, 7219, 7229, 7237, 7243, 7247, 7253, 7283, 7297, 7307,
                7309, 7321, 7331, 7333, 7349, 7351, 7369, 7393, 7411, 7417, 7433, 7451, 7457, 7459, 7477, 7481, 7487,
                7489, 7499, 7507, 7517, 7523, 7529, 7537, 7541, 7547, 7549, 7559,
                7561, 7573, 7577, 7583, 7589, 7591, 7603, 7607, 7621, 7639, 7643, 7649, 7669, 7673, 7681, 7687, 7691,
                7699, 7703, 7717, 7723, 7727, 7741, 7753, 7757, 7759, 7789, 7793,
                7817, 7823, 7829, 7841, 7853, 7867, 7873, 7877, 7879, 7883, 7901, 7907, 7919
         };

        [Test]
        public void IsPrime_1st1000Primes()
        {
            for (var n = 2; n < Primes.Last(); n++)
            {
                var isprime = Function.IsPrime(n);
                var expected = Primes.ToList().Exists(p => p == n);
                isprime.ShouldBe(expected);
            }
        }

        [TestCase(30, 0, 0, 150.69)]
        [TestCase(30, 10, 10, 929.50)]
        [TestCase(30, -10, 10, -438.95)]
        [TestCase(30, 10, -10, 774.73)]
        [TestCase(30, -10, -10, -590.63)]
        public void CyclingPowers(double velocity, double gradient, double headWind, double power)
        {
            Function.CyclingPowers(velocity * 1000.0 / 3600.0, 75, 8, gradient / 100, headWind * 1000.0 / 3600.0).power.ShouldBe(power, 1e-1);
        }

        [TestCase(30, 0, 0)]
        [TestCase(30, 10, 10)]
        [TestCase(39.961492169697827, -10, 10)]
        [TestCase(30, 10, -10)]
        [TestCase(83.946511929765663, -10, -10)]
        public void CyclingVelocity(double velocity, double gradient, double headWind)
        {
            var power = Function.CyclingPowers(velocity * 1000.0 / 3600.0, 75, 10, gradient / 100, headWind * 1000.0 / 3600.0).power;
            Function.CyclingVelocity(power, 75, 10, gradient / 100, headWind * 1000.0 / 3600.0).ShouldBe(velocity * 1000.0 / 3600.0, 1e-6);
        }
    }
}