using System;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSystem
{
	// Token: 0x02000008 RID: 8
	public interface IStepDeserializer
	{
		// Token: 0x06000013 RID: 19
		bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep);
	}
}
