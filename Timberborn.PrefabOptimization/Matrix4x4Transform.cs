using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000019 RID: 25
	public struct Matrix4x4Transform : ITransform
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000040D8 File Offset: 0x000022D8
		public Matrix4x4Transform(Matrix4x4 matrix)
		{
			this._matrix = matrix;
			this._normalMatrix = matrix.inverse.transpose;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004104 File Offset: 0x00002304
		public void MultiplyPoints(Vector3[] source, Vector3[] destination, int destinationIndex, int count)
		{
			for (int i = 0; i < count; i++)
			{
				destination[destinationIndex + i] = this._matrix.MultiplyPoint(source[i]);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000413C File Offset: 0x0000233C
		public void MultiplyNormals(Vector3[] source, Vector3[] destination, int destinationIndex, int count)
		{
			for (int i = 0; i < count; i++)
			{
				destination[destinationIndex + i] = this._normalMatrix.MultiplyVector(source[i]).normalized;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000417C File Offset: 0x0000237C
		public void MultiplyTangents(Vector4[] source, Vector4[] destination, int destinationIndex, int count)
		{
			for (int i = 0; i < count; i++)
			{
				Vector4 vector = source[i];
				Vector3 normalized = this._normalMatrix.MultiplyVector(vector).normalized;
				destination[destinationIndex + i] = new Vector4(normalized.x, normalized.y, normalized.z, vector.w);
			}
		}

		// Token: 0x04000065 RID: 101
		public Matrix4x4 _matrix;

		// Token: 0x04000066 RID: 102
		public Matrix4x4 _normalMatrix;
	}
}
