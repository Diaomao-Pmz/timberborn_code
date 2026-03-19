using System;

namespace Timberborn.Multithreading
{
	// Token: 0x02000009 RID: 9
	public interface ITaskRunner
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23
		int ExpectedRuns { get; }

		// Token: 0x06000018 RID: 24
		void Run(int runIndex);
	}
}
