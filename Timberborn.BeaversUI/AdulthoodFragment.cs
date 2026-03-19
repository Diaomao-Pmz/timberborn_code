using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.BeaversUI
{
	// Token: 0x02000007 RID: 7
	public class AdulthoodFragment : IEntityPanelFragment
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
		public AdulthoodFragment(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002208 File Offset: 0x00000408
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/AdulthoodFragment");
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002254 File Offset: 0x00000454
		public void ShowFragment(BaseComponent entity)
		{
			this._child = entity.GetComponent<Child>();
			if (this._child)
			{
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000227B File Offset: 0x0000047B
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._child = null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002290 File Offset: 0x00000490
		public void UpdateFragment()
		{
			if (this._child)
			{
				float progress = Mathf.Clamp01(this._child.GrowthProgress);
				this._progressBar.SetProgress(progress);
			}
		}

		// Token: 0x0400000F RID: 15
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000010 RID: 16
		public Child _child;

		// Token: 0x04000011 RID: 17
		public VisualElement _root;

		// Token: 0x04000012 RID: 18
		public ProgressBar _progressBar;
	}
}
