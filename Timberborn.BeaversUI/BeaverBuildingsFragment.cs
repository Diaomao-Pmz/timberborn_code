using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Buildings;
using Timberborn.CoreUI;
using Timberborn.DwellingSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.BeaversUI
{
	// Token: 0x0200000A RID: 10
	public class BeaverBuildingsFragment : IEntityPanelFragment
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002739 File Offset: 0x00000939
		public BeaverBuildingsFragment(VisualElementLoader visualElementLoader, ILoc loc, BeaverBuildingViewFactory beaverBuildingViewFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._beaverBuildingViewFactory = beaverBuildingViewFactory;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002758 File Offset: 0x00000958
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/BeaverBuildingsFragment");
			this._home = this._beaverBuildingViewFactory.Create(UQueryExtensions.Q<Button>(this._root, "Home", null));
			this._workplace = this._beaverBuildingViewFactory.Create(UQueryExtensions.Q<Button>(this._root, "Workplace", null));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027D4 File Offset: 0x000009D4
		public void ShowFragment(BaseComponent entity)
		{
			this._dweller = entity.GetComponent<Dweller>();
			this._worker = entity.GetComponent<Worker>();
			this._contaminable = entity.GetComponent<Contaminable>();
			this._root.ToggleDisplayStyle(this._dweller || this.IsWorkplaceVisible);
			this._home.Root.ToggleDisplayStyle(this._dweller);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002841 File Offset: 0x00000A41
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._dweller = null;
			this._worker = null;
			this._contaminable = null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002864 File Offset: 0x00000A64
		public void UpdateFragment()
		{
			this.UpdateHomePanel();
			this.UpdateWorkplacePanel();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002874 File Offset: 0x00000A74
		public bool IsWorkplaceVisible
		{
			get
			{
				if (this._worker)
				{
					Contaminable contaminable = this._contaminable;
					return contaminable == null || !contaminable.IsContaminated;
				}
				return false;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028A8 File Offset: 0x00000AA8
		public void UpdateHomePanel()
		{
			if (this._dweller)
			{
				if (this._dweller.HasHome)
				{
					Building component = this._dweller.Home.GetComponent<Building>();
					this._home.SetBuilding(component, this._loc.T<string>(BeaverBuildingsFragment.HouseLocKey, this.GetDisplayName(component)));
					return;
				}
				this._home.SetDescriptionOnly(this._loc.T(BeaverBuildingsFragment.HomelessLocKey));
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002920 File Offset: 0x00000B20
		public void UpdateWorkplacePanel()
		{
			if (!this.IsWorkplaceVisible)
			{
				this._workplace.Root.ToggleDisplayStyle(false);
				return;
			}
			this._workplace.Root.ToggleDisplayStyle(true);
			if (this._worker.Workplace)
			{
				Building component = this._worker.Workplace.GetComponent<Building>();
				this._workplace.SetBuilding(component, this._loc.T<string>(BeaverBuildingsFragment.WorkplaceLocKey, this.GetDisplayName(component)));
				return;
			}
			this._workplace.SetDescriptionOnly(this._loc.T(BeaverBuildingsFragment.UnemployedLocKey));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029BA File Offset: 0x00000BBA
		public string GetDisplayName(Building building)
		{
			return this._loc.T(building.GetComponent<LabeledEntitySpec>().DisplayNameLocKey);
		}

		// Token: 0x04000026 RID: 38
		public static readonly string HomelessLocKey = "Beaver.Homeless";

		// Token: 0x04000027 RID: 39
		public static readonly string HouseLocKey = "Beaver.House";

		// Token: 0x04000028 RID: 40
		public static readonly string WorkplaceLocKey = "Beaver.Workplace";

		// Token: 0x04000029 RID: 41
		public static readonly string UnemployedLocKey = "Beaver.Unemployed";

		// Token: 0x0400002A RID: 42
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002B RID: 43
		public readonly ILoc _loc;

		// Token: 0x0400002C RID: 44
		public readonly BeaverBuildingViewFactory _beaverBuildingViewFactory;

		// Token: 0x0400002D RID: 45
		public VisualElement _root;

		// Token: 0x0400002E RID: 46
		public BeaverBuildingView _home;

		// Token: 0x0400002F RID: 47
		public BeaverBuildingView _workplace;

		// Token: 0x04000030 RID: 48
		public Dweller _dweller;

		// Token: 0x04000031 RID: 49
		public Worker _worker;

		// Token: 0x04000032 RID: 50
		public Contaminable _contaminable;
	}
}
