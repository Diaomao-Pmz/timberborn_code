using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Bots;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsMigration;
using Timberborn.SliderToggleSystem;
using Timberborn.TooltipSystem;
using Timberborn.WorkerTypesUI;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x0200000F RID: 15
	public class DistrictCenterFragment : IEntityPanelFragment
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002620 File Offset: 0x00000820
		public DistrictCenterFragment(VisualElementLoader visualElementLoader, IBatchControlBox batchControlBox, ManualMigrationDistrictSetter manualMigrationDistrictSetter, BotPopulation botPopulation, ITooltipRegistrar tooltipRegistrar, WorkerTypeHelper workerTypeHelper, SliderToggleFactory sliderToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._batchControlBox = batchControlBox;
			this._manualMigrationDistrictSetter = manualMigrationDistrictSetter;
			this._botPopulation = botPopulation;
			this._tooltipRegistrar = tooltipRegistrar;
			this._workerTypeHelper = workerTypeHelper;
			this._sliderToggleFactory = sliderToggleFactory;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002660 File Offset: 0x00000860
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DistrictCenterFragment");
			UQueryExtensions.Q<Button>(this._root, "MigrateButtonLeft", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OpenMigrationTabAsLeft), 0);
			UQueryExtensions.Q<Button>(this._root, "MigrateButtonRight", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OpenMigrationTabAsRight), 0);
			this._root.ToggleDisplayStyle(false);
			this._workerTypeRoot = UQueryExtensions.Q<VisualElement>(this._root, "WorkerType", null);
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(new Func<string>(this.GetBeaverWorkerTooltip), DistrictCenterFragment.BeaverClass, new Action(this.SetBeaverWorkerType), new Func<bool>(this.IsBeaverWorkerType));
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(new Func<string>(this.GetBotWorkerTooltip), DistrictCenterFragment.BotClass, new Action(this.SetBotWorkerType), new Func<bool>(this.IsBotWorkerType));
			this._sliderToggle = this._sliderToggleFactory.Create(this._workerTypeRoot, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2
			});
			this._tooltipRegistrar.RegisterLocalizable(this._workerTypeRoot, DistrictCenterFragment.WorkerTypeLocKey);
			return this._root;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002793 File Offset: 0x00000993
		public void ShowFragment(BaseComponent entity)
		{
			this._districtCenter = entity.GetComponent<DistrictCenter>();
			if (this._districtCenter)
			{
				this._districtDefaultWorkerType = this._districtCenter.GetComponent<DistrictDefaultWorkerType>();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027BF File Offset: 0x000009BF
		public void ClearFragment()
		{
			this._districtCenter = null;
			this._districtDefaultWorkerType = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027DC File Offset: 0x000009DC
		public void UpdateFragment()
		{
			if (this._districtCenter && this._districtCenter.Enabled)
			{
				this._root.ToggleDisplayStyle(true);
				this._sliderToggle.Update();
				this._workerTypeRoot.ToggleDisplayStyle(this._botPopulation.BotCreated);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002830 File Offset: 0x00000A30
		public void OpenMigrationTabAsLeft(ClickEvent evt)
		{
			this._manualMigrationDistrictSetter.SetLeftDistrictWithHighlight(this._districtCenter);
			this._batchControlBox.OpenMigrationTab();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000284E File Offset: 0x00000A4E
		public void OpenMigrationTabAsRight(ClickEvent evt)
		{
			this._manualMigrationDistrictSetter.SetRightDistrictWithHighlight(this._districtCenter);
			this._batchControlBox.OpenMigrationTab();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000286C File Offset: 0x00000A6C
		public void SetBeaverWorkerType()
		{
			this._districtDefaultWorkerType.SetWorkerType(WorkerTypeHelper.BeaverWorkerType);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000287E File Offset: 0x00000A7E
		public void SetBotWorkerType()
		{
			this._districtDefaultWorkerType.SetWorkerType(WorkerTypeHelper.BotWorkerType);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002890 File Offset: 0x00000A90
		public string GetBeaverWorkerTooltip()
		{
			return this._workerTypeHelper.GetBeaverWorkerTypeDisplayText();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000289D File Offset: 0x00000A9D
		public string GetBotWorkerTooltip()
		{
			return this._workerTypeHelper.GetBotWorkerTypeDisplayText();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028AA File Offset: 0x00000AAA
		public bool IsBeaverWorkerType()
		{
			return this._workerTypeHelper.IsBeaverWorkerType(this._districtDefaultWorkerType.WorkerType);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000028C2 File Offset: 0x00000AC2
		public bool IsBotWorkerType()
		{
			return this._workerTypeHelper.IsBotWorkerType(this._districtDefaultWorkerType.WorkerType);
		}

		// Token: 0x04000019 RID: 25
		public static readonly string BeaverClass = "worker-type-toggle__icon--beaver";

		// Token: 0x0400001A RID: 26
		public static readonly string BotClass = "worker-type-toggle__icon--bot";

		// Token: 0x0400001B RID: 27
		public static readonly string WorkerTypeLocKey = "Work.DefaultAllowedWorker.Tooltip";

		// Token: 0x0400001C RID: 28
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001D RID: 29
		public readonly IBatchControlBox _batchControlBox;

		// Token: 0x0400001E RID: 30
		public readonly ManualMigrationDistrictSetter _manualMigrationDistrictSetter;

		// Token: 0x0400001F RID: 31
		public readonly BotPopulation _botPopulation;

		// Token: 0x04000020 RID: 32
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000021 RID: 33
		public readonly WorkerTypeHelper _workerTypeHelper;

		// Token: 0x04000022 RID: 34
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000023 RID: 35
		public DistrictCenter _districtCenter;

		// Token: 0x04000024 RID: 36
		public DistrictDefaultWorkerType _districtDefaultWorkerType;

		// Token: 0x04000025 RID: 37
		public VisualElement _root;

		// Token: 0x04000026 RID: 38
		public VisualElement _workerTypeRoot;

		// Token: 0x04000027 RID: 39
		public SliderToggle _sliderToggle;
	}
}
