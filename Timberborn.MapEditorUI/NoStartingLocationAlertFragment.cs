using System;
using Timberborn.AlertPanelSystem;
using Timberborn.CoreUI;
using Timberborn.StartingLocationSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorUI
{
	// Token: 0x02000011 RID: 17
	public class NoStartingLocationAlertFragment : IAlertFragment
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002FE6 File Offset: 0x000011E6
		public NoStartingLocationAlertFragment(AlertPanelRowFactory alertPanelRowFactory, StartingLocationService startingLocationService)
		{
			this._alertPanelRowFactory = alertPanelRowFactory;
			this._startingLocationService = startingLocationService;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FFC File Offset: 0x000011FC
		public void InitializeAlertFragment(VisualElement root)
		{
			this._root = this._alertPanelRowFactory.Create(NoStartingLocationAlertFragment.LabelLocKey, "NoStartingLocation");
			root.Add(this._root);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003025 File Offset: 0x00001225
		public void UpdateAlertFragment()
		{
			this._root.ToggleDisplayStyle(!this._startingLocationService.HasStartingLocation());
		}

		// Token: 0x04000056 RID: 86
		public static readonly string LabelLocKey = "MapEditor.NoStartingLocation";

		// Token: 0x04000057 RID: 87
		public readonly AlertPanelRowFactory _alertPanelRowFactory;

		// Token: 0x04000058 RID: 88
		public readonly StartingLocationService _startingLocationService;

		// Token: 0x04000059 RID: 89
		public VisualElement _root;
	}
}
