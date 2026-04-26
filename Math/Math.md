<a name='assembly'></a>
# Math

## Contents

- [ANeighbourDistanceCalculator](#T-Math-Gps-ANeighbourDistanceCalculator 'Math.Gps.ANeighbourDistanceCalculator')
- [BitmapFileWriter](#T-Math-Gfx-BitmapFileWriter 'Math.Gfx.BitmapFileWriter')
- [BoundingBox](#T-Math-BoundingBox 'Math.BoundingBox')
  - [#ctor()](#M-Math-BoundingBox-#ctor 'Math.BoundingBox.#ctor')
  - [#ctor(v)](#M-Math-BoundingBox-#ctor-Math-Vector3D- 'Math.BoundingBox.#ctor(Math.Vector3D)')
  - [#ctor(b)](#M-Math-BoundingBox-#ctor-Math-BoundingBox- 'Math.BoundingBox.#ctor(Math.BoundingBox)')
  - [Max](#P-Math-BoundingBox-Max 'Math.BoundingBox.Max')
  - [Min](#P-Math-BoundingBox-Min 'Math.BoundingBox.Min')
  - [Clone()](#M-Math-BoundingBox-Clone 'Math.BoundingBox.Clone')
  - [Equals()](#M-Math-BoundingBox-Equals-System-Object- 'Math.BoundingBox.Equals(System.Object)')
  - [Expand()](#M-Math-BoundingBox-Expand-Math-Vector3D- 'Math.BoundingBox.Expand(Math.Vector3D)')
  - [Expand()](#M-Math-BoundingBox-Expand-Math-Interfaces-IBounding{Math-Vector3D}- 'Math.BoundingBox.Expand(Math.Interfaces.IBounding{Math.Vector3D})')
  - [ExpandLayer()](#M-Math-BoundingBox-ExpandLayer-System-Double- 'Math.BoundingBox.ExpandLayer(System.Double)')
  - [ExpandX(x)](#M-Math-BoundingBox-ExpandX-System-Double- 'Math.BoundingBox.ExpandX(System.Double)')
  - [ExpandY(y)](#M-Math-BoundingBox-ExpandY-System-Double- 'Math.BoundingBox.ExpandY(System.Double)')
  - [ExpandZ(z)](#M-Math-BoundingBox-ExpandZ-System-Double- 'Math.BoundingBox.ExpandZ(System.Double)')
  - [GetHashCode()](#M-Math-BoundingBox-GetHashCode 'Math.BoundingBox.GetHashCode')
  - [IsEmpty()](#M-Math-BoundingBox-IsEmpty 'Math.BoundingBox.IsEmpty')
  - [IsEqual()](#M-Math-BoundingBox-IsEqual-Math-BoundingBox- 'Math.BoundingBox.IsEqual(Math.BoundingBox)')
  - [IsEqual()](#M-Math-BoundingBox-IsEqual-Math-BoundingBox,System-Double- 'Math.BoundingBox.IsEqual(Math.BoundingBox,System.Double)')
  - [IsInside()](#M-Math-BoundingBox-IsInside-Math-Vector3D- 'Math.BoundingBox.IsInside(Math.Vector3D)')
  - [IsInside()](#M-Math-BoundingBox-IsInside-Math-Vector3D,System-Double- 'Math.BoundingBox.IsInside(Math.Vector3D,System.Double)')
  - [Reset()](#M-Math-BoundingBox-Reset 'Math.BoundingBox.Reset')
- [BoundingRect](#T-Math-BoundingRect 'Math.BoundingRect')
  - [#ctor()](#M-Math-BoundingRect-#ctor 'Math.BoundingRect.#ctor')
  - [#ctor(v)](#M-Math-BoundingRect-#ctor-Math-Vector2D- 'Math.BoundingRect.#ctor(Math.Vector2D)')
  - [#ctor(b)](#M-Math-BoundingRect-#ctor-Math-BoundingRect- 'Math.BoundingRect.#ctor(Math.BoundingRect)')
  - [Max](#P-Math-BoundingRect-Max 'Math.BoundingRect.Max')
  - [Min](#P-Math-BoundingRect-Min 'Math.BoundingRect.Min')
  - [Clone()](#M-Math-BoundingRect-Clone 'Math.BoundingRect.Clone')
  - [Equals()](#M-Math-BoundingRect-Equals-System-Object- 'Math.BoundingRect.Equals(System.Object)')
  - [Expand()](#M-Math-BoundingRect-Expand-Math-Vector2D- 'Math.BoundingRect.Expand(Math.Vector2D)')
  - [Expand()](#M-Math-BoundingRect-Expand-Math-Interfaces-IBounding{Math-Vector2D}- 'Math.BoundingRect.Expand(Math.Interfaces.IBounding{Math.Vector2D})')
  - [ExpandLayer()](#M-Math-BoundingRect-ExpandLayer-System-Double- 'Math.BoundingRect.ExpandLayer(System.Double)')
  - [ExpandX(x)](#M-Math-BoundingRect-ExpandX-System-Double- 'Math.BoundingRect.ExpandX(System.Double)')
  - [ExpandY(y)](#M-Math-BoundingRect-ExpandY-System-Double- 'Math.BoundingRect.ExpandY(System.Double)')
  - [GetHashCode()](#M-Math-BoundingRect-GetHashCode 'Math.BoundingRect.GetHashCode')
  - [IsEmpty()](#M-Math-BoundingRect-IsEmpty 'Math.BoundingRect.IsEmpty')
  - [IsEqual()](#M-Math-BoundingRect-IsEqual-Math-BoundingRect- 'Math.BoundingRect.IsEqual(Math.BoundingRect)')
  - [IsEqual()](#M-Math-BoundingRect-IsEqual-Math-BoundingRect,System-Double- 'Math.BoundingRect.IsEqual(Math.BoundingRect,System.Double)')
  - [IsInside()](#M-Math-BoundingRect-IsInside-Math-Vector2D- 'Math.BoundingRect.IsInside(Math.Vector2D)')
  - [IsInside()](#M-Math-BoundingRect-IsInside-Math-Vector2D,System-Double- 'Math.BoundingRect.IsInside(Math.Vector2D,System.Double)')
  - [Reset()](#M-Math-BoundingRect-Reset 'Math.BoundingRect.Reset')
- [Comparison](#T-Math-Comparison 'Math.Comparison')
  - [RelativeEpsilon](#F-Math-Comparison-RelativeEpsilon 'Math.Comparison.RelativeEpsilon')
  - [HashCode()](#M-Math-Comparison-HashCode-System-Double- 'Math.Comparison.HashCode(System.Double)')
  - [IsEqualRelative()](#M-Math-Comparison-IsEqualRelative-System-Double,System-Double,System-Double- 'Math.Comparison.IsEqualRelative(System.Double,System.Double,System.Double)')
  - [IsNegative()](#M-Math-Comparison-IsNegative-System-Double,System-Double- 'Math.Comparison.IsNegative(System.Double,System.Double)')
  - [IsPositive()](#M-Math-Comparison-IsPositive-System-Double,System-Double- 'Math.Comparison.IsPositive(System.Double,System.Double)')
  - [IsZeroRelative()](#M-Math-Comparison-IsZeroRelative-System-Double,System-Double,System-Double- 'Math.Comparison.IsZeroRelative(System.Double,System.Double,System.Double)')
- [CyclistDefaults](#T-Math-CyclistDefaults 'Math.CyclistDefaults')
  - [AirDensity](#F-Math-CyclistDefaults-AirDensity 'Math.CyclistDefaults.AirDensity')
  - [DragCoefficient](#F-Math-CyclistDefaults-DragCoefficient 'Math.CyclistDefaults.DragCoefficient')
  - [DriveTrainLoss](#F-Math-CyclistDefaults-DriveTrainLoss 'Math.CyclistDefaults.DriveTrainLoss')
  - [FrontalArea](#F-Math-CyclistDefaults-FrontalArea 'Math.CyclistDefaults.FrontalArea')
  - [RollingResistance](#F-Math-CyclistDefaults-RollingResistance 'Math.CyclistDefaults.RollingResistance')
- [DBScan\`2](#T-Math-Clustering-DBScan`2 'Math.Clustering.DBScan`2')
  - [#ctor(list)](#M-Math-Clustering-DBScan`2-#ctor-System-Collections-Generic-IList{`1}- 'Math.Clustering.DBScan`2.#ctor(System.Collections.Generic.IList{`1})')
  - [Cluster(eps,n,direction)](#M-Math-Clustering-DBScan`2-Cluster-System-Double,System-Int32,System-Boolean- 'Math.Clustering.DBScan`2.Cluster(System.Double,System.Int32,System.Boolean)')
- [DisconnectedPointPruner](#T-Math-Gps-DisconnectedPointPruner 'Math.Gps.DisconnectedPointPruner')
- [Function](#T-Math-Function 'Math.Function')
  - [FastSin()](#M-Math-Function-FastSin-System-Double- 'Math.Function.FastSin(System.Double)')
- [IArray](#T-Math-Interfaces-IArray 'Math.Interfaces.IArray')
  - [Item](#P-Math-Interfaces-IArray-Item-System-Int32- 'Math.Interfaces.IArray.Item(System.Int32)')
  - [ToArray()](#M-Math-Interfaces-IArray-ToArray 'Math.Interfaces.IArray.ToArray')
- [IBitmapFormatWriter](#T-Math-Gfx-IBitmapFormatWriter 'Math.Gfx.IBitmapFormatWriter')
  - [Write()](#M-Math-Gfx-IBitmapFormatWriter-Write-System-IO-Stream,System-Double[0-,0-],Math-Gfx-IColorMapping- 'Math.Gfx.IBitmapFormatWriter.Write(System.IO.Stream,System.Double[0:,0:],Math.Gfx.IColorMapping)')
- [IBoundingFacade\`1](#T-Math-Interfaces-IBoundingFacade`1 'Math.Interfaces.IBoundingFacade`1')
  - [Bounding()](#M-Math-Interfaces-IBoundingFacade`1-Bounding 'Math.Interfaces.IBoundingFacade`1.Bounding')
- [IBounding\`1](#T-Math-Interfaces-IBounding`1 'Math.Interfaces.IBounding`1')
  - [Expand()](#M-Math-Interfaces-IBounding`1-Expand-`0- 'Math.Interfaces.IBounding`1.Expand(`0)')
  - [Expand()](#M-Math-Interfaces-IBounding`1-Expand-Math-Interfaces-IBounding{`0}- 'Math.Interfaces.IBounding`1.Expand(Math.Interfaces.IBounding{`0})')
  - [ExpandLayer()](#M-Math-Interfaces-IBounding`1-ExpandLayer-System-Double- 'Math.Interfaces.IBounding`1.ExpandLayer(System.Double)')
  - [Reset()](#M-Math-Interfaces-IBounding`1-Reset 'Math.Interfaces.IBounding`1.Reset')
- [ICloneable\`1](#T-Math-Interfaces-ICloneable`1 'Math.Interfaces.ICloneable`1')
  - [Clone()](#M-Math-Interfaces-ICloneable`1-Clone 'Math.Interfaces.ICloneable`1.Clone')
- [ICubicBezier\`2](#T-Math-Interfaces-ICubicBezier`2 'Math.Interfaces.ICubicBezier`2')
- [ICurve\`2](#T-Math-Interfaces-ICurve`2 'Math.Interfaces.ICurve`2')
  - [Evaluate(t)](#M-Math-Interfaces-ICurve`2-Evaluate-System-Double- 'Math.Interfaces.ICurve`2.Evaluate(System.Double)')
  - [Kappa(t)](#M-Math-Interfaces-ICurve`2-Kappa-System-Double- 'Math.Interfaces.ICurve`2.Kappa(System.Double)')
  - [Length(accuracy)](#M-Math-Interfaces-ICurve`2-Length-System-Double- 'Math.Interfaces.ICurve`2.Length(System.Double)')
  - [Split(t)](#M-Math-Interfaces-ICurve`2-Split-System-Double- 'Math.Interfaces.ICurve`2.Split(System.Double)')
  - [Tangent(t)](#M-Math-Interfaces-ICurve`2-Tangent-System-Double- 'Math.Interfaces.ICurve`2.Tangent(System.Double)')
  - [d2Evaluate(t)](#M-Math-Interfaces-ICurve`2-d2Evaluate-System-Double- 'Math.Interfaces.ICurve`2.d2Evaluate(System.Double)')
  - [dEvaluate(t)](#M-Math-Interfaces-ICurve`2-dEvaluate-System-Double- 'Math.Interfaces.ICurve`2.dEvaluate(System.Double)')
- [IDimension](#T-Math-Interfaces-IDimension 'Math.Interfaces.IDimension')
  - [Dimensions](#P-Math-Interfaces-IDimension-Dimensions 'Math.Interfaces.IDimension.Dimensions')
- [IGeometryObject\`1](#T-Math-Interfaces-IGeometryObject`1 'Math.Interfaces.IGeometryObject`1')
- [IInnerProduct\`1](#T-Math-Interfaces-IInnerProduct`1 'Math.Interfaces.IInnerProduct`1')
  - [Angle()](#M-Math-Interfaces-IInnerProduct`1-Angle-`0- 'Math.Interfaces.IInnerProduct`1.Angle(`0)')
  - [AngleAbs()](#M-Math-Interfaces-IInnerProduct`1-AngleAbs-`0- 'Math.Interfaces.IInnerProduct`1.AngleAbs(`0)')
  - [CrossNorm()](#M-Math-Interfaces-IInnerProduct`1-CrossNorm-`0- 'Math.Interfaces.IInnerProduct`1.CrossNorm(`0)')
  - [Dot()](#M-Math-Interfaces-IInnerProduct`1-Dot-`0- 'Math.Interfaces.IInnerProduct`1.Dot(`0)')
- [IInterpolate\`1](#T-Math-Interfaces-IInterpolate`1 'Math.Interfaces.IInterpolate`1')
  - [Interpolate(t,x)](#M-Math-Interfaces-IInterpolate`1-Interpolate-`0,System-Double- 'Math.Interfaces.IInterpolate`1.Interpolate(`0,System.Double)')
- [IIsEqual\`1](#T-Math-Interfaces-IIsEqual`1 'Math.Interfaces.IIsEqual`1')
  - [IsEqual(a)](#M-Math-Interfaces-IIsEqual`1-IsEqual-`0- 'Math.Interfaces.IIsEqual`1.IsEqual(`0)')
  - [IsEqual(a,epsilon)](#M-Math-Interfaces-IIsEqual`1-IsEqual-`0,System-Double- 'Math.Interfaces.IIsEqual`1.IsEqual(`0,System.Double)')
- [INorm\`1](#T-Math-Interfaces-INorm`1 'Math.Interfaces.INorm`1')
  - [EuclideanNorm(d)](#M-Math-Interfaces-INorm`1-EuclideanNorm-`0- 'Math.Interfaces.INorm`1.EuclideanNorm(`0)')
  - [ModifiedNorm(d,direction)](#M-Math-Interfaces-INorm`1-ModifiedNorm-`0,System-Boolean- 'Math.Interfaces.INorm`1.ModifiedNorm(`0,System.Boolean)')
- [INormalizable\`1](#T-Math-Interfaces-INormalizable`1 'Math.Interfaces.INormalizable`1')
  - [Norm()](#M-Math-Interfaces-INormalizable`1-Norm 'Math.Interfaces.INormalizable`1.Norm')
  - [Norm2()](#M-Math-Interfaces-INormalizable`1-Norm2 'Math.Interfaces.INormalizable`1.Norm2')
  - [Normalize()](#M-Math-Interfaces-INormalizable`1-Normalize 'Math.Interfaces.INormalizable`1.Normalize')
  - [Normalize()](#M-Math-Interfaces-INormalizable`1-Normalize-System-Double- 'Math.Interfaces.INormalizable`1.Normalize(System.Double)')
  - [Normalized()](#M-Math-Interfaces-INormalizable`1-Normalized 'Math.Interfaces.INormalizable`1.Normalized')
  - [Normalized()](#M-Math-Interfaces-INormalizable`1-Normalized-System-Double- 'Math.Interfaces.INormalizable`1.Normalized(System.Double)')
- [IReadOnlyBounding\`1](#T-Math-Interfaces-IReadOnlyBounding`1 'Math.Interfaces.IReadOnlyBounding`1')
  - [Max](#P-Math-Interfaces-IReadOnlyBounding`1-Max 'Math.Interfaces.IReadOnlyBounding`1.Max')
  - [Min](#P-Math-Interfaces-IReadOnlyBounding`1-Min 'Math.Interfaces.IReadOnlyBounding`1.Min')
  - [IsEmpty()](#M-Math-Interfaces-IReadOnlyBounding`1-IsEmpty 'Math.Interfaces.IReadOnlyBounding`1.IsEmpty')
  - [IsInside()](#M-Math-Interfaces-IReadOnlyBounding`1-IsInside-`0- 'Math.Interfaces.IReadOnlyBounding`1.IsInside(`0)')
  - [IsInside()](#M-Math-Interfaces-IReadOnlyBounding`1-IsInside-`0,System-Double- 'Math.Interfaces.IReadOnlyBounding`1.IsInside(`0,System.Double)')
- [ISegment\`2](#T-Math-Interfaces-ISegment`2 'Math.Interfaces.ISegment`2')
  - [A](#P-Math-Interfaces-ISegment`2-A 'Math.Interfaces.ISegment`2.A')
  - [B](#P-Math-Interfaces-ISegment`2-B 'Math.Interfaces.ISegment`2.B')
  - [IsIntersecting(s,eps)](#M-Math-Interfaces-ISegment`2-IsIntersecting-`1,System-Double- 'Math.Interfaces.ISegment`2.IsIntersecting(`1,System.Double)')
  - [Vector()](#M-Math-Interfaces-ISegment`2-Vector 'Math.Interfaces.ISegment`2.Vector')
- [IVectorArith\`1](#T-Math-Interfaces-IVectorArith`1 'Math.Interfaces.IVectorArith`1')
  - [Add()](#M-Math-Interfaces-IVectorArith`1-Add-`0- 'Math.Interfaces.IVectorArith`1.Add(`0)')
  - [Div()](#M-Math-Interfaces-IVectorArith`1-Div-System-Double- 'Math.Interfaces.IVectorArith`1.Div(System.Double)')
  - [Mul()](#M-Math-Interfaces-IVectorArith`1-Mul-System-Double- 'Math.Interfaces.IVectorArith`1.Mul(System.Double)')
  - [Sub()](#M-Math-Interfaces-IVectorArith`1-Sub-`0- 'Math.Interfaces.IVectorArith`1.Sub(`0)')
- [IVector\`1](#T-Math-Interfaces-IVector`1 'Math.Interfaces.IVector`1')
  - [X](#P-Math-Interfaces-IVector`1-X 'Math.Interfaces.IVector`1.X')
- [PerpendicularDistanceProjector](#T-Math-Gps-PerpendicularDistanceProjector 'Math.Gps.PerpendicularDistanceProjector')
- [PgmBitmapFormatWriter](#T-Math-Gfx-PgmBitmapFormatWriter 'Math.Gfx.PgmBitmapFormatWriter')
- [PhysicalConstants](#T-Math-PhysicalConstants 'Math.PhysicalConstants')
  - [AirDensitySeaLevel](#F-Math-PhysicalConstants-AirDensitySeaLevel 'Math.PhysicalConstants.AirDensitySeaLevel')
  - [GravitationalAcceleration](#F-Math-PhysicalConstants-GravitationalAcceleration 'Math.PhysicalConstants.GravitationalAcceleration')
- [PngBitmapFormatWriter](#T-Math-Gfx-PngBitmapFormatWriter 'Math.Gfx.PngBitmapFormatWriter')
- [PngTripleChannelBitmapWriter](#T-Math-Gfx-PngTripleChannelBitmapWriter 'Math.Gfx.PngTripleChannelBitmapWriter')
- [Polar3D](#T-Math-Polar3D 'Math.Polar3D')
  - [ModifiedNorm()](#M-Math-Polar3D-ModifiedNorm-Math-Polar3D,System-Boolean- 'Math.Polar3D.ModifiedNorm(Math.Polar3D,System.Boolean)')
- [PolylineNeighbours](#T-Math-Clustering-PolylineNeighbours 'Math.Clustering.PolylineNeighbours')
- [Polynomial](#T-Math-Polynomial 'Math.Polynomial')
  - [#ctor(coefficients)](#M-Math-Polynomial-#ctor-System-Collections-Generic-IEnumerable{System-Double}- 'Math.Polynomial.#ctor(System.Collections.Generic.IEnumerable{System.Double})')
  - [DivideByRoot()](#M-Math-Polynomial-DivideByRoot-System-Double- 'Math.Polynomial.DivideByRoot(System.Double)')
  - [DivideByRootAndConjugate()](#M-Math-Polynomial-DivideByRootAndConjugate-System-Numerics-Complex- 'Math.Polynomial.DivideByRootAndConjugate(System.Numerics.Complex)')
  - [FindRoot(x)](#M-Math-Polynomial-FindRoot-System-Numerics-Complex- 'Math.Polynomial.FindRoot(System.Numerics.Complex)')
  - [P()](#M-Math-Polynomial-P 'Math.Polynomial.P')
  - [P()](#M-Math-Polynomial-P-System-Double- 'Math.Polynomial.P(System.Double)')
  - [P()](#M-Math-Polynomial-P-System-Numerics-Complex- 'Math.Polynomial.P(System.Numerics.Complex)')
  - [dp()](#M-Math-Polynomial-dp 'Math.Polynomial.dp')
  - [dp()](#M-Math-Polynomial-dp-System-Double- 'Math.Polynomial.dp(System.Double)')
  - [dp()](#M-Math-Polynomial-dp-System-Numerics-Complex- 'Math.Polynomial.dp(System.Numerics.Complex)')
  - [p()](#M-Math-Polynomial-p 'Math.Polynomial.p')
  - [p()](#M-Math-Polynomial-p-System-Double- 'Math.Polynomial.p(System.Double)')
  - [p()](#M-Math-Polynomial-p-System-Numerics-Complex- 'Math.Polynomial.p(System.Numerics.Complex)')
- [PpmBitmapFormatWriter](#T-Math-Gfx-PpmBitmapFormatWriter 'Math.Gfx.PpmBitmapFormatWriter')
- [RadiusCutOff](#T-Math-Gps-RadiusCutOff 'Math.Gps.RadiusCutOff')
- [Result\`1](#T-Math-Clustering-TraClus-Result`1 'Math.Clustering.TraClus.Result`1')
  - [#ctor()](#M-Math-Clustering-TraClus-Result`1-#ctor 'Math.Clustering.TraClus.Result`1.#ctor')
  - [PointIndices](#P-Math-Clustering-TraClus-Result`1-PointIndices 'Math.Clustering.TraClus.Result`1.PointIndices')
  - [Segment](#P-Math-Clustering-TraClus-Result`1-Segment 'Math.Clustering.TraClus.Result`1.Segment')
  - [SegmentIndices](#P-Math-Clustering-TraClus-Result`1-SegmentIndices 'Math.Clustering.TraClus.Result`1.SegmentIndices')
- [Solver](#T-Math-Solver 'Math.Solver')
  - [PolynomialEq()](#M-Math-Solver-PolynomialEq-System-Collections-Generic-List{System-Double}- 'Math.Solver.PolynomialEq(System.Collections.Generic.List{System.Double})')
- [SparseArray\`1](#T-Math-SparseArray`1 'Math.SparseArray`1')
- [TraClus](#T-Math-Clustering-TraClus 'Math.Clustering.TraClus')
  - [Cluster(tracks,n,eps,direction,minL,mdlCostAdvantage)](#M-Math-Clustering-TraClus-Cluster-System-Collections-Generic-IList{System-Collections-Generic-List{Math-Vector2D}},System-Int32,System-Double,System-Boolean,System-Double,System-Int32- 'Math.Clustering.TraClus.Cluster(System.Collections.Generic.IList{System.Collections.Generic.List{Math.Vector2D}},System.Int32,System.Double,System.Boolean,System.Double,System.Int32)')
  - [Cluster(tracks,n,eps,direction,minL,mdlCostAdvantage)](#M-Math-Clustering-TraClus-Cluster-System-Collections-Generic-IList{System-Collections-Generic-List{Math-Vector3D}},System-Int32,System-Double,System-Boolean,System-Double,System-Int32- 'Math.Clustering.TraClus.Cluster(System.Collections.Generic.IList{System.Collections.Generic.List{Math.Vector3D}},System.Int32,System.Double,System.Boolean,System.Double,System.Int32)')
- [TreeBuilder](#T-Math-KDTree-TreeBuilder 'Math.KDTree.TreeBuilder')
  - [Build\`\`2()](#M-Math-KDTree-TreeBuilder-Build``2-System-Collections-Generic-IEnumerable{``1},System-Int32- 'Math.KDTree.TreeBuilder.Build``2(System.Collections.Generic.IEnumerable{``1},System.Int32)')
- [Vector2D](#T-Math-Vector2D 'Math.Vector2D')
  - [ModifiedNorm()](#M-Math-Vector2D-ModifiedNorm-Math-Vector2D,System-Boolean- 'Math.Vector2D.ModifiedNorm(Math.Vector2D,System.Boolean)')
- [Vector3D](#T-Math-Vector3D 'Math.Vector3D')
  - [ModifiedNorm()](#M-Math-Vector3D-ModifiedNorm-Math-Vector3D,System-Boolean- 'Math.Vector3D.ModifiedNorm(Math.Vector3D,System.Boolean)')

<a name='T-Math-Gps-ANeighbourDistanceCalculator'></a>
## ANeighbourDistanceCalculator `type`

##### Namespace

Math.Gps

##### Summary

Aggregates all points which are at least as close (perpendicular distance) as a given
radius to the reference track. The four pipeline stages live in dedicated helpers:

<a name='T-Math-Gfx-BitmapFileWriter'></a>
## BitmapFileWriter `type`

##### Namespace

Math.Gfx

##### Summary

Path-based facade over [IBitmapFormatWriter](#T-Math-Gfx-IBitmapFormatWriter 'Math.Gfx.IBitmapFormatWriter') strategies. The four overloads
are kept for source compatibility; new format implementations can plug in by adding a
fresh [IBitmapFormatWriter](#T-Math-Gfx-IBitmapFormatWriter 'Math.Gfx.IBitmapFormatWriter') without modifying this type (Open/Closed).
All [](#N-System-Drawing 'System.Drawing') usage in this assembly is delegated to the
`Png*` strategies ([PngBitmapFormatWriter](#T-Math-Gfx-PngBitmapFormatWriter 'Math.Gfx.PngBitmapFormatWriter'),
[PngTripleChannelBitmapWriter](#T-Math-Gfx-PngTripleChannelBitmapWriter 'Math.Gfx.PngTripleChannelBitmapWriter')) so that a future assembly split (DIP) only
has to relocate those files.

<a name='T-Math-BoundingBox'></a>
## BoundingBox `type`

##### Namespace

Math

##### Summary

3D bounding box

<a name='M-Math-BoundingBox-#ctor'></a>
### #ctor() `constructor`

##### Summary

Empty bounding box

##### Parameters

This constructor has no parameters.

<a name='M-Math-BoundingBox-#ctor-Math-Vector3D-'></a>
### #ctor(v) `constructor`

##### Summary

Bounding box with one point

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [Math.Vector3D](#T-Math-Vector3D 'Math.Vector3D') |  |

<a name='M-Math-BoundingBox-#ctor-Math-BoundingBox-'></a>
### #ctor(b) `constructor`

##### Summary

Bounding box

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| b | [Math.BoundingBox](#T-Math-BoundingBox 'Math.BoundingBox') |  |

<a name='P-Math-BoundingBox-Max'></a>
### Max `property`

##### Summary

*Inherit from parent.*

<a name='P-Math-BoundingBox-Min'></a>
### Min `property`

##### Summary

*Inherit from parent.*

<a name='M-Math-BoundingBox-Clone'></a>
### Clone() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-Expand-Math-Vector3D-'></a>
### Expand() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-Expand-Math-Interfaces-IBounding{Math-Vector3D}-'></a>
### Expand() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-ExpandLayer-System-Double-'></a>
### ExpandLayer() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-ExpandX-System-Double-'></a>
### ExpandX(x) `method`

##### Summary

Expand by X-coordinate

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-BoundingBox-ExpandY-System-Double-'></a>
### ExpandY(y) `method`

##### Summary

Expand by Y-coordinate

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| y | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-BoundingBox-ExpandZ-System-Double-'></a>
### ExpandZ(z) `method`

##### Summary

Expand by Z-coordinate

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| z | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-BoundingBox-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-IsEmpty'></a>
### IsEmpty() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-IsEqual-Math-BoundingBox-'></a>
### IsEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-IsEqual-Math-BoundingBox,System-Double-'></a>
### IsEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-IsInside-Math-Vector3D-'></a>
### IsInside() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-IsInside-Math-Vector3D,System-Double-'></a>
### IsInside() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingBox-Reset'></a>
### Reset() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Math-BoundingRect'></a>
## BoundingRect `type`

##### Namespace

Math

##### Summary

2D bounding rectangle

<a name='M-Math-BoundingRect-#ctor'></a>
### #ctor() `constructor`

##### Summary

Empty bounding rectangle

##### Parameters

This constructor has no parameters.

<a name='M-Math-BoundingRect-#ctor-Math-Vector2D-'></a>
### #ctor(v) `constructor`

##### Summary

Bounding rectangle with one point

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [Math.Vector2D](#T-Math-Vector2D 'Math.Vector2D') |  |

<a name='M-Math-BoundingRect-#ctor-Math-BoundingRect-'></a>
### #ctor(b) `constructor`

##### Summary

Bounding rectangle

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| b | [Math.BoundingRect](#T-Math-BoundingRect 'Math.BoundingRect') |  |

<a name='P-Math-BoundingRect-Max'></a>
### Max `property`

##### Summary

*Inherit from parent.*

<a name='P-Math-BoundingRect-Min'></a>
### Min `property`

##### Summary

*Inherit from parent.*

<a name='M-Math-BoundingRect-Clone'></a>
### Clone() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-Expand-Math-Vector2D-'></a>
### Expand() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-Expand-Math-Interfaces-IBounding{Math-Vector2D}-'></a>
### Expand() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-ExpandLayer-System-Double-'></a>
### ExpandLayer() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-ExpandX-System-Double-'></a>
### ExpandX(x) `method`

##### Summary

Expand by X-coordinate

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-BoundingRect-ExpandY-System-Double-'></a>
### ExpandY(y) `method`

##### Summary

Expand by Y-coordinate

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| y | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-BoundingRect-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-IsEmpty'></a>
### IsEmpty() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-IsEqual-Math-BoundingRect-'></a>
### IsEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-IsEqual-Math-BoundingRect,System-Double-'></a>
### IsEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-IsInside-Math-Vector2D-'></a>
### IsInside() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-IsInside-Math-Vector2D,System-Double-'></a>
### IsInside() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-Math-BoundingRect-Reset'></a>
### Reset() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-Math-Comparison'></a>
## Comparison `type`

##### Namespace

Math

<a name='F-Math-Comparison-RelativeEpsilon'></a>
### RelativeEpsilon `constants`

##### Summary

Default relative tolerance for [IsEqualRelative](#M-Math-Comparison-IsEqualRelative-System-Double,System-Double,System-Double- 'Math.Comparison.IsEqualRelative(System.Double,System.Double,System.Double)') /
[IsZeroRelative](#M-Math-Comparison-IsZeroRelative-System-Double,System-Double,System-Double- 'Math.Comparison.IsZeroRelative(System.Double,System.Double,System.Double)'). Chosen ~3 orders of magnitude
looser than [Epsilon](#F-Math-Comparison-Epsilon 'Math.Comparison.Epsilon') so that the absolute tolerance at unit scale
(1.0) stays at 1e-13 while large-magnitude values automatically get a proportional
tolerance (e.g. 1e-7 m at Earth-radius scale 6.37e6 m).

<a name='M-Math-Comparison-HashCode-System-Double-'></a>
### HashCode() `method`

##### Summary

Returns a hash code for `x` that is consistent with epsilon-tolerant [IsEqual](#M-Math-Comparison-IsEqual-System-Double,System-Double,System-Double- 'Math.Comparison.IsEqual(System.Double,System.Double,System.Double)'):
values within the snap granularity hash to the same bucket. Granularity is chosen much coarser
than [Epsilon](#F-Math-Comparison-Epsilon 'Math.Comparison.Epsilon') so that any two values reported equal by [IsEqual](#M-Math-Comparison-IsEqual-System-Double,System-Double,System-Double- 'Math.Comparison.IsEqual(System.Double,System.Double,System.Double)')
in the common case produce the same hash, satisfying the GetHashCode contract.

##### Parameters

This method has no parameters.

<a name='M-Math-Comparison-IsEqualRelative-System-Double,System-Double,System-Double-'></a>
### IsEqualRelative() `method`

##### Summary

Epsilon-tolerant equality scaled by the larger operand magnitude. Equivalent to
`|x - y| < relEps * max(|x|, |y|, 1)`: callers do not have to pre-scale
their epsilon when comparing two large numbers (e.g. metres at Earth-radius
scale). The unit-scale baseline (the trailing `1`) keeps behaviour close to
the absolute [IsEqual](#M-Math-Comparison-IsEqual-System-Double,System-Double,System-Double- 'Math.Comparison.IsEqual(System.Double,System.Double,System.Double)') for small operands.
Infinities of the same sign compare equal, just like [IsEqual](#M-Math-Comparison-IsEqual-System-Double,System-Double,System-Double- 'Math.Comparison.IsEqual(System.Double,System.Double,System.Double)').

##### Parameters

This method has no parameters.

<a name='M-Math-Comparison-IsNegative-System-Double,System-Double-'></a>
### IsNegative() `method`

##### Summary

Returns true iff `x` is a finite value strictly less than -`eps`.

##### Parameters

This method has no parameters.

##### Remarks

Returns false for [NaN](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.NaN 'System.Double.NaN'), [PositiveInfinity](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.PositiveInfinity 'System.Double.PositiveInfinity'),
[NegativeInfinity](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.NegativeInfinity 'System.Double.NegativeInfinity'), zero, and any value within +/- `eps` of zero.
Infinity is intentionally excluded; see [IsPositive](#M-Math-Comparison-IsPositive-System-Double,System-Double- 'Math.Comparison.IsPositive(System.Double,System.Double)').

<a name='M-Math-Comparison-IsPositive-System-Double,System-Double-'></a>
### IsPositive() `method`

##### Summary

Returns true iff `x` is a finite value strictly greater than `eps`.

##### Parameters

This method has no parameters.

##### Remarks

Returns false for [NaN](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.NaN 'System.Double.NaN'), [PositiveInfinity](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.PositiveInfinity 'System.Double.PositiveInfinity'),
[NegativeInfinity](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.NegativeInfinity 'System.Double.NegativeInfinity'), zero, and any value within +/- `eps` of zero.
Infinity is intentionally excluded so that "positive" implies "finite numeric magnitude
the rest of the library can safely arithmetic on" - callers that genuinely want to admit
+Infinity should test it explicitly with [IsPositiveInfinity](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.IsPositiveInfinity 'System.Double.IsPositiveInfinity(System.Double)').

<a name='M-Math-Comparison-IsZeroRelative-System-Double,System-Double,System-Double-'></a>
### IsZeroRelative() `method`

##### Summary

Relative-tolerance "is zero" with an explicit reference scale. Returns true iff
`|x| < relEps * max(|scale|, 1)`, i.e. `x` is small
compared to `scale`. Useful for testing residuals of length-scaled
quantities (e.g. metres) where the absolute [Epsilon](#F-Math-Comparison-Epsilon 'Math.Comparison.Epsilon') 1e-13 would be
meaninglessly tight.

##### Parameters

This method has no parameters.

<a name='T-Math-CyclistDefaults'></a>
## CyclistDefaults `type`

##### Namespace

Math

##### Summary

Sensible defaults for the cycling power/velocity model. The values are sourced from
gribble.org's analysis (https://www.gribble.org/cycling/power_v_speed.html) and match
the historical literal arguments of [CyclingForces](#M-Math-Function-CyclingForces-System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double- 'Math.Function.CyclingForces(System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double)') /
[CyclingPowers](#M-Math-Function-CyclingPowers-System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double- 'Math.Function.CyclingPowers(System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double)') / [CyclingVelocity](#M-Math-Function-CyclingVelocity-System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double,System-Double- 'Math.Function.CyclingVelocity(System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double)'). They
are exposed here so callers can refer to them by name instead of repeating literals
at every call site.

<a name='F-Math-CyclistDefaults-AirDensity'></a>
### AirDensity `constants`

##### Summary

Air density default - alias for [AirDensitySeaLevel](#F-Math-PhysicalConstants-AirDensitySeaLevel 'Math.PhysicalConstants.AirDensitySeaLevel').

<a name='F-Math-CyclistDefaults-DragCoefficient'></a>
### DragCoefficient `constants`

##### Summary

Aerodynamic drag coefficient, dimensionless.

<a name='F-Math-CyclistDefaults-DriveTrainLoss'></a>
### DriveTrainLoss `constants`

##### Summary

Mechanical loss across the drive train (chain + bearings), dimensionless.

<a name='F-Math-CyclistDefaults-FrontalArea'></a>
### FrontalArea `constants`

##### Summary

Frontal area, m^2 (drops/hoods average).

<a name='F-Math-CyclistDefaults-RollingResistance'></a>
### RollingResistance `constants`

##### Summary

Coefficient of rolling resistance (Crr) for a road bike on tarmac.

<a name='T-Math-Clustering-DBScan`2'></a>
## DBScan\`2 `type`

##### Namespace

Math.Clustering

##### Summary

Density-based spatial clustering of applications with noise (DBSCAN) is a data clustering algorithm proposed by Martin Ester, Hans-Peter Kriegel, Jörg Sander and Xiaowei Xu in 1996. https://en.wikipedia.org/wiki/DBSCAN

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Point type of dimension n, e.g., Vector3D |
| S | A geometric object of dimension n with a norm based on point type T, e.g., Segment3D |

<a name='M-Math-Clustering-DBScan`2-#ctor-System-Collections-Generic-IList{`1}-'></a>
### #ctor(list) `constructor`

##### Summary

Defining a DBScan with a list of the geometric objects to be clustered. The constructor takes a defensive copy of the input list.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| list | [System.Collections.Generic.IList{\`1}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{`1}') | List of object of dimension n with a norm based on point type T, e.g., Segment3D. |

<a name='M-Math-Clustering-DBScan`2-Cluster-System-Double,System-Int32,System-Boolean-'></a>
### Cluster(eps,n,direction) `method`

##### Summary

Clustering the list of objects with a given epsilon and with a threshold.

##### Returns

A list of cluster as list of object indices.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| eps | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Epsilon of neighborhood between to objects using objects (modified) norm. |
| n | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Minimum number of objects required to be recognized as a cluster. |
| direction | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Boolean defining if the direction for directional objects shall be considered when evaluating the norm between two objects, e.g., Trajectory Hausdorff distance. |

<a name='T-Math-Gps-DisconnectedPointPruner'></a>
## DisconnectedPointPruner `type`

##### Namespace

Math.Gps

##### Summary

Pipeline stage 4 of [Analyze](#M-Math-Gps-ANeighbourDistanceCalculator-Analyze-Math-Gps-FlatTrack,System-Double- 'Math.Gps.ANeighbourDistanceCalculator.Analyze(Math.Gps.FlatTrack,System.Double)'):
remove faulty neighbour reference points - detours, cross-overs, opposite direction matches,
start/end mix-ups - by grouping points into arc-length-connected segments and keeping the
segment whose centroid is closest to the rolling reference index. Pure, stateless helper.

<a name='T-Math-Function'></a>
## Function `type`

##### Namespace

Math

##### Summary

Math facade. The implementation is split across files by responsibility:

The `partial` split is purely organisational; existing callers continue to use
`Math.Function.Xxx` unchanged.

<a name='M-Math-Function-FastSin-System-Double-'></a>
### FastSin() `method`

##### Summary

Cheap polynomial approximation of sin(x) on x in [-pi/2, pi/2] with absolute error < 0.0205.

##### Parameters

This method has no parameters.

##### Remarks

Outside [-pi/2, pi/2] the polynomial diverges quickly: it is the caller's responsibility
to range-reduce the argument first (e.g. via [NormalizeAnglePi](#M-Math-Function-NormalizeAnglePi-System-Double- 'Math.Function.NormalizeAnglePi(System.Double)') and a
half-period reflection). The function does not assert this in release builds to keep
"fast" honest, but a debug-only assertion guards against accidental misuse.

<a name='T-Math-Interfaces-IArray'></a>
## IArray `type`

##### Namespace

Math.Interfaces

##### Summary

Interface geometry object for coordinate(s) in linear / array representation.
Either point object or 2-point (min-max) object

<a name='P-Math-Interfaces-IArray-Item-System-Int32-'></a>
### Item `property`

##### Summary

Array access of the coordinate(s).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| i | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Component index. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') | Thrown when `i` is outside the implementation's component range.
All concrete implementations in this library throw
[ArgumentOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') rather than the older bare
[ArgumentException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentException 'System.ArgumentException'); tests should assert the more specific type. |

<a name='M-Math-Interfaces-IArray-ToArray'></a>
### ToArray() `method`

##### Summary

Array representation of the coordinate(s).
Allocates a new array on every call; prefer the indexer for single-element access.

##### Parameters

This method has no parameters.

<a name='T-Math-Gfx-IBitmapFormatWriter'></a>
## IBitmapFormatWriter `type`

##### Namespace

Math.Gfx

##### Summary

Strategy interface for serialising a `double[,]` intensity raster to a stream in a
specific bitmap container format (PGM, PPM, PNG, ...). Implementations are expected to be
stateless and thread-safe so that [BitmapFileWriter](#T-Math-Gfx-BitmapFileWriter 'Math.Gfx.BitmapFileWriter') can pick a writer based
on file extension or caller intent without coupling the call site to a specific format.

<a name='M-Math-Gfx-IBitmapFormatWriter-Write-System-IO-Stream,System-Double[0-,0-],Math-Gfx-IColorMapping-'></a>
### Write() `method`

##### Summary

Writes `bitmap` to `stream` using
`colorMap` to translate intensities into pixel values. The caller
retains ownership of the stream and is responsible for disposing it.

##### Parameters

This method has no parameters.

<a name='T-Math-Interfaces-IBoundingFacade`1'></a>
## IBoundingFacade\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Bounding box facade for geometry objects or curves

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-Math-Interfaces-IBoundingFacade`1-Bounding'></a>
### Bounding() `method`

##### Summary

Bounding box

##### Returns



##### Parameters

This method has no parameters.

<a name='T-Math-Interfaces-IBounding`1'></a>
## IBounding\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Mutable axis-aligned bounding region. Extends the read-only view in
[IReadOnlyBounding\`1](#T-Math-Interfaces-IReadOnlyBounding`1 'Math.Interfaces.IReadOnlyBounding`1') with growth operations (Reset, Expand, ExpandLayer).
Consumers that only need to read bounds should depend on [IReadOnlyBounding\`1](#T-Math-Interfaces-IReadOnlyBounding`1 'Math.Interfaces.IReadOnlyBounding`1')
instead so they cannot accidentally widen the box (Interface Segregation).

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Coordinate type (Vector2D, Vector3D, ...). |

<a name='M-Math-Interfaces-IBounding`1-Expand-`0-'></a>
### Expand() `method`

##### Summary

Expands the bounding region to cover `v`.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IBounding`1-Expand-Math-Interfaces-IBounding{`0}-'></a>
### Expand() `method`

##### Summary

Expands the bounding region to cover another bounding region.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IBounding`1-ExpandLayer-System-Double-'></a>
### ExpandLayer() `method`

##### Summary

Adds an isotropic margin of width `r` around the current region.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IBounding`1-Reset'></a>
### Reset() `method`

##### Summary

Resets the bounding region to the empty state.

##### Parameters

This method has no parameters.

<a name='T-Math-Interfaces-ICloneable`1'></a>
## ICloneable\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Creates a new object that is a copy of the current instance.

<a name='M-Math-Interfaces-ICloneable`1-Clone'></a>
### Clone() `method`

##### Summary

A new object that is a copy of this instance.

##### Returns



##### Parameters

This method has no parameters.

<a name='T-Math-Interfaces-ICubicBezier`2'></a>
## ICubicBezier\`2 `type`

##### Namespace

Math.Interfaces

##### Summary

Interface of Bézier curves

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Vector type |
| S | Bézier type |

<a name='T-Math-Interfaces-ICurve`2'></a>
## ICurve\`2 `type`

##### Namespace

Math.Interfaces

##### Summary

Interface of a curve

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Vector type |
| S | Curve type |

<a name='M-Math-Interfaces-ICurve`2-Evaluate-System-Double-'></a>
### Evaluate(t) `method`

##### Summary

Point at t

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| t | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | parameter t, [0,1] |

<a name='M-Math-Interfaces-ICurve`2-Kappa-System-Double-'></a>
### Kappa(t) `method`

##### Summary

Curvature of the curve at t

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| t | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | parameter t, [0,1] |

<a name='M-Math-Interfaces-ICurve`2-Length-System-Double-'></a>
### Length(accuracy) `method`

##### Summary

Length of the curve

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accuracy | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Relative accuracy |

<a name='M-Math-Interfaces-ICurve`2-Split-System-Double-'></a>
### Split(t) `method`

##### Summary

Split curve into two parts at given split point

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| t | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | split at t, [0,1] |

<a name='M-Math-Interfaces-ICurve`2-Tangent-System-Double-'></a>
### Tangent(t) `method`

##### Summary

Tangent at t

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| t | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | parameter t, [0,1] |

<a name='M-Math-Interfaces-ICurve`2-d2Evaluate-System-Double-'></a>
### d2Evaluate(t) `method`

##### Summary

2nd derivative at t

##### Returns

2nd derivative

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| t | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | parameter t, [0,1] |

<a name='M-Math-Interfaces-ICurve`2-dEvaluate-System-Double-'></a>
### dEvaluate(t) `method`

##### Summary

Derivative at t

##### Returns

Derivative

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| t | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | parameter t, [0,1] |

<a name='T-Math-Interfaces-IDimension'></a>
## IDimension `type`

##### Namespace

Math.Interfaces

##### Summary

Interface of dimension of geometry object

<a name='P-Math-Interfaces-IDimension-Dimensions'></a>
### Dimensions `property`

##### Summary

Dimension of object

<a name='T-Math-Interfaces-IGeometryObject`1'></a>
## IGeometryObject\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Interface of geometry object

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='T-Math-Interfaces-IInnerProduct`1'></a>
## IInnerProduct\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Inner-product / angular operations on a vector type. Lifted out of [IVector\`1](#T-Math-Interfaces-IVector`1 'Math.Interfaces.IVector`1')
so callers that only need pairwise dot products and angles can depend on this narrower
surface (Interface Segregation).

<a name='M-Math-Interfaces-IInnerProduct`1-Angle-`0-'></a>
### Angle() `method`

##### Summary

Signed angle between this vector and `v`.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IInnerProduct`1-AngleAbs-`0-'></a>
### AngleAbs() `method`

##### Summary

Unsigned angle between this vector and `v`.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IInnerProduct`1-CrossNorm-`0-'></a>
### CrossNorm() `method`

##### Summary

Norm of the cross product (well-defined in 2D and 3D).

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IInnerProduct`1-Dot-`0-'></a>
### Dot() `method`

##### Summary

Dot product.

##### Parameters

This method has no parameters.

<a name='T-Math-Interfaces-IInterpolate`1'></a>
## IInterpolate\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Interface for interpolation between two objects of same type

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-Math-Interfaces-IInterpolate`1-Interpolate-`0,System-Double-'></a>
### Interpolate(t,x) `method`

##### Summary

Interpolation

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| t | [\`0](#T-`0 '`0') |  |
| x | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | interpolate by x, [0,1] |

<a name='T-Math-Interfaces-IIsEqual`1'></a>
## IIsEqual\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Interface is equal

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-Math-Interfaces-IIsEqual`1-IsEqual-`0-'></a>
### IsEqual(a) `method`

##### Summary

Is equal

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| a | [\`0](#T-`0 '`0') |  |

<a name='M-Math-Interfaces-IIsEqual`1-IsEqual-`0,System-Double-'></a>
### IsEqual(a,epsilon) `method`

##### Summary

Is equal with epsilon

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| a | [\`0](#T-`0 '`0') |  |
| epsilon | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | epsilon |

<a name='T-Math-Interfaces-INorm`1'></a>
## INorm\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Interface for norms

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-Math-Interfaces-INorm`1-EuclideanNorm-`0-'></a>
### EuclideanNorm(d) `method`

##### Summary

Euclidean norm, minimal Euclidean norm between to geometry objects

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [\`0](#T-`0 '`0') |  |

<a name='M-Math-Interfaces-INorm`1-ModifiedNorm-`0,System-Boolean-'></a>
### ModifiedNorm(d,direction) `method`

##### Summary

Modified norm

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [\`0](#T-`0 '`0') |  |
| direction | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='T-Math-Interfaces-INormalizable`1'></a>
## INormalizable\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Capability of having a (Euclidean) length and being scaled to unit length. Split out of
the previous monolithic [IVector\`1](#T-Math-Interfaces-IVector`1 'Math.Interfaces.IVector`1') so callers that only need a "I have a
magnitude" view of a value can ask for it without dragging in arithmetic and inner-product
dependencies (Interface Segregation).

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Concrete vector type returned by [Normalized](#M-Math-Interfaces-INormalizable`1-Normalized 'Math.Interfaces.INormalizable`1.Normalized'). |

<a name='M-Math-Interfaces-INormalizable`1-Norm'></a>
### Norm() `method`

##### Summary

Norm of the vector.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-INormalizable`1-Norm2'></a>
### Norm2() `method`

##### Summary

Squared norm of the vector.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-INormalizable`1-Normalize'></a>
### Normalize() `method`

##### Summary

Normalise this vector in place; returns the original length.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-INormalizable`1-Normalize-System-Double-'></a>
### Normalize() `method`

##### Summary

Normalise this vector in place using `epsilon`; returns the original length.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-INormalizable`1-Normalized'></a>
### Normalized() `method`

##### Summary

Returns a normalised copy.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-INormalizable`1-Normalized-System-Double-'></a>
### Normalized() `method`

##### Summary

Returns a normalised copy using `epsilon`.

##### Parameters

This method has no parameters.

<a name='T-Math-Interfaces-IReadOnlyBounding`1'></a>
## IReadOnlyBounding\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Read-only view of an axis-aligned bounding box / rectangle. Lets callers ask "where are
you and is point P inside?" without inheriting permission to mutate the box (Interface
Segregation). The full mutable contract lives in [IBounding\`1](#T-Math-Interfaces-IBounding`1 'Math.Interfaces.IBounding`1').

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Coordinate type (Vector2D, Vector3D, ...). |

<a name='P-Math-Interfaces-IReadOnlyBounding`1-Max'></a>
### Max `property`

##### Summary

Upper-right corner of the bounding region.

<a name='P-Math-Interfaces-IReadOnlyBounding`1-Min'></a>
### Min `property`

##### Summary

Lower-left corner of the bounding region.

<a name='M-Math-Interfaces-IReadOnlyBounding`1-IsEmpty'></a>
### IsEmpty() `method`

##### Summary

True iff the region carries no points (Min > Max in some dimension).

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IReadOnlyBounding`1-IsInside-`0-'></a>
### IsInside() `method`

##### Summary

Tests whether `v` lies inside the bounding region.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IReadOnlyBounding`1-IsInside-`0,System-Double-'></a>
### IsInside() `method`

##### Summary

Tests whether `v` lies inside the region within `eps`.

##### Parameters

This method has no parameters.

<a name='T-Math-Interfaces-ISegment`2'></a>
## ISegment\`2 `type`

##### Namespace

Math.Interfaces

##### Summary

Interface line segment

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |
| S |  |

<a name='P-Math-Interfaces-ISegment`2-A'></a>
### A `property`

##### Summary

Start point A

<a name='P-Math-Interfaces-ISegment`2-B'></a>
### B `property`

##### Summary

End point B

<a name='M-Math-Interfaces-ISegment`2-IsIntersecting-`1,System-Double-'></a>
### IsIntersecting(s,eps) `method`

##### Summary

Intersection between two segments

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| s | [\`1](#T-`1 '`1') |  |
| eps | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-Interfaces-ISegment`2-Vector'></a>
### Vector() `method`

##### Summary

Create new vector

##### Returns



##### Parameters

This method has no parameters.

<a name='T-Math-Interfaces-IVectorArith`1'></a>
## IVectorArith\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Vector-space arithmetic operations. Pulled out of [IVector\`1](#T-Math-Interfaces-IVector`1 'Math.Interfaces.IVector`1') so callers
that only need add/sub or scaling can depend on this narrower surface (Interface
Segregation).

<a name='M-Math-Interfaces-IVectorArith`1-Add-`0-'></a>
### Add() `method`

##### Summary

Returns this + v.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IVectorArith`1-Div-System-Double-'></a>
### Div() `method`

##### Summary

Returns this / c.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IVectorArith`1-Mul-System-Double-'></a>
### Mul() `method`

##### Summary

Returns this * c.

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IVectorArith`1-Sub-`0-'></a>
### Sub() `method`

##### Summary

Returns this - v.

##### Parameters

This method has no parameters.

<a name='T-Math-Interfaces-IVector`1'></a>
## IVector\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Aggregated contract for full vector-like types. Composed from smaller capabilities so that
downstream code can ask for the narrowest interface it actually needs (Interface
Segregation):

Existing implementations (Vector2D, Vector3D, GpsPoint, ...) already provide every
member; the split is source-compatible.

<a name='P-Math-Interfaces-IVector`1-X'></a>
### X `property`

##### Summary

X coordinate (mutable; preserved from the original IVector contract).

<a name='T-Math-Gps-PerpendicularDistanceProjector'></a>
## PerpendicularDistanceProjector `type`

##### Namespace

Math.Gps

##### Summary

Pipeline stage 2 of [Analyze](#M-Math-Gps-ANeighbourDistanceCalculator-Analyze-Math-Gps-FlatTrack,System-Double- 'Math.Gps.ANeighbourDistanceCalculator.Analyze(Math.Gps.FlatTrack,System.Double)'):
for each candidate (reference, current) point pairing produced by the grid lookup, snap
the current point onto the closer of the two adjacent reference segments and recompute the
distance metric and reference-track arc-length parameterisation. Pure, stateless helper.

<a name='T-Math-Gfx-PgmBitmapFormatWriter'></a>
## PgmBitmapFormatWriter `type`

##### Namespace

Math.Gfx

##### Summary

Writes the bitmap as a binary Portable Greymap (P5) using
[Grey](#M-Math-Gfx-IColorMapping-Grey-System-Double- 'Math.Gfx.IColorMapping.Grey(System.Double)') for the per-pixel luminance.

<a name='T-Math-PhysicalConstants'></a>
## PhysicalConstants `type`

##### Namespace

Math

##### Summary

Centralized physical constants used by the geodesy / dynamics models.

<a name='F-Math-PhysicalConstants-AirDensitySeaLevel'></a>
### AirDensitySeaLevel `constants`

##### Summary

Air density at 15 C, sea level, dry-air ISA model, kg/m^3.

<a name='F-Math-PhysicalConstants-GravitationalAcceleration'></a>
### GravitationalAcceleration `constants`

##### Summary

Standard gravity, m/s^2 (CGPM definition).

<a name='T-Math-Gfx-PngBitmapFormatWriter'></a>
## PngBitmapFormatWriter `type`

##### Namespace

Math.Gfx

##### Summary

Writes the bitmap as a PNG using [](#N-System-Drawing 'System.Drawing'). This is the only writer
that pulls in the System.Drawing dependency, so isolating it here keeps the PGM/PPM
writers (and any future cross-platform alternatives) free of that coupling.

<a name='T-Math-Gfx-PngTripleChannelBitmapWriter'></a>
## PngTripleChannelBitmapWriter `type`

##### Namespace

Math.Gfx

##### Summary

Three-channel PNG writer: each input raster supplies one of R, G, B mixed through the
shared [GreenMapping](#T-Math-Gfx-GreenMapping 'Math.Gfx.GreenMapping') grey ramp. Kept as a separate writer so that all
[](#N-System-Drawing 'System.Drawing') usage in the Math assembly is confined to the two
`Png*BitmapWriter` files - a future split into a separate
`Math.Gfx.SystemDrawing` assembly only has to relocate this file and
[PngBitmapFormatWriter](#T-Math-Gfx-PngBitmapFormatWriter 'Math.Gfx.PngBitmapFormatWriter').

<a name='T-Math-Polar3D'></a>
## Polar3D `type`

##### Namespace

Math

<a name='M-Math-Polar3D-ModifiedNorm-Math-Polar3D,System-Boolean-'></a>
### ModifiedNorm() `method`

##### Summary

Polar points carry no orientation, so `direction` is ignored.

##### Parameters

This method has no parameters.

<a name='T-Math-Clustering-PolylineNeighbours'></a>
## PolylineNeighbours `type`

##### Namespace

Math.Clustering

##### Summary

Find neighbouring ploylines given minimum distance

<a name='T-Math-Polynomial'></a>
## Polynomial `type`

##### Namespace

Math

##### Summary

Representation of a polynomial of n-th degree by coefficients. Functions to evaluate the polynomial, and also 1st derivatives and its integral.

##### Remarks

General root finder based on Laguerre's_method.

<a name='M-Math-Polynomial-#ctor-System-Collections-Generic-IEnumerable{System-Double}-'></a>
### #ctor(coefficients) `constructor`

##### Summary

Defining a polynomial by coefficients of n-th degree.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| coefficients | [System.Collections.Generic.IEnumerable{System.Double}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Double}') | Coefficients in decreasing order. E.g., 2x^2 + x + 3 as {2,1,3}. |

<a name='M-Math-Polynomial-DivideByRoot-System-Double-'></a>
### DivideByRoot() `method`

##### Summary

Returns the polynomial divided by a root

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-DivideByRootAndConjugate-System-Numerics-Complex-'></a>
### DivideByRootAndConjugate() `method`

##### Summary

Returns the polynomial divided by a complex root and with its conjugated

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-FindRoot-System-Numerics-Complex-'></a>
### FindRoot(x) `method`

##### Summary

Generic root solver based on Laguerre's_method.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x | [System.Numerics.Complex](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Numerics.Complex 'System.Numerics.Complex') | Start point for finding a root. |

<a name='M-Math-Polynomial-P'></a>
### P() `method`

##### Summary

Returns the Integral of the polynomial

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-P-System-Double-'></a>
### P() `method`

##### Summary

Evaluates for integral of polynomial

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-P-System-Numerics-Complex-'></a>
### P() `method`

##### Summary

Evaluates the integral of polynomial for complex numbers

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-dp'></a>
### dp() `method`

##### Summary

Returns the 1st derivative of the polynomial

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-dp-System-Double-'></a>
### dp() `method`

##### Summary

Evaluates for 1st derivative of polynomial

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-dp-System-Numerics-Complex-'></a>
### dp() `method`

##### Summary

Evaluates 1st derivative of polynomial for complex numbers

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-p'></a>
### p() `method`

##### Summary

Returns the polynomial coefficients

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-p-System-Double-'></a>
### p() `method`

##### Summary

Evaluates the polynomial

##### Parameters

This method has no parameters.

<a name='M-Math-Polynomial-p-System-Numerics-Complex-'></a>
### p() `method`

##### Summary

Evaluates polynomial for complex numbers

##### Parameters

This method has no parameters.

<a name='T-Math-Gfx-PpmBitmapFormatWriter'></a>
## PpmBitmapFormatWriter `type`

##### Namespace

Math.Gfx

##### Summary

Writes the bitmap as a binary Portable Pixmap (P6) using
[Color](#M-Math-Gfx-IColorMapping-Color-System-Double- 'Math.Gfx.IColorMapping.Color(System.Double)') for the per-pixel RGB triplet.

<a name='T-Math-Gps-RadiusCutOff'></a>
## RadiusCutOff `type`

##### Namespace

Math.Gps

##### Summary

Pipeline stage 3 of [Analyze](#M-Math-Gps-ANeighbourDistanceCalculator-Analyze-Math-Gps-FlatTrack,System-Double- 'Math.Gps.ANeighbourDistanceCalculator.Analyze(Math.Gps.FlatTrack,System.Double)'):
drop projected pairings whose perpendicular distance exceeds `radius` and remove
reference buckets that became empty as a result. Pure, stateless helper.

<a name='T-Math-Clustering-TraClus-Result`1'></a>
## Result\`1 `type`

##### Namespace

Math.Clustering.TraClus

##### Summary

Representative common segment.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Vector of dimension n, e.g., Vector2D or Vector3D. |

<a name='M-Math-Clustering-TraClus-Result`1-#ctor'></a>
### #ctor() `constructor`

##### Summary

Representative common segment.

##### Parameters

This constructor has no parameters.

<a name='P-Math-Clustering-TraClus-Result`1-PointIndices'></a>
### PointIndices `property`

##### Summary

Returns a list of tracks of point indices as sorted list, which were used during clustering.

<a name='P-Math-Clustering-TraClus-Result`1-Segment'></a>
### Segment `property`

##### Summary

Returns the representative common segment as a polyline, list of points.

<a name='P-Math-Clustering-TraClus-Result`1-SegmentIndices'></a>
### SegmentIndices `property`

##### Summary

Returns a list of tracks of list of segments (pair point index), which were used during clustering.

<a name='T-Math-Solver'></a>
## Solver `type`

##### Namespace

Math

<a name='M-Math-Solver-PolynomialEq-System-Collections-Generic-List{System-Double}-'></a>
### PolynomialEq() `method`

##### Summary

Returns the (deduplicated, sorted) list of real roots of the polynomial described by
`coefficients`, where coefficients are stored constant-first
(coefficients[i] is the factor of x^i).

##### Parameters

This method has no parameters.

##### Remarks

Zero-root handling: when the constant term is (near) zero, x=0 is recorded once and the
polynomial is divided by x^k where k is the multiplicity of zero. The returned list
therefore reports distinct real roots only - it does not encode multiplicity.
Constant or empty inputs produce an empty list (no real roots in the conventional sense).

<a name='T-Math-SparseArray`1'></a>
## SparseArray\`1 `type`

##### Namespace

Math

##### Summary

Index-keyed sparse container backed by a Dictionary. Intentionally does NOT implement
IList<T>: an IList is positional and contiguous, this collection is neither.
Earlier versions claimed IList<T> while throwing NotImplementedException for half
of its members, which broke generic IList consumers.

<a name='T-Math-Clustering-TraClus'></a>
## TraClus `type`

##### Namespace

Math.Clustering

##### Summary

Implementation of "Trajectory Clustering: A Partition-and-Group Framework", by Jae-Gil Lee, Jiawei Han and Kyu-Young Whang.

<a name='M-Math-Clustering-TraClus-Cluster-System-Collections-Generic-IList{System-Collections-Generic-List{Math-Vector2D}},System-Int32,System-Double,System-Boolean,System-Double,System-Int32-'></a>
### Cluster(tracks,n,eps,direction,minL,mdlCostAdvantage) `method`

##### Summary

Clustering of 2D trajectories.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tracks | [System.Collections.Generic.IList{System.Collections.Generic.List{Math.Vector2D}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{System.Collections.Generic.List{Math.Vector2D}}') | List of tracks |
| n | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Minimum number of common segments required to be recognized as a cluster. |
| eps | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Epsilon of neighborhood between to segments using trajectory Hausdorff distance. |
| direction | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Boolean defining if the direction between two segments shall include in the trajectory Hausdorff distance. |
| minL | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Minimum length of a segment |
| mdlCostAdvantage | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Maximum cost allowed for minimum description length (MDL). |

<a name='M-Math-Clustering-TraClus-Cluster-System-Collections-Generic-IList{System-Collections-Generic-List{Math-Vector3D}},System-Int32,System-Double,System-Boolean,System-Double,System-Int32-'></a>
### Cluster(tracks,n,eps,direction,minL,mdlCostAdvantage) `method`

##### Summary

Clustering of 3D trajectories.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tracks | [System.Collections.Generic.IList{System.Collections.Generic.List{Math.Vector3D}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{System.Collections.Generic.List{Math.Vector3D}}') | List of tracks |
| n | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Minimum number of common segments required to be recognized as a cluster. |
| eps | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Epsilon of neighborhood between to segments using trajectory Hausdorff distance. |
| direction | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Boolean defining if the direction between two segments shall include in the trajectory Hausdorff distance. |
| minL | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Minimum length of a segment |
| mdlCostAdvantage | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Maximum cost allowed for minimum description length (MDL). |

<a name='T-Math-KDTree-TreeBuilder'></a>
## TreeBuilder `type`

##### Namespace

Math.KDTree

##### Summary

Builds k-d trees for any payload type that exposes an axis-aligned bounding box via
[IBoundingFacade\`1](#T-Math-Interfaces-IBoundingFacade`1 'Math.Interfaces.IBoundingFacade`1'). The OCP entry point is the generic
[Build\`\`2](#M-Math-KDTree-TreeBuilder-Build``2-System-Collections-Generic-IEnumerable{``1},System-Int32- 'Math.KDTree.TreeBuilder.Build``2(System.Collections.Generic.IEnumerable{``1},System.Int32)'); the four typed overloads below are pure
ergonomic shortcuts so callers do not have to spell out the type parameters for the
first-party Vector/Segment combinations.

<a name='M-Math-KDTree-TreeBuilder-Build``2-System-Collections-Generic-IEnumerable{``1},System-Int32-'></a>
### Build\`\`2() `method`

##### Summary

Generic builder accepting any payload `S` that can produce a
`T`-valued bounding box. Adding a new geometric primitive to the
k-d tree only requires implementing [IBoundingFacade\`1](#T-Math-Interfaces-IBoundingFacade`1 'Math.Interfaces.IBoundingFacade`1') on it - the
builder itself does not need to change (Open/Closed Principle).

##### Parameters

This method has no parameters.

<a name='T-Math-Vector2D'></a>
## Vector2D `type`

##### Namespace

Math

<a name='M-Math-Vector2D-ModifiedNorm-Math-Vector2D,System-Boolean-'></a>
### ModifiedNorm() `method`

##### Summary

For a directionless 2D point the modified norm coincides with the Euclidean norm, so
`direction` is intentionally ignored. The parameter exists to satisfy
the [INorm\`1](#T-Math-Interfaces-INorm`1 'Math.Interfaces.INorm`1') contract shared with directional types like Segment2D.

##### Parameters

This method has no parameters.

<a name='T-Math-Vector3D'></a>
## Vector3D `type`

##### Namespace

Math

<a name='M-Math-Vector3D-ModifiedNorm-Math-Vector3D,System-Boolean-'></a>
### ModifiedNorm() `method`

##### Summary

For a directionless 3D point the modified norm coincides with the Euclidean norm;
`direction` is ignored. See [ModifiedNorm](#M-Math-Vector2D-ModifiedNorm-Math-Vector2D,System-Boolean- 'Math.Vector2D.ModifiedNorm(Math.Vector2D,System.Boolean)').

##### Parameters

This method has no parameters.
