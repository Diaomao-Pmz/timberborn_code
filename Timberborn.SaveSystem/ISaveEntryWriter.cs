using System;
using System.IO;

namespace Timberborn.SaveSystem
{
	// Token: 0x02000007 RID: 7
	public interface ISaveEntryWriter
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		string EntryName { get; }

		// Token: 0x06000008 RID: 8
		void WriteToSaveEntryStream(Stream entryStream);
	}
}
