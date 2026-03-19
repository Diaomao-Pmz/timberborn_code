using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsMigration;
using Timberborn.Localization;
using Timberborn.Population;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000014 RID: 20
	public class ManualMigrationPopulationRowFactory
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002C75 File Offset: 0x00000E75
		public ManualMigrationPopulationRowFactory(ILoc loc, ManualMigrationBlocker manualMigrationBlocker, PopulationService populationService, PopulationDistributorRetriever populationDistributorRetriever, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._loc = loc;
			this._manualMigrationBlocker = manualMigrationBlocker;
			this._populationService = populationService;
			this._populationDistributorRetriever = populationDistributorRetriever;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CAA File Offset: 0x00000EAA
		public List<ManualMigrationPopulationRow> CreateLeftRows()
		{
			return this.CreatePopulationRows("Game/BatchControl/ManualMigrationPopulationRowLeft");
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002CB7 File Offset: 0x00000EB7
		public List<ManualMigrationPopulationRow> CreateRightRows()
		{
			return this.CreatePopulationRows("Game/BatchControl/ManualMigrationPopulationRowRight");
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public List<ManualMigrationPopulationRow> CreatePopulationRows(string rowTemplate)
		{
			List<ManualMigrationPopulationRow> list = new List<ManualMigrationPopulationRow>();
			list.Add(this.CreatePopulationRow<AdultsDistributorTemplate>(rowTemplate, ManualMigrationPopulationRowFactory.AdultIcon, () => true));
			list.Add(this.CreatePopulationRow<ChildrenDistributorTemplate>(rowTemplate, ManualMigrationPopulationRowFactory.ChildIcon, () => true));
			list.Add(this.CreatePopulationRow<ContaminatedDistributorTemplate>(rowTemplate, ManualMigrationPopulationRowFactory.ContaminatedIcon, () => this._populationService.IsAnyoneContaminated));
			list.Add(this.CreatePopulationRow<BotsDistributorTemplate>(rowTemplate, ManualMigrationPopulationRowFactory.BotIcon, () => this._populationService.BotCreated));
			return list;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D74 File Offset: 0x00000F74
		public ManualMigrationPopulationRow CreatePopulationRow<T>(string rowTemplate, string icon, Func<bool> visibilityGetter) where T : BaseComponent, IDistributorTemplate
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlRow");
			VisualElement visualElement2 = this._visualElementLoader.LoadVisualElement(rowTemplate);
			visualElement.Add(visualElement2);
			UQueryExtensions.Q<VisualElement>(visualElement2, "PopulationIcon", null).AddToClassList(icon);
			UQueryExtensions.Q<Label>(visualElement2, "MinimumLabel", null).text = this._loc.T(ManualMigrationPopulationRowFactory.MinimumLocKey) + ":";
			ManualMigrationPopulationRow manualMigrationPopulationRow = new ManualMigrationPopulationRow(this._manualMigrationBlocker, this._tooltipRegistrar, visualElement, new Func<DistrictCenter, PopulationDistributor>(this._populationDistributorRetriever.GetPopulationDistributor<T>), visibilityGetter);
			manualMigrationPopulationRow.Initialize();
			return manualMigrationPopulationRow;
		}

		// Token: 0x0400003D RID: 61
		public static readonly string AdultIcon = "population-counter__icon--adult";

		// Token: 0x0400003E RID: 62
		public static readonly string ChildIcon = "population-counter__icon--child";

		// Token: 0x0400003F RID: 63
		public static readonly string BotIcon = "population-counter__icon--bot";

		// Token: 0x04000040 RID: 64
		public static readonly string ContaminatedIcon = "population-counter__icon--contamination";

		// Token: 0x04000041 RID: 65
		public static readonly string MinimumLocKey = "Migration.Minimum";

		// Token: 0x04000042 RID: 66
		public readonly ILoc _loc;

		// Token: 0x04000043 RID: 67
		public readonly ManualMigrationBlocker _manualMigrationBlocker;

		// Token: 0x04000044 RID: 68
		public readonly PopulationService _populationService;

		// Token: 0x04000045 RID: 69
		public readonly PopulationDistributorRetriever _populationDistributorRetriever;

		// Token: 0x04000046 RID: 70
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000047 RID: 71
		public readonly VisualElementLoader _visualElementLoader;
	}
}
