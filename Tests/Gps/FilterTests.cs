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
using System.Linq;
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class FilterTests
    {
        private static List<double> CreateTimeArray(ICollection<GpsPoint> track, double size = 1.0)
        {
            var time = new List<double>();
            for (var i = 0; i < track.Count; i++)
            {
                time.Add(i*size);
            }
            return time;
        }

        [Test]
        public void CountDuplicates_ExampleStart_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(60.2932420, 5.2931860),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931410, 5.2931440)
            };
            Filter.CountDuplicates(track).ShouldBe(1);
        }

        [Test]
        public void FindDuplicates_ExampleError_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(60.3179650, 5.3487150),
                new GpsPoint(60.3180170, 5.3483900),
                new GpsPoint(60.3180420, 5.3482290),
                new GpsPoint(60.3180420, 5.3482290),
                new GpsPoint(60.3180700, 5.3480650)
            };

            var res = Filter.InterpolateDublicates(track).ToList();
            res[0].ShouldBe(track[0]);
            res[0].HaversineDistance(res[1]).ShouldBe(res[1].HaversineDistance(res[2]), 1e-7);
            res[2].ShouldBe(track[1]);
            res[3].ShouldBe(track[3]);
            res[4].ShouldBe(track[4]);

            var d0 = track[1].HaversineDistance(track[2]);
            var d1 = track[3].HaversineDistance(track[4]);
            var d01 = track[1].HaversineDistance(track[4]);
        }

        [Test]
        public void FindDuplicates_ExampleErrorSynthetic_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(2, 0),
                new GpsPoint(3, 0),
                new GpsPoint(3, 0),
                new GpsPoint(4, 0)
            };

            var res = Filter.InterpolateDublicates(track).ToList();
            res[0].ShouldBe(track[0]);
            res[0].HaversineDistance(res[1]).ShouldBe(res[1].HaversineDistance(res[2]), 1e-7);
            res[2].ShouldBe(track[1]);
            res[3].ShouldBe(track[3]);
            res[4].ShouldBe(track[4]);
        }

        [Test]
        public void FindDuplicates_ExampleErrorSyntheticLong_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(2, 0),
                new GpsPoint(3, 0),
                new GpsPoint(3, 0),
                new GpsPoint(3, 0),
                new GpsPoint(3, 0),
                new GpsPoint(4, 0)
            };

            var res = Filter.InterpolateDublicates(track, new List<double> {0, 1, 2, 2.5, 3, 3.5, 4}).ToList();
            res[0].ShouldBe(track[0]);
            res[1].ShouldBe(track[1]);
            res[1].Latitude.ShouldBe(2);
            res[2].Latitude.ShouldBe(3);
            res[3].Latitude.ShouldBe(3.25);
            res[4].Latitude.ShouldBe(3.5);
            res[5].Latitude.ShouldBe(3.75, 1e-8);
            res[6].ShouldBe(track[6]);
        }

        [Test]
        public void FindDuplicates_ExampleStart_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(60.2932420, 5.2931860),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931410, 5.2931440)
            };
            var res = Filter.FindDuplicates(track).ToList();
            res.Count.ShouldBe(2);
            res[0].ShouldBe(1);
            res[1].ShouldBe(2);
        }

        [Test]
        public void InterpolateDublicates_1ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0)};
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_2ElementList_ReturnsCopy()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            var res = Filter.InterpolateDublicates(track);
            res[0].Latitude += 0.1;
            track[0].Latitude.ShouldNotBe(res[0].Latitude);
        }

        [Test]
        public void InterpolateDublicates_2ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_3ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0), new GpsPoint(2, 0)};
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_4ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0)
            };
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_4ElementListDublicatesAtBegin_ReturnsSame()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(0, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0)
            };
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_4ElementListDublicatesAtEnd_ReturnsSame()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(4, 0),
                new GpsPoint(4, 0)
            };
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_EmptyList_ReturnsSame()
        {
            var track = new List<GpsPoint>();
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_ExampleStart_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(60.2932420, 5.2931860),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931410, 5.2931440)
            };
            var res = Filter.InterpolateDublicates(track);
            res[0].HaversineDistance(res[1]).ShouldBe(res[1].HaversineDistance(res[2]), 1e-7);
        }

        [Test]
        public void InterpolateDublicates_ExampleTunnel_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(60.3746950, 5.3589420),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3746230, 5.3589370),
                new GpsPoint(60.3722500, 5.3596050)
            };

            var res = Filter.InterpolateDublicates(track);
            for (var i = 1; i + 2 < res.Count; i++)
                res[i].HaversineDistance(res[i + 1]).ShouldBe(res[i + 1].HaversineDistance(res[i + 2]), 1e-7);
        }

        [Test]
        public void InterpolateDublicates_LongHoleBack_ReturnsExpected()
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
            var res = Filter.InterpolateDublicates(track);
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
        public void InterpolateDublicates_LongHoleFront_ReturnsExpected()
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
            var res = Filter.InterpolateDublicates(track);
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
        public void InterpolateDublicates_SimpleHoleBack_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0)
            };
            var res = Filter.InterpolateDublicates(track);
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
        public void InterpolateDublicates_SimpleHoleFront_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(3, 0),
                new GpsPoint(3, 0),
                new GpsPoint(4, 0),
                new GpsPoint(5, 0)
            };
            var res = Filter.InterpolateDublicates(track);
            res.Count.ShouldBe(6);
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
            res[5].Latitude.ShouldBe(5);
            res[5].Longitude.ShouldBe(0);
        }


        [Test]
        public void InterpolateDublicates_SimpleStayPoint_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2 + 1e-7, 0),
                new GpsPoint(3, 0)
            };
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_StayPoint_ReturnsExpected()
        {
            var eps = 1e-7;
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2 + eps, 0),
                new GpsPoint(2, eps),
                new GpsPoint(2 - eps, 0),
                new GpsPoint(3, 0)
            };
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_StayPointElevationChange_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0, 0.1),
                new GpsPoint(2, 0, 0.2),
                new GpsPoint(2, 0, 0.3),
                new GpsPoint(3, 0)
            };
            Filter.InterpolateDublicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_TrivialWeighted1ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0)};
            Filter.InterpolateDublicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_TrivialWeighted2ElementList_ReturnsCopy()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            var res = Filter.InterpolateDublicates(track, CreateTimeArray(track));
            res[0].Latitude += 0.1;
            track[0].Latitude.ShouldNotBe(res[0].Latitude);
        }

        [Test]
        public void InterpolateDublicates_TrivialWeighted2ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            Filter.InterpolateDublicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_TrivialWeighted3ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0), new GpsPoint(2, 0)};
            Filter.InterpolateDublicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_TrivialWeighted4ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(3, 0)
            };
            Filter.InterpolateDublicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_TrivialWeightedEmptyList_ReturnsSame()
        {
            var track = new List<GpsPoint>();
            Filter.InterpolateDublicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDublicates_WeightedHoleBack_ReturnsExpected()
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
            var res = Filter.InterpolateDublicates(track, time);
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


        [Test]
        public void InterpolateDublicates_WeightedHoleFront_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(5, 0),
                new GpsPoint(5, 0),
                new GpsPoint(6, 0)
            };

            var time = new List<double> {0, 1, 2, 5, 6};
            var res = Filter.InterpolateDublicates(track, time);
            res.Count.ShouldBe(5);
            res[0].Latitude.ShouldBe(0);
            res[0].Longitude.ShouldBe(0);
            res[1].Latitude.ShouldBe(1);
            res[1].Longitude.ShouldBe(0);
            res[2].Latitude.ShouldBe(2, 1e-7);
            res[2].Longitude.ShouldBe(0);
            res[3].Latitude.ShouldBe(5, 1e-7);
            res[3].Longitude.ShouldBe(0);
            res[4].Latitude.ShouldBe(6);
            res[4].Longitude.ShouldBe(0);
        }
    }
}