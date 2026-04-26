# tmath

A pragmatic .NET / C# math toolbox built around vectors, geodesy / GPS tracks,
clustering, geometry, and a small graphics layer for rendering tracks to
bitmaps.

The library targets `netstandard2.0` and the test suite runs on `net8.0`.

## Layout

```
Math/                 Library (netstandard2.0)
Math.Tests/           NUnit + Shouldly suite (net8.0)
Math/Math.md          Auto-generated API reference (do not edit by hand)
```

The full XML-doc API reference lives in `Math/Math.md`; this README focuses on
the most common things users actually call.

## Install

The project is consumed as a project reference - clone and add `Math/Math.csproj`
to your solution, or build a NuGet locally:

```bash
dotnet build -c Release
```

Run the tests:

```bash
dotnet test Math.Tests/Math.Tests.csproj -c Release
```

## Highlights

- `Vector2D`, `Vector3D`, `Polar3D`, `GpsPoint` - mutable point types with
  arithmetic operators, `Norm()`, `Normalized()`, `Dot()`, `Cross*()`,
  `Angle()`, and an indexer that throws `ArgumentOutOfRangeException` on
  out-of-range component access.
- `Comparison` - epsilon-tolerant `IsEqual` / `IsZero` / `IsLess` etc.
  with absolute (`Comparison.Epsilon = 1e-13`) and relative
  (`IsEqualRelative`, `IsZeroRelative`) variants.
- `Function` (split into `Function.Numeric.cs`, `Function.Angles.cs`,
  `Function.Cycling.cs`, `Function.Energetics.cs`) - factorials, Fibonacci,
  GCD, primality, `NormalizeAngle*`, fast trig, the cycling power model,
  and Minetti walking-energy cost.
- `Solver` - linear / quadratic / cubic / quartic / general polynomial
  root finding, plus secant and bisection root-finding for arbitrary
  delegates.
- `Geometry` - line / segment / triangle / polygon utilities, smallest
  enclosing circle on a plane (`MinCircle`) and on a sphere
  (`MinCircleOnSphere`).
- `Gps.Geodesy` / `Gps.GpsTrack` / `Gps.FlatTrack` - haversine distances,
  spherical-Earth helpers, lazy-evaluated track centre / minimum-circle.
- `KDTree` - generic K-D tree over any `IBoundingFacade<T>` for fast
  range / nearest queries (specialised builders for `Vector2D`,
  `Vector3D`, `Segment2D`, `Segment3D`).
- `Clustering` - DBSCAN over arbitrary `IGeometryObject<T>`, plus a
  TraClus implementation for trajectory clustering.
- `Gfx` - `Bitmap`, `Color`, line-drawing (Bresenham, Xiaolin Wu),
  heat-map rendering, and a strategy-based file writer that can emit
  PGM / PPM / PNG (`IBitmapFormatWriter`).

## Examples

### Vectors

```csharp
using Math;

var a = new Vector2D(1.0, 2.0);
var b = new Vector2D(3.0, 4.0);

double dot   = a * b;            // == a.Dot(b) == 11
double norm  = a.EuclideanNorm(b); // sqrt(8)
var unit     = (b - a).Normalized();
var rotated  = a.Rotate(System.Math.PI / 2);
```

`Vector2D.Zero` / `Vector2D.One` etc. return fresh instances on every
access, so it is safe to mutate them without corrupting global state.

### Polynomial roots

```csharp
using Math;

// Solve x^3 - 6x^2 + 11x - 6 = 0  -> roots 1, 2, 3
var roots = Solver.PolynomialEq(new System.Collections.Generic.List<double>
{
    -6.0,  // x^0
    11.0,  // x^1
    -6.0,  // x^2
    1.0    // x^3
});
// roots: [1.0, 2.0, 3.0] (sorted, deduplicated)
```

`Solver.LinearEq` / `QuadraticEq` / `CubicEq` / `QuarticEq` are also
exposed directly for the closed-form cases. Methods that may have no
real solution return `double.NaN` or an empty list rather than throwing
- "no real root" is a value-domain answer, not an error.

### GPS distances and centroid circle

```csharp
using Math.Gps;

var track = new System.Collections.Generic.List<GpsPoint>
{
    new GpsPoint { Latitude = 60.39, Longitude = 5.32, Elevation = 0 }, // Bergen
    new GpsPoint { Latitude = 59.91, Longitude = 10.75, Elevation = 0 } // Oslo
};

double metres = Geodesy.Distance.HaversineTotal(track);

var gpsTrack = new GpsTrack(track);
var centre   = gpsTrack.MinCircleCenter; // unit-vector on the sphere
var radius   = gpsTrack.MinCircleAngle * Geodesy.EarthRadius;
```

`GpsTrack` defensively copies the input list and exposes `Track` as
`IReadOnlyList<GpsPoint>`, so its lazy `Center` / `MinCircle*`
properties can never be invalidated by external mutation.

### Spatial lookup with K-D tree

```csharp
using Math;
using Math.KDTree;
using System.Collections.Generic;

var points = new List<Vector2D> { /* ... */ };
var tree   = TreeBuilder.Build(points);
var hits   = tree.Search(new BoundingRect(new Vector2D(0, 0), new Vector2D(1, 1)));
```

Build is generic over any `IBoundingFacade<T>`; specialised overloads
exist for `Vector2D`, `Vector3D`, `Segment2D`, `Segment3D`.

### DBSCAN clustering

```csharp
using Math;
using Math.Clustering;
using System.Collections.Generic;

var samples = new List<Vector2D> { /* ... */ };
var dbscan  = new DBScan<Vector2D, Vector2D>(samples);
var clusters = dbscan.Cluster(epsilon: 1.0, minPts: 5);
```

The constructor takes a defensive copy of the input list so that
subsequent mutation of the caller's list does not invalidate the
internal K-D tree cache.

### Rendering a heat map

```csharp
using Math.Gfx;
using Math.Gps;

var heatMap = new HeatMap();
heatMap.Add(track); // track is IEnumerable<GpsPoint>

double[,] pixels = heatMap.Normalized(pixelSize: 5.0, maxLength: 1024);
BitmapFileWriter.PGM("heatmap.pgm", pixels, GreyMapping.Default);
```

`BitmapFileWriter` is a thin facade over the `IBitmapFormatWriter`
strategy interface (`Pgm`, `Ppm`, `Png` are exposed as singletons).
PGM and PPM are pure netstandard; PNG is delegated to small
`System.Drawing`-backed strategies (`PngBitmapFormatWriter`,
`PngTripleChannelBitmapWriter`) that live in their own files so the
rest of the library stays free of the GDI+ dependency.

## Coding conventions

- Floating-point comparisons use `Comparison.IsEqual` /
  `Comparison.IsZero`. The default tolerance is absolute
  (`1e-13`); use `IsEqualRelative` / `IsZeroRelative` when the operand
  scale is large (e.g. metres at Earth-radius scale).
- Out-of-range indexer access throws `ArgumentOutOfRangeException`.
- Public `static readonly` numerics are promoted to `const` whenever the
  value is a literal so the JIT can fold them at the call site.
  Physical constants live in `PhysicalConstants` and `CyclistDefaults`.
- Math facade types (`Function`, `Comparison`, `Geometry`) are split
  across `partial` files by responsibility - `Function.Numeric.cs`,
  `Function.Angles.cs`, etc. - rather than collected in one giant file.

## License

MIT - see the per-file headers.
