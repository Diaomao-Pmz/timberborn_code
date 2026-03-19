using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorWaterSourceSystemUI
{
	// Token: 0x02000006 RID: 6
	internal class WaterSourceFlowPreviewFragment : IEntityPanelFragment
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002237 File Offset: 0x00000437
		public WaterSourceFlowPreviewFragment(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002250 File Offset: 0x00000450
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/EntityPanel/WaterSourceFlowPreviewFragment");
			this._button = this._root.Q("Button", null);
			this._button.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleForced), TrickleDown.NoTrickleDown);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022B4 File Offset: 0x000004B4
		public void ShowFragment(BaseComponent entity)
		{
			WaterSourceFlowPreview component = entity.GetComponent<WaterSourceFlowPreview>();
			if (component != null)
			{
				this._waterSourceFlowPreview = component;
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022DE File Offset: 0x000004DE
		public void ClearFragment()
		{
			this._waterSourceFlowPreview = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022F4 File Offset: 0x000004F4
		public void UpdateFragment()
		{
			if (this._waterSourceFlowPreview)
			{
				this._root.ToggleDisplayStyle(this._waterSourceFlowPreview.CanEnable);
				this._button.text = (this._waterSourceFlowPreview.IsEnabled ? this._loc.T(WaterSourceFlowPreviewFragment.DisableFlowLocKey) : this._loc.T(WaterSourceFlowPreviewFragment.EnableFlowLocKey));
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000235E File Offset: 0x0000055E
		private void ToggleForced(ClickEvent evt)
		{
			if (this._waterSourceFlowPreview.IsEnabled)
			{
				this._waterSourceFlowPreview.DisableFlowPreview();
				return;
			}
			this._waterSourceFlowPreview.EnableFlowPreview();
		}

		// Token: 0x04000006 RID: 6
		private static readonly string EnableFlowLocKey = "MapEditor.FlowPreview.StartTest";

		// Token: 0x04000007 RID: 7
		private static readonly string DisableFlowLocKey = "MapEditor.FlowPreview.StopTest";

		// Token: 0x04000008 RID: 8
		private readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		private readonly ILoc _loc;

		// Token: 0x0400000A RID: 10
		private WaterSourceFlowPreview _waterSourceFlowPreview;

		// Token: 0x0400000B RID: 11
		private VisualElement _root;

		// Token: 0x0400000C RID: 12
		private Button _button;
	}
}
