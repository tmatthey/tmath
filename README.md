# Math #

A collection of mathematical function and data structures written in C#. The library based on Test Driven Development.

(c) 2016 Thierry Matthey (matthey@gmail.com) -  MIT license

### Solver ###
* Analytical solution of linear, quadratic, cubic and quartic polynomial equations
* Numerical general polynomial equation root solver
* General root solver with either bisection or secant method
* First order regression with optional weigth

### Functions ###
* Cubic root
* Quintic root
* Fast sin evaluation [-PI/2, -PI/2]
* Normalize angle
* Nomerical stable sin-cos evaluation
* Factorial; ulong (n <= 20) and double
* Fibonacci; ulong (n <= 93) and double
* Greatest common divisor (GDC)
* IsPrime; no cache; ~3s for max long / 10

### Geometry
* Convex hull 2D (Jarvis march & Graham scan; point reduction algorithm)
* Minimal bounding circle 2D
* Minimal bounding circle on Sphere 3D

### Data structures
* BoundingBox
* BoundingRect
* Circle2D
* Circle3D
* Polar3D
* Polynomial
* * Division by real roots (linear root), and imaginary and conjugated (quadratic root) 
* * Evaluation of polynomial and its derivative and integral
* Vector2D
* Vector3D

### GPS 
* GpsPoint
* GpsTrack
* Transformer to local 2D
* Geodesy
* * Haversine distance
* GridLookup / Analyzer : Finding neighbors of two GPS tracks in O(N)
* Intersection / overlapping (overestimating) of two GPS tracks
* * grid / lookup table based - fastest and pretty precise depending on given resolution
* * minimal rectangular bounding box 
* * minimal circle on sphere - slow on 1st call to calculate min cricle 

### Gfx
* PNG and PGM bitmap writer
* Simple bitmap
* Line draw - antialiasing (Xiaolin Wu's line algorithm)
* Heatmap for arbitrary set of GPS tracks

![heatMapCenter.png](https://bitbucket.org/repo/LEp4rd/images/4106320861-heatMapCenter.png)