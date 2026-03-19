using System;
using Timberborn.AlertPanelSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MapEditorPersistenceUI;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.Versioning;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorUI
{
	// Token: 0x02000010 RID: 16
	public class NonCompatibleMapAlertFragment : IAlertFragment
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002EF9 File Offset: 0x000010F9
		public NonCompatibleMapAlertFragment(ILoc loc, AlertPanelRowFactory alertPanelRowFactory, MapPersistenceController mapPersistenceController, ITooltipRegistrar tooltipRegistrar, EventBus eventBus)
		{
			this._loc = loc;
			this._alertPanelRowFactory = alertPanelRowFactory;
			this._mapPersistenceController = mapPersistenceController;
			this._tooltipRegistrar = tooltipRegistrar;
			this._eventBus = eventBus;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F28 File Offset: 0x00001128
		public void InitializeAlertFragment(VisualElement root)
		{
			this._root = this._alertPanelRowFactory.Create(NonCompatibleMapAlertFragment.LabelLocKey, "NonCompatibleVersion");
			this._tooltipRegistrar.Register(this._root, new Func<string>(this.GetTooltip));
			this._eventBus.Register(this);
			this.UpdateVisibility();
			root.Add(this._root);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B37 File Offset: 0x00000D37
		public void UpdateAlertFragment()
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F8B File Offset: 0x0000118B
		[OnEvent]
		public void OnMapSaved(MapSavedEvent mapSavedEvent)
		{
			this.UpdateVisibility();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F93 File Offset: 0x00001193
		public string GetTooltip()
		{
			return this._loc.T<Version, Version>(NonCompatibleMapAlertFragment.TooltipLocKey, this._mapPersistenceController.CurrentMapVersion, GameVersions.CurrentVersion);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002FB5 File Offset: 0x000011B5
		public void UpdateVisibility()
		{
			this._root.ToggleDisplayStyle(!this._mapPersistenceController.IsCurrentMapCompatible);
		}

		// Token: 0x0400004E RID: 78
		public static readonly string LabelLocKey = "MapEditor.NonCompatibleMapVersion";

		// Token: 0x0400004F RID: 79
		public static readonly string TooltipLocKey = "MapEditor.NonCompatibleMapVersion.Tooltip";

		// Token: 0x04000050 RID: 80
		public readonly ILoc _loc;

		// Token: 0x04000051 RID: 81
		public readonly AlertPanelRowFactory _alertPanelRowFactory;

		// Token: 0x04000052 RID: 82
		public readonly MapPersistenceController _mapPersistenceController;

		// Token: 0x04000053 RID: 83
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000054 RID: 84
		public readonly EventBus _eventBus;

		// Token: 0x04000055 RID: 85
		public VisualElement _root;
	}
}
