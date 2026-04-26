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

namespace Math.Gps
{
    /// <summary>
    /// Aggregates all points which are at least as close (perpendicular distance) as a given
    /// radius to the reference track. The four pipeline stages live in dedicated helpers:
    /// <list type="number">
    /// <item><description>grid candidate lookup - <see cref="GridLookup.Find"/></description></item>
    /// <item><description>perpendicular projection - <see cref="PerpendicularDistanceProjector"/></description></item>
    /// <item><description>radius cut-off - <see cref="RadiusCutOff"/></description></item>
    /// <item><description>disconnected-point pruning - <see cref="DisconnectedPointPruner"/></description></item>
    /// </list>
    /// </summary>
    public abstract class ANeighbourDistanceCalculator
    {
        protected FlatTrack _flatTrack;
        protected GridLookup _gridLookup;

        public FlatTrack ReferenceFlattenedTrack => _flatTrack;

        public NeighbourDistance Analyze(FlatTrack trackCur, double radius)
        {
            var neighboursCur = _gridLookup.Find(trackCur.Track, trackCur.Displacement, radius);
            var adjustedNeighboursCur =
                PerpendicularDistanceProjector.Project(neighboursCur, _gridLookup.FlattenedTrack, trackCur);
            var cutNeighboursCutoff = RadiusCutOff.Apply(adjustedNeighboursCur, radius);
            var cutNeighboursCur =
                DisconnectedPointPruner.Prune(radius, cutNeighboursCutoff, _gridLookup.FlattenedTrack);

            return new NeighbourDistance(trackCur, cutNeighboursCur);
        }

        public NeighbourDistance Analyze(IList<Vector2D> current, double radius)
        {
            return Analyze(new FlatTrack(current), radius);
        }
    }
}
