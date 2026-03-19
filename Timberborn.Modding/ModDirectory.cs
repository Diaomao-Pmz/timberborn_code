using System;
using System.IO;
using Timberborn.Versioning;

namespace Timberborn.Modding
{
	// Token: 0x02000010 RID: 16
	public readonly struct ModDirectory
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002B16 File Offset: 0x00000D16
		public DirectoryInfo Directory { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002B1E File Offset: 0x00000D1E
		public bool IsUserMod { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002B26 File Offset: 0x00000D26
		public string DisplaySource { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002B2E File Offset: 0x00000D2E
		public Version GameVersion { get; }

		// Token: 0x06000045 RID: 69 RVA: 0x00002B36 File Offset: 0x00000D36
		public ModDirectory(DirectoryInfo directory, bool isUserMod, string displaySource, Version gameVersion, bool isSubdirectory)
		{
			this.Directory = directory;
			this.IsUserMod = isUserMod;
			this.DisplaySource = displaySource;
			this.GameVersion = gameVersion;
			this._isSubdirectory = isSubdirectory;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002B5D File Offset: 0x00000D5D
		public string Path
		{
			get
			{
				return this.Directory.FullName;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002B6A File Offset: 0x00000D6A
		public string OriginPath
		{
			get
			{
				if (!this._isSubdirectory)
				{
					return this.Path;
				}
				return this.Directory.Parent.FullName;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002B8B File Offset: 0x00000D8B
		public string OriginName
		{
			get
			{
				if (!this._isSubdirectory)
				{
					return this.Directory.Name;
				}
				return this.Directory.Parent.Name;
			}
		}

		// Token: 0x04000029 RID: 41
		public readonly bool _isSubdirectory;
	}
}
