using System;
using System.Collections.Immutable;
using Timberborn.SelectionSystem;
using Timberborn.TemplateSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000045 RID: 69
	public class SelectEntityStep : ITutorialStep
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00005D1D File Offset: 0x00003F1D
		public SelectEntityStep(EntitySelectionService entitySelectionService, ImmutableArray<string> templateNames, string description)
		{
			this._entitySelectionService = entitySelectionService;
			this._templateNames = templateNames;
			this._description = description;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005D3A File Offset: 0x00003F3A
		public string Description()
		{
			return this._description;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005D42 File Offset: 0x00003F42
		public bool Achieved()
		{
			this._wasAchieved |= this.IsTemplateSelected();
			return this._wasAchieved;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005D60 File Offset: 0x00003F60
		public bool IsTemplateSelected()
		{
			if (this._entitySelectionService.IsAnythingSelected)
			{
				string templateName = this._entitySelectionService.SelectedObject.GetComponent<TemplateSpec>().TemplateName;
				return this._templateNames.Contains(templateName);
			}
			return false;
		}

		// Token: 0x040000E1 RID: 225
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x040000E2 RID: 226
		public readonly ImmutableArray<string> _templateNames;

		// Token: 0x040000E3 RID: 227
		public readonly string _description;

		// Token: 0x040000E4 RID: 228
		public bool _wasAchieved;
	}
}
