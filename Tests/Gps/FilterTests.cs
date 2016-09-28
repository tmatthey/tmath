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
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class FilterTests
    {
        private static List<double> CreateTimeArray(ICollection<GpsPoint> track)
        {
            var time = new List<double>();
            for (var i = 0; i < track.Count; i++)
            {
                time.Add(i);
            }
            return time;
        }

        [Test]
        public void SmoothZeroDisplacements_1ElementList_ReturnsId()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0)};
            Filter.SmoothZeroDisplacements(track).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_2ElementList_ReturnsId()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            Filter.SmoothZeroDisplacements(track).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_3ElementList_ReturnsId()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0), new GpsPoint(2, 0)};
            Filter.SmoothZeroDisplacements(track).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_4ElementList_ReturnsId()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0)
            };
            Filter.SmoothZeroDisplacements(track).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_EmptyList_ReturnsId()
        {
            var track = new List<GpsPoint>();
            Filter.SmoothZeroDisplacements(track).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_LongHoleBack_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(6, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track);
            res.Count.ShouldBe(7);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(3, 1e-7);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(4, 1e-7);
            res[4].Longitude.ShouldBe(0);
            res[5].Latitude.ShouldBe(5, 1e-7);
            res[5].Longitude.ShouldBe(0);
            res[6].Latitude.ShouldBe(6);
            res[6].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_LongHoleFront_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(5, 0),
                new GpsPoint(5, 0),
                new GpsPoint(5, 0),
                new GpsPoint(5, 0),
                new GpsPoint(6, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track);
            res.Count.ShouldBe(7);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2, 1e-7);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(3, 1e-7);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(4, 1e-7);
            res[4].Longitude.ShouldBe(0);
            res[5].Latitude.ShouldBe(5, 1e-7);
            res[5].Longitude.ShouldBe(0);
            res[6].Latitude.ShouldBe(6);
            res[6].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_LongStayPoint_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2.1, 0),
                new GpsPoint(3, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track);
            res.Count.ShouldBe(7);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(2);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(2);
            res[4].Longitude.ShouldBe(0);
            res[5].Latitude.ShouldBe(2.1);
            res[5].Longitude.ShouldBe(0);
            res[6].Latitude.ShouldBe(3);
            res[6].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_LongStayPointWithBigResume_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0),
                new GpsPoint(6, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track);
            res.Count.ShouldBe(7);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(2);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(2);
            res[4].Longitude.ShouldBe(0);
            res[5].Latitude.ShouldBe(4);
            res[5].Longitude.ShouldBe(0);
            res[6].Latitude.ShouldBe(6);
            res[6].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_SimpleHoleBack_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track);
            res.Count.ShouldBe(5);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(3, 1e-7);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(4);
            res[4].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_SimpleHoleFront_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(3, 0),
                new GpsPoint(3, 0),
                new GpsPoint(4, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track);
            res.Count.ShouldBe(5);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2, 1e-7);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(3);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(4);
            res[4].Longitude.ShouldBe(0);
        }


        [Test]
        public void SmoothZeroDisplacements_SimpleStayPoint_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(3, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track);
            res.Count.ShouldBe(5);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(2);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(3);
            res[4].Longitude.ShouldBe(0);
        }

        // Weighted

        [Test]
        public void SmoothZeroDisplacements_TrivialWeighted1ElementList_ReturnsId()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0)};
            Filter.SmoothZeroDisplacements(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_TrivialWeighted2ElementList_ReturnsId()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            Filter.SmoothZeroDisplacements(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_TrivialWeighted3ElementList_ReturnsId()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0), new GpsPoint(2, 0)};
            Filter.SmoothZeroDisplacements(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_TrivialWeighted4ElementList_ReturnsId()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(3, 0)
            };
            Filter.SmoothZeroDisplacements(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_TrivialWeightedEmptyList_ReturnsId()
        {
            var track = new List<GpsPoint>();
            Filter.SmoothZeroDisplacements(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void SmoothZeroDisplacements_TrivialWeightedLongHoleBack_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(6, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track, CreateTimeArray(track));
            res.Count.ShouldBe(7);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(3, 1e-7);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(4, 1e-7);
            res[4].Longitude.ShouldBe(0);
            res[5].Latitude.ShouldBe(5, 1e-7);
            res[5].Longitude.ShouldBe(0);
            res[6].Latitude.ShouldBe(6);
            res[6].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_TrivialWeightedLongHoleFront_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(5, 0),
                new GpsPoint(5, 0),
                new GpsPoint(5, 0),
                new GpsPoint(5, 0),
                new GpsPoint(6, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track, CreateTimeArray(track));
            res.Count.ShouldBe(7);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2, 1e-7);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(3, 1e-7);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(4, 1e-7);
            res[4].Longitude.ShouldBe(0);
            res[5].Latitude.ShouldBe(5, 1e-7);
            res[5].Longitude.ShouldBe(0);
            res[6].Latitude.ShouldBe(6);
            res[6].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_TrivialWeightedLongStayPoint_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2.1, 0),
                new GpsPoint(3, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track, CreateTimeArray(track));
            res.Count.ShouldBe(7);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(2);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(2);
            res[4].Longitude.ShouldBe(0);
            res[5].Latitude.ShouldBe(2.1);
            res[5].Longitude.ShouldBe(0);
            res[6].Latitude.ShouldBe(3);
            res[6].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_TrivialWeightedSimpleHoleBack_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track, CreateTimeArray(track));
            res.Count.ShouldBe(5);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(3, 1e-7);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(4);
            res[4].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_TrivialWeightedSimpleHoleFront_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(3, 0),
                new GpsPoint(3, 0),
                new GpsPoint(4, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track, CreateTimeArray(track));
            res.Count.ShouldBe(5);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2, 1e-7);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(3);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(4);
            res[4].Longitude.ShouldBe(0);
        }


        [Test]
        public void SmoothZeroDisplacements_TrivialWeightedSimpleStayPoint_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(3, 0)
            };
            var res = Filter.SmoothZeroDisplacements(track, CreateTimeArray(track));
            res.Count.ShouldBe(5);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(2);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(3);
            res[4].Longitude.ShouldBe(0);
        }

        [Test]
        public void SmoothZeroDisplacements_WeightedLongHoleBack_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(6, 0)
            };

            var time = new List<double> {0, 1, 2, 5, 6};
            var res = Filter.SmoothZeroDisplacements(track, time);
            res.Count.ShouldBe(5);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(5, 1e-7);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(6);
            res[4].Longitude.ShouldBe(0);
        }
    }
}