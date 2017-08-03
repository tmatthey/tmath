/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2017 Thierry Matthey
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
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class StatisticsTests
    {
        [TestCase(-17.19)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.NaN)]
        public void ArithmeticMean_OneNumber_ReturnsElement(double x)
        {
            var list = new List<double> {x};
            Statistics.Arithmetic.Mean(list).ShouldBe(x);
        }

        [TestCase(-17.19, 0.0)]
        [TestCase(double.NegativeInfinity, 1.23)]
        [TestCase(double.NaN, 2.34)]
        public void ArithmeticMeanWeighted_OneNumber_ReturnsElement(double x, double w)
        {
            var list = new List<double> {x};
            var weight = new List<double> {w};
            Statistics.Arithmetic.Mean(list, weight).ShouldBe(x);
        }

        [TestCase(-17.19)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.NaN)]
        public void ArithmeticVariance_OneNumber_ReturnsElement(double x)
        {
            var list = new List<double> {x};
            Statistics.Arithmetic.Variance(list).ShouldBe(0.0);
        }

        [TestCase(-17.19, 0.0)]
        [TestCase(double.NegativeInfinity, 1.23)]
        [TestCase(double.NaN, 2.34)]
        public void ArithmeticVarianceWeighted_OneNumber_ReturnsElement(double x, double w)
        {
            var list = new List<double> {x};
            var weight = new List<double> {w};
            Statistics.Arithmetic.Variance(list, weight).ShouldBe(0.0);
        }

        [TestCase(1.0)]
        [TestCase(double.NaN)]
        [TestCase(-1.17)]
        [TestCase(0.0)]
        public void CenteredMovingAverage_OneElement_ReturnsSame(double x)
        {
            var list = new List<double> {x};
            var res = Statistics.Arithmetic.CenteredMovingAverage(list, 2);
            res.Count.ShouldBe(list.Count);
            res[0].ShouldBe(x);
        }

        private static List<double> CreateWeightArray(ICollection<double> list, double weight = 1.0)
        {
            var w = new List<double>();
            for (var i = 0; i < list.Count; i++)
            {
                w.Add(weight);
            }
            return w;
        }

        [Test]
        public void ArithmeticMean_EmptyList_ReturnsNaN()
        {
            var list = new List<double>();
            Statistics.Arithmetic.Mean(list).ShouldBe(double.NaN);
        }

        [Test]
        public void ArithmeticMean_ThreeNumbers_ReturnsMean()
        {
            var list = new List<double> {3.1, 17, -19};
            Statistics.Arithmetic.Mean(list).ShouldBe((3.1 + 17 - 19)/3.0);
        }

        [Test]
        public void ArithmeticMeanWeighted_EmptyList_ReturnsNaN()
        {
            var list = new List<double>();
            var weight = new List<double>();
            Statistics.Arithmetic.Mean(list, weight).ShouldBe(double.NaN);
        }

        [Test]
        public void ArithmeticMeanWeighted_ThreeNumbers_ReturnsMean()
        {
            var list = new List<double> {3.1, 17, -19};
            var weight = new List<double> {2.3, 3.4, 5.6};
            Statistics.Arithmetic.Mean(list, weight).ShouldBe((3.1*2.3 + 17*3.4 - 19*5.6)/(2.3 + 3.4 + 5.6));
        }

        //
        [Test]
        public void ArithmeticVariance_EmptyList_ReturnsNaN()
        {
            var list = new List<double>();
            Statistics.Arithmetic.Variance(list).ShouldBe(double.NaN);
        }

        [Test]
        public void ArithmeticVariance_ThreeNumbers_ReturnsVariance()
        {
            var list = new List<double> {2, 3, 7};
            Statistics.Arithmetic.Variance(list).ShouldBe((2*2 + 1 + 3*3)/3.0);
        }

        [Test]
        public void ArithmeticVarianceWeighted_EmptyList_ReturnsNaN()
        {
            var list = new List<double>();
            var weight = new List<double>();
            Statistics.Arithmetic.Variance(list, weight).ShouldBe(double.NaN);
        }

        [Test]
        public void ArithmeticVarianceWeighted_ThreeNumbers_ReturnsVariance()
        {
            var list = new List<double> {30, 20, 15};
            var weight = new List<double> {400, 500, 600};
            var u = Statistics.Arithmetic.Mean(list, weight);
            Statistics.Arithmetic.Variance(list, weight)
                .ShouldBe((400*(30 - u)*(30 - u) + 500*(20 - u)*(20 - u) + 600*(15 - u)*(15 - u))/weight.Sum());
        }

        [Test]
        public void ArithmeticVarianceWeighted_ThreeNumbers_ReturnsVarianceExpandedNoWeights()
        {
            var list = new List<double> {30, 20, 15};
            var weight = new List<double> {1, 2, 3};
            var listNoWeights = new List<double> {30, 20, 20, 15, 15, 15};
            Statistics.Arithmetic.Variance(list, weight)
                .ShouldBe(Statistics.Arithmetic.Variance(listNoWeights), 1e-10);
        }

        [Test]
        public void ArithmeticVarianceWeighted_ThreeNumbersTrivialWeigths_ReturnsVarianceNoWeights()
        {
            var list = new List<double> {30, 20, 15};
            var weight = new List<double> {1, 1, 1};
            Statistics.Arithmetic.Variance(list, weight)
                .ShouldBe(Statistics.Arithmetic.Variance(list), 1e-10);
        }

        [Test]
        public void AverageAngleDefault_Vector2DE1_ReturnsSames()
        {
            var list = new List<Vector2D> {Vector2D.E1};
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(Statistics.Arithmetic.MeanAngle(list, Vector2D.E1), 1e-13);
        }

        [Test]
        public void CenteredMovingAverage_3ElementsSize2_ReturnsExpected()
        {
            var list = new List<double> {2, 3, 4};
            var expected = new List<double> {7.0/3.0, 3, 11.0/3.0};
            Statistics.Arithmetic.CenteredMovingAverage(list, 2.0).ShouldBe(expected);
        }

        [Test]
        public void CenteredMovingAverage_3ElementsSize3_ReturnsExpected()
        {
            var list = new List<double> {2, 3, 4};
            var expected = new List<double> {2.5, 3, 3.5};
            Statistics.Arithmetic.CenteredMovingAverage(list, 3.0).ShouldBe(expected);
        }

        [Test]
        public void CenteredMovingAverage_EmptyList_ReturnsEmptyList()
        {
            var list = new List<double>();
            Statistics.Arithmetic.CenteredMovingAverage(list, 2.0).Count.ShouldBe(list.Count);
        }

        [Test]
        public void CenteredMovingAverage_OneElement_ReturnsCopy()
        {
            var list = new List<double> {17.19};
            var res = Statistics.Arithmetic.CenteredMovingAverage(list, 2);
            res[0] += 0.1;
            res[0].ShouldNotBe(list[0]);
        }

        [Test]
        public void CenteredMovingAverage_TooSmallSize_ReturnsSame()
        {
            var list = new List<double> {17.19, 13.4, 5, 6, 23, 45};
            Statistics.Arithmetic.CenteredMovingAverage(list, 1.0).ShouldBe(list);
        }

        [Test]
        public void CenteredMovingAverageWeighted_EmptyList_ReturnsEmptyList()
        {
            var list = new List<double>();
            Statistics.Arithmetic.CenteredMovingAverage(list, 2.0*3.4, CreateWeightArray(list, 3.4))
                .Count.ShouldBe(list.Count);
        }

        [Test]
        public void CenteredMovingAverageWeighted_OneElement_ReturnsCopy()
        {
            var list = new List<double> {17.19};
            var res = Statistics.Arithmetic.CenteredMovingAverage(list, 2*3.4, CreateWeightArray(list, 3.4));
            res[0] += 0.1;
            res[0].ShouldNotBe(list[0]);
        }

        [Test]
        public void CenteredMovingAverageWeighted_TrivialWeights3ElementsSize2_ReturnsExpected()
        {
            var list = new List<double> {2, 3, 4};
            var expected = new List<double> {7.0/3.0, 3, 11.0/3.0};
            Statistics.Arithmetic.CenteredMovingAverage(list, 2.0*3.4, CreateWeightArray(list, 3.4))
                .ShouldBe(expected, 1e-8);
        }

        [Test]
        public void CenteredMovingAverageWeighted_TrivialWeights3ElementsSize3_ReturnsExpected()
        {
            var list = new List<double> {2, 3, 4};
            var expected = new List<double> {2.5, 3, 3.5};
            Statistics.Arithmetic.CenteredMovingAverage(list, 3.0*3.4, CreateWeightArray(list, 3.4))
                .ShouldBe(expected, 1e-8);
        }

        [Test]
        public void CenteredMovingAverageWeighted_TrivialWeightsTooSmallSize_ReturnsSame()
        {
            var list = new List<double> {17.19, 13.4, 5, 6, 23, 45};
            Statistics.Arithmetic.CenteredMovingAverage(list, 1.0*3.4, CreateWeightArray(list, 3.4)).ShouldBe(list);
        }

        [Test]
        public void MeanAngle_EmptyInput_ReturnsNaN()
        {
            var list = new List<double>();
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(double.NaN);
        }

        [Test]
        public void MeanAngle_FourDirection_ReturnsExpected()
        {
            var list = new List<double> {0, 0.5*System.Math.PI, System.Math.PI, 1.5*System.Math.PI};
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(0.75*System.Math.PI, 1e-13);
        }

        [Test]
        public void MeanAngle_OneElement_ReturnsElementNormalized()
        {
            var list = new List<double> {System.Math.PI*2.0 + 3.0};
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(3.0);
        }

        [Test]
        public void MeanAngle_OppositeHorDirection_ReturnsExpected()
        {
            var list = new List<double> {0.5*System.Math.PI, 1.5*System.Math.PI};
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(System.Math.PI, 1e-13);
        }

        [Test]
        public void MeanAngle_OppositeVerDirection_ReturnsExpected()
        {
            var list = new List<double> {0, System.Math.PI};
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(0.5*System.Math.PI, 1e-13);
        }

        [Test]
        public void MeanAngle_TwoElementsDifferentPlusMinus_ReturnsExpected()
        {
            var list = new List<double> {System.Math.PI*2.0 - 0.1, 0.3};
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(0.1, 1e-13);
        }

        [Test]
        public void MeanAngle_TwoElementsNotNormalizedDifferentPlusMinus_ReturnsExpected()
        {
            var list = new List<double> {-0.1, 0.3};
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(0.1, 1e-13);
        }

        [Test]
        public void MeanAngle_TwoElementsNotNormalizedPlusMinus_ReturnsZero()
        {
            var list = new List<double> {-0.1, 0.1};
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(0.0);
        }

        [Test]
        public void MeanAngle_TwoElementsPlusMinus_ReturnsZero()
        {
            var list = new List<double> {System.Math.PI*2.0 - 0.1, 0.1};
            Statistics.Arithmetic.MeanAngle(list).ShouldBe(0.0);
        }

        [Test]
        public void MeanAngle_Vector2DE1_ReturnsExpected()
        {
            var list = new List<Vector2D> {Vector2D.E1};
            Statistics.Arithmetic.MeanAngle(list, Vector2D.E1).ShouldBe(0.0, 1e-13);
        }

        [Test]
        public void MeanAngle_Vector2DE2_ReturnsExpected()
        {
            var list = new List<Vector2D> {Vector2D.E2};
            Statistics.Arithmetic.MeanAngle(list, Vector2D.E1).ShouldBe(0.5*System.Math.PI, 1e-13);
        }

        [Test]
        public void MeanAngle_Vector2DList_ReturnsSamesAsAngleList()
        {
            var angles = new List<double> {Conversion.DegToRad(180 + 45), Conversion.DegToRad(180 + 45 + 90)};
            var expected = Conversion.RadToDeg(Statistics.Arithmetic.MeanAngle(angles));
            var list = new List<Vector2D>();
            foreach (var angle in angles)
            {
                list.Add(new Vector2D(System.Math.Cos(angle), System.Math.Sin(angle)));
            }
            var res = Conversion.RadToDeg(Statistics.Arithmetic.MeanAngle(list, Vector2D.E1));
            res.ShouldBe(expected);
        }


        [Test]
        public void MeanAngle_Vector2DZero_ReturnsNaN()
        {
            var list = new List<Vector2D> {Vector2D.Zero};
            Statistics.Arithmetic.MeanAngle(list, Vector2D.E1).ShouldBe(double.NaN);
        }
    }
}