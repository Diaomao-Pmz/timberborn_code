using System;

namespace Timberborn.Multithreading
{
	// Token: 0x02000007 RID: 7
	public interface IScheduledTask
	{
		// Token: 0x06000011 RID: 17
		bool AddDependent(int expectedVersion, IScheduledTask dependent);

		// Token: 0x06000012 RID: 18
		void Run(Parallelizer parallelizer);

		// Token: 0x06000013 RID: 19
		void AdvancePrerequisites(Parallelizer parallelizer);
	}
}
