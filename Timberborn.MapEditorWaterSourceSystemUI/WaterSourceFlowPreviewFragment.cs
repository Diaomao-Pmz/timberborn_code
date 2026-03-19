using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorWaterSourceSystemUI
{
	// Token: 0x02000008 RID: 8
	public class WaterSourceFlowPreviewFragment : IEntityPanelFragment
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000225F File Offset: 0x0000045F
		public WaterSourceFlowPreviewFragment(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002278 File Offset: 0x00000478
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/EntityPanel/WaterSourceFlowPreviewFragment");
			this._button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			this._button.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleForced), 0);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022DC File Offset: 0x000004DC
		public void ShowFragment(BaseComponent entity)
		{
			WaterSourceFlowPreview component = entity.GetComponent<WaterSourceFlowPreview>();
			if (component != null)
			{
				this._waterSourceFlowPreview = component;
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002306 File Offset: 0x00000506
		public void ClearFragment()
		{
			this._waterSourceFlowPreview = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000231C File Offset: 0x0000051C
		public void UpdateFragment()
		{
			if (this._waterSourceFlowPreview)
			{
				this._root.ToggleDisplayStyle(this._waterSourceFlowPreview.CanEnable);
				this._button.text = (this._waterSourceFlowPreview.IsEnabled ? this._loc.T(WaterSourceFlowPreviewFragment.DisableFlowLocKey) : this._loc.T(WaterSourceFlowPreviewFragment.EnableFlowLocKey));
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002386 File Offset: 0x00000586
		public void ToggleForced(ClickEvent evt)
		{
			if (this._waterSourceFlowPreview.IsEnabled)
			{
				this._waterSourceFlowPreview.DisableFlowPreview();
				return;
			}
			this._waterSourceFlowPreview.EnableFlowPreview();
		}

		// Token: 0x0400000C RID: 12
		public static readonly string EnableFlowLocKey = "MapEditor.FlowPreview.StartTest";

		// Token: 0x0400000D RID: 13
		public static readonly string DisableFlowLocKey = "MapEditor.FlowPreview.StopTest";

		// Token: 0x0400000E RID: 14
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000F RID: 15
		public readonly ILoc _loc;

		// Token: 0x04000010 RID: 16
		public WaterSourceFlowPreview _waterSourceFlowPreview;

		// Token: 0x04000011 RID: 17
		public VisualElement _root;

		// Token: 0x04000012 RID: 18
		public Button _button;
	}
}
