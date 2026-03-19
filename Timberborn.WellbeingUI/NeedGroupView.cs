using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.NeedSystem;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200000D RID: 13
	public class NeedGroupView
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002893 File Offset: 0x00000A93
		public VisualElement Root { get; }

		// Token: 0x0600002E RID: 46 RVA: 0x0000289B File Offset: 0x00000A9B
		public NeedGroupView(VisualElement root, VisualElement items)
		{
			this.Root = root;
			this._items = items;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028BC File Offset: 0x00000ABC
		public void AddNeed(NeedView needView)
		{
			this._needViews.Add(needView);
			this._items.Add(needView.Root);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028DC File Offset: 0x00000ADC
		public void Update(NeedManager needManager, bool showControls)
		{
			if (this._items.IsDisplayed())
			{
				bool flag = false;
				foreach (NeedView needView in this._needViews)
				{
					needView.Update(needManager, showControls);
					needView.UpdateVisibility(needManager, showControls);
					flag |= needView.Root.IsDisplayed();
				}
				this.UpdateVisibility(flag);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000295C File Offset: 0x00000B5C
		public void UpdateVisibility()
		{
			this.UpdateVisibility(this._needViews.Count > 0);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002972 File Offset: 0x00000B72
		public void ClearNeedViews()
		{
			this._items.Clear();
			this._needViews.Clear();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000298A File Offset: 0x00000B8A
		public void UpdateVisibility(bool visible)
		{
			this.Root.ToggleDisplayStyle(visible);
		}

		// Token: 0x04000037 RID: 55
		public readonly List<NeedView> _needViews = new List<NeedView>();

		// Token: 0x04000038 RID: 56
		public readonly VisualElement _items;
	}
}
