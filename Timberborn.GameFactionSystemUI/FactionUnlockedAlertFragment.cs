using System;
using Timberborn.AlertPanelSystem;
using Timberborn.CoreUI;
using Timberborn.FactionSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.WellbeingUI;
using UnityEngine.UIElements;

namespace Timberborn.GameFactionSystemUI
{
	// Token: 0x02000004 RID: 4
	public class FactionUnlockedAlertFragment : IAlertFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public FactionUnlockedAlertFragment(AlertPanelRowFactory alertPanelRowFactory, EventBus eventBus, ILoc loc, PopulationWellbeingBox populationWellbeingBox)
		{
			this._alertPanelRowFactory = alertPanelRowFactory;
			this._eventBus = eventBus;
			this._loc = loc;
			this._populationWellbeingBox = populationWellbeingBox;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public void InitializeAlertFragment(VisualElement root)
		{
			this._root = this._alertPanelRowFactory.CreateClosable("NewFactionUnlocked");
			UQueryExtensions.Q<Button>(this._root, "Button", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClicked), 0);
			this._eventBus.Register(this);
			root.Add(this._root);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002142 File Offset: 0x00000342
		public void UpdateAlertFragment()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002144 File Offset: 0x00000344
		[OnEvent]
		public void OnFactionUnlocked(FactionUnlockedEvent factionUnlockedEvent)
		{
			string value = factionUnlockedEvent.Faction.DisplayName.Value;
			UQueryExtensions.Q<Button>(this._root, "Button", null).text = this._loc.T(FactionUnlockedAlertFragment.NewFactionUnlockedLocKey) + " " + value + "!";
			this._root.ToggleDisplayStyle(true);
			this._unlockedFaction = factionUnlockedEvent.Faction;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021B0 File Offset: 0x000003B0
		public void OnClicked(ClickEvent evt)
		{
			this._root.ToggleDisplayStyle(false);
			this._populationWellbeingBox.ShowUnlockedFaction(this._unlockedFaction);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string NewFactionUnlockedLocKey = "FactionSelection.NewFactionUnlocked";

		// Token: 0x04000007 RID: 7
		public readonly AlertPanelRowFactory _alertPanelRowFactory;

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly ILoc _loc;

		// Token: 0x0400000A RID: 10
		public readonly PopulationWellbeingBox _populationWellbeingBox;

		// Token: 0x0400000B RID: 11
		public VisualElement _root;

		// Token: 0x0400000C RID: 12
		public FactionSpec _unlockedFaction;
	}
}
