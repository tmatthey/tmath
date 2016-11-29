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
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Math.Tools.TrackReaders.Gpx;
using Math.Tools.TrackReaders.Tcx;

namespace Math.Tools.TrackReaders
{
    /// <summary>
    /// Reading activities with GPS tracks
    /// </summary>
    public static class Reader
    {
        /// <summary>
        /// Reading and parsing a TCX or GPX based on its file type
        /// </summary>
        /// <param name="filename">File name with *.tcx and *.gpx file type</param>
        /// <returns></returns>
        public static Track ParseFile(string filename)
        {
            Track track = null;
            if (File.Exists(filename))
            {
                var extension = Path.GetExtension(filename);
                if (extension.Contains("tcx"))
                {
                    track = TcxConverter.Convert(Parse<TrainingCenterDatabase_t>(filename));
                }
                if (extension.Contains("gpx"))
                {
                    track = GpxConverter.Convert(Parse<gpx>(filename));
                }
                if (track != null)
                    track.Name = Path.GetFileNameWithoutExtension(filename);
            }
            return track;
        }

        /// <summary>
        /// Parses all TCX and GPX files of a given directory
        /// </summary>
        /// <param name="path">Path name</param>
        /// <returns></returns>
        public static IEnumerable<Track> ParseDirectory(string path)
        {
            var files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(path, "*.tcx").ToList());
            }
            catch
            {
            }
            try
            {
                files.AddRange(Directory.GetFiles(path, "*.gpx").ToList());
            }
            catch
            {
            }

            foreach (var file in files)
            {
                Track track = null;
                try
                {
                    track = ParseFile(file);
                }
                catch
                {
                }
                if (track != null && track.GpsPoints().Any())
                {
                    yield return track;
                }
            }
        }

        private static T Parse<T>(string input) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            T data;
            using (var reader = new StreamReader(input))
            {
                data = serializer.Deserialize(reader) as T;
            }

            return data;
        }
    }
}