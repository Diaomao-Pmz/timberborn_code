using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000024 RID: 36
	public readonly struct TranslationTransform : ITransform
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00005314 File Offset: 0x00003514
		public TranslationTransform(Vector3 translation)
		{
			this._translation = translation;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005320 File Offset: 0x00003520
		public void MultiplyPoints(Vector3[] source, Vector3[] destination, int destinationIndex, int count)
		{
			for (int i = 0; i < count; i++)
			{
				destination[destinationIndex + i] = source[i] + this._translation;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005355 File Offset: 0x00003555
		public void MultiplyNormals(Vector3[] source, Vector3[] destination, int destinationIndex, int count)
		{
			Array.Copy(source, 0, destination, destinationIndex, count);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005355 File Offset: 0x00003555
		public void MultiplyTangents(Vector4[] source, Vector4[] destination, int destinationIndex, int count)
		{
			Array.Copy(source, 0, destination, destinationIndex, count);
		}

		// Token: 0x04000091 RID: 145
		public readonly Vector3 _translation;
	}
}
