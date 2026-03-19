using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Common;

namespace Timberborn.Wandering
{
	// Token: 0x0200000C RID: 12
	public class VariedIdleAnimation : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000259C File Offset: 0x0000079C
		public VariedIdleAnimation(IRandomNumberGenerator randomNumberGenerator)
		{
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025AB File Offset: 0x000007AB
		public void Awake()
		{
			this._variedIdleAnimationSpec = base.GetComponent<VariedIdleAnimationSpec>();
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			base.GetComponent<WanderRootBehavior>().IdleStarted += this.OnIdleStarted;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025DC File Offset: 0x000007DC
		public void OnIdleStarted(object sender, EventArgs e)
		{
			ImmutableArray<string> variants = this._variedIdleAnimationSpec.Variants;
			foreach (string parameterName in variants)
			{
				this._characterAnimator.SetBool(parameterName, false);
			}
			int num = this._randomNumberGenerator.Range(0, variants.Length + 1);
			if (num < variants.Length)
			{
				this._characterAnimator.SetBool(variants[num], true);
			}
		}

		// Token: 0x04000016 RID: 22
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000017 RID: 23
		public VariedIdleAnimationSpec _variedIdleAnimationSpec;

		// Token: 0x04000018 RID: 24
		public CharacterAnimator _characterAnimator;
	}
}
