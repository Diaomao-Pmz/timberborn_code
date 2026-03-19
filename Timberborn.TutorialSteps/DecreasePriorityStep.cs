using System;
using Timberborn.PrioritySystem;
using Timberborn.SelectionSystem;
using Timberborn.TemplateSystem;
using Timberborn.TutorialSystem;
using Timberborn.WorkSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000027 RID: 39
	public class DecreasePriorityStep : ITutorialStep
	{
		// Token: 0x06000117 RID: 279 RVA: 0x000046D5 File Offset: 0x000028D5
		public DecreasePriorityStep(EntitySelectionService entitySelectionService, string description, string templateName)
		{
			this._entitySelectionService = entitySelectionService;
			this._description = description;
			this._templateName = templateName;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000046F2 File Offset: 0x000028F2
		public string Description()
		{
			return this._description;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000046FC File Offset: 0x000028FC
		public bool Achieved()
		{
			if (this._wasAchieved)
			{
				return true;
			}
			SelectableObject selectedObject = this._entitySelectionService.SelectedObject;
			if (selectedObject)
			{
				WorkplacePriority component = selectedObject.GetComponent<WorkplacePriority>();
				if (component != null && component.GetComponent<TemplateSpec>().IsNamedExactly(this._templateName))
				{
					this._wasAchieved = (component.Priority < Priority.Normal);
					return this._wasAchieved;
				}
			}
			return false;
		}

		// Token: 0x04000084 RID: 132
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000085 RID: 133
		public readonly string _description;

		// Token: 0x04000086 RID: 134
		public readonly string _templateName;

		// Token: 0x04000087 RID: 135
		public bool _wasAchieved;
	}
}
