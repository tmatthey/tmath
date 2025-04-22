<a name='assembly'></a>
# Math

## Contents

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
- [DBScan\`2](#T-Math-Clustering-DBScan`2 'Math.Clustering.DBScan`2')
  - [#ctor(list)](#M-Math-Clustering-DBScan`2-#ctor-System-Collections-Generic-IList{`1}- 'Math.Clustering.DBScan`2.#ctor(System.Collections.Generic.IList{`1})')
  - [Cluster(eps,n,direction)](#M-Math-Clustering-DBScan`2-Cluster-System-Double,System-Int32,System-Boolean- 'Math.Clustering.DBScan`2.Cluster(System.Double,System.Int32,System.Boolean)')
- [IArray](#T-Math-Interfaces-IArray 'Math.Interfaces.IArray')
  - [Array](#P-Math-Interfaces-IArray-Array 'Math.Interfaces.IArray.Array')
  - [Item](#P-Math-Interfaces-IArray-Item-System-Int32- 'Math.Interfaces.IArray.Item(System.Int32)')
- [IBoundingFacade\`1](#T-Math-Interfaces-IBoundingFacade`1 'Math.Interfaces.IBoundingFacade`1')
  - [Bounding()](#M-Math-Interfaces-IBoundingFacade`1-Bounding 'Math.Interfaces.IBoundingFacade`1.Bounding')
- [IBounding\`1](#T-Math-Interfaces-IBounding`1 'Math.Interfaces.IBounding`1')
  - [Max](#P-Math-Interfaces-IBounding`1-Max 'Math.Interfaces.IBounding`1.Max')
  - [Min](#P-Math-Interfaces-IBounding`1-Min 'Math.Interfaces.IBounding`1.Min')
  - [Expand(v)](#M-Math-Interfaces-IBounding`1-Expand-`0- 'Math.Interfaces.IBounding`1.Expand(`0)')
  - [Expand(b)](#M-Math-Interfaces-IBounding`1-Expand-Math-Interfaces-IBounding{`0}- 'Math.Interfaces.IBounding`1.Expand(Math.Interfaces.IBounding{`0})')
  - [ExpandLayer(r)](#M-Math-Interfaces-IBounding`1-ExpandLayer-System-Double- 'Math.Interfaces.IBounding`1.ExpandLayer(System.Double)')
  - [IsEmpty()](#M-Math-Interfaces-IBounding`1-IsEmpty 'Math.Interfaces.IBounding`1.IsEmpty')
  - [IsInside(v)](#M-Math-Interfaces-IBounding`1-IsInside-`0- 'Math.Interfaces.IBounding`1.IsInside(`0)')
  - [IsInside(v,eps)](#M-Math-Interfaces-IBounding`1-IsInside-`0,System-Double- 'Math.Interfaces.IBounding`1.IsInside(`0,System.Double)')
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
- [IInterpolate\`1](#T-Math-Interfaces-IInterpolate`1 'Math.Interfaces.IInterpolate`1')
  - [Interpolate(t,x)](#M-Math-Interfaces-IInterpolate`1-Interpolate-`0,System-Double- 'Math.Interfaces.IInterpolate`1.Interpolate(`0,System.Double)')
- [IIsEqual\`1](#T-Math-Interfaces-IIsEqual`1 'Math.Interfaces.IIsEqual`1')
  - [IsEqual(a)](#M-Math-Interfaces-IIsEqual`1-IsEqual-`0- 'Math.Interfaces.IIsEqual`1.IsEqual(`0)')
  - [IsEqual(a,epsilon)](#M-Math-Interfaces-IIsEqual`1-IsEqual-`0,System-Double- 'Math.Interfaces.IIsEqual`1.IsEqual(`0,System.Double)')
- [INorm\`1](#T-Math-Interfaces-INorm`1 'Math.Interfaces.INorm`1')
  - [EuclideanNorm(d)](#M-Math-Interfaces-INorm`1-EuclideanNorm-`0- 'Math.Interfaces.INorm`1.EuclideanNorm(`0)')
  - [ModifiedNorm(d,direction)](#M-Math-Interfaces-INorm`1-ModifiedNorm-`0,System-Boolean- 'Math.Interfaces.INorm`1.ModifiedNorm(`0,System.Boolean)')
- [ISegment\`2](#T-Math-Interfaces-ISegment`2 'Math.Interfaces.ISegment`2')
  - [A](#P-Math-Interfaces-ISegment`2-A 'Math.Interfaces.ISegment`2.A')
  - [B](#P-Math-Interfaces-ISegment`2-B 'Math.Interfaces.ISegment`2.B')
  - [IsIntersecting(s,eps)](#M-Math-Interfaces-ISegment`2-IsIntersecting-`1,System-Double- 'Math.Interfaces.ISegment`2.IsIntersecting(`1,System.Double)')
  - [Vector()](#M-Math-Interfaces-ISegment`2-Vector 'Math.Interfaces.ISegment`2.Vector')
- [IVector\`1](#T-Math-Interfaces-IVector`1 'Math.Interfaces.IVector`1')
  - [Add(v)](#M-Math-Interfaces-IVector`1-Add-`0- 'Math.Interfaces.IVector`1.Add(`0)')
  - [Angle(v)](#M-Math-Interfaces-IVector`1-Angle-`0- 'Math.Interfaces.IVector`1.Angle(`0)')
  - [AngleAbs(v)](#M-Math-Interfaces-IVector`1-AngleAbs-`0- 'Math.Interfaces.IVector`1.AngleAbs(`0)')
  - [CrossNorm(v)](#M-Math-Interfaces-IVector`1-CrossNorm-`0- 'Math.Interfaces.IVector`1.CrossNorm(`0)')
  - [Div(c)](#M-Math-Interfaces-IVector`1-Div-System-Double- 'Math.Interfaces.IVector`1.Div(System.Double)')
  - [Dot(v)](#M-Math-Interfaces-IVector`1-Dot-`0- 'Math.Interfaces.IVector`1.Dot(`0)')
  - [Interpolate(v,x)](#M-Math-Interfaces-IVector`1-Interpolate-`0,System-Double- 'Math.Interfaces.IVector`1.Interpolate(`0,System.Double)')
  - [IsEqual(v)](#M-Math-Interfaces-IVector`1-IsEqual-`0- 'Math.Interfaces.IVector`1.IsEqual(`0)')
  - [IsEqual(v,epsilon)](#M-Math-Interfaces-IVector`1-IsEqual-`0,System-Double- 'Math.Interfaces.IVector`1.IsEqual(`0,System.Double)')
  - [Mul(c)](#M-Math-Interfaces-IVector`1-Mul-System-Double- 'Math.Interfaces.IVector`1.Mul(System.Double)')
  - [Norm()](#M-Math-Interfaces-IVector`1-Norm 'Math.Interfaces.IVector`1.Norm')
  - [Norm2()](#M-Math-Interfaces-IVector`1-Norm2 'Math.Interfaces.IVector`1.Norm2')
  - [Normalize()](#M-Math-Interfaces-IVector`1-Normalize 'Math.Interfaces.IVector`1.Normalize')
  - [Normalize(epsilon)](#M-Math-Interfaces-IVector`1-Normalize-System-Double- 'Math.Interfaces.IVector`1.Normalize(System.Double)')
  - [Normalized()](#M-Math-Interfaces-IVector`1-Normalized 'Math.Interfaces.IVector`1.Normalized')
  - [Normalized(epsilon)](#M-Math-Interfaces-IVector`1-Normalized-System-Double- 'Math.Interfaces.IVector`1.Normalized(System.Double)')
  - [Sub(v)](#M-Math-Interfaces-IVector`1-Sub-`0- 'Math.Interfaces.IVector`1.Sub(`0)')
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
- [Result\`1](#T-Math-Clustering-TraClus-Result`1 'Math.Clustering.TraClus.Result`1')
  - [#ctor()](#M-Math-Clustering-TraClus-Result`1-#ctor 'Math.Clustering.TraClus.Result`1.#ctor')
  - [PointIndices](#P-Math-Clustering-TraClus-Result`1-PointIndices 'Math.Clustering.TraClus.Result`1.PointIndices')
  - [Segment](#P-Math-Clustering-TraClus-Result`1-Segment 'Math.Clustering.TraClus.Result`1.Segment')
  - [SegmentIndices](#P-Math-Clustering-TraClus-Result`1-SegmentIndices 'Math.Clustering.TraClus.Result`1.SegmentIndices')
- [TraClus](#T-Math-Clustering-TraClus 'Math.Clustering.TraClus')
  - [Cluster(tracks,n,eps,direction,minL,mdlCostAdvantage)](#M-Math-Clustering-TraClus-Cluster-System-Collections-Generic-IList{System-Collections-Generic-List{Math-Vector2D}},System-Int32,System-Double,System-Boolean,System-Double,System-Int32- 'Math.Clustering.TraClus.Cluster(System.Collections.Generic.IList{System.Collections.Generic.List{Math.Vector2D}},System.Int32,System.Double,System.Boolean,System.Double,System.Int32)')
  - [Cluster(tracks,n,eps,direction,minL,mdlCostAdvantage)](#M-Math-Clustering-TraClus-Cluster-System-Collections-Generic-IList{System-Collections-Generic-List{Math-Vector3D}},System-Int32,System-Double,System-Boolean,System-Double,System-Int32- 'Math.Clustering.TraClus.Cluster(System.Collections.Generic.IList{System.Collections.Generic.List{Math.Vector3D}},System.Int32,System.Double,System.Boolean,System.Double,System.Int32)')

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

Defining a DBScan with a list of the geometric objects to be clustered.

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

<a name='T-Math-Interfaces-IArray'></a>
## IArray `type`

##### Namespace

Math.Interfaces

##### Summary

Interface geometry object for coordinate(s) in linear / array representation.
Either point object or 2-point (min-max) object

<a name='P-Math-Interfaces-IArray-Array'></a>
### Array `property`

##### Summary

Array representation of the coordinate(s)

<a name='P-Math-Interfaces-IArray-Item-System-Int32-'></a>
### Item `property`

##### Summary

Array access of the coordinate(s)

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| i | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | index |

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

Interface of bounding box or rectangle

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='P-Math-Interfaces-IBounding`1-Max'></a>
### Max `property`

##### Summary

Max

<a name='P-Math-Interfaces-IBounding`1-Min'></a>
### Min `property`

##### Summary

Min

<a name='M-Math-Interfaces-IBounding`1-Expand-`0-'></a>
### Expand(v) `method`

##### Summary

Expand bounding box with vector / point

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') | vector / point |

<a name='M-Math-Interfaces-IBounding`1-Expand-Math-Interfaces-IBounding{`0}-'></a>
### Expand(b) `method`

##### Summary

Expanding with another bounding box

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| b | [Math.Interfaces.IBounding{\`0}](#T-Math-Interfaces-IBounding{`0} 'Math.Interfaces.IBounding{`0}') |  |

<a name='M-Math-Interfaces-IBounding`1-ExpandLayer-System-Double-'></a>
### ExpandLayer(r) `method`

##### Summary

Add layer aorund current bounding box

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| r | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | radius |

<a name='M-Math-Interfaces-IBounding`1-IsEmpty'></a>
### IsEmpty() `method`

##### Summary

Is empty bounding box

##### Returns



##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IBounding`1-IsInside-`0-'></a>
### IsInside(v) `method`

##### Summary

Is point inside

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') | vector / point |

<a name='M-Math-Interfaces-IBounding`1-IsInside-`0,System-Double-'></a>
### IsInside(v,eps) `method`

##### Summary

Is point inside

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') | point / vector |
| eps | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | epsilon |

<a name='M-Math-Interfaces-IBounding`1-Reset'></a>
### Reset() `method`

##### Summary

Reset

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

<a name='T-Math-Interfaces-IVector`1'></a>
## IVector\`1 `type`

##### Namespace

Math.Interfaces

##### Summary

Interface vector

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-Math-Interfaces-IVector`1-Add-`0-'></a>
### Add(v) `method`

##### Summary

Add vector

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') |  |

<a name='M-Math-Interfaces-IVector`1-Angle-`0-'></a>
### Angle(v) `method`

##### Summary

Angle between two vectors

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') |  |

<a name='M-Math-Interfaces-IVector`1-AngleAbs-`0-'></a>
### AngleAbs(v) `method`

##### Summary

Unsigned angle between two vectors

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') |  |

<a name='M-Math-Interfaces-IVector`1-CrossNorm-`0-'></a>
### CrossNorm(v) `method`

##### Summary

Norm of cross product

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') |  |

<a name='M-Math-Interfaces-IVector`1-Div-System-Double-'></a>
### Div(c) `method`

##### Summary

Divide with a scalar

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| c | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-Interfaces-IVector`1-Dot-`0-'></a>
### Dot(v) `method`

##### Summary

Dot product

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') |  |

<a name='M-Math-Interfaces-IVector`1-Interpolate-`0,System-Double-'></a>
### Interpolate(v,x) `method`

##### Summary

Interpolate between two vectors

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') |  |
| x | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-Interfaces-IVector`1-IsEqual-`0-'></a>
### IsEqual(v) `method`

##### Summary

Is equal

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') |  |

<a name='M-Math-Interfaces-IVector`1-IsEqual-`0,System-Double-'></a>
### IsEqual(v,epsilon) `method`

##### Summary

Is equal with epsilon

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') |  |
| epsilon | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-Interfaces-IVector`1-Mul-System-Double-'></a>
### Mul(c) `method`

##### Summary

Multiply with a scalar

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| c | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-Interfaces-IVector`1-Norm'></a>
### Norm() `method`

##### Summary

Norm

##### Returns



##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IVector`1-Norm2'></a>
### Norm2() `method`

##### Summary

Squared norm of vector

##### Returns



##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IVector`1-Normalize'></a>
### Normalize() `method`

##### Summary

Normalize vector

##### Returns

Length of vector

##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IVector`1-Normalize-System-Double-'></a>
### Normalize(epsilon) `method`

##### Summary

Normalize vector with epsilon

##### Returns

Length of vector

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| epsilon | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-Interfaces-IVector`1-Normalized'></a>
### Normalized() `method`

##### Summary

Return normalized vector

##### Returns



##### Parameters

This method has no parameters.

<a name='M-Math-Interfaces-IVector`1-Normalized-System-Double-'></a>
### Normalized(epsilon) `method`

##### Summary

Return normalized vector with epsilon

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| epsilon | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') |  |

<a name='M-Math-Interfaces-IVector`1-Sub-`0-'></a>
### Sub(v) `method`

##### Summary

Subtract vector

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [\`0](#T-`0 '`0') |  |

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
