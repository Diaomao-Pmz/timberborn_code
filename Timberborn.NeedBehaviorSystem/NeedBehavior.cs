using System;
using Timberborn.BehaviorSystem;
using Timberborn.EntitySystem;
using Timberborn.NeedSystem;
using UnityEngine;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200001A RID: 26
	public abstract class NeedBehavior : Behavior, IRegisteredComponent
	{
		// Token: 0x06000070 RID: 112
		public abstract Vector3? ActionPosition(NeedManager needManager);

		// Token: 0x06000071 RID: 113 RVA: 0x0000307C File Offset: 0x0000127C
		public NeedBehavior()
		{
		}
	}
}
