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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Math.Tools.TrackReaders.Gpx;
using Math.Tools.TrackReaders.Kml;
using Math.Tools.TrackReaders.Tcx;

namespace Math.Tools.TrackReaders
{
    /// <summary>
    /// Reading activities containing GPS track by file, directory or string definition. Supported file formate: TCX, GPX and KML.
    /// </summary>
    public static class Deserializer
    {
        /// <summary>
        /// Tryining to parse a string as a TCX, GPX or KML
        /// </summary>
        /// <param name="input">String containing a TCX, GPX or KML definition</param>
        /// <returns></returns>
        public static Track String(string input)
        {
            Track track;
            using (var stream = new StringReader(input))
            {
                track = TcxConverter.Convert(Parse<TrainingCenterDatabase_t>(stream));

                if (track == null)
                    track = GpxConverter.Convert(Parse<gpx>(stream));

                if (track == null)
                    track = KmlConverter.Convert(Parse<kml>(stream));
            }
            return track;
        }

        /// <summary>
        /// Reading and trying to parse either as TCX, GPX or KML based on its file type
        /// </summary>
        /// <param name="filename">File name with *.tcx, *.gpx and *.kml file type</param>
        /// <returns></returns>
        public static Track File(string filename)
        {
            Track track = null;
            if (System.IO.File.Exists(filename))
            {
                var extension = Path.GetExtension(filename);
                if (extension == null)
                    return null;
                try
                {
                    using (var reader = System.IO.File.OpenText(filename))
                    {
                        if (extension.Contains("tcx"))
                        {
                            track = TcxConverter.Convert(Parse<TrainingCenterDatabase_t>(reader));
                        }
                        else if (extension.Contains("gpx"))
                        {
                            track = GpxConverter.Convert(Parse<gpx>(reader));
                        }
                        else if (extension.Contains("kml"))
                        {
                            track = KmlConverter.Convert(Parse<kml>(reader));
                        }
                    }
                    if (track != null)
                        track.Name = Path.GetFileNameWithoutExtension(filename);
                }
                catch 
                {
                    // ignored
                }
            }
            return track;
        }

        /// <summary>
        /// Parses all TCX and GPX files of a given directory
        /// </summary>
        /// <param name="path">Path name</param>
        /// <returns></returns>
        public static IEnumerable<Track> Directory(string path)
        {
            var files = new List<string>();
            try
            {
                files.AddRange(System.IO.Directory.GetFiles(path, "*.tcx").ToList());
            }
            catch
            {
                // ignored
            }
            try
            {
                files.AddRange(System.IO.Directory.GetFiles(path, "*.gpx").ToList());
            }
            catch
            {
                // ignored
            }

            try
            {
                files.AddRange(System.IO.Directory.GetFiles(path, "*.kml").ToList());
            }
            catch
            {
                // ignored
            }

            foreach (var file in files)
            {
                var track = File(file);
                if (track != null && track.GpsPoints().Any())
                {
                    yield return track;
                }
            }
        }

        private static T Parse<T>(TextReader input) where T : class
        {
            try
            {
                 using (var xmlReader = XmlReader.Create(input))
                 {
                    var serializer = new XmlSerializer(typeof(T));
                    if (serializer.CanDeserialize(xmlReader))
                        return serializer.Deserialize(xmlReader) as T;
                 }
            }
            catch
            {
                // ignored
            }
            return null;
        }
    }
}