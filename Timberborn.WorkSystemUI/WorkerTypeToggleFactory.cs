using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using Timberborn.WorkerTypesUI;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200000A RID: 10
	public class WorkerTypeToggleFactory
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002739 File Offset: 0x00000939
		public WorkerTypeToggleFactory(SliderToggleFactory sliderToggleFactory, VisualElementLoader visualElementLoader, WorkerTypeHelper workerTypeHelper, WorkplaceUnlockingDialogService workplaceUnlockingDialogService, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._visualElementLoader = visualElementLoader;
			this._workerTypeHelper = workerTypeHelper;
			this._workplaceUnlockingDialogService = workplaceUnlockingDialogService;
			this._loc = loc;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002766 File Offset: 0x00000966
		public WorkerTypeToggle Create(VisualElement parent)
		{
			return this.CreateBindable(parent, null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002770 File Offset: 0x00000970
		public WorkerTypeToggle CreateBindable(VisualElement parent, string toggleBindingKey)
		{
			WorkerTypeToggle workerTypeToggle = new WorkerTypeToggle(this._sliderToggleFactory, this._visualElementLoader, this._workerTypeHelper, this._workplaceUnlockingDialogService, this._loc);
			workerTypeToggle.Initialize(parent, toggleBindingKey);
			return workerTypeToggle;
		}

		// Token: 0x04000022 RID: 34
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000023 RID: 35
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000024 RID: 36
		public readonly WorkerTypeHelper _workerTypeHelper;

		// Token: 0x04000025 RID: 37
		public readonly WorkplaceUnlockingDialogService _workplaceUnlockingDialogService;

		// Token: 0x04000026 RID: 38
		public readonly ILoc _loc;
	}
}
