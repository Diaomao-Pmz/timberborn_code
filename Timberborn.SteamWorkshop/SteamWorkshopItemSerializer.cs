using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.Persistence;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x0200000B RID: 11
	public class SteamWorkshopItemSerializer : IValueSerializer<SteamWorkshopItem>
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002374 File Offset: 0x00000574
		public void Serialize(SteamWorkshopItem value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(SteamWorkshopItemSerializer.ItemIdKey, value.ItemId.ToString());
			objectSaver.Set(SteamWorkshopItemSerializer.NameKey, value.Name);
			objectSaver.Set(SteamWorkshopItemSerializer.VisibilityKey, value.Visibility);
			objectSaver.Set(SteamWorkshopItemSerializer.UpdateDescriptionKey, value.UpdateDescription);
			objectSaver.Set(SteamWorkshopItemSerializer.UpdateVisibilityKey, value.UpdateVisibility);
			objectSaver.Set(SteamWorkshopItemSerializer.UpdatePreviewKey, value.UpdatePreview);
			objectSaver.Set(SteamWorkshopItemSerializer.UpdateTagsKey, value.UpdateTags);
			objectSaver.Set(SteamWorkshopItemSerializer.TagsKey, value.Tags);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000241C File Offset: 0x0000061C
		[BackwardCompatible(2025, 11, 14, Compatibility.Map | Compatibility.Mod)]
		public Obsoletable<SteamWorkshopItem> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			ulong itemId = ulong.Parse(objectLoader.Get(SteamWorkshopItemSerializer.ItemIdKey));
			string name = objectLoader.Get(SteamWorkshopItemSerializer.NameKey);
			string visibility = objectLoader.Get(SteamWorkshopItemSerializer.VisibilityKey);
			bool updateDescription = objectLoader.Get(SteamWorkshopItemSerializer.UpdateDescriptionKey);
			bool updateVisibility = objectLoader.Get(SteamWorkshopItemSerializer.UpdateVisibilityKey);
			bool updatePreview = objectLoader.Get(SteamWorkshopItemSerializer.UpdatePreviewKey);
			bool updateTags = objectLoader.Has<bool>(SteamWorkshopItemSerializer.UpdateTagsKey) && objectLoader.Get(SteamWorkshopItemSerializer.UpdateTagsKey);
			IEnumerable<string> tags;
			if (!objectLoader.Has<string>(SteamWorkshopItemSerializer.TagsKey))
			{
				IEnumerable<string> enumerable = ImmutableArray<string>.Empty;
				tags = enumerable;
			}
			else
			{
				IEnumerable<string> enumerable = objectLoader.Get(SteamWorkshopItemSerializer.TagsKey);
				tags = enumerable;
			}
			return new SteamWorkshopItem(itemId, name, visibility, updateDescription, updateVisibility, updatePreview, updateTags, tags);
		}

		// Token: 0x04000013 RID: 19
		public static readonly PropertyKey<string> ItemIdKey = new PropertyKey<string>("ItemId");

		// Token: 0x04000014 RID: 20
		public static readonly PropertyKey<string> NameKey = new PropertyKey<string>("Name");

		// Token: 0x04000015 RID: 21
		public static readonly PropertyKey<string> VisibilityKey = new PropertyKey<string>("Visibility");

		// Token: 0x04000016 RID: 22
		public static readonly PropertyKey<bool> UpdateDescriptionKey = new PropertyKey<bool>("UpdateDescription");

		// Token: 0x04000017 RID: 23
		public static readonly PropertyKey<bool> UpdateVisibilityKey = new PropertyKey<bool>("UpdateVisibility");

		// Token: 0x04000018 RID: 24
		public static readonly PropertyKey<bool> UpdatePreviewKey = new PropertyKey<bool>("UpdatePreview");

		// Token: 0x04000019 RID: 25
		public static readonly PropertyKey<bool> UpdateTagsKey = new PropertyKey<bool>("UpdateTags");

		// Token: 0x0400001A RID: 26
		public static readonly ListKey<string> TagsKey = new ListKey<string>("Tags");
	}
}
