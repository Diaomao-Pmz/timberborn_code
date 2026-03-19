using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.ThumbnailSystem
{
	// Token: 0x02000005 RID: 5
	public class ThumbnailCache<TKey>
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020BE File Offset: 0x000002BE
		public ThumbnailCache(Func<TKey, Texture2D> thumbnailGetter)
		{
			this._thumbnailGetter = thumbnailGetter;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020D8 File Offset: 0x000002D8
		public Texture2D GetThumbnail(TKey key)
		{
			Texture2D texture2D;
			if (this._cache.TryGetValue(key, out texture2D))
			{
				return texture2D;
			}
			texture2D = this._thumbnailGetter(key);
			this._cache.Add(key, texture2D);
			return texture2D;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		public void Clear()
		{
			foreach (Texture2D texture2D in this._cache.Values)
			{
				if (texture2D)
				{
					Object.Destroy(texture2D);
				}
			}
			this._cache.Clear();
		}

		// Token: 0x04000006 RID: 6
		public readonly Func<TKey, Texture2D> _thumbnailGetter;

		// Token: 0x04000007 RID: 7
		public readonly Dictionary<TKey, Texture2D> _cache = new Dictionary<TKey, Texture2D>();
	}
}
