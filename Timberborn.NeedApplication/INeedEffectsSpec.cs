using System;
using System.Collections.Immutable;

namespace Timberborn.NeedApplication
{
	// Token: 0x0200000F RID: 15
	public interface INeedEffectsSpec
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000055 RID: 85
		ImmutableArray<NeedApplierEffectSpec> Effects { get; }
	}
}
