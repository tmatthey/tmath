/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2018 Thierry Matthey
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

using System;
using System.Collections.Generic;

namespace Math.Tests
{
    public static class TrajectoryExamples
    {
        private static IList<List<Vector2D>> ReadTracks(string name)
        {
            var tracks = new List<List<Vector2D>>();
            var reader = TestUtils.ReadResourceFile(name);

            var l = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                l++;
                if (l < 3)
                    continue;

                var array = line.Split(' ');
                var list = new List<Vector2D>();
                for (var i = 2; i + 1 < array.Length; i += 2)
                {
                    var x = Convert.ToDouble(array[i]);
                    var y = Convert.ToDouble(array[i + 1]);
                    list.Add(new Vector2D(x, y));
                }

                tracks.Add(list);
            }


            return tracks;
        }

        public static IList<List<Vector2D>> deer_1995()
        {
            return ReadTracks("deer_1995.tra");
        }

        //public static IList<List<Vector2D>> hurricane1950_2006()

        //{
        //    return ReadTracks("hurricane1950_2006.tra");
        //}
    }
}