using System;
using System.Collections.Generic;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200001F RID: 31
	public interface ISpecService
	{
		// Token: 0x060000C1 RID: 193
		Blueprint GetBlueprint(string blueprintPath);

		// Token: 0x060000C2 RID: 194
		T GetSingleSpec<T>() where T : ComponentSpec;

		// Token: 0x060000C3 RID: 195
		IEnumerable<T> GetSpecs<T>() where T : ComponentSpec;
	}
}
