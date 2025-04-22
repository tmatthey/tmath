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

using NUnit.Framework;
using Shouldly;

namespace Math.Tools.TrackReaders.Tests
{
    [TestFixture]
    public class DeserializerTest
    {
        [Test]
        public void  Deserializer_WithGpx_ReturnsData()
        {
            var data = Deserializer.String("<?xml version=\"1.0\"?>\n<gpx version=\"1.1\" creator=\"gpxgenerator.com\" xmlns=\"http://www.topografix.com/GPX/1/1\">\n<trk>\n<trkseg>\n<trkpt lat=\"50.089533\" lon=\"14.427098\">\n    <ele>190.20</ele>\n    <time>2019-03-29T12:31:34Z</time>\n</trkpt>\n</trkseg>\n</trk>\n</gpx>\n");
            data.ShouldNotBeNull();
        }

    }
}
