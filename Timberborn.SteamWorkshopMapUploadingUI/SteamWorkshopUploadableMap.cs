using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.FileSystem;
using Timberborn.MapEditorPersistenceUI;
using Timberborn.MapMetadataSystem;
using Timberborn.MapRepositorySystem;
using Timberborn.SteamWorkshop;
using Timberborn.SteamWorkshopUI;
using UnityEngine;

namespace Timberborn.SteamWorkshopMapUploadingUI
{
	// Token: 0x02000007 RID: 7
	public class SteamWorkshopUploadableMap : ISteamWorkshopUploadable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023FF File Offset: 0x000005FF
		public IEnumerable<WorkshopTag> AvailableTags { get; } = Enumerable.Empty<WorkshopTag>();

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002407 File Offset: 0x00000607
		public IEnumerable<string> ChosenTags { get; } = Enumerable.Empty<string>();

		// Token: 0x0600001A RID: 26 RVA: 0x00002410 File Offset: 0x00000610
		public SteamWorkshopUploadableMap(SteamWorkshopMapDataService steamWorkshopMapDataService, FilenameValidator filenameValidator, MapSaverLoader mapSaverLoader, SteamWorkshopMapContent steamWorkshopMapContent, MapFileReference mapFileReference, MapMetadata mapMetadata)
		{
			this._steamWorkshopMapDataService = steamWorkshopMapDataService;
			this._mapSaverLoader = mapSaverLoader;
			this._steamWorkshopMapContent = steamWorkshopMapContent;
			this._mapFileReference = mapFileReference;
			this._mapMetadata = mapMetadata;
			this._filenameValidator = filenameValidator;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002468 File Offset: 0x00000668
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002493 File Offset: 0x00000693
		public string Name
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				return ((steamWorkshopItem != null) ? steamWorkshopItem.Name : null) ?? this._mapFileReference.Name;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024B6 File Offset: 0x000006B6
		public bool NameIsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024B9 File Offset: 0x000006B9
		public string Description
		{
			get
			{
				MapMetadata mapMetadata = this._mapMetadata;
				if (mapMetadata == null)
				{
					return null;
				}
				return mapMetadata.MapDescription;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024CC File Offset: 0x000006CC
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024F4 File Offset: 0x000006F4
		public IEnumerable<string> MandatoryTags
		{
			get
			{
				return SteamWorkshopUploadableMap.MapMandatoryTags;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000024FB File Offset: 0x000006FB
		public string ContentPath
		{
			get
			{
				return this._steamWorkshopMapContent.ContentDirectory;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002508 File Offset: 0x00000708
		public Texture2D Preview
		{
			get
			{
				return this._steamWorkshopMapContent.Thumbnail;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002515 File Offset: 0x00000715
		public string PreviewInfo
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000251C File Offset: 0x0000071C
		public string PreviewPath
		{
			get
			{
				return this._steamWorkshopMapContent.ThumbnailPath;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002529 File Offset: 0x00000729
		public bool UpdateDescription
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				return steamWorkshopItem == null || steamWorkshopItem.UpdateDescription;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000253C File Offset: 0x0000073C
		public bool UpdateVisibility
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				return steamWorkshopItem == null || steamWorkshopItem.UpdateVisibility;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000254F File Offset: 0x0000074F
		public bool UpdatePreview
		{
			get
			{
				SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
				return steamWorkshopItem == null || steamWorkshopItem.UpdatePreview;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000024B6 File Offset: 0x000006B6
		public bool UpdateTags
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002562 File Offset: 0x00000762
		public void RefreshPreview()
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002564 File Offset: 0x00000764
		public bool ValidateName(string name)
		{
			return !this._filenameValidator.NameIsInvalid(name);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002578 File Offset: 0x00000778
		public void OnItemCreated(ulong itemId, string name, SteamWorkshopVisibility visibility, IEnumerable<string> tags)
		{
			this._steamWorkshopMapDataService.SetMapData(new SteamWorkshopItem(itemId, name, visibility.ToString(), true, true, true, true, tags));
			this._mapSaverLoader.SaveCurrentSilently();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025B5 File Offset: 0x000007B5
		public void OnUpdateStarted(string name)
		{
			this._steamWorkshopMapContent.CreateTemporaryFiles(name);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002562 File Offset: 0x00000762
		public void OnUpdateRequestCreated(SteamWorkshopUpdateRequest updateRequest)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025C4 File Offset: 0x000007C4
		public void OnUpdateFinished(SteamWorkshopUpdateResponse updateResponse)
		{
			if (updateResponse.Successful)
			{
				this._steamWorkshopMapDataService.SetMapData(SteamWorkshopItem.CreateFromUpdateRequest(updateResponse.Request, this.Name, this.Visibility));
				this._mapSaverLoader.SaveCurrentSilently();
			}
			this._steamWorkshopMapContent.DeleteTemporaryFiles();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002562 File Offset: 0x00000762
		public void Clear()
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002611 File Offset: 0x00000811
		public SteamWorkshopItem SteamWorkshopItem
		{
			get
			{
				return this._steamWorkshopMapDataService.SteamWorkshopItem;
			}
		}

		// Token: 0x04000014 RID: 20
		public static readonly string[] MapMandatoryTags = new string[]
		{
			"Map"
		};

		// Token: 0x04000017 RID: 23
		public readonly SteamWorkshopMapDataService _steamWorkshopMapDataService;

		// Token: 0x04000018 RID: 24
		public readonly FilenameValidator _filenameValidator;

		// Token: 0x04000019 RID: 25
		public readonly MapSaverLoader _mapSaverLoader;

		// Token: 0x0400001A RID: 26
		public readonly SteamWorkshopMapContent _steamWorkshopMapContent;

		// Token: 0x0400001B RID: 27
		public readonly MapFileReference _mapFileReference;

		// Token: 0x0400001C RID: 28
		public readonly MapMetadata _mapMetadata;
	}
}
