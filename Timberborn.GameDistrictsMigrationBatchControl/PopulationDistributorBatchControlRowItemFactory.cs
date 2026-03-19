using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.GameDistrictsMigration;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000020 RID: 32
	public class PopulationDistributorBatchControlRowItemFactory
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000374A File Offset: 0x0000194A
		public PopulationDistributorBatchControlRowItemFactory(AlternateClickableFactory alternateClickableFactory, ILoc loc, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._alternateClickableFactory = alternateClickableFactory;
			this._loc = loc;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003770 File Offset: 0x00001970
		public IBatchControlRowItem Create(PopulationDistributor populationDistributor)
		{
			string elementName = "Game/BatchControl/PopulationDistributorBatchControlRowItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			IntegerField integerField = UQueryExtensions.Q<IntegerField>(visualElement, "MinimumValue", null);
			ValueTuple<AlternateClickable, AlternateClickable> valueTuple = this.InitializeMinimumControls(populationDistributor, integerField, visualElement);
			AlternateClickable item = valueTuple.Item1;
			AlternateClickable item2 = valueTuple.Item2;
			this.InitializeImmigrationToggle(populationDistributor, visualElement);
			this.InitializeEmigrationToggle(populationDistributor, visualElement);
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(visualElement, "NeedingIcon", null);
			this._tooltipRegistrar.RegisterLocalizable(visualElement2, PopulationDistributorBatchControlRowItemFactory.HighMinimumWarningLocKey);
			return new PopulationDistributorBatchControlRowItem(visualElement, integerField, item, item2, visualElement2, populationDistributor);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000037F4 File Offset: 0x000019F4
		public ValueTuple<AlternateClickable, AlternateClickable> InitializeMinimumControls(PopulationDistributor populationDistributor, IntegerField minimumValue, VisualElement root)
		{
			TextFields.InitializeIntegerField(minimumValue, populationDistributor.Minimum, 0, int.MaxValue, new Action<int>(populationDistributor.SetMinimumAndMigrate));
			return new ValueTuple<AlternateClickable, AlternateClickable>(this._alternateClickableFactory.Create(UQueryExtensions.Q<Button>(root, "MinusButton", null), delegate
			{
				PopulationDistributorBatchControlRowItemFactory.ChangeMigrationMinimum(-1, populationDistributor);
			}, delegate
			{
				PopulationDistributorBatchControlRowItemFactory.ChangeMigrationMinimum(-10, populationDistributor);
			}), this._alternateClickableFactory.Create(UQueryExtensions.Q<Button>(root, "PlusButton", null), delegate
			{
				PopulationDistributorBatchControlRowItemFactory.ChangeMigrationMinimum(1, populationDistributor);
			}, delegate
			{
				PopulationDistributorBatchControlRowItemFactory.ChangeMigrationMinimum(10, populationDistributor);
			}));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000389C File Offset: 0x00001A9C
		public void InitializeImmigrationToggle(PopulationDistributor populationDistributor, VisualElement root)
		{
			Toggle immigrationToggle = UQueryExtensions.Q<Toggle>(root, "ImmigrationToggle", null);
			immigrationToggle.SetValueWithoutNotify(populationDistributor.AllowImmigration);
			immigrationToggle.RegisterCallback<ChangeEvent<bool>>(delegate(ChangeEvent<bool> _)
			{
				populationDistributor.ToggleAllowImmigrationAndMigrate();
			}, 0);
			this._tooltipRegistrar.Register(immigrationToggle, () => this.GetImmigrationToggleTooltip(immigrationToggle));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000391C File Offset: 0x00001B1C
		public void InitializeEmigrationToggle(PopulationDistributor populationDistributor, VisualElement root)
		{
			Toggle emigrationToggle = UQueryExtensions.Q<Toggle>(root, "EmigrationToggle", null);
			emigrationToggle.SetValueWithoutNotify(populationDistributor.AllowEmigration);
			emigrationToggle.RegisterCallback<ChangeEvent<bool>>(delegate(ChangeEvent<bool> _)
			{
				populationDistributor.ToggleAllowEmigrationAndMigrate();
			}, 0);
			this._tooltipRegistrar.Register(emigrationToggle, () => this.GetEmigrationToggleTooltip(emigrationToggle));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000399C File Offset: 0x00001B9C
		public static void ChangeMigrationMinimum(int change, PopulationDistributor populationDistributor)
		{
			int minimumAndMigrate = populationDistributor.Minimum + change;
			populationDistributor.SetMinimumAndMigrate(minimumAndMigrate);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000039B9 File Offset: 0x00001BB9
		public string GetImmigrationToggleTooltip(Toggle toggle)
		{
			return this._loc.T(toggle.value ? PopulationDistributorBatchControlRowItemFactory.ImmigrationEnabledLocKey : PopulationDistributorBatchControlRowItemFactory.ImmigrationDisabledLocKey);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000039DA File Offset: 0x00001BDA
		public string GetEmigrationToggleTooltip(Toggle toggle)
		{
			return this._loc.T(toggle.value ? PopulationDistributorBatchControlRowItemFactory.EmigrationEnabledLocKey : PopulationDistributorBatchControlRowItemFactory.EmigrationDisabledLocKey);
		}

		// Token: 0x0400007B RID: 123
		public static readonly string EmigrationDisabledLocKey = "Migration.AutomaticEmigrationDisabled";

		// Token: 0x0400007C RID: 124
		public static readonly string EmigrationEnabledLocKey = "Migration.AutomaticEmigrationEnabled";

		// Token: 0x0400007D RID: 125
		public static readonly string ImmigrationDisabledLocKey = "Migration.AutomaticImmigrationDisabled";

		// Token: 0x0400007E RID: 126
		public static readonly string ImmigrationEnabledLocKey = "Migration.AutomaticImmigrationEnabled";

		// Token: 0x0400007F RID: 127
		public static readonly string HighMinimumWarningLocKey = "Migration.HighMinimumWarning";

		// Token: 0x04000080 RID: 128
		public readonly AlternateClickableFactory _alternateClickableFactory;

		// Token: 0x04000081 RID: 129
		public readonly ILoc _loc;

		// Token: 0x04000082 RID: 130
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000083 RID: 131
		public readonly VisualElementLoader _visualElementLoader;
	}
}
