using System;
using Timberborn.EnterableSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200000A RID: 10
	public interface ISlot
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001F RID: 31
		string Name { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32
		Enterer AssignedEnterer { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33
		bool IsAvailable { get; }

		// Token: 0x06000022 RID: 34
		void AssignEnterer(Enterer enterer);

		// Token: 0x06000023 RID: 35
		void UnassignEnterer();

		// Token: 0x06000024 RID: 36
		void Update(float deltaTime);
	}
}
