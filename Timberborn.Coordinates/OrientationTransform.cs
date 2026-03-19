using System;
using Timberborn.PrefabOptimization;
using UnityEngine;

namespace Timberborn.Coordinates
{
	// Token: 0x02000017 RID: 23
	public readonly struct OrientationTransform : ITransform
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003873 File Offset: 0x00001A73
		public OrientationTransform(Orientation orientation)
		{
			this._orientation = orientation;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000387C File Offset: 0x00001A7C
		public void MultiplyPoints(Vector3[] source, Vector3[] destination, int destinationIndex, int count)
		{
			for (int i = 0; i < count; i++)
			{
				destination[destinationIndex + i] = this._orientation.TransformInWorldSpace(source[i]);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000038B1 File Offset: 0x00001AB1
		public void MultiplyNormals(Vector3[] source, Vector3[] destination, int destinationIndex, int count)
		{
			this.MultiplyPoints(source, destination, destinationIndex, count);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000038C0 File Offset: 0x00001AC0
		public void MultiplyTangents(Vector4[] source, Vector4[] destination, int destinationIndex, int count)
		{
			for (int i = 0; i < count; i++)
			{
				Vector4 vector = source[i];
				Vector3 vector2 = this._orientation.TransformInWorldSpace(vector);
				destination[destinationIndex + i] = new Vector4(vector2.x, vector2.y, vector2.z, vector.w);
			}
		}

		// Token: 0x04000049 RID: 73
		public readonly Orientation _orientation;
	}
}
