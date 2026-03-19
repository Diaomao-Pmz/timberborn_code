using System;
using Timberborn.SelectionSystem;
using Timberborn.TemplateSystem;
using Timberborn.TutorialSystem;
using Timberborn.WorkSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200002E RID: 46
	public class IncreaseDesiredWorkersStep : ITutorialStep
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00004D31 File Offset: 0x00002F31
		public IncreaseDesiredWorkersStep(EntitySelectionService entitySelectionService, string description, string templateName)
		{
			this._entitySelectionService = entitySelectionService;
			this._description = description;
			this._templateName = templateName;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004D4E File Offset: 0x00002F4E
		public string Description()
		{
			return this._description;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004D58 File Offset: 0x00002F58
		public bool Achieved()
		{
			if (this._wasAchieved)
			{
				return true;
			}
			SelectableObject selectedObject = this._entitySelectionService.SelectedObject;
			if (selectedObject)
			{
				Workplace component = selectedObject.GetComponent<Workplace>();
				if (component != null && component.GetComponent<TemplateSpec>().IsNamedExactly(this._templateName))
				{
					this._wasAchieved = (component.DesiredWorkers > component.GetComponent<WorkplaceSpec>().DefaultWorkers);
					return this._wasAchieved;
				}
			}
			return false;
		}

		// Token: 0x04000098 RID: 152
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000099 RID: 153
		public readonly string _description;

		// Token: 0x0400009A RID: 154
		public readonly string _templateName;

		// Token: 0x0400009B RID: 155
		public bool _wasAchieved;
	}
}
