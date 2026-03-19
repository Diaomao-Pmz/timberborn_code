using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.RecoveredGoodSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.RecoveredGoodSystemUI
{
	// Token: 0x02000006 RID: 6
	public class RecoveredGoodStackDisintegrationFragment : IEntityPanelFragment
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022D1 File Offset: 0x000004D1
		public RecoveredGoodStackDisintegrationFragment(ILoc loc, VisualElementLoader visualElementLoader)
		{
			this._loc = loc;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E8 File Offset: 0x000004E8
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/RecoveredGoodStackDisintegrationFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000234D File Offset: 0x0000054D
		public void ShowFragment(BaseComponent entity)
		{
			this._recoveredGoodStackDisintegration = entity.GetComponent<RecoveredGoodStackDisintegration>();
			if (this._recoveredGoodStackDisintegration)
			{
				this._root.ToggleDisplayStyle(true);
				this.UpdateFragment();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000237A File Offset: 0x0000057A
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._recoveredGoodStackDisintegration = null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002390 File Offset: 0x00000590
		public void UpdateFragment()
		{
			if (this._recoveredGoodStackDisintegration)
			{
				this._progressBar.SetProgress(this._recoveredGoodStackDisintegration.Progress);
				string param = NumberFormatter.CeilToTenthsPlace((double)this._recoveredGoodStackDisintegration.DaysToDisintegration);
				this._text.text = this._loc.T<string>(RecoveredGoodStackDisintegrationFragment.DisintegrationCountdownLocKey, param);
			}
		}

		// Token: 0x04000014 RID: 20
		public static readonly string DisintegrationCountdownLocKey = "RecoveredGoodStack.DisintegrationCountdown";

		// Token: 0x04000015 RID: 21
		public readonly ILoc _loc;

		// Token: 0x04000016 RID: 22
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000017 RID: 23
		public RecoveredGoodStackDisintegration _recoveredGoodStackDisintegration;

		// Token: 0x04000018 RID: 24
		public VisualElement _root;

		// Token: 0x04000019 RID: 25
		public ProgressBar _progressBar;

		// Token: 0x0400001A RID: 26
		public Label _text;
	}
}
