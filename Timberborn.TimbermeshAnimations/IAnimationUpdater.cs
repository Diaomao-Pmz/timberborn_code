using System;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200000C RID: 12
	public interface IAnimationUpdater
	{
		// Token: 0x06000039 RID: 57
		void Initialize();

		// Token: 0x0600003A RID: 58
		void SetAnimation(string animationName, bool looped);

		// Token: 0x0600003B RID: 59
		void UpdateAnimation(float normalizedTime);
	}
}
