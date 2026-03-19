using System;
using Timberborn.CharactersUI;
using Timberborn.CoreUI;
using Timberborn.TooltipSystem;
using Timberborn.WorkerTypesUI;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200000C RID: 12
	public class WorkerViewFactory
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002885 File Offset: 0x00000A85
		public WorkerViewFactory(VisualElementLoader visualElementLoader, CharacterButtonFactory characterButtonFactory, ITooltipRegistrar tooltipRegistrar, WorkerTypeHelper workerTypeHelper)
		{
			this._visualElementLoader = visualElementLoader;
			this._characterButtonFactory = characterButtonFactory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._workerTypeHelper = workerTypeHelper;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028AC File Offset: 0x00000AAC
		public WorkerView Create(WorkplaceWorkerType workplaceWorkerType)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/WorkerView");
			CharacterButton characterButton = this._characterButtonFactory.Create(UQueryExtensions.Q<Button>(visualElement, "CharacterButton", null));
			UQueryExtensions.Q<Button>(visualElement, "WorkerView", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				characterButton.ClickAction();
			}, 0);
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(visualElement, "VacantIcon", null);
			this._tooltipRegistrar.RegisterLocalizable(visualElement2, WorkerViewFactory.VacantLocKey);
			return new WorkerView(this._workerTypeHelper, visualElement, characterButton, UQueryExtensions.Q<Label>(visualElement, "Name", null), visualElement2, workplaceWorkerType);
		}

		// Token: 0x0400002D RID: 45
		public static readonly string VacantLocKey = "Work.Vacant";

		// Token: 0x0400002E RID: 46
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002F RID: 47
		public readonly CharacterButtonFactory _characterButtonFactory;

		// Token: 0x04000030 RID: 48
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000031 RID: 49
		public readonly WorkerTypeHelper _workerTypeHelper;
	}
}
