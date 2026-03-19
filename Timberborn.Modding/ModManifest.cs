using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Versioning;

namespace Timberborn.Modding
{
	// Token: 0x02000012 RID: 18
	public class ModManifest
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002C5A File Offset: 0x00000E5A
		public string Name { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002C62 File Offset: 0x00000E62
		public string Description { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002C6A File Offset: 0x00000E6A
		public Version Version { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002C72 File Offset: 0x00000E72
		public string Id { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002C7A File Offset: 0x00000E7A
		public Version MinimumGameVersion { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002C82 File Offset: 0x00000E82
		public ImmutableArray<VersionedMod> RequiredMods { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002C8A File Offset: 0x00000E8A
		public ImmutableArray<VersionedMod> OptionalMods { get; }

		// Token: 0x06000054 RID: 84 RVA: 0x00002C94 File Offset: 0x00000E94
		public ModManifest(string name, string description, Version version, string id, Version minimumGameVersion, IEnumerable<VersionedMod> requiredMods, IEnumerable<VersionedMod> optionalMods)
		{
			this.Name = name;
			this.Description = description;
			this.Version = version;
			this.Id = id;
			this.MinimumGameVersion = minimumGameVersion;
			this.RequiredMods = requiredMods.ToImmutableArray<VersionedMod>();
			this.OptionalMods = optionalMods.ToImmutableArray<VersionedMod>();
		}
	}
}
