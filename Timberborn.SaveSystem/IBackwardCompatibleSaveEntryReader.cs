using System;
using System.IO;

namespace Timberborn.SaveSystem
{
	// Token: 0x02000004 RID: 4
	public interface IBackwardCompatibleSaveEntryReader<out T> : ISaveEntryReader<T>
	{
		// Token: 0x06000003 RID: 3
		T BackwardCompatibleRead(Stream fileStream);
	}
}
