using System;
using Timberborn.PrioritySystemUI;
using UnityEngine.UIElements;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x0200000F RID: 15
	public class BuilderPriorityToggleGroupFactory
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000269A File Offset: 0x0000089A
		public BuilderPriorityToggleGroupFactory(BuilderPrioritySpriteLoader builderPrioritySpriteLoader, PriorityToggleGroupFactory priorityToggleGroupFactory)
		{
			this._builderPrioritySpriteLoader = builderPrioritySpriteLoader;
			this._priorityToggleGroupFactory = priorityToggleGroupFactory;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026B0 File Offset: 0x000008B0
		public PriorityToggleGroup Create(VisualElement parent, string labelLocKey)
		{
			return this._priorityToggleGroupFactory.Create(parent, labelLocKey, this._builderPrioritySpriteLoader, BuilderPriorityToggleGroupFactory.DecreaseBuildersPriorityKey, BuilderPriorityToggleGroupFactory.IncreaseBuildersPriorityKey);
		}

		// Token: 0x04000026 RID: 38
		public static readonly string DecreaseBuildersPriorityKey = "DecreaseBuildersPriority";

		// Token: 0x04000027 RID: 39
		public static readonly string IncreaseBuildersPriorityKey = "IncreaseBuildersPriority";

		// Token: 0x04000028 RID: 40
		public readonly BuilderPrioritySpriteLoader _builderPrioritySpriteLoader;

		// Token: 0x04000029 RID: 41
		public readonly PriorityToggleGroupFactory _priorityToggleGroupFactory;
	}
}
