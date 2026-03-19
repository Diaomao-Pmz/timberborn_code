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
	// Token: 0x02000009 RID: 9
	internal class NonCompatibleMapAlertFragment : IAlertFragment
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002961 File Offset: 0x00000B61
		public NonCompatibleMapAlertFragment(ILoc loc, AlertPanelRowFactory alertPanelRowFactory, MapPersistenceController mapPersistenceController, ITooltipRegistrar tooltipRegistrar, EventBus eventBus)
		{
			this._loc = loc;
			this._alertPanelRowFactory = alertPanelRowFactory;
			this._mapPersistenceController = mapPersistenceController;
			this._tooltipRegistrar = tooltipRegistrar;
			this._eventBus = eventBus;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002990 File Offset: 0x00000B90
		public void InitializeAlertFragment(VisualElement root)
		{
			this._root = this._alertPanelRowFactory.Create(NonCompatibleMapAlertFragment.LabelLocKey, "NonCompatibleVersion");
			this._tooltipRegistrar.Register(this._root, new Func<string>(this.GetTooltip));
			this._eventBus.Register(this);
			this.UpdateVisibility();
			root.Add(this._root);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029F3 File Offset: 0x00000BF3
		public void UpdateAlertFragment()
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029F5 File Offset: 0x00000BF5
		[OnEvent]
		public void OnMapSaved(MapSavedEvent mapSavedEvent)
		{
			this.UpdateVisibility();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029FD File Offset: 0x00000BFD
		private string GetTooltip()
		{
			return this._loc.T<Version, Version>(NonCompatibleMapAlertFragment.TooltipLocKey, this._mapPersistenceController.CurrentMapVersion, GameVersions.CurrentVersion);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A1F File Offset: 0x00000C1F
		private void UpdateVisibility()
		{
			this._root.ToggleDisplayStyle(!this._mapPersistenceController.IsCurrentMapCompatible);
		}

		// Token: 0x04000038 RID: 56
		private static readonly string LabelLocKey = "MapEditor.NonCompatibleMapVersion";

		// Token: 0x04000039 RID: 57
		private static readonly string TooltipLocKey = "MapEditor.NonCompatibleMapVersion.Tooltip";

		// Token: 0x0400003A RID: 58
		private readonly ILoc _loc;

		// Token: 0x0400003B RID: 59
		private readonly AlertPanelRowFactory _alertPanelRowFactory;

		// Token: 0x0400003C RID: 60
		private readonly MapPersistenceController _mapPersistenceController;

		// Token: 0x0400003D RID: 61
		private readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400003E RID: 62
		private readonly EventBus _eventBus;

		// Token: 0x0400003F RID: 63
		private VisualElement _root;
	}
}
