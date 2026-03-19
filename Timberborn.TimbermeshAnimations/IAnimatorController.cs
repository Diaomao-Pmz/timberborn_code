using System;
using System.Collections.Generic;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200000E RID: 14
	public interface IAnimatorController
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004A RID: 74
		IEnumerable<string> AnimationNames { get; }

		// Token: 0x0600004B RID: 75
		bool HasParameter(string parameterName);

		// Token: 0x0600004C RID: 76
		void SetFloat(string parameterName, float value);

		// Token: 0x0600004D RID: 77
		void SetBool(string parameterName, bool state);

		// Token: 0x0600004E RID: 78
		void Enable();

		// Token: 0x0600004F RID: 79
		void Disable();
	}
}
