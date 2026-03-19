using System;
using System.Collections.Generic;
using Timberborn.FactionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200001A RID: 26
	public class PopulationWellbeingGoals : IUpdatableSingleton
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003AAB File Offset: 0x00001CAB
		public PopulationWellbeingGoals(GoalRowFactory goalRowFactory, FactionSpecService factionSpecService)
		{
			this._goalRowFactory = goalRowFactory;
			this._factionSpecService = factionSpecService;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003ACC File Offset: 0x00001CCC
		public void Initialize(VisualElement root)
		{
			this._goalsWrapper = UQueryExtensions.Q<VisualElement>(root, "GoalsWrapper", null);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public void StartBlinking(FactionSpec factionSpec)
		{
			this._blinkingElement = this._goals[factionSpec.Id];
			this._remainingBlinks = PopulationWellbeingGoals.BlinkCount * 2;
			this._timeToBlink = PopulationWellbeingGoals.BlinkInterval;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003B11 File Offset: 0x00001D11
		public void UpdateSingleton()
		{
			if (this._blinkingElement != null)
			{
				this._timeToBlink -= Time.unscaledDeltaTime;
				if (this._timeToBlink <= 0f)
				{
					this.Blink();
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003B40 File Offset: 0x00001D40
		public void AddGoals()
		{
			foreach (UnlockableFactionSpec unlockableFactionSpec in this._factionSpecService.UnlockableFactions)
			{
				VisualElement visualElement = this._goalRowFactory.CreateRow(unlockableFactionSpec);
				this._goalsWrapper.Add(visualElement);
				this._goals[unlockableFactionSpec.GetSpec<FactionSpec>().Id] = visualElement;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003BBC File Offset: 0x00001DBC
		public void Clear()
		{
			this._goalsWrapper.Clear();
			this._goals.Clear();
			this._blinkingElement = null;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003BDC File Offset: 0x00001DDC
		public void Blink()
		{
			this._timeToBlink = PopulationWellbeingGoals.BlinkInterval;
			this._remainingBlinks--;
			if (this._remainingBlinks > 0)
			{
				this._blinkingElement.ToggleInClassList(PopulationWellbeingGoals.BlinkClass);
				return;
			}
			this._blinkingElement.RemoveFromClassList(PopulationWellbeingGoals.BlinkClass);
			this._blinkingElement = null;
		}

		// Token: 0x04000078 RID: 120
		public static readonly float BlinkInterval = 0.5f;

		// Token: 0x04000079 RID: 121
		public static readonly int BlinkCount = 5;

		// Token: 0x0400007A RID: 122
		public static readonly string BlinkClass = "blink";

		// Token: 0x0400007B RID: 123
		public readonly GoalRowFactory _goalRowFactory;

		// Token: 0x0400007C RID: 124
		public readonly FactionSpecService _factionSpecService;

		// Token: 0x0400007D RID: 125
		public VisualElement _goalsWrapper;

		// Token: 0x0400007E RID: 126
		public readonly Dictionary<string, VisualElement> _goals = new Dictionary<string, VisualElement>();

		// Token: 0x0400007F RID: 127
		public VisualElement _blinkingElement;

		// Token: 0x04000080 RID: 128
		public int _remainingBlinks;

		// Token: 0x04000081 RID: 129
		public float _timeToBlink;
	}
}
