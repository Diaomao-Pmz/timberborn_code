using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using Timberborn.TemplateSystem;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.WorkerTypesUI;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000009 RID: 9
	public class WorkerTypeToggle
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000233F File Offset: 0x0000053F
		public WorkerTypeToggle(SliderToggleFactory sliderToggleFactory, VisualElementLoader visualElementLoader, WorkerTypeHelper workerTypeHelper, WorkplaceUnlockingDialogService workplaceUnlockingDialogService, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._visualElementLoader = visualElementLoader;
			this._workerTypeHelper = workerTypeHelper;
			this._workplaceUnlockingDialogService = workplaceUnlockingDialogService;
			this._loc = loc;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000236C File Offset: 0x0000056C
		public VisualElement Root
		{
			get
			{
				return this._sliderToggle.Root;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000237C File Offset: 0x0000057C
		public void Initialize(VisualElement parent, string toggleBindingKey = null)
		{
			SliderToggleItem sliderToggleItem = SliderToggleItem.CreateBlockable(new Func<TooltipContent>(this.GetBeaverButtonTooltip), WorkerTypeToggle.BeaverClass, new Action(this.SetBeaverWorkerType), new Func<SliderToggleState>(this.GetBeaverToggleState));
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.CreateBlockable(new Func<TooltipContent>(this.GetBotButtonTooltip), WorkerTypeToggle.BotClass, new Action(this.SetBotWorkerType), new Func<SliderToggleState>(this.GetBotToggleState));
			this._sliderToggle = (string.IsNullOrWhiteSpace(toggleBindingKey) ? this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2
			}) : this._sliderToggleFactory.CreateBindable(parent, toggleBindingKey, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2
			}));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000243C File Offset: 0x0000063C
		public void Show(WorkplaceWorkerType workplaceWorkerType)
		{
			this._workplaceWorkerType = workplaceWorkerType;
			this._workplaceSpec = this._workplaceWorkerType.GetComponent<WorkplaceSpec>();
			this.SetEnabledState();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000245C File Offset: 0x0000065C
		public void Update()
		{
			if (!this._sliderToggle.IsBound)
			{
				this._sliderToggle.Bind();
			}
			this._sliderToggle.Update();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002481 File Offset: 0x00000681
		public void Clear()
		{
			this._sliderToggle.Unbind();
			this._sliderToggle.Clear();
			this._workplaceWorkerType = null;
			this._workplaceSpec = null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024A7 File Offset: 0x000006A7
		public void SetBeaverWorkerType()
		{
			this._workplaceWorkerType.SetWorkerType(WorkerTypeHelper.BeaverWorkerType);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024B9 File Offset: 0x000006B9
		public void SetBotWorkerType()
		{
			if (this.IsBotUnlocked())
			{
				this._workplaceWorkerType.SetWorkerType(WorkerTypeHelper.BotWorkerType);
				return;
			}
			this.TryToUnlock();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024DC File Offset: 0x000006DC
		public bool IsBotUnlocked()
		{
			UnlockableWorkerType botUnlockableWorkerType = this.GetBotUnlockableWorkerType();
			return this._workplaceUnlockingDialogService.IsWorkerTypeUnlocked(botUnlockableWorkerType);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024FC File Offset: 0x000006FC
		public void TryToUnlock()
		{
			UnlockableWorkerType botUnlockableWorkerType = this.GetBotUnlockableWorkerType();
			this._workplaceUnlockingDialogService.TryToUnlockWorkerType(botUnlockableWorkerType, new Action(this.SetBotWorkerType));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002528 File Offset: 0x00000728
		public UnlockableWorkerType GetBotUnlockableWorkerType()
		{
			return new UnlockableWorkerType(this._workplaceWorkerType.GetComponent<TemplateSpec>().TemplateName, WorkerTypeHelper.BotWorkerType);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002544 File Offset: 0x00000744
		public void SetEnabledState()
		{
			string defaultWorkerType = this._workplaceSpec.DefaultWorkerType;
			bool flag = !this._workplaceSpec.DisallowOtherWorkerTypes;
			this._botEnabled = (flag || this._workerTypeHelper.IsBotWorkerType(defaultWorkerType));
			this._beaverEnabled = (flag || this._workerTypeHelper.IsBeaverWorkerType(defaultWorkerType));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000259C File Offset: 0x0000079C
		public TooltipContent GetBeaverButtonTooltip()
		{
			if (this.IsBotUnlocked())
			{
				return TooltipContent.Create(new Func<string>(this.GetBeaverTooltipText));
			}
			return TooltipContent.CreateInstant(new Func<VisualElement>(this.GetBotLockedTooltipElement));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025C9 File Offset: 0x000007C9
		public TooltipContent GetBotButtonTooltip()
		{
			if (this.IsBotUnlocked())
			{
				return TooltipContent.Create(new Func<string>(this.GetBotTooltipText));
			}
			return TooltipContent.CreateInstant(new Func<VisualElement>(this.GetBotLockedTooltipElement));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025F6 File Offset: 0x000007F6
		public string GetBeaverTooltipText()
		{
			if (this._workplaceSpec.DisallowOtherWorkerTypes)
			{
				return this._workerTypeHelper.GetDisallowedWorkerText(this._workplaceSpec.DefaultWorkerType);
			}
			return this._workerTypeHelper.GetBeaverWorkerTypeDisplayText();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002627 File Offset: 0x00000827
		public string GetBotTooltipText()
		{
			if (this._workplaceSpec.DisallowOtherWorkerTypes)
			{
				return this._workerTypeHelper.GetDisallowedWorkerText(this._workplaceSpec.DefaultWorkerType);
			}
			return this._workerTypeHelper.GetBotWorkerTypeDisplayText();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002658 File Offset: 0x00000858
		public VisualElement GetBotLockedTooltipElement()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/ScienceCostTooltip");
			UQueryExtensions.Q<Label>(visualElement, "TooltipText", null).text = this._loc.T(WorkerTypeToggle.WorkplaceUnlockTooltipLocKey);
			UnlockableWorkerType botUnlockableWorkerType = this.GetBotUnlockableWorkerType();
			int workerTypeUnlockCost = this._workplaceUnlockingDialogService.GetWorkerTypeUnlockCost(botUnlockableWorkerType);
			UQueryExtensions.Q<Label>(visualElement, "ScienceCost", null).text = NumberFormatter.Format(workerTypeUnlockCost);
			return visualElement;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026C1 File Offset: 0x000008C1
		public SliderToggleState GetBeaverToggleState()
		{
			if (!this._beaverEnabled)
			{
				return SliderToggleState.Unclickable;
			}
			if (!this._workerTypeHelper.IsBeaverWorkerType(this._workplaceWorkerType.WorkerType))
			{
				return SliderToggleState.None;
			}
			return SliderToggleState.Active;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026E8 File Offset: 0x000008E8
		public SliderToggleState GetBotToggleState()
		{
			if (!this._botEnabled)
			{
				return SliderToggleState.Unclickable;
			}
			if (!this.IsBotUnlocked())
			{
				return SliderToggleState.Locked;
			}
			if (!this._workerTypeHelper.IsBotWorkerType(this._workplaceWorkerType.WorkerType))
			{
				return SliderToggleState.None;
			}
			return SliderToggleState.Active;
		}

		// Token: 0x04000015 RID: 21
		public static readonly string BeaverClass = "worker-type-toggle__icon--beaver";

		// Token: 0x04000016 RID: 22
		public static readonly string BotClass = "worker-type-toggle__icon--bot";

		// Token: 0x04000017 RID: 23
		public static readonly string WorkplaceUnlockTooltipLocKey = "Work.WorkplaceUnlock.Tooltip";

		// Token: 0x04000018 RID: 24
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000019 RID: 25
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001A RID: 26
		public readonly WorkerTypeHelper _workerTypeHelper;

		// Token: 0x0400001B RID: 27
		public readonly WorkplaceUnlockingDialogService _workplaceUnlockingDialogService;

		// Token: 0x0400001C RID: 28
		public readonly ILoc _loc;

		// Token: 0x0400001D RID: 29
		public SliderToggle _sliderToggle;

		// Token: 0x0400001E RID: 30
		public WorkplaceWorkerType _workplaceWorkerType;

		// Token: 0x0400001F RID: 31
		public WorkplaceSpec _workplaceSpec;

		// Token: 0x04000020 RID: 32
		public bool _botEnabled;

		// Token: 0x04000021 RID: 33
		public bool _beaverEnabled;
	}
}
