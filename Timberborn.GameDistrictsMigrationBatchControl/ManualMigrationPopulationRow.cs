using System;
using Timberborn.CoreUI;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsMigration;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000013 RID: 19
	public class ManualMigrationPopulationRow
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000029C4 File Offset: 0x00000BC4
		public VisualElement Root { get; }

		// Token: 0x0600003E RID: 62 RVA: 0x000029CC File Offset: 0x00000BCC
		public ManualMigrationPopulationRow(ManualMigrationBlocker manualMigrationBlocker, ITooltipRegistrar tooltipRegistrar, VisualElement root, Func<DistrictCenter, PopulationDistributor> populationDistributorGetter, Func<bool> visibilityGetter)
		{
			this._manualMigrationBlocker = manualMigrationBlocker;
			this._tooltipRegistrar = tooltipRegistrar;
			this.Root = root;
			this._populationDistributorGetter = populationDistributorGetter;
			this._visibilityGetter = visibilityGetter;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029F9 File Offset: 0x00000BF9
		public void Initialize()
		{
			this._currentPopulationLabel = UQueryExtensions.Q<Label>(this.Root, "CurrentPopulation", null);
			this._currentMinimumLabel = UQueryExtensions.Q<Label>(this.Root, "CurrentMinimum", null);
			this.SetupButtons();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A2F File Offset: 0x00000C2F
		public void SetDistricts(DistrictCenter source, DistrictCenter target)
		{
			this._populationDistributor = this._populationDistributorGetter(source);
			this._target = target;
			this.UpdateRow();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A50 File Offset: 0x00000C50
		public void UpdateRow()
		{
			bool flag = this._visibilityGetter();
			this.Root.ToggleDisplayStyle(flag);
			if (flag)
			{
				TextElement currentPopulationLabel = this._currentPopulationLabel;
				int num = this._populationDistributor.Current;
				currentPopulationLabel.text = num.ToString();
				this._currentMinimumLabel.text = this._populationDistributor.Minimum.ToString();
				this.SetButtonsEnabledState();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002ABC File Offset: 0x00000CBC
		public void SetupButtons()
		{
			this._buttonOne = UQueryExtensions.Q<Button>(this.Root, "ButtonOne", null);
			this._buttonTen = UQueryExtensions.Q<Button>(this.Root, "ButtonTen", null);
			this._buttonAll = UQueryExtensions.Q<Button>(this.Root, "ButtonAll", null);
			this._buttonOne.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.MigratePopulation(1);
			}, 0);
			this._buttonTen.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.MigratePopulation(10);
			}, 0);
			this._buttonAll.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.MigratePopulation(this._populationDistributor.Current);
			}, 0);
			this._tooltipRegistrar.Register(this._buttonOne, new Func<TooltipContent>(this.GetTooltip));
			this._tooltipRegistrar.Register(this._buttonTen, new Func<TooltipContent>(this.GetTooltip));
			this._tooltipRegistrar.Register(this._buttonAll, new Func<TooltipContent>(this.GetTooltip));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BAD File Offset: 0x00000DAD
		public TooltipContent GetTooltip()
		{
			if (this._manualMigrationBlocker.IsEnabled)
			{
				return TooltipContent.CreateEmpty();
			}
			return TooltipContent.CreateInstant(this._manualMigrationBlocker.TooltipText);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public void MigratePopulation(int amount)
		{
			amount = Math.Min(amount, this._populationDistributor.Current);
			if (amount > 0)
			{
				this._populationDistributor.MigrateToAndCheckAutomaticMigration(this._target, amount);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C00 File Offset: 0x00000E00
		public void SetButtonsEnabledState()
		{
			this._buttonOne.SetEnabled(this._manualMigrationBlocker.IsEnabled);
			this._buttonTen.SetEnabled(this._manualMigrationBlocker.IsEnabled);
			this._buttonAll.SetEnabled(this._manualMigrationBlocker.IsEnabled);
		}

		// Token: 0x04000032 RID: 50
		public readonly ManualMigrationBlocker _manualMigrationBlocker;

		// Token: 0x04000033 RID: 51
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000034 RID: 52
		public readonly Func<DistrictCenter, PopulationDistributor> _populationDistributorGetter;

		// Token: 0x04000035 RID: 53
		public Label _currentPopulationLabel;

		// Token: 0x04000036 RID: 54
		public Label _currentMinimumLabel;

		// Token: 0x04000037 RID: 55
		public PopulationDistributor _populationDistributor;

		// Token: 0x04000038 RID: 56
		public DistrictCenter _target;

		// Token: 0x04000039 RID: 57
		public Button _buttonOne;

		// Token: 0x0400003A RID: 58
		public Button _buttonTen;

		// Token: 0x0400003B RID: 59
		public Button _buttonAll;

		// Token: 0x0400003C RID: 60
		public readonly Func<bool> _visibilityGetter;
	}
}
