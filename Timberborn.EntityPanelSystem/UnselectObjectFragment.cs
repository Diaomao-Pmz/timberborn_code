using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x0200001C RID: 28
	public class UnselectObjectFragment : IEntityPanelFragment
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003EBC File Offset: 0x000020BC
		public UnselectObjectFragment(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, EntitySelectionService entitySelectionService, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._entitySelectionService = entitySelectionService;
			this._loc = loc;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003EE4 File Offset: 0x000020E4
		public VisualElement InitializeFragment()
		{
			this._root = (Button)this._visualElementLoader.LoadVisualElement("Common/EntityPanel/UnselectObjectFragment");
			this._root.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClicked), 0);
			this._tooltipRegistrar.RegisterWithKeyBinding(this._root, this._loc.T(UnselectObjectFragment.CloseLocKey), UnselectObjectFragment.CloseKeyBindingId);
			return this._root;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003F50 File Offset: 0x00002150
		public void ShowFragment(BaseComponent entity)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003F50 File Offset: 0x00002150
		public void ClearFragment()
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003F50 File Offset: 0x00002150
		public void UpdateFragment()
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003F52 File Offset: 0x00002152
		public void OnClicked(ClickEvent clickEvent)
		{
			this._entitySelectionService.Unselect();
		}

		// Token: 0x0400007F RID: 127
		public static readonly string CloseLocKey = "EntityPanel.Close";

		// Token: 0x04000080 RID: 128
		public static readonly string CloseKeyBindingId = "Cancel";

		// Token: 0x04000081 RID: 129
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000082 RID: 130
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000083 RID: 131
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000084 RID: 132
		public readonly ILoc _loc;

		// Token: 0x04000085 RID: 133
		public Button _root;
	}
}
