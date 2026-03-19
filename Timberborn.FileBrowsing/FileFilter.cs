using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using UnityEngine;

namespace Timberborn.FileBrowsing
{
	// Token: 0x0200000A RID: 10
	public class FileFilter
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public Sprite Icon { get; }

		// Token: 0x06000042 RID: 66 RVA: 0x00002AEC File Offset: 0x00000CEC
		public FileFilter(Sprite icon, IEnumerable<string> extensions)
		{
			this.Icon = icon;
			this._extensions = extensions.ToImmutableArray<string>();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B07 File Offset: 0x00000D07
		public bool IsValidFile(FileSystemInfo fileSystemInfo)
		{
			return this._extensions.Contains(fileSystemInfo.Extension);
		}

		// Token: 0x04000028 RID: 40
		public readonly ImmutableArray<string> _extensions;
	}
}
