using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Illumination;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.IlluminationUI
{
	// Token: 0x02000009 RID: 9
	public class CustomizeIlluminationFragment : IEntityPanelFragment
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000244E File Offset: 0x0000064E
		public CustomizeIlluminationFragment(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000246C File Offset: 0x0000066C
		public VisualElement InitializeFragment()
		{
			this._root = (Button)this._visualElementLoader.LoadVisualElement("Common/EntityPanel/CustomizeIlluminationFragment");
			this._root.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClicked), 0);
			this._tooltipRegistrar.Register(this._root, this._loc.T(CustomizeIlluminationFragment.CustomizeLocKey));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024DF File Offset: 0x000006DF
		public void ShowFragment(BaseComponent entity)
		{
			this._customizableIlluminator = entity.GetComponent<CustomizableIlluminator>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024ED File Offset: 0x000006ED
		public void ClearFragment()
		{
			this._customizableIlluminator = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002502 File Offset: 0x00000702
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._customizableIlluminator && !this._customizableIlluminator.IsLocked);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000252D File Offset: 0x0000072D
		public void OnClicked(ClickEvent evt)
		{
			this._customizableIlluminator.SetIsCustomized(!this._customizableIlluminator.IsCustomized);
		}

		// Token: 0x04000011 RID: 17
		public static readonly string CustomizeLocKey = "EntityPanel.Customize";

		// Token: 0x04000012 RID: 18
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000013 RID: 19
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000014 RID: 20
		public readonly ILoc _loc;

		// Token: 0x04000015 RID: 21
		public Button _root;

		// Token: 0x04000016 RID: 22
		public CustomizableIlluminator _customizableIlluminator;
	}
}
