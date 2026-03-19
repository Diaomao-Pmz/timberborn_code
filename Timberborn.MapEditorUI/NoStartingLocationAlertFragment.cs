using System;
using Timberborn.AlertPanelSystem;
using Timberborn.CoreUI;
using Timberborn.StartingLocationSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorUI
{
	// Token: 0x0200000A RID: 10
	internal class NoStartingLocationAlertFragment : IAlertFragment
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002A50 File Offset: 0x00000C50
		public NoStartingLocationAlertFragment(AlertPanelRowFactory alertPanelRowFactory, StartingLocationService startingLocationService)
		{
			this._alertPanelRowFactory = alertPanelRowFactory;
			this._startingLocationService = startingLocationService;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A66 File Offset: 0x00000C66
		public void InitializeAlertFragment(VisualElement root)
		{
			this._root = this._alertPanelRowFactory.Create(NoStartingLocationAlertFragment.LabelLocKey, "NoStartingLocation");
			root.Add(this._root);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A8F File Offset: 0x00000C8F
		public void UpdateAlertFragment()
		{
			this._root.ToggleDisplayStyle(!this._startingLocationService.HasStartingLocation());
		}

		// Token: 0x04000040 RID: 64
		private static readonly string LabelLocKey = "MapEditor.NoStartingLocation";

		// Token: 0x04000041 RID: 65
		private readonly AlertPanelRowFactory _alertPanelRowFactory;

		// Token: 0x04000042 RID: 66
		private readonly StartingLocationService _startingLocationService;

		// Token: 0x04000043 RID: 67
		private VisualElement _root;
	}
}
