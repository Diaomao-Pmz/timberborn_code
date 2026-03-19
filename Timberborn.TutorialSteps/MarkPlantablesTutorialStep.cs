using System;
using Timberborn.Localization;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000031 RID: 49
	public class MarkPlantablesTutorialStep : ITutorialStep
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00004FA0 File Offset: 0x000031A0
		public MarkPlantablesTutorialStep(PlantableResourceCounter plantableResourceCounter, ILoc loc, string templateName, int requiredAmount, string localizedResourceName)
		{
			this._plantableResourceCounter = plantableResourceCounter;
			this._loc = loc;
			this._templateName = templateName;
			this._requiredAmount = requiredAmount;
			this._localizedResourceName = localizedResourceName;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004FCD File Offset: 0x000031CD
		public string Description()
		{
			return this._loc.T<string, int, int>(MarkPlantablesTutorialStep.MarkPlantablesLocKey, this._localizedResourceName, Math.Min(this.NumberOfResources, this._requiredAmount), this._requiredAmount);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004FFC File Offset: 0x000031FC
		public bool Achieved()
		{
			return this.NumberOfResources >= this._requiredAmount;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000500F File Offset: 0x0000320F
		public int NumberOfResources
		{
			get
			{
				return this._plantableResourceCounter.GetNumberOfResources(this._templateName);
			}
		}

		// Token: 0x040000A0 RID: 160
		public static readonly string MarkPlantablesLocKey = "Tutorial.MarkPlantables";

		// Token: 0x040000A1 RID: 161
		public readonly PlantableResourceCounter _plantableResourceCounter;

		// Token: 0x040000A2 RID: 162
		public readonly ILoc _loc;

		// Token: 0x040000A3 RID: 163
		public readonly string _templateName;

		// Token: 0x040000A4 RID: 164
		public readonly int _requiredAmount;

		// Token: 0x040000A5 RID: 165
		public readonly string _localizedResourceName;
	}
}
