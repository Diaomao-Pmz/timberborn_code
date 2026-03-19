using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x0200000F RID: 15
	public class SteamWorkshopUpdateRequest
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000272F File Offset: 0x0000092F
		public ulong ItemId { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002737 File Offset: 0x00000937
		public string Name { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000273F File Offset: 0x0000093F
		public string Description { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002747 File Offset: 0x00000947
		public SteamWorkshopVisibility? Visibility { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000274F File Offset: 0x0000094F
		public ImmutableArray<string> MandatoryTags { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002757 File Offset: 0x00000957
		public ImmutableArray<string> ChosenTags { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000275F File Offset: 0x0000095F
		public string PreviewPath { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002767 File Offset: 0x00000967
		public string ContentPath { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000276F File Offset: 0x0000096F
		public string Changelog { get; }

		// Token: 0x06000033 RID: 51 RVA: 0x00002778 File Offset: 0x00000978
		public SteamWorkshopUpdateRequest(ulong itemId, string name, string description, SteamWorkshopVisibility? visibility, IEnumerable<string> mandatoryTags, IEnumerable<string> chosenTags, string previewPath, string contentPath, string changelog)
		{
			this.ItemId = itemId;
			this.Name = name;
			this.Description = description;
			this.Visibility = visibility;
			this.MandatoryTags = mandatoryTags.ToImmutableArray<string>();
			this.ChosenTags = chosenTags.ToImmutableArray<string>();
			this.PreviewPath = previewPath;
			this.ContentPath = contentPath;
			this.Changelog = changelog;
		}

		// Token: 0x02000010 RID: 16
		public class Builder
		{
			// Token: 0x06000034 RID: 52 RVA: 0x000027DA File Offset: 0x000009DA
			public Builder(ulong itemId, string name)
			{
				this._itemId = itemId;
				this._name = name;
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00002806 File Offset: 0x00000A06
			public void SetDescription(string description)
			{
				this._description = description;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x0000280F File Offset: 0x00000A0F
			public void SetVisibility(SteamWorkshopVisibility? visibility)
			{
				this._visibility = visibility;
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002818 File Offset: 0x00000A18
			public SteamWorkshopUpdateRequest.Builder AddMandatoryTags(IEnumerable<string> tags)
			{
				foreach (string item in tags)
				{
					this._mandatoryTags.Add(item);
				}
				return this;
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002868 File Offset: 0x00000A68
			public SteamWorkshopUpdateRequest.Builder AddChosenTags(IEnumerable<string> tags)
			{
				foreach (string item in tags)
				{
					this._chosenTags.Add(item);
				}
				return this;
			}

			// Token: 0x06000039 RID: 57 RVA: 0x000028B8 File Offset: 0x00000AB8
			public void SetPreviewPath(string previewPath)
			{
				this._previewPath = previewPath;
			}

			// Token: 0x0600003A RID: 58 RVA: 0x000028C1 File Offset: 0x00000AC1
			public SteamWorkshopUpdateRequest.Builder SetContentPath(string contentPath)
			{
				this._contentPath = contentPath;
				return this;
			}

			// Token: 0x0600003B RID: 59 RVA: 0x000028CB File Offset: 0x00000ACB
			public void SetChangelog(string changelog)
			{
				this._changelog = changelog;
			}

			// Token: 0x0600003C RID: 60 RVA: 0x000028D4 File Offset: 0x00000AD4
			public SteamWorkshopUpdateRequest Build()
			{
				return new SteamWorkshopUpdateRequest(this._itemId, this._name, this._description, this._visibility, this._mandatoryTags, this._chosenTags, this._previewPath, this._contentPath, this._changelog);
			}

			// Token: 0x04000027 RID: 39
			public readonly ulong _itemId;

			// Token: 0x04000028 RID: 40
			public readonly string _name;

			// Token: 0x04000029 RID: 41
			public string _description;

			// Token: 0x0400002A RID: 42
			public SteamWorkshopVisibility? _visibility;

			// Token: 0x0400002B RID: 43
			public readonly HashSet<string> _mandatoryTags = new HashSet<string>();

			// Token: 0x0400002C RID: 44
			public readonly HashSet<string> _chosenTags = new HashSet<string>();

			// Token: 0x0400002D RID: 45
			public string _previewPath;

			// Token: 0x0400002E RID: 46
			public string _contentPath;

			// Token: 0x0400002F RID: 47
			public string _changelog;
		}
	}
}
