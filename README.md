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

### Data structures
* Polar3D
* Vector2D
* Vecotr3D
* Polynomial
* * Division by real roots (linear root), and imaginary and conjugated (quadratic root) 
* * Evaluation of polynomial and its derivative and integral

### GPS 
* GpsPoint
* GpsTrack
* Geodesy
* * Haversine distance
* Finding neighbors of two GPS tracks in O(N)