using System;
using Timberborn.PrioritySystemUI;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200001F RID: 31
	public class WorkplacePriorityToggleGroupFactory
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003B7D File Offset: 0x00001D7D
		public WorkplacePriorityToggleGroupFactory(PriorityToggleGroupFactory priorityToggleGroupFactory, WorkplacePrioritySpriteLoader workplacePrioritySpriteLoader)
		{
			this._priorityToggleGroupFactory = priorityToggleGroupFactory;
			this._workplacePrioritySpriteLoader = workplacePrioritySpriteLoader;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003B93 File Offset: 0x00001D93
		public PriorityToggleGroup Create(VisualElement parent, string labelLocKey)
		{
			return this._priorityToggleGroupFactory.Create(parent, labelLocKey, this._workplacePrioritySpriteLoader, WorkplacePriorityToggleGroupFactory.DecreaseWorkplacePriorityKey, WorkplacePriorityToggleGroupFactory.IncreaseWorkplacePriorityKey);
		}

		// Token: 0x04000090 RID: 144
		public static readonly string DecreaseWorkplacePriorityKey = "DecreaseWorkplacePriority";

		// Token: 0x04000091 RID: 145
		public static readonly string IncreaseWorkplacePriorityKey = "IncreaseWorkplacePriority";

		// Token: 0x04000092 RID: 146
		public readonly PriorityToggleGroupFactory _priorityToggleGroupFactory;

		// Token: 0x04000093 RID: 147
		public readonly WorkplacePrioritySpriteLoader _workplacePrioritySpriteLoader;
	}
}
