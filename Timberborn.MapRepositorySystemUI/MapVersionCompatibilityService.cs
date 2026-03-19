using System;
using Timberborn.MapRepositorySystem;
using Timberborn.SingletonSystem;
using Timberborn.Versioning;
using Timberborn.VersioningSerialization;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x02000011 RID: 17
	public class MapVersionCompatibilityService : ILoadableSingleton
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002B61 File Offset: 0x00000D61
		public MapVersionCompatibilityService(MapDeserializer mapDeserializer, VersionSerializer versionSerializer)
		{
			this._mapDeserializer = mapDeserializer;
			this._versionSerializer = versionSerializer;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B77 File Offset: 0x00000D77
		public void Load()
		{
			this._versionCompatibilityService = new VersionCompatibilityService(GameVersions.CurrentVersion, GameVersions.ReadSoftCapVersionFromFile(), GameVersions.ReadHardCapMapVersionFromFile());
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B94 File Offset: 0x00000D94
		public bool IsMapFullyCompatible(MapFileReference mapFileReference)
		{
			Version mapVersionNumber = this.GetMapVersionNumber(mapFileReference);
			return this.VersionIsFullyCompatible(mapVersionNumber);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public Version GetMapVersionNumber(MapFileReference mapFileReference)
		{
			return this._mapDeserializer.ReadFromMapFileUnsafe<Version>(mapFileReference, this._versionSerializer);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public bool VersionIsFullyCompatible(Version mapVersion)
		{
			return this._versionCompatibilityService.VersionIsFullyCompatible(mapVersion);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public bool VersionIsSemiCompatible(Version mapVersion)
		{
			return this._versionCompatibilityService.VersionIsSemiCompatible(mapVersion);
		}

		// Token: 0x04000034 RID: 52
		public readonly MapDeserializer _mapDeserializer;

		// Token: 0x04000035 RID: 53
		public readonly VersionSerializer _versionSerializer;

		// Token: 0x04000036 RID: 54
		public VersionCompatibilityService _versionCompatibilityService;
	}
}
