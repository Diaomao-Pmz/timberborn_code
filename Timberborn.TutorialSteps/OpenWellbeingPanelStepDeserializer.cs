using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.TutorialSystem;
using Timberborn.WellbeingUI;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200003A RID: 58
	public class OpenWellbeingPanelStepDeserializer : IStepDeserializer
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00005554 File Offset: 0x00003754
		public OpenWellbeingPanelStepDeserializer(PanelStack panelStack, BasicStatisticsPanel basicStatisticsPanel, PopulationWellbeingBox populationWellbeingBox, ILoc loc)
		{
			this._panelStack = panelStack;
			this._basicStatisticsPanel = basicStatisticsPanel;
			this._populationWellbeingBox = populationWellbeingBox;
			this._loc = loc;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000557C File Offset: 0x0000377C
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			if (step.Specs[0] is OpenWellbeingPanelStepSpec)
			{
				tutorialStep = this.Create();
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000055B0 File Offset: 0x000037B0
		public TutorialStep Create()
		{
			string description = this._loc.T(OpenWellbeingPanelStepDeserializer.DescriptionLocKey);
			return TutorialStep.Create(new OpenWellbeingPanelStep(this._panelStack, this._populationWellbeingBox, description), delegate(bool state)
			{
				this._basicStatisticsPanel.ToggleWellbeingButtonHighlight(state);
			}, OpenWellbeingPanelStepDeserializer.OpenWellbeingBoxKey, null);
		}

		// Token: 0x040000BA RID: 186
		public static readonly string OpenWellbeingBoxKey = "OpenWellbeingBox";

		// Token: 0x040000BB RID: 187
		public static readonly string DescriptionLocKey = "Tutorial.Wellbeing.OpenWellbeingPanel";

		// Token: 0x040000BC RID: 188
		public readonly PanelStack _panelStack;

		// Token: 0x040000BD RID: 189
		public readonly BasicStatisticsPanel _basicStatisticsPanel;

		// Token: 0x040000BE RID: 190
		public readonly PopulationWellbeingBox _populationWellbeingBox;

		// Token: 0x040000BF RID: 191
		public readonly ILoc _loc;
	}
}
