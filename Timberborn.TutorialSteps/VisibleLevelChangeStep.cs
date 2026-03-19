using System;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200005E RID: 94
	public class VisibleLevelChangeStep : ITutorialStep
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000749D File Offset: 0x0000569D
		public VisibleLevelChangeStep(VisibleLevelChangeService visibleLevelChangeService, string description, VisibleLevelChangeType visibleLevelChangeType)
		{
			this._visibleLevelChangeService = visibleLevelChangeService;
			this._description = description;
			this._visibleLevelChangeType = visibleLevelChangeType;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000074BA File Offset: 0x000056BA
		public string Description()
		{
			return this._description;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000074C4 File Offset: 0x000056C4
		public bool Achieved()
		{
			bool result;
			switch (this._visibleLevelChangeType)
			{
			case VisibleLevelChangeType.Decrease:
				result = this._visibleLevelChangeService.WasAtZero;
				break;
			case VisibleLevelChangeType.Increase:
				result = (this._visibleLevelChangeService.LevelsIncreasedSinceZero > 0);
				break;
			case VisibleLevelChangeType.Reset:
				result = (this._visibleLevelChangeService.WasAtZero && this._visibleLevelChangeService.IsAtMax);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x04000139 RID: 313
		public readonly VisibleLevelChangeService _visibleLevelChangeService;

		// Token: 0x0400013A RID: 314
		public readonly string _description;

		// Token: 0x0400013B RID: 315
		public readonly VisibleLevelChangeType _visibleLevelChangeType;
	}
}
