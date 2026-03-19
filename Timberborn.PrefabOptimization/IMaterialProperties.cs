using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000013 RID: 19
	public interface IMaterialProperties
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000097 RID: 151
		Color32 Color { get; }

		// Token: 0x06000098 RID: 152
		void ApplyToMaterial(Material material);

		// Token: 0x06000099 RID: 153
		IMaterialProperties GetWithoutColor();
	}
}
