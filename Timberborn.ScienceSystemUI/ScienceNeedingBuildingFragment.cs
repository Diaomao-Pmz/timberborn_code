using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.ScienceSystem;
using Timberborn.TooltipSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.ScienceSystemUI
{
	// Token: 0x0200000A RID: 10
	public class ScienceNeedingBuildingFragment : IEntityPanelFragment
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000243B File Offset: 0x0000063B
		public ScienceNeedingBuildingFragment(ScienceCostPerHourFactory scienceCostPerHourFactory, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._scienceCostPerHourFactory = scienceCostPerHourFactory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002458 File Offset: 0x00000658
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/ScienceNeedingBuildingFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._scienceCostPerHour = this._scienceCostPerHourFactory.Create();
			this._progressBar.Add(this._scienceCostPerHour.Root);
			this._tooltipRegistrar.Register(this._root, new Func<string>(this.GetTooltipText));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024EA File Offset: 0x000006EA
		public void ShowFragment(BaseComponent entity)
		{
			this._scienceNeedingBuilding = entity.GetComponent<ScienceNeedingBuilding>();
			if (this._scienceNeedingBuilding)
			{
				this._scienceNeedingBuildingDescriber = entity.GetComponent<ScienceNeedingBuildingDescriber>();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002514 File Offset: 0x00000714
		public void UpdateFragment()
		{
			if (this._scienceNeedingBuilding && this._scienceNeedingBuilding.Enabled)
			{
				float progress = Mathf.Clamp01(this._scienceNeedingBuilding.ScienceStoredPercentage);
				this._scienceCostPerHour.UpdateCost(this._scienceNeedingBuilding.ScienceUsedPerHour);
				this._progressBar.SetProgress(progress);
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002587 File Offset: 0x00000787
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._scienceNeedingBuilding = null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000259C File Offset: 0x0000079C
		public string GetTooltipText()
		{
			return this._scienceNeedingBuildingDescriber.DescribeScienceUsage();
		}

		// Token: 0x0400001E RID: 30
		public readonly ScienceCostPerHourFactory _scienceCostPerHourFactory;

		// Token: 0x0400001F RID: 31
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000020 RID: 32
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000021 RID: 33
		public ScienceNeedingBuilding _scienceNeedingBuilding;

		// Token: 0x04000022 RID: 34
		public ScienceNeedingBuildingDescriber _scienceNeedingBuildingDescriber;

		// Token: 0x04000023 RID: 35
		public VisualElement _root;

		// Token: 0x04000024 RID: 36
		public ProgressBar _progressBar;

		// Token: 0x04000025 RID: 37
		public ScienceCostPerHour _scienceCostPerHour;
	}
}
