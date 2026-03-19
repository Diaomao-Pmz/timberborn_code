using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Timberborn.Modding
{
	// Token: 0x0200000E RID: 14
	public static class ModdedState
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000029A9 File Offset: 0x00000BA9
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000029B0 File Offset: 0x00000BB0
		public static bool HasOfficialMods { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000029B8 File Offset: 0x00000BB8
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000029BF File Offset: 0x00000BBF
		public static bool HasUnofficialMods { get; private set; }

		// Token: 0x0600003B RID: 59 RVA: 0x000029C8 File Offset: 0x00000BC8
		public static void SetOfficialMods(IEnumerable<Mod> mods)
		{
			ModdedState.HasOfficialMods = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("official");
			foreach (Mod mod in mods)
			{
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"- ",
					mod.Manifest.Name,
					" (",
					mod.Manifest.Version.Formatted,
					")"
				}));
			}
			ModdedState.LogModded(stringBuilder.ToString());
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A7C File Offset: 0x00000C7C
		public static void SetUnofficialMods()
		{
			if (!ModdedState.HasUnofficialMods)
			{
				ModdedState.HasUnofficialMods = true;
				ModdedState.LogModded("unofficial");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A95 File Offset: 0x00000C95
		public static bool IsModded
		{
			get
			{
				return ModdedState.HasOfficialMods || ModdedState.HasUnofficialMods;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AA5 File Offset: 0x00000CA5
		public static void LogModded(string description)
		{
			Debug.Log("Modded: true, " + description);
		}
	}
}
