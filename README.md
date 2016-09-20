# Math #

A collection of mathematical function and data structures written in C#. 

(c) 2016 Thierry Matthey (matthey@gmail.com) -  MIT license

### Base ###
* Floating point comparison and epsilon
* Conversion radian-degree, date time to seconds

### Solver ###
* Analytical solution of linear, quadratic, cubic and quartic polynomial equations
* Numerical general polynomial equation root solver
* General root solver with either bisection or secant method
* First and second order regression with optional weight

### Functions ###
* Cubic root
* Quintic root
* Fast sin evaluation [-PI/2, -PI/2]
* Normalize angle
* Nomerical stable sin-cos evaluation
* Factorial; ulong (n <= 20) and double
* Fibonacci; ulong (n <= 93), double and Binet
* Greatest common divisor (GDC)
* IsPrime; no cache; ~3s for max long / 10
* Averageing of angles

### Geometry ###
* Convex hull 2D (Jarvis march & Andrew's monotone chain; point reduction algorithm)
* Minimal bounding circle 2D
* Minimal bounding circle on sphere 3D
* Perpendicular (segment / line ) distance (2D & 3D)
* Trajectory Hausdorff Distance (2D & 3D)
* Filter of significant points based on Minimum Description Length Principle (2D & 3D)
* k-d tree search for vector & segment (2D & 3D)

### Data structures ###
* BoundingBox
* BoundingRect
* Circle2D
* Circle3D
* Color
* Polar3D
* Polynomial
* * Division by real roots (linear root), and imaginary and conjugated (quadratic root) 
* * Evaluation of polynomial and its derivative and integral
* Segment2D
* Segment3D
* Sparse array
* Vector2D
* Vector3D

### GPS ###
* GpsPoint
* GpsTrack
* Flatten to local 2D; single object and collection
* Geodesy
* * Haversine distance
* GridLookup / NeighbourDistanceCalculator : Finding neighbors of two GPS tracks in O(N)
* Intersection / overlapping (overestimating) of two GPS tracks
* * grid / lookup table based - fastest and pretty precise depending on given resolution
* * minimal rectangular bounding box 
* * minimal circle on sphere - slow on 1st call to calculate min circle 

### Clustering ###
* DBScan vector / segment for 2D / 3D
* TraClus (Trajectory Clustering: A Partition-and-Group Framework) for 2D and 3D
* Finding trajectory neighborhoods for 2D and 3D
* GPS segment clustering; finding common segments for locally collocated GPS tracks or globally

### Gfx ###
* PNG, PPM and PGM bitmap writer
* Simple bitmap
* Line draw - Bresenham and anti-aliasing (Xiaolin Wu's line algorithm)
* Point plot (anti-aliasing)
* Heatmap for arbitrary set of GPS tracks with color schemes

## Applications / examples ##
* GPS cluster
* GPS heatmap
* GPS activity
* HR index


![heatmap.png](https://bitbucket.org/repo/LEp4rd/images/2412494808-heatmap.png)