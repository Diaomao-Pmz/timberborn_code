using System;
using Timberborn.Versioning;

namespace Timberborn.Modding
{
	// Token: 0x02000020 RID: 32
	public class VersionedMod
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003B97 File Offset: 0x00001D97
		public string Id { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003B9F File Offset: 0x00001D9F
		public Version MinimumVersion { get; }

		// Token: 0x060000B3 RID: 179 RVA: 0x00003BA7 File Offset: 0x00001DA7
		public VersionedMod(string id, Version minimumVersion)
		{
			this.Id = id;
			this.MinimumVersion = minimumVersion;
		}
	}
}
