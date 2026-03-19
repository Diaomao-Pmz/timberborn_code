using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000014 RID: 20
	public class FollowObjectFragment : IEntityPanelFragment
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public FollowObjectFragment(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, AlternateClickableFactory alternateClickableFactory, EntitySelectionService entitySelectionService, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._alternateClickableFactory = alternateClickableFactory;
			this._entitySelectionService = entitySelectionService;
			this._loc = loc;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public VisualElement InitializeFragment()
		{
			this._root = (Button)this._visualElementLoader.LoadVisualElement("Common/EntityPanel/FollowObjectFragment");
			this._tooltipRegistrar.Register(this._root, this._loc.T(FollowObjectFragment.FocusLocKey));
			this._alternateClickable = this._alternateClickableFactory.Create(this._root, new Action(this.SelectAndFollow), new Action(this.UnselectAndFollow));
			return this._root;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C6E File Offset: 0x00001E6E
		public void ShowFragment(BaseComponent entity)
		{
			this._shownEntity = entity;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003C77 File Offset: 0x00001E77
		public void ClearFragment()
		{
			this._shownEntity = null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003C80 File Offset: 0x00001E80
		public void UpdateFragment()
		{
			this._alternateClickable.Update();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003C8D File Offset: 0x00001E8D
		public void SelectAndFollow()
		{
			this._entitySelectionService.SelectAndFollow(this._shownEntity);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public void UnselectAndFollow()
		{
			this._entitySelectionService.UnselectAndFollow(this._shownEntity);
		}

		// Token: 0x04000070 RID: 112
		public static readonly string FocusLocKey = "EntityPanel.Focus";

		// Token: 0x04000071 RID: 113
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000072 RID: 114
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000073 RID: 115
		public readonly AlternateClickableFactory _alternateClickableFactory;

		// Token: 0x04000074 RID: 116
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000075 RID: 117
		public readonly ILoc _loc;

		// Token: 0x04000076 RID: 118
		public Button _root;

		// Token: 0x04000077 RID: 119
		public AlternateClickable _alternateClickable;

		// Token: 0x04000078 RID: 120
		public BaseComponent _shownEntity;
	}
}
