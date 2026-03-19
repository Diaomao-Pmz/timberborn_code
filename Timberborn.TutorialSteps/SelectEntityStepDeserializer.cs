using System;
using System.Collections.Immutable;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;
using Timberborn.SelectionSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000046 RID: 70
	public class SelectEntityStepDeserializer : IStepDeserializer
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00005D9E File Offset: 0x00003F9E
		public SelectEntityStepDeserializer(EntitySelectionService entitySelectionService)
		{
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			SelectEntityStepSpec selectEntityStepSpec = step.Specs[0] as SelectEntityStepSpec;
			if (selectEntityStepSpec != null)
			{
				tutorialStep = this.Create(selectEntityStepSpec.TemplateNames, selectEntityStepSpec.Description);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005DEF File Offset: 0x00003FEF
		public TutorialStep Create(ImmutableArray<string> templateNames, LocalizedText description)
		{
			return TutorialStep.Create(new SelectEntityStep(this._entitySelectionService, templateNames, description.Value), null, null, null);
		}

		// Token: 0x040000E5 RID: 229
		public readonly EntitySelectionService _entitySelectionService;
	}
}
