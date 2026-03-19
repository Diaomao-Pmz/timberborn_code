using System;
using Timberborn.CoreUI;
using Timberborn.Demolishing;
using UnityEngine.UIElements;

namespace Timberborn.DemolishingUI
{
	// Token: 0x0200000A RID: 10
	public class DemolishableScienceRewardLabel
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002865 File Offset: 0x00000A65
		public VisualElement Root { get; }

		// Token: 0x06000029 RID: 41 RVA: 0x0000286D File Offset: 0x00000A6D
		public DemolishableScienceRewardLabel(VisualElement root, Label points)
		{
			this.Root = root;
			this._points = points;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002884 File Offset: 0x00000A84
		public void Show(DemolishableScienceRewardSpec spec)
		{
			bool flag = spec != null;
			if (flag)
			{
				this._points.text = spec.SciencePoints.ToString();
			}
			this.Root.ToggleDisplayStyle(flag);
		}

		// Token: 0x04000026 RID: 38
		public readonly Label _points;
	}
}
