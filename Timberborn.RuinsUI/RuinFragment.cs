using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Goods;
using Timberborn.Ruins;
using UnityEngine.UIElements;

namespace Timberborn.RuinsUI
{
	// Token: 0x02000004 RID: 4
	public class RuinFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public RuinFragment(VisualElementLoader visualElementLoader, IGoodService goodService)
		{
			this._visualElementLoader = visualElementLoader;
			this._goodService = goodService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/RuinFragment");
			this._goodRemaining = UQueryExtensions.Q<Label>(this._root, "GoodRemaining", null);
			this._goodName = UQueryExtensions.Q<Label>(this._root, "GoodName", null);
			this._goodIcon = UQueryExtensions.Q<Image>(this._root, "GoodIcon", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002150 File Offset: 0x00000350
		public void ShowFragment(BaseComponent entity)
		{
			this._ruin = entity.GetComponent<Ruin>();
			if (this._ruin)
			{
				string id = this._ruin.YielderSpec.Yield.Id;
				GoodSpec good = this._goodService.GetGood(id);
				this._goodName.text = good.PluralDisplayName.Value;
				this._goodIcon.sprite = good.IconSmall.Value;
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021D1 File Offset: 0x000003D1
		public void ClearFragment()
		{
			this._ruin = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021E8 File Offset: 0x000003E8
		public void UpdateFragment()
		{
			if (this._ruin)
			{
				this._goodRemaining.text = this._ruin.Yielder.Yield.Amount.ToString();
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly IGoodService _goodService;

		// Token: 0x04000008 RID: 8
		public Ruin _ruin;

		// Token: 0x04000009 RID: 9
		public VisualElement _root;

		// Token: 0x0400000A RID: 10
		public Label _goodRemaining;

		// Token: 0x0400000B RID: 11
		public Label _goodName;

		// Token: 0x0400000C RID: 12
		public Image _goodIcon;
	}
}
