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

using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class RegressionTests
    {
        [Test]
        public void Linear_PressureExample()
        {
            var x = new List<double>
            {
                2059.172363,
                2163.431396,
                2263.08252,
                2362.733398,
                2462.384521,
                2562.035645,
                2661.686523,
                2761.337646,
                2860.98877,
                2960.639893,
                3060.290771,
                3159.941895,
                3259.593018,
                3359.244141,
                3458.89502,
                3558.546143,
                3658.197266,
                3757.848145,
                3857.499268,
                3957.150391,
                4056.801514,
                4156.452637,
                4256.103516,
                4355.754395,
                4455.405762,
                4555.056641,
                4654.708008,
                4754.358887,
                4854.009766,
                4953.661133,
                5053.312012,
                5152.962891,
                5252.614258,
                5352.265137,
                5451.916016,
                5551.567383,
                5651.218262,
                5750.869629,
                5850.520508,
                5950.171387,
                6049.822754,
                6149.473633,
                6249.124512,
                6348.775879,
                6448.426758,
                6548.077637,
                6647.729004,
                6747.379883,
                6847.030762,
                6946.682129,
                7046.333008,
                7145.984375,
                7245.635254,
                7345.286133,
                7444.9375,
                7544.588379,
                7644.239258,
                7743.890625,
                7843.541504,
                7943.192383,
                8042.84375,
                8142.494629,
                8242.145508,
                8341.796875,
                8441.448242,
                8544.836914,
                8650.388672,
                8752.672852,
                8851.446289,
                8946.477539,
                9037.556641,
                9124.495117,
                9207.138672,
                9285.366211,
                9359.091797,
                9428.274414,
                9492.911133,
                9553.041016,
                9608.744141,
                9660.134766,
                9707.363281,
                9750.607422,
                9790.068359,
                9825.964844,
                9858.530273,
                9888.000977,
                9914.619141,
                9938.62207,
                9960.243164,
                9977.947266,
                9990.063477,
                9997.360352
            };
            var y = new List<double>
            {
                207.54812,
                218.14716,
                228.29172,
                238.44964,
                248.62092,
                258.80558,
                269.00362,
                279.21508,
                289.43996,
                299.67824,
                309.92996,
                320.19512,
                330.47372,
                340.7658,
                351.07132,
                361.39036,
                371.72292,
                382.06896,
                392.42856,
                402.80172,
                413.18844,
                423.58872,
                434.0026,
                444.43012,
                454.8712,
                465.32596,
                475.79432,
                486.27636,
                496.77204,
                507.28144,
                517.80452,
                528.34128,
                538.8918,
                549.45604,
                560.03404,
                570.62576,
                581.23128,
                591.8506,
                602.48368,
                613.1306,
                623.79136,
                634.46592,
                645.15436,
                655.85664,
                666.5728,
                677.30288,
                688.0468,
                698.80472,
                709.57656,
                720.36232,
                731.162,
                741.97568,
                752.80336,
                763.64504,
                774.50072,
                785.3704,
                796.25416,
                807.15192,
                818.06384,
                828.98976,
                839.92976,
                850.88392,
                861.85224,
                872.83464,
                883.8312,
                895.25544,
                906.934,
                918.26624,
                929.2236,
                939.77912,
                949.9076,
                959.58688,
                968.79792,
                977.52576,
                985.75944,
                993.49264,
                1000.72384,
                1007.456,
                1013.6968,
                1019.45808,
                1024.75568,
                1029.60872,
                1034.03904,
                1038.07064,
                1041.7292,
                1045.04104,
                1048.03296,
                1050.7316,
                1053.1628,
                1055.15368,
                1056.51648,
                1057.33712
            };
            for (var i = 0; i < y.Count; i++)
            {
                y[i] -= 1.01325 + x[i]*9.80665/100.0;
                y[i] *= 1e5;
            }
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(937.97, 1e-1);
            b.ShouldBe(-2292965.91, 1e-1);
        }

        [Test]
        public void Linear_WithDifferentListCounts_ReturnsNaN()
        {
            var x = new List<double> {2.4};
            var y = new List<double> {225, 184, 220, 240, 180, 184, 186, 215};
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(double.NaN);
            b.ShouldBe(double.NaN);
        }

        [Test]
        public void Linear_WithOnePoint_ReturnsNaN()
        {
            var x = new List<double> {1};
            var y = new List<double> {1};
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(double.NaN);
            b.ShouldBe(double.NaN);
        }

        [Test]
        public void Linear_WithoutWeightExample2_ReturnsExpected()
        {
            // http://academic.macewan.ca/burok/Stat252/notes/regression1.pdf
            var x = new List<double> {2.4, 1.6, 2.0, 2.6, 1.4, 1.6, 2.0, 2.2};
            var y = new List<double> {225, 184, 220, 240, 180, 184, 186, 215};
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(62.65/1.235, 1e-10);
            b.ShouldBe(1634.0/8.0 - 62.65/1.235*15.8/8.0, 1e-10);
        }

        [Test]
        public void Linear_WithTwoPoints_ReturnsNaN()
        {
            var x = new List<double> {1, 2};
            var y = new List<double> {1, 1};
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(0.0);
            b.ShouldBe(1.0);
        }

        [Test]
        public void Linear_WithZeroListCounts_ReturnsNaN()
        {
            var x = new List<double>();
            var y = new List<double>();
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(double.NaN);
            b.ShouldBe(double.NaN);
        }
    }
}