using System;

namespace Timberborn.SaveSystem
{
	// Token: 0x02000005 RID: 5
	public interface IOptionalSaveEntryWriter : ISaveEntryWriter
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4
		bool ShouldWrite { get; }
	}
}
