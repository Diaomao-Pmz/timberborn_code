using System;
using System.IO;
using Timberborn.SerializationSystem;

namespace Timberborn.ModdingAssets
{
	// Token: 0x02000004 RID: 4
	public interface IModFileConverter<T>
	{
		// Token: 0x06000003 RID: 3
		bool CanConvert(FileInfo fileInfo);

		// Token: 0x06000004 RID: 4
		bool TryConvert(OrderedFile orderedFile, string path, SerializedObject metadata, out T asset);

		// Token: 0x06000005 RID: 5
		void Reset();
	}
}
