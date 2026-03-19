using System;
using Timberborn.TickSystem;

namespace Timberborn.GameScene
{
	// Token: 0x02000009 RID: 9
	public class GameSceneTickingMode : ITickingMode
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000023F4 File Offset: 0x000005F4
		public bool SingletonIsActiveInThisMode(object singleton)
		{
			return true;
		}
	}
}
