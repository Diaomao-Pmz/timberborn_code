using System;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x0200001E RID: 30
	public class EmptyBatchControlRowItem : IBatchControlRowItem
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003DEF File Offset: 0x00001FEF
		public VisualElement Root { get; }

		// Token: 0x060000B2 RID: 178 RVA: 0x00003DF7 File Offset: 0x00001FF7
		public EmptyBatchControlRowItem(VisualElement root)
		{
			this.Root = root;
		}
	}
}
