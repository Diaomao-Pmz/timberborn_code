using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Common;
using Timberborn.Forestry;

namespace Timberborn.ForestryEffects
{
	// Token: 0x0200000A RID: 10
	public class TreeCutterSideRandomizer : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000023D9 File Offset: 0x000005D9
		public TreeCutterSideRandomizer(IRandomNumberGenerator randomNumberGenerator)
		{
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023E8 File Offset: 0x000005E8
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			TreeCutter component = base.GetComponent<TreeCutter>();
			component.CuttingStarted += this.RandomizeCuttingSide;
			component.CuttingStopped += this.ClearMirroredCutting;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000241F File Offset: 0x0000061F
		public void RandomizeCuttingSide(object sender, EventArgs e)
		{
			this._characterAnimator.SetBool(TreeCutterSideRandomizer.FlipAnimationParameter, this._randomNumberGenerator.CheckProbability(0.5f));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002441 File Offset: 0x00000641
		public void ClearMirroredCutting(object sender, EventArgs e)
		{
			this._characterAnimator.SetBool(TreeCutterSideRandomizer.FlipAnimationParameter, false);
		}

		// Token: 0x0400000D RID: 13
		public static readonly string FlipAnimationParameter = "CuttingFlipped";

		// Token: 0x0400000E RID: 14
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000F RID: 15
		public CharacterAnimator _characterAnimator;
	}
}
