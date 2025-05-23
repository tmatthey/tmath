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

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Math.Tests
{
    public static class TestUtils
    {
        private static Stopwatch _stopwatch;

        public static void StartTimer()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public static void StopTimer()
        {
            _stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", _stopwatch.Elapsed);
        }

        public static string Resources(string name)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var assemblyName = assembly.GetName().Name;

                var filename = $"{assemblyName}.Resources.{name}";

                var resourceStream = assembly.GetManifestResourceStream(filename);
                if (resourceStream != null)
                {
                    using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        public static string OutputPath()
        {
            var path = Assembly.GetExecutingAssembly().CodeBase.Substring(0, Assembly.GetExecutingAssembly().CodeBase.IndexOf("/bin/")) + "/Output/";
            path = path.Replace("file:///", "");
            path = path.Replace('/', '\\');

            if (!string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}