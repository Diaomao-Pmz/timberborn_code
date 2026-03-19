using System;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000007 RID: 7
	public interface IWorkshopAnimationSpeedModifier
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7
		// (remove) Token: 0x06000008 RID: 8
		event EventHandler SpeedModifierChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9
		float SpeedModifier { get; }
	}
}
