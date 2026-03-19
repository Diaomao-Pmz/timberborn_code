using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.TooltipSystem;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000010 RID: 16
	public class WorkplaceBatchControlRowItemFactory
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002C78 File Offset: 0x00000E78
		public WorkplaceBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C90 File Offset: 0x00000E90
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Workplace workplace = entity.GetComponent<Workplace>();
			if (workplace != null)
			{
				string elementName = "Game/BatchControl/WorkplaceBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Label label = UQueryExtensions.Q<Label>(visualElement, "Info", null);
				Button button = UQueryExtensions.Q<Button>(visualElement, "Decrease", null);
				button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					workplace.DecreaseDesiredWorkers();
				}, 0);
				button.SetEnabled(workplace.MaxWorkers > 1);
				Button button2 = UQueryExtensions.Q<Button>(visualElement, "Increase", null);
				button2.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					workplace.IncreaseDesiredWorkers();
				}, 0);
				button2.SetEnabled(workplace.MaxWorkers > 1);
				WorkplaceDescriber component = workplace.GetComponent<WorkplaceDescriber>();
				this._tooltipRegistrar.Register(label, new Func<string>(component.GetWorkersTooltip));
				this._tooltipRegistrar.Register(UQueryExtensions.Q<VisualElement>(visualElement, "Workers", null), new Func<string>(component.GetWorkersTooltip));
				return new WorkplaceBatchControlRowItem(visualElement, workplace, label, button2, button);
			}
			return null;
		}

		// Token: 0x0400004A RID: 74
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400004B RID: 75
		public readonly ITooltipRegistrar _tooltipRegistrar;
	}
}
