using System;
using Timberborn.BlueprintSystem;
using Timberborn.LevelVisibilitySystemUI;
using Timberborn.Localization;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200005F RID: 95
	public class VisibleLevelChangeStepDeserializer : IStepDeserializer
	{
		// Token: 0x0600027F RID: 639 RVA: 0x00007530 File Offset: 0x00005730
		public VisibleLevelChangeStepDeserializer(ILoc loc, VisibleLevelChangeService visibleLevelChangeService, ILevelVisibilityPanel levelVisibilityPanel)
		{
			this._loc = loc;
			this._visibleLevelChangeService = visibleLevelChangeService;
			this._levelVisibilityPanel = levelVisibilityPanel;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007550 File Offset: 0x00005750
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			VisibleLevelChangeStepSpec visibleLevelChangeStepSpec = step.Specs[0] as VisibleLevelChangeStepSpec;
			if (visibleLevelChangeStepSpec != null)
			{
				tutorialStep = this.Create(visibleLevelChangeStepSpec.VisibleLevelChangeType, visibleLevelChangeStepSpec.ShowKeybindings);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007590 File Offset: 0x00005790
		public TutorialStep Create(VisibleLevelChangeType visibleLevelChangeType, bool showKeybindings)
		{
			ITutorialStep step = new VisibleLevelChangeStep(this._visibleLevelChangeService, this._loc.T(VisibleLevelChangeStepDeserializer.GetLocKey(visibleLevelChangeType)), visibleLevelChangeType);
			string keyBinding = showKeybindings ? VisibleLevelChangeStepDeserializer.GetKeybindingLocKey(visibleLevelChangeType) : null;
			return TutorialStep.Create(step, delegate(bool state)
			{
				this._levelVisibilityPanel.TogglePanelHighlight(state);
			}, keyBinding, null);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000075DC File Offset: 0x000057DC
		public static string GetLocKey(VisibleLevelChangeType visibleLevelChangeType)
		{
			string result;
			switch (visibleLevelChangeType)
			{
			case VisibleLevelChangeType.Decrease:
				result = "Tutorial.LayerTool.Decrease";
				break;
			case VisibleLevelChangeType.Increase:
				result = "Tutorial.LayerTool.Increase";
				break;
			case VisibleLevelChangeType.Reset:
				result = "Tutorial.LayerTool.Reset";
				break;
			default:
				throw new ArgumentOutOfRangeException("visibleLevelChangeType", visibleLevelChangeType, null);
			}
			return result;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007628 File Offset: 0x00005828
		public static string GetKeybindingLocKey(VisibleLevelChangeType visibleLevelChangeType)
		{
			string result;
			switch (visibleLevelChangeType)
			{
			case VisibleLevelChangeType.Decrease:
				result = "LowerVisibleLayer";
				break;
			case VisibleLevelChangeType.Increase:
				result = "RaiseVisibleLayer";
				break;
			case VisibleLevelChangeType.Reset:
				result = null;
				break;
			default:
				throw new ArgumentOutOfRangeException("visibleLevelChangeType", visibleLevelChangeType, null);
			}
			return result;
		}

		// Token: 0x0400013C RID: 316
		public readonly ILoc _loc;

		// Token: 0x0400013D RID: 317
		public readonly VisibleLevelChangeService _visibleLevelChangeService;

		// Token: 0x0400013E RID: 318
		public readonly ILevelVisibilityPanel _levelVisibilityPanel;
	}
}
