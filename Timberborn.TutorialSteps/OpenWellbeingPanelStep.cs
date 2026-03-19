using System;
using Timberborn.CoreUI;
using Timberborn.TutorialSystem;
using Timberborn.WellbeingUI;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000039 RID: 57
	public class OpenWellbeingPanelStep : ITutorialStep
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00005509 File Offset: 0x00003709
		public OpenWellbeingPanelStep(PanelStack panelStack, PopulationWellbeingBox populationWellbeingBox, string description)
		{
			this._panelStack = panelStack;
			this._populationWellbeingBox = populationWellbeingBox;
			this._description = description;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005526 File Offset: 0x00003726
		public string Description()
		{
			return this._description;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000552E File Offset: 0x0000372E
		public bool Achieved()
		{
			this._wasAchieved |= this._panelStack.IsPanelOnTop(this._populationWellbeingBox);
			return this._wasAchieved;
		}

		// Token: 0x040000B6 RID: 182
		public readonly PanelStack _panelStack;

		// Token: 0x040000B7 RID: 183
		public readonly PopulationWellbeingBox _populationWellbeingBox;

		// Token: 0x040000B8 RID: 184
		public readonly string _description;

		// Token: 0x040000B9 RID: 185
		public bool _wasAchieved;
	}
}
