using System;
using UnityEngine.UIElements;

namespace Timberborn.ScienceSystemUI
{
	// Token: 0x02000006 RID: 6
	public class ScienceCostPerHour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021C7 File Offset: 0x000003C7
		public VisualElement Root { get; }

		// Token: 0x0600000C RID: 12 RVA: 0x000021CF File Offset: 0x000003CF
		public ScienceCostPerHour(VisualElement root, Label scienceCostValue)
		{
			this.Root = root;
			this._scienceCostValue = scienceCostValue;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E5 File Offset: 0x000003E5
		public void UpdateCost(int scienceCost)
		{
			this._scienceCostValue.text = scienceCost.ToString();
		}

		// Token: 0x0400000D RID: 13
		public readonly Label _scienceCostValue;
	}
}
