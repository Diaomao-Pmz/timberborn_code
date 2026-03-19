using System;
using Timberborn.Buildings;
using Timberborn.SelectionSystem;
using Timberborn.TemplateSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200001D RID: 29
	public class ChangePausedStateStep : ITutorialStep
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00003C2D File Offset: 0x00001E2D
		public ChangePausedStateStep(EntitySelectionService entitySelectionService, string description, bool shouldBePaused, string templateName)
		{
			this._entitySelectionService = entitySelectionService;
			this._description = description;
			this._shouldBePaused = shouldBePaused;
			this._templateName = templateName;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003C52 File Offset: 0x00001E52
		public string Description()
		{
			return this._description;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003C5C File Offset: 0x00001E5C
		public bool Achieved()
		{
			if (this._wasAchieved)
			{
				return true;
			}
			SelectableObject selectedObject = this._entitySelectionService.SelectedObject;
			if (selectedObject)
			{
				PausableBuilding component = selectedObject.GetComponent<PausableBuilding>();
				if (component != null && component.GetComponent<TemplateSpec>().IsNamedExactly(this._templateName))
				{
					if (this._wasOppositeState)
					{
						this._wasAchieved = ((this._shouldBePaused && component.Paused) || (!this._shouldBePaused && !component.Paused));
						return this._wasAchieved;
					}
					this._wasOppositeState = ((this._shouldBePaused && !component.Paused) || (!this._shouldBePaused && component.Paused));
				}
			}
			return false;
		}

		// Token: 0x04000058 RID: 88
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000059 RID: 89
		public readonly string _description;

		// Token: 0x0400005A RID: 90
		public readonly bool _shouldBePaused;

		// Token: 0x0400005B RID: 91
		public readonly string _templateName;

		// Token: 0x0400005C RID: 92
		public bool _wasOppositeState;

		// Token: 0x0400005D RID: 93
		public bool _wasAchieved;
	}
}
