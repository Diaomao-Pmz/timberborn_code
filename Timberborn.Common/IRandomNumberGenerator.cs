using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000022 RID: 34
	public interface IRandomNumberGenerator
	{
		// Token: 0x06000077 RID: 119
		float Range(float inclusiveMin, float inclusiveMax);

		// Token: 0x06000078 RID: 120
		int Range(int inclusiveMin, int exclusiveMax);

		// Token: 0x06000079 RID: 121
		Vector2 InsideUnitCircle();

		// Token: 0x0600007A RID: 122
		T GetListElement<T>(IReadOnlyList<T> list);

		// Token: 0x0600007B RID: 123
		bool TryGetListElement<T>(IReadOnlyList<T> list, out T randomElement);

		// Token: 0x0600007C RID: 124
		T GetListElementOrDefault<T>(IReadOnlyList<T> list);

		// Token: 0x0600007D RID: 125
		T GetEnumerableElement<T>(IEnumerable<T> source);

		// Token: 0x0600007E RID: 126
		bool TryGetEnumerableElement<T>(IEnumerable<T> source, out T randomElement);

		// Token: 0x0600007F RID: 127
		bool CheckProbability(float normalizedProbability);
	}
}
