using System;
using Timberborn.SteamWorkshopUI;

namespace Timberborn.SteamWorkshopModUploadingUI
{
	// Token: 0x02000005 RID: 5
	public static class SteamWorkshopModTags
	{
		// Token: 0x0400000B RID: 11
		public static readonly string[] MandatoryTags = new string[]
		{
			"Mod"
		};

		// Token: 0x0400000C RID: 12
		public static readonly WorkshopTagCategory CompatibilityCategory = new WorkshopTagCategory("Compatibility", 0);

		// Token: 0x0400000D RID: 13
		public static readonly WorkshopTagCategory TypeCategory = new WorkshopTagCategory("Type", 10);

		// Token: 0x0400000E RID: 14
		public static readonly WorkshopTagCategory ContentCategory = new WorkshopTagCategory("Content", 20);

		// Token: 0x0400000F RID: 15
		public static readonly WorkshopTag[] AvailableTags = new WorkshopTag[]
		{
			new WorkshopTag(SteamWorkshopModTags.CompatibilityCategory, "Update 1.0", 0),
			new WorkshopTag(SteamWorkshopModTags.CompatibilityCategory, "Update 0.7", 10),
			new WorkshopTag(SteamWorkshopModTags.CompatibilityCategory, "Update 0.6", 20),
			new WorkshopTag(SteamWorkshopModTags.TypeCategory, "New content", 0),
			new WorkshopTag(SteamWorkshopModTags.TypeCategory, "Quality of life", 10),
			new WorkshopTag(SteamWorkshopModTags.TypeCategory, "Balance", 20),
			new WorkshopTag(SteamWorkshopModTags.TypeCategory, "Cheats", 30),
			new WorkshopTag(SteamWorkshopModTags.TypeCategory, "Visuals", 40),
			new WorkshopTag(SteamWorkshopModTags.TypeCategory, "Modding tools", 50),
			new WorkshopTag(SteamWorkshopModTags.TypeCategory, "Other", 60),
			new WorkshopTag(SteamWorkshopModTags.ContentCategory, "Buildings", 0),
			new WorkshopTag(SteamWorkshopModTags.ContentCategory, "Plants", 10),
			new WorkshopTag(SteamWorkshopModTags.ContentCategory, "Goods", 20),
			new WorkshopTag(SteamWorkshopModTags.ContentCategory, "Factions", 30),
			new WorkshopTag(SteamWorkshopModTags.ContentCategory, "Outfits", 40),
			new WorkshopTag(SteamWorkshopModTags.ContentCategory, "Decals", 50),
			new WorkshopTag(SteamWorkshopModTags.ContentCategory, "Translations", 60),
			new WorkshopTag(SteamWorkshopModTags.ContentCategory, "Miscellaneous", 70)
		};
	}
}
