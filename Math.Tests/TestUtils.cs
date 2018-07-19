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
using System.Diagnostics;
using System.IO;
using System.Reflection;

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

        public static StreamReader ReadResourceFile(string name)
        {
#if NETCOREAPP1_1
            const string assemblyName = "Math.Tests";
            var assembly = typeof(TestUtils).GetTypeInfo().Assembly;
#elif NETSTANDARD1_5
			const string assemblyName = "Math.Tests";
            var assembly = typeof(TestUtils).GetTypeInfo().Assembly;
#else
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;
#endif
            var filename = string.Format("{0}.Resources.{1}", assemblyName, name);

            return new StreamReader(assembly.GetManifestResourceStream(filename));
        }

        public static string OutputPath()
        {
#if NETCOREAPP1_1
            var i = AppContext.BaseDirectory.IndexOf("\\bin\\", StringComparison.Ordinal);
            var path = i >= 0 ? AppContext.BaseDirectory.Substring(0, i) + "\\Output\\" : "";
#elif NETSTANDARD1_5
			var i = AppContext.BaseDirectory.IndexOf("\\bin\\", StringComparison.Ordinal);
			var path = i >= 0 ? AppContext.BaseDirectory.Substring(0, i) + "\\Output\\" : "";
#else
			var path =
Assembly.GetExecutingAssembly().CodeBase.Substring(0, Assembly.GetExecutingAssembly().CodeBase.IndexOf("/bin/")) + "/Output/";
            path = path.Replace("file:///", "");
            path = path.Replace('/', '\\');
#endif
            if (!string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}