using System;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Timberborn.Versioning
{
	// Token: 0x02000004 RID: 4
	public static class GameVersions
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public static Version CurrentVersion { get; } = Version.Create(Application.version);

		// Token: 0x06000004 RID: 4 RVA: 0x000020C2 File Offset: 0x000002C2
		public static Version ReadCurrentVersionFromFile()
		{
			return GameVersions.ReadVersionFromFile("CurrentVersion");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public static Version ReadSoftCapVersionFromFile()
		{
			return GameVersions.ReadVersionFromFile("SoftCapVersion");
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DA File Offset: 0x000002DA
		public static Version ReadHardCapSaveVersionFromFile()
		{
			return GameVersions.ReadVersionFromFile("HardCapSaveVersion");
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E6 File Offset: 0x000002E6
		public static Version ReadHardCapMapVersionFromFile()
		{
			return GameVersions.ReadVersionFromFile("HardCapMapVersion");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F2 File Offset: 0x000002F2
		public static Version ReadVersionFromFile(string versionType)
		{
			return Version.Create(JObject.Parse(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, GameVersions.VersionsFileName))).Value<string>(versionType));
		}

		// Token: 0x04000006 RID: 6
		public static readonly string VersionsFileName = "VersionNumbers.json";
	}
}
