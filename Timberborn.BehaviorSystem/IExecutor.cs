using System;
using Timberborn.WorldPersistence;

namespace Timberborn.BehaviorSystem
{
	// Token: 0x0200000E RID: 14
	public interface IExecutor
	{
		// Token: 0x06000033 RID: 51
		ExecutorStatus Tick(float deltaTimeInHours);

		// Token: 0x06000034 RID: 52
		void Save(IEntitySaver entitySaver);

		// Token: 0x06000035 RID: 53
		void Load(IEntityLoader entityLoader);
	}
}
