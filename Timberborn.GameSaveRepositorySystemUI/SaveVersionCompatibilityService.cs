using System;
using Timberborn.SingletonSystem;
using Timberborn.Versioning;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000012 RID: 18
	public class SaveVersionCompatibilityService : ILoadableSingleton
	{
		// Token: 0x06000057 RID: 87 RVA: 0x0000307D File Offset: 0x0000127D
		public void Load()
		{
			this._versionCompatibilityService = new VersionCompatibilityService(GameVersions.CurrentVersion, GameVersions.ReadSoftCapVersionFromFile(), GameVersions.ReadHardCapSaveVersionFromFile());
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003099 File Offset: 0x00001299
		public bool VersionIsFullyCompatible(Version saveVersion)
		{
			return this._versionCompatibilityService.VersionIsFullyCompatible(saveVersion);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030A7 File Offset: 0x000012A7
		public bool VersionIsSemiCompatible(Version saveVersion)
		{
			return this._versionCompatibilityService.VersionIsSemiCompatible(saveVersion);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000030B5 File Offset: 0x000012B5
		public bool VersionIsForwardCompatible(Version saveVersion)
		{
			return this._versionCompatibilityService.VersionIsForwardCompatible(saveVersion);
		}

		// Token: 0x0400004F RID: 79
		public VersionCompatibilityService _versionCompatibilityService;
	}
}
