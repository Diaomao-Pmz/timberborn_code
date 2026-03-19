using System;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200000A RID: 10
	public class BeaverBirthStep : ITutorialStep
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000023A5 File Offset: 0x000005A5
		public BeaverBirthStep(FirstbornService firstbornService, string description)
		{
			this._firstbornService = firstbornService;
			this._description = description;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023BB File Offset: 0x000005BB
		public string Description()
		{
			return this._description;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023C3 File Offset: 0x000005C3
		public bool Achieved()
		{
			return this._firstbornService.FirstbornBorn;
		}

		// Token: 0x04000013 RID: 19
		public readonly FirstbornService _firstbornService;

		// Token: 0x04000014 RID: 20
		public readonly string _description;
	}
}
