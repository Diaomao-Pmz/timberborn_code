using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DwellingSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.TooltipSystem;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.BeaversUI
{
	// Token: 0x02000008 RID: 8
	public class BeaverBuildingsBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022C7 File Offset: 0x000004C7
		public VisualElement Root { get; }

		// Token: 0x06000012 RID: 18 RVA: 0x000022D0 File Offset: 0x000004D0
		public BeaverBuildingsBatchControlRowItem(VisualElement root, ITooltipRegistrar tooltipRegistrar, ILoc loc, EntitySelectionService entitySelectionService, Dweller dweller, Button dwellerButton, Image dwellerImage, Worker worker, Button workerButton, Image workerImage)
		{
			this.Root = root;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
			this._entitySelectionService = entitySelectionService;
			this._dweller = dweller;
			this._dwellerButton = dwellerButton;
			this._dwellerImage = dwellerImage;
			this._worker = worker;
			this._workerButton = workerButton;
			this._workerImage = workerImage;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002330 File Offset: 0x00000530
		public void UpdateRowItem()
		{
			this.UpdateHome();
			this.UpdateWorkplace();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002340 File Offset: 0x00000540
		public void Initialize()
		{
			this._dwellerButton.ToggleDisplayStyle(this._dweller);
			this._workerButton.ToggleDisplayStyle(this._worker);
			if (this._dweller)
			{
				this._tooltipRegistrar.Register(this._dwellerButton, new Func<string>(this.GetDwellerButtonTooltip));
				this._dwellerButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SelectHome), 0);
			}
			if (this._worker)
			{
				this._tooltipRegistrar.Register(this._workerButton, new Func<string>(this.GetWorkerButtonTooltip));
				this._workerButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SelectWorkplace), 0);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002400 File Offset: 0x00000600
		public void UpdateHome()
		{
			if (this._dweller)
			{
				this._dwellerButton.SetEnabled(this._dweller.HasHome);
				this._dwellerImage.EnableInClassList(BeaverBuildingsBatchControlRowItem.HideDefaultClass, this._dweller.HasHome);
				this._dwellerImage.sprite = (this._dweller.HasHome ? this._dweller.Home.GetComponent<LabeledEntity>().Image : null);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000247C File Offset: 0x0000067C
		public void UpdateWorkplace()
		{
			if (this._worker)
			{
				this._workerButton.SetEnabled(this._worker.Employed);
				this._workerImage.EnableInClassList(BeaverBuildingsBatchControlRowItem.HideDefaultClass, this._worker.Employed);
				this._workerImage.sprite = (this._worker.Employed ? this._worker.Workplace.GetComponent<LabeledEntity>().Image : null);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024F8 File Offset: 0x000006F8
		public string GetDwellerButtonTooltip()
		{
			if (!this._dweller)
			{
				return null;
			}
			if (!this._dweller.HasHome)
			{
				return this._loc.T(BeaverBuildingsBatchControlRowItem.HomelessLocKey);
			}
			return this._loc.T<string>(BeaverBuildingsBatchControlRowItem.HouseLocKey, this.GetDisplayName(this._dweller.Home));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002554 File Offset: 0x00000754
		public string GetWorkerButtonTooltip()
		{
			if (!this._worker)
			{
				return null;
			}
			if (!this._worker.Employed)
			{
				return this._loc.T(BeaverBuildingsBatchControlRowItem.UnemployedLocKey);
			}
			return this._loc.T<string>(BeaverBuildingsBatchControlRowItem.WorkplaceLocKey, this.GetDisplayName(this._worker.Workplace));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025B0 File Offset: 0x000007B0
		public void SelectHome(ClickEvent evt)
		{
			if (this._dweller)
			{
				Dwelling home = this._dweller.Home;
				if (home != null)
				{
					this._entitySelectionService.SelectAndFollow(home);
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025E8 File Offset: 0x000007E8
		public void SelectWorkplace(ClickEvent evt)
		{
			if (this._worker)
			{
				Workplace workplace = this._worker.Workplace;
				if (workplace != null)
				{
					this._entitySelectionService.SelectAndFollow(workplace);
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000261D File Offset: 0x0000081D
		public string GetDisplayName(BaseComponent building)
		{
			return this._loc.T(building.GetComponent<LabeledEntitySpec>().DisplayNameLocKey);
		}

		// Token: 0x04000013 RID: 19
		public static readonly string HideDefaultClass = "beaver-buildings-batch-control-row-item__icon--empty";

		// Token: 0x04000014 RID: 20
		public static readonly string HomelessLocKey = "Beaver.Homeless";

		// Token: 0x04000015 RID: 21
		public static readonly string HouseLocKey = "Beaver.House";

		// Token: 0x04000016 RID: 22
		public static readonly string WorkplaceLocKey = "Beaver.Workplace";

		// Token: 0x04000017 RID: 23
		public static readonly string UnemployedLocKey = "Beaver.Unemployed";

		// Token: 0x04000019 RID: 25
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400001A RID: 26
		public readonly ILoc _loc;

		// Token: 0x0400001B RID: 27
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400001C RID: 28
		public readonly Dweller _dweller;

		// Token: 0x0400001D RID: 29
		public readonly Button _dwellerButton;

		// Token: 0x0400001E RID: 30
		public readonly Image _dwellerImage;

		// Token: 0x0400001F RID: 31
		public readonly Worker _worker;

		// Token: 0x04000020 RID: 32
		public readonly Button _workerButton;

		// Token: 0x04000021 RID: 33
		public readonly Image _workerImage;
	}
}
