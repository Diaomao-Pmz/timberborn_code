using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Localization;
using Timberborn.Modding;
using Timberborn.SteamWorkshop;
using Timberborn.SteamWorkshopUI;
using UnityEngine;

namespace Timberborn.SteamWorkshopModUploadingUI
{
	// Token: 0x02000009 RID: 9
	public class SteamWorkshopUploadableMod : ISteamWorkshopUploadable
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000025CD File Offset: 0x000007CD
		public SteamWorkshopUploadableMod(ILoc loc, SteamWorkshopModDataFile steamWorkshopModDataFile, Mod mod, SteamWorkshopModThumbnail steamWorkshopModThumbnail)
		{
			this._loc = loc;
			this._steamWorkshopModDataFile = steamWorkshopModDataFile;
			this._mod = mod;
			this._steamWorkshopModThumbnail = steamWorkshopModThumbnail;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000025F4 File Offset: 0x000007F4
		public ulong? ItemId
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				if (steamWorkshopItem == null)
				{
					return null;
				}
				return new ulong?(steamWorkshopItem.ItemId);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000261F File Offset: 0x0000081F
		public string Name
		{
			get
			{
				return this._mod.Manifest.Name;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002631 File Offset: 0x00000831
		public bool NameIsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002634 File Offset: 0x00000834
		public string Description
		{
			get
			{
				return this._mod.Manifest.Description;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002646 File Offset: 0x00000846
		public SteamWorkshopVisibility Visibility
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				if (((steamWorkshopItem != null) ? steamWorkshopItem.Visibility : null) == null)
				{
					return SteamWorkshopVisibility.Private;
				}
				return Enum.Parse<SteamWorkshopVisibility>(this.SteamWorkshopItem.Visibility);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000266E File Offset: 0x0000086E
		public IEnumerable<string> MandatoryTags
		{
			get
			{
				return SteamWorkshopModTags.MandatoryTags;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002675 File Offset: 0x00000875
		public IEnumerable<WorkshopTag> AvailableTags
		{
			get
			{
				return SteamWorkshopModTags.AvailableTags;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000267C File Offset: 0x0000087C
		public IEnumerable<string> ChosenTags
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				return (steamWorkshopItem != null) ? steamWorkshopItem.Tags : ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000269C File Offset: 0x0000089C
		public string ContentPath
		{
			get
			{
				return this._mod.ModDirectory.OriginPath;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026BC File Offset: 0x000008BC
		public Texture2D Preview
		{
			get
			{
				return this._steamWorkshopModThumbnail.Thumbnail;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026C9 File Offset: 0x000008C9
		public string PreviewInfo
		{
			get
			{
				return this._loc.T(SteamWorkshopUploadableMod.ThumbnailInfoLocKey);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000026DB File Offset: 0x000008DB
		public string PreviewPath
		{
			get
			{
				return this._steamWorkshopModThumbnail.GetThumbnailPath();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000026E8 File Offset: 0x000008E8
		public bool UpdateDescription
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				return steamWorkshopItem == null || steamWorkshopItem.UpdateDescription;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000026FB File Offset: 0x000008FB
		public bool UpdateVisibility
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				return steamWorkshopItem == null || steamWorkshopItem.UpdateVisibility;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000270E File Offset: 0x0000090E
		public bool UpdateTags
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				return steamWorkshopItem == null || steamWorkshopItem.UpdateTags;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002721 File Offset: 0x00000921
		public bool UpdatePreview
		{
			get
			{
				return this.SteamWorkshopItem == null || (this.SteamWorkshopItem.UpdatePreview && this.Preview);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002747 File Offset: 0x00000947
		public void RefreshPreview()
		{
			this._steamWorkshopModThumbnail.UpdateThumbnail();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002631 File Offset: 0x00000831
		public bool ValidateName(string name)
		{
			return true;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002754 File Offset: 0x00000954
		public void OnItemCreated(ulong itemId, string name, SteamWorkshopVisibility visibility, IEnumerable<string> tags)
		{
			this._steamWorkshopModDataFile.SaveSteamWorkshopItem(new SteamWorkshopItem(itemId, name, visibility.ToString(), true, true, true, true, tags));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002786 File Offset: 0x00000986
		public void OnUpdateStarted(string name)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002788 File Offset: 0x00000988
		public void OnUpdateRequestCreated(SteamWorkshopUpdateRequest updateRequest)
		{
			this._steamWorkshopModDataFile.SaveSteamWorkshopItem(SteamWorkshopItem.CreateFromUpdateRequest(updateRequest, this.Name, this.Visibility));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002786 File Offset: 0x00000986
		public void OnUpdateFinished(SteamWorkshopUpdateResponse updateResponse)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027A7 File Offset: 0x000009A7
		public void Clear()
		{
			this._steamWorkshopModThumbnail.Clear();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000027B4 File Offset: 0x000009B4
		public SteamWorkshopItem SteamWorkshopItem
		{
			get
			{
				return this._steamWorkshopModDataFile.SteamWorkshopItem;
			}
		}

		// Token: 0x0400001B RID: 27
		public static readonly string ThumbnailInfoLocKey = "Modding.ThumbnailInfo";

		// Token: 0x0400001C RID: 28
		public readonly ILoc _loc;

		// Token: 0x0400001D RID: 29
		public readonly SteamWorkshopModDataFile _steamWorkshopModDataFile;

		// Token: 0x0400001E RID: 30
		public readonly Mod _mod;

		// Token: 0x0400001F RID: 31
		public readonly SteamWorkshopModThumbnail _steamWorkshopModThumbnail;
	}
}
