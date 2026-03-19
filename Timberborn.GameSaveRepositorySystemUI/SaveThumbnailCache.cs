using System;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.SaveThumbnail;
using Timberborn.SingletonSystem;
using Timberborn.ThumbnailSystem;
using UnityEngine;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000011 RID: 17
	public class SaveThumbnailCache : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00003017 File Offset: 0x00001217
		public SaveThumbnailCache(GameSaveDeserializer gameSaveDeserializer, SaveThumbnailSaveEntryReader saveThumbnailSaveEntryReader)
		{
			this._gameSaveDeserializer = gameSaveDeserializer;
			this._saveThumbnailSaveEntryReader = saveThumbnailSaveEntryReader;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000302D File Offset: 0x0000122D
		public void Load()
		{
			this._thumbnailCache = new ThumbnailCache<SaveReference>((SaveReference save) => this._gameSaveDeserializer.ReadFromSaveFile<Texture2D>(save, this._saveThumbnailSaveEntryReader));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003046 File Offset: 0x00001246
		public void Unload()
		{
			this.Clear();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000304E File Offset: 0x0000124E
		public void Clear()
		{
			this._thumbnailCache.Clear();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000305B File Offset: 0x0000125B
		public Texture2D GetThumbnail(SaveReference saveReference)
		{
			return this._thumbnailCache.GetThumbnail(saveReference);
		}

		// Token: 0x0400004C RID: 76
		public readonly GameSaveDeserializer _gameSaveDeserializer;

		// Token: 0x0400004D RID: 77
		public readonly SaveThumbnailSaveEntryReader _saveThumbnailSaveEntryReader;

		// Token: 0x0400004E RID: 78
		public ThumbnailCache<SaveReference> _thumbnailCache;
	}
}
