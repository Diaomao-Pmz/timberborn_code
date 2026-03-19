using System;
using System.IO;

namespace Timberborn.FileBrowsing
{
	// Token: 0x02000006 RID: 6
	public readonly struct DiskSystemEntry
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002595 File Offset: 0x00000795
		public string Parent { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000259D File Offset: 0x0000079D
		public string Name { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025A5 File Offset: 0x000007A5
		public string Path { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000025AD File Offset: 0x000007AD
		public bool IsDirectory { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000025B5 File Offset: 0x000007B5
		public bool Exists { get; }

		// Token: 0x06000024 RID: 36 RVA: 0x000025BD File Offset: 0x000007BD
		public DiskSystemEntry(string parent, string name, string path, bool isDirectory, bool exists)
		{
			this.Parent = parent;
			this.Name = name;
			this.Path = path;
			this.IsDirectory = isDirectory;
			this.Exists = exists;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025E4 File Offset: 0x000007E4
		public static DiskSystemEntry Create(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return new DiskSystemEntry(null, "", "", true, true);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (directoryInfo.Exists)
			{
				DirectoryInfo parent = directoryInfo.Parent;
				return new DiskSystemEntry((parent != null) ? parent.FullName : null, directoryInfo.Name, directoryInfo.FullName, true, true);
			}
			FileInfo fileInfo = new FileInfo(path);
			if (fileInfo.Exists)
			{
				DirectoryInfo directory = fileInfo.Directory;
				return new DiskSystemEntry((directory != null) ? directory.FullName : null, fileInfo.Name, fileInfo.FullName, false, true);
			}
			return new DiskSystemEntry(null, "", "", false, false);
		}
	}
}
