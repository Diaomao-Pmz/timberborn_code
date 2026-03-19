using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Bots;
using Timberborn.CoreUI;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000023 RID: 35
	public class WorkplaceWorkerTypeBatchControlRowItemFactory
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003D7D File Offset: 0x00001F7D
		public WorkplaceWorkerTypeBatchControlRowItemFactory(VisualElementLoader visualElementLoader, WorkerTypeToggleFactory workerTypeToggleFactory, BotPopulation botPopulation)
		{
			this._visualElementLoader = visualElementLoader;
			this._workerTypeToggleFactory = workerTypeToggleFactory;
			this._botPopulation = botPopulation;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003D9C File Offset: 0x00001F9C
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			if (this._botPopulation.BotCreated)
			{
				WorkplaceWorkerType component = entity.GetComponent<WorkplaceWorkerType>();
				if (component != null)
				{
					string elementName = "Game/BatchControl/SelectionToggleBatchControlRowItem";
					VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
					WorkerTypeToggle workerTypeToggle = this._workerTypeToggleFactory.Create(visualElement);
					workerTypeToggle.Show(component);
					return new WorkplaceWorkerTypeBatchControlRowItem(visualElement, workerTypeToggle);
				}
			}
			return null;
		}

		// Token: 0x040000A0 RID: 160
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000A1 RID: 161
		public readonly WorkerTypeToggleFactory _workerTypeToggleFactory;

		// Token: 0x040000A2 RID: 162
		public readonly BotPopulation _botPopulation;
	}
}
