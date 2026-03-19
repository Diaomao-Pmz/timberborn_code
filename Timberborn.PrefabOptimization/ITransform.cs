using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000017 RID: 23
	public interface ITransform
	{
		// Token: 0x060000B1 RID: 177
		void MultiplyPoints(Vector3[] source, Vector3[] destination, int destinationIndex, int count);

		// Token: 0x060000B2 RID: 178
		void MultiplyNormals(Vector3[] source, Vector3[] destination, int destinationIndex, int count);

		// Token: 0x060000B3 RID: 179
		void MultiplyTangents(Vector4[] source, Vector4[] destination, int destinationIndex, int count);
	}
}
