using System;
using System.IO;

namespace Timberborn.SaveSystem
{
	// Token: 0x02000006 RID: 6
	public interface ISaveEntryReader<out T>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5
		string EntryName { get; }

		// Token: 0x06000006 RID: 6
		T ReadFromSaveEntryStream(Stream entryStream);
	}
}
