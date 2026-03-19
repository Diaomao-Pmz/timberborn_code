using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.WindSystem;
using UnityEngine.UIElements;

namespace Timberborn.WindSystemUI
{
	// Token: 0x02000005 RID: 5
	public class WeathervaneFragment : IEntityPanelFragment
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000219A File Offset: 0x0000039A
		public WeathervaneFragment(VisualElementLoader visualElementLoader, ILoc loc, WindService windService)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._windService = windService;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021B8 File Offset: 0x000003B8
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/WeathervaneFragment");
			this._root.ToggleDisplayStyle(false);
			this._windStrengthLabel = UQueryExtensions.Q<Label>(this._root, "WindStrengthLabel", null);
			return this._root;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002204 File Offset: 0x00000404
		public void ShowFragment(BaseComponent entity)
		{
			this._weathervaneSpec = entity.GetComponent<WeathervaneSpec>();
			if (this._weathervaneSpec != null)
			{
				this._blockObject = entity.GetComponent<BlockObject>();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000222C File Offset: 0x0000042C
		public void ClearFragment()
		{
			this._weathervaneSpec = null;
			this._blockObject = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002248 File Offset: 0x00000448
		public void UpdateFragment()
		{
			if (this._weathervaneSpec != null && this._blockObject && this._blockObject.IsFinished)
			{
				float num = this._windService.WindStrength * 100f;
				this._root.ToggleDisplayStyle(true);
				this._windStrengthLabel.text = this._loc.T<string>(WeathervaneFragment.WindStrengthLocKey, num.ToString("0"));
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string WindStrengthLocKey = "Building.Weathervane.WindStrength";

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly ILoc _loc;

		// Token: 0x0400000B RID: 11
		public readonly WindService _windService;

		// Token: 0x0400000C RID: 12
		public Label _windStrengthLabel;

		// Token: 0x0400000D RID: 13
		public VisualElement _root;

		// Token: 0x0400000E RID: 14
		public WeathervaneSpec _weathervaneSpec;

		// Token: 0x0400000F RID: 15
		public BlockObject _blockObject;
	}
}
