using System;
using System.IO;

namespace Timberborn.MapRepositorySystem
{
	// Token: 0x02000005 RID: 5
	public readonly struct MapFileReference : IEquatable<MapFileReference>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021D4 File Offset: 0x000003D4
		public string Name { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021DC File Offset: 0x000003DC
		public string Path { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021E4 File Offset: 0x000003E4
		public bool Resource { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021EC File Offset: 0x000003EC
		public bool UserFolder { get; }

		// Token: 0x0600000B RID: 11 RVA: 0x000021F4 File Offset: 0x000003F4
		public MapFileReference(string name, string path, bool resource, bool userFolder)
		{
			this.Name = name;
			this.Path = path;
			this.Resource = resource;
			this.UserFolder = userFolder;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002213 File Offset: 0x00000413
		public static MapFileReference FromResource(string name)
		{
			return new MapFileReference(name, string.Empty, true, false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002222 File Offset: 0x00000422
		public static MapFileReference FromUserFolder(string name)
		{
			return new MapFileReference(name, string.Empty, false, true);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002231 File Offset: 0x00000431
		public static MapFileReference FromDisk(string path)
		{
			return new MapFileReference(System.IO.Path.GetFileNameWithoutExtension(path), path, false, false);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002244 File Offset: 0x00000444
		public bool Equals(MapFileReference other)
		{
			return this.Name == other.Name && this.Path == other.Path && this.Resource == other.Resource && this.UserFolder == other.UserFolder;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000229C File Offset: 0x0000049C
		public override bool Equals(object obj)
		{
			if (obj is MapFileReference)
			{
				MapFileReference other = (MapFileReference)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C4 File Offset: 0x000004C4
		public override int GetHashCode()
		{
			return ((((this.Name != null) ? this.Name.GetHashCode() : 0) * 397 ^ ((this.Path != null) ? this.Path.GetHashCode() : 0)) * 397 ^ this.Resource.GetHashCode()) * 397 ^ this.UserFolder.GetHashCode();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002330 File Offset: 0x00000530
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}, {4}: {5}", new object[]
			{
				"Name",
				this.Name,
				"Path",
				this.Path,
				"Resource",
				this.Resource
			});
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002385 File Offset: 0x00000585
		public static bool operator ==(MapFileReference left, MapFileReference right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000238F File Offset: 0x0000058F
		public static bool operator !=(MapFileReference left, MapFileReference right)
		{
			return !left.Equals(right);
		}
	}
}
