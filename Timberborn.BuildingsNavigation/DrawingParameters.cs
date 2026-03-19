using System;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000018 RID: 24
	public readonly struct DrawingParameters : IEquatable<DrawingParameters>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003AF6 File Offset: 0x00001CF6
		public bool IsPreview { get; }

		// Token: 0x060000A1 RID: 161 RVA: 0x00003AFE File Offset: 0x00001CFE
		public DrawingParameters(bool isPreview, Vector3 coordinates, Orientation orientation, bool isSingle)
		{
			this.IsPreview = isPreview;
			this._coordinates = coordinates;
			this._orientation = orientation;
			this._isSingle = isSingle;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003B20 File Offset: 0x00001D20
		public bool Equals(DrawingParameters other)
		{
			return this._coordinates.Equals(other._coordinates) && this._orientation == other._orientation && this._isSingle == other._isSingle && this.IsPreview == other.IsPreview;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B70 File Offset: 0x00001D70
		public override bool Equals(object obj)
		{
			if (obj is DrawingParameters)
			{
				DrawingParameters other = (DrawingParameters)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B95 File Offset: 0x00001D95
		public override int GetHashCode()
		{
			return HashCode.Combine<Vector3, Orientation, bool, bool>(this._coordinates, this._orientation, this._isSingle, this.IsPreview);
		}

		// Token: 0x0400005A RID: 90
		public readonly Vector3 _coordinates;

		// Token: 0x0400005B RID: 91
		public readonly Orientation _orientation;

		// Token: 0x0400005C RID: 92
		public readonly bool _isSingle;
	}
}
