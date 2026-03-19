using System;
using Timberborn.Modding;

namespace Timberborn.ModdingUI
{
	// Token: 0x02000004 RID: 4
	public interface IModItemFactory
	{
		// Token: 0x06000003 RID: 3
		ModItem CreateModItem(Mod mod, Action<Mod, bool> onPriorityIncreased, Action<Mod, bool> onPriorityDecreased);
	}
}
