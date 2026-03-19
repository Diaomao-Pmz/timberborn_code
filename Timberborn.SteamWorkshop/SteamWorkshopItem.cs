using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x02000008 RID: 8
	public class SteamWorkshopItem
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021CA File Offset: 0x000003CA
		public ulong ItemId { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021D2 File Offset: 0x000003D2
		public string Name { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021DA File Offset: 0x000003DA
		public string Visibility { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021E2 File Offset: 0x000003E2
		public bool UpdateDescription { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021EA File Offset: 0x000003EA
		public bool UpdateVisibility { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021F2 File Offset: 0x000003F2
		public bool UpdatePreview { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021FA File Offset: 0x000003FA
		public bool UpdateTags { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002202 File Offset: 0x00000402
		public ImmutableArray<string> Tags { get; }

		// Token: 0x06000017 RID: 23 RVA: 0x0000220C File Offset: 0x0000040C
		public SteamWorkshopItem(ulong itemId, string name, string visibility, bool updateDescription, bool updateVisibility, bool updatePreview, bool updateTags, IEnumerable<string> tags)
		{
			this.ItemId = itemId;
			this.Name = name;
			this.Visibility = visibility;
			this.UpdateDescription = updateDescription;
			this.UpdateVisibility = updateVisibility;
			this.UpdatePreview = updatePreview;
			this.UpdateTags = updateTags;
			this.Tags = tags.ToImmutableArray<string>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002264 File Offset: 0x00000464
		public static SteamWorkshopItem CreateFromUpdateRequest(SteamWorkshopUpdateRequest updateRequest, string defaultName, SteamWorkshopVisibility defaultVisibility)
		{
			return new SteamWorkshopItem(updateRequest.ItemId, updateRequest.Name ?? defaultName, (updateRequest.Visibility ?? defaultVisibility).ToString(), updateRequest.Description != null, updateRequest.Visibility != null, updateRequest.PreviewPath != null, updateRequest.MandatoryTags.Length > 0 || updateRequest.ChosenTags.Length > 0, updateRequest.ChosenTags);
		}
	}
}
