/*
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

using System.Collections.Generic;
using System.Linq;
using Math.Gps;
using Math.Gps.Filters;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class GpsFilteringTests
    {
        private static List<double> CreateTimeArray(ICollection<GpsPoint> track, double size = 1.0)
        {
            var time = new List<double>();
            for (var i = 0; i < track.Count; i++)
            {
                time.Add(i * size);
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
            GpsFiltering.CountDuplicates(track).ShouldBe(1);
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

            var res = GpsFiltering.InterpolateDuplicates(track).ToList();
            res[0].ShouldBe(track[0]);
            res[0].HaversineDistance(res[1]).ShouldBe(res[1].HaversineDistance(res[2]), 1e-7);
            res[2].ShouldBe(track[1]);
            res[3].ShouldBe(track[3]);
            res[4].ShouldBe(track[4]);
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

            var res = GpsFiltering.InterpolateDuplicates(track).ToList();
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

            var res = GpsFiltering.InterpolateDuplicates(track, new List<double> {0, 1, 2, 2.5, 3, 3.5, 4}).ToList();
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
            var res = GpsFiltering.FindDuplicates(track).ToList();
            res.Count.ShouldBe(2);
            res[0].ShouldBe(1);
            res[1].ShouldBe(2);
        }


        [Test]
        public void InterpolateDuplicates_1ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0)};
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_2ElementList_ReturnsCopy()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            var res = GpsFiltering.InterpolateDuplicates(track);
            res[0].Latitude += 0.1;
            track[0].Latitude.ShouldNotBe(res[0].Latitude);
        }

        [Test]
        public void InterpolateDuplicates_2ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_3ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0), new GpsPoint(2, 0)};
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_4ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0)
            };
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_4ElementListDuplicatesAtBegin_ReturnsSame()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(0, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0)
            };
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_4ElementListDuplicatesAtEnd_ReturnsSame()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(4, 0),
                new GpsPoint(4, 0)
            };
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_DoubleDuplicates_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(6, 0),
                new GpsPoint(6, 0),
                new GpsPoint(8, 0),
                new GpsPoint(8, 0),
                new GpsPoint(9, 0),
                new GpsPoint(10, 0)
            };
            var res = GpsFiltering.InterpolateDuplicates(track, new List<double> {0, 1, 3, 5, 6, 7, 8, 9});

            var l0 = res[0].HaversineDistance(res[1]);
            var l1 = res[1].HaversineDistance(res[2]);
            var l2 = res[2].HaversineDistance(res[3]);
            var l3 = res[3].HaversineDistance(res[4]);
            var l4 = res[4].HaversineDistance(res[5]);
            var l5 = res[5].HaversineDistance(res[6]);
            var l6 = res[6].HaversineDistance(res[7]);

            l0.ShouldBe(Geodesy.DistanceOneDeg, 1e-4);
            l1.ShouldBe(Geodesy.DistanceOneDeg * 2.0, 1e-4);
            l2.ShouldBe(Geodesy.DistanceOneDeg * 2.0, 1e-4);
            l3.ShouldBe(Geodesy.DistanceOneDeg, 1e-4);
            l4.ShouldBe(Geodesy.DistanceOneDeg, 1e-4);
            l5.ShouldBe(Geodesy.DistanceOneDeg, 1e-4);
            l6.ShouldBe(Geodesy.DistanceOneDeg, 1e-4);
        }

        [Test]
        public void InterpolateDuplicates_EmptyList_ReturnsSame()
        {
            var track = new List<GpsPoint>();
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_ExampleDoubleDuplicates_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(60.4094310, 5.3474750),
                new GpsPoint(60.4094170, 5.3475010),
                new GpsPoint(60.4093860, 5.3475340),
                new GpsPoint(60.4093860, 5.3475340),
                new GpsPoint(60.4093630, 5.3475700),
                new GpsPoint(60.4093630, 5.3475700),
                new GpsPoint(60.4093550, 5.3476010),
                new GpsPoint(60.4093480, 5.3476550)
            };

            var res = GpsFiltering.InterpolateDuplicates(track);
            var l0 = res[1].HaversineDistance(res[2]);
            var l1 = res[2].HaversineDistance(res[3]);
            var l2 = res[3].HaversineDistance(res[4]);
            var l3 = res[4].HaversineDistance(res[5]);
            var x = l1 / l0 - 1.0;
            l1.ShouldBe(l0 * (1.0 + x), 1e-8);
            l2.ShouldBe(l0 * (1.0 + 2.0 * x), 1e-8);
            l3.ShouldBe(l0 * (1.0 + 3.0 * x), 1e-8);
        }

        [Test]
        public void InterpolateDuplicates_ExampleStart_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(60.2932420, 5.2931860),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931410, 5.2931440)
            };
            var res = GpsFiltering.InterpolateDuplicates(track);
            res[0].HaversineDistance(res[1]).ShouldBe(res[1].HaversineDistance(res[2]), 1e-7);
        }

        [Test]
        public void InterpolateDuplicates_ExampleStart_ReturnsImprovedVariance()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(60.2932420, 5.2931860),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931640, 5.2931540),
                new GpsPoint(60.2931410, 5.2931440)
            };
            var res = GpsFiltering.InterpolateDuplicates(track);
            var f0 = new FilterDuplicatesBegin();
            f0.Filter(track, new List<double> {0, 1, 2, 3}, new List<int> {0}, new List<int> {3});
            var f1 = new FilterDuplicatesBegin();
            f1.Filter(res.ToList(), new List<double> {0, 1, 2, 3}, new List<int> {0}, new List<int> {3});
            f0.NewAccelerationVariance.ShouldBe(f1.OldAccelerationVariance);
            f0.NewVelocityVariance.ShouldBe(f1.OldVelocityVariance);
        }

        [Test]
        public void InterpolateDuplicates_ExampleTunnel_ReturnsExpected()
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

            var res = GpsFiltering.InterpolateDuplicates(track);
            for (var i = 1; i + 2 < res.Count; i++)
                res[i].HaversineDistance(res[i + 1]).ShouldBe(res[i + 1].HaversineDistance(res[i + 2]), 1e-7);
        }

        [Test]
        public void InterpolateDuplicates_LongHoleBack_ReturnsExpected()
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
            var res = GpsFiltering.InterpolateDuplicates(track);
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
        public void InterpolateDuplicates_LongHoleFront_ReturnsExpected()
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
            var res = GpsFiltering.InterpolateDuplicates(track);
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
        public void InterpolateDuplicates_SimpleHoleBack_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2, 0),
                new GpsPoint(4, 0)
            };
            var res = GpsFiltering.InterpolateDuplicates(track);
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
        public void InterpolateDuplicates_SimpleHoleFront_ReturnsExpected()
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
            var res = GpsFiltering.InterpolateDuplicates(track);
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
        public void InterpolateDuplicates_SimpleStayPoint_ReturnsExpected()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(2 + 1e-7, 0),
                new GpsPoint(3, 0)
            };
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_StayPoint_ReturnsExpected()
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
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_StayPointElevationChange_ReturnsExpected()
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
            GpsFiltering.InterpolateDuplicates(track).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_TrivialWeighted1ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0)};
            GpsFiltering.InterpolateDuplicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_TrivialWeighted2ElementList_ReturnsCopy()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            var res = GpsFiltering.InterpolateDuplicates(track, CreateTimeArray(track));
            res[0].Latitude += 0.1;
            track[0].Latitude.ShouldNotBe(res[0].Latitude);
        }

        [Test]
        public void InterpolateDuplicates_TrivialWeighted2ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            GpsFiltering.InterpolateDuplicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_TrivialWeighted3ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0), new GpsPoint(2, 0)};
            GpsFiltering.InterpolateDuplicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_TrivialWeighted4ElementList_ReturnsSame()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint(0, 0),
                new GpsPoint(1, 0),
                new GpsPoint(2, 0),
                new GpsPoint(3, 0)
            };
            GpsFiltering.InterpolateDuplicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_TrivialWeightedEmptyList_ReturnsSame()
        {
            var track = new List<GpsPoint>();
            GpsFiltering.InterpolateDuplicates(track, CreateTimeArray(track)).ShouldBe(track);
        }

        [Test]
        public void InterpolateDuplicates_WeightedHoleBack_ReturnsExpected()
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
            var res = GpsFiltering.InterpolateDuplicates(track, time);
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
        public void InterpolateDuplicates_WeightedHoleFront_ReturnsExpected()
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
            var res = GpsFiltering.InterpolateDuplicates(track, time);
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