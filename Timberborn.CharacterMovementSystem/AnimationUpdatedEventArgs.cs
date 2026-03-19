using System;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x02000009 RID: 9
	public readonly struct AnimationUpdatedEventArgs
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002451 File Offset: 0x00000651
		public float AnimationSpeed { get; }

		// Token: 0x06000026 RID: 38 RVA: 0x00002459 File Offset: 0x00000659
		public AnimationUpdatedEventArgs(float animationSpeed)
		{
			this.AnimationSpeed = animationSpeed;
		}
	}
}
