using System;
using System.Collections.Generic;
using Timberborn.SteamWorkshop;
using UnityEngine;

namespace Timberborn.SteamWorkshopUI
{
	// Token: 0x02000004 RID: 4
	public interface ISteamWorkshopUploadable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		ulong? ItemId { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		string Name { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5
		bool NameIsReadOnly { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6
		string Description { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7
		SteamWorkshopVisibility Visibility { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8
		IEnumerable<string> MandatoryTags { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9
		IEnumerable<WorkshopTag> AvailableTags { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10
		IEnumerable<string> ChosenTags { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000B RID: 11
		string ContentPath { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000C RID: 12
		Texture2D Preview { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000D RID: 13
		string PreviewInfo { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000E RID: 14
		string PreviewPath { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000F RID: 15
		bool UpdateDescription { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000010 RID: 16
		bool UpdateVisibility { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000011 RID: 17
		bool UpdatePreview { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000012 RID: 18
		bool UpdateTags { get; }

		// Token: 0x06000013 RID: 19
		void RefreshPreview();

		// Token: 0x06000014 RID: 20
		bool ValidateName(string name);

		// Token: 0x06000015 RID: 21
		void OnItemCreated(ulong itemId, string name, SteamWorkshopVisibility visibility, IEnumerable<string> tags);

		// Token: 0x06000016 RID: 22
		void OnUpdateStarted(string name);

		// Token: 0x06000017 RID: 23
		void OnUpdateRequestCreated(SteamWorkshopUpdateRequest updateRequest);

		// Token: 0x06000018 RID: 24
		void OnUpdateFinished(SteamWorkshopUpdateResponse updateResponse);

		// Token: 0x06000019 RID: 25
		void Clear();
	}
}
