using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DeteriorationSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.DeteriorationSystemUI
{
	// Token: 0x02000008 RID: 8
	public class DeteriorableFragment : IEntityPanelFragment
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000229F File Offset: 0x0000049F
		public DeteriorableFragment(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022B8 File Offset: 0x000004B8
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DeteriorableFragment");
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._durabilityLabel = UQueryExtensions.Q<Label>(this._root, "Durability", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000231B File Offset: 0x0000051B
		public void ShowFragment(BaseComponent entity)
		{
			this._deteriorable = entity.GetComponent<Deteriorable>();
			if (this._deteriorable)
			{
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002342 File Offset: 0x00000542
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._deteriorable = null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002358 File Offset: 0x00000558
		public void UpdateFragment()
		{
			if (this._deteriorable)
			{
				float num = Mathf.Clamp01(this._deteriorable.DeteriorationProgress);
				string str = NumberFormatter.FormatAsPercentFloored((double)num);
				this._progressBar.SetProgress(num);
				this._durabilityLabel.text = this._loc.T(DeteriorableFragment.DurabilityLocKey) + ": " + str;
			}
		}

		// Token: 0x04000012 RID: 18
		public static readonly string DurabilityLocKey = "Bot.Durability";

		// Token: 0x04000013 RID: 19
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000014 RID: 20
		public readonly ILoc _loc;

		// Token: 0x04000015 RID: 21
		public Deteriorable _deteriorable;

		// Token: 0x04000016 RID: 22
		public VisualElement _root;

		// Token: 0x04000017 RID: 23
		public ProgressBar _progressBar;

		// Token: 0x04000018 RID: 24
		public Label _durabilityLabel;
	}
}
