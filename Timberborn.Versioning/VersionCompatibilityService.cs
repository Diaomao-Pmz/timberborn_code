using System;

namespace Timberborn.Versioning
{
	// Token: 0x02000006 RID: 6
	public class VersionCompatibilityService
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002355 File Offset: 0x00000555
		public VersionCompatibilityService(Version currentVersion, Version softCapVersion, Version hardCapVersion)
		{
			this._currentVersion = currentVersion;
			this._softCapVersion = softCapVersion;
			this._hardCapVersion = hardCapVersion;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002372 File Offset: 0x00000572
		public bool VersionIsFullyCompatible(Version versionToCheck)
		{
			return versionToCheck.IsDevelopmentVersion || (this.VersionIsForwardCompatible(versionToCheck) && this.VersionIsBackwardCompatible(versionToCheck));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002394 File Offset: 0x00000594
		public bool VersionIsSemiCompatible(Version versionToCheck)
		{
			return versionToCheck.IsEqualOrHigherThan(this._hardCapVersion, null);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B8 File Offset: 0x000005B8
		public bool VersionIsForwardCompatible(Version versionToCheck)
		{
			if (this._currentVersion.IsDevelopmentVersion || versionToCheck.IsDevelopmentVersion)
			{
				return true;
			}
			if (!versionToCheck.IsFromSameBranch(this._currentVersion))
			{
				return this._currentVersion.IsEqualOrHigherThan(versionToCheck, null);
			}
			return this._currentVersion.IsEqualOrHigherThan(versionToCheck, new int?(VersionCompatibilityService.ForwardCompatibilityDepth));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002418 File Offset: 0x00000618
		public bool VersionIsBackwardCompatible(Version versionToCheck)
		{
			return versionToCheck.IsEqualOrHigherThan(this._softCapVersion, null);
		}

		// Token: 0x0400000D RID: 13
		public static readonly int ForwardCompatibilityDepth = 2;

		// Token: 0x0400000E RID: 14
		public readonly Version _currentVersion;

		// Token: 0x0400000F RID: 15
		public readonly Version _softCapVersion;

		// Token: 0x04000010 RID: 16
		public readonly Version _hardCapVersion;
	}
}
