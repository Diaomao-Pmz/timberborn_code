using System;
using Timberborn.Forestry;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000035 RID: 53
	public class MarkTreesTutorialStep : ITutorialStep
	{
		// Token: 0x06000178 RID: 376 RVA: 0x000052E5 File Offset: 0x000034E5
		public MarkTreesTutorialStep(TreeCuttingArea treeCuttingArea, string description)
		{
			this._treeCuttingArea = treeCuttingArea;
			this._description = description;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000052FB File Offset: 0x000034FB
		public string Description()
		{
			return this._description;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005303 File Offset: 0x00003503
		public bool Achieved()
		{
			return this._treeCuttingArea.AnyYielderSelected;
		}

		// Token: 0x040000AC RID: 172
		public readonly TreeCuttingArea _treeCuttingArea;

		// Token: 0x040000AD RID: 173
		public readonly string _description;
	}
}
