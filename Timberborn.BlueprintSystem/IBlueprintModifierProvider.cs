using System;
using System.Collections.Generic;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200001D RID: 29
	public interface IBlueprintModifierProvider
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BD RID: 189
		string ModifierName { get; }

		// Token: 0x060000BE RID: 190
		IEnumerable<string> GetModifiers(string blueprintPath);
	}
}
