using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200001E RID: 30
	public class PreviewWaterInputPipeBlockerService
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00003E75 File Offset: 0x00002075
		public PreviewWaterInputPipeBlockerService(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003E9C File Offset: 0x0000209C
		public void Block(IEnumerable<Vector3Int> coordinates)
		{
			this._coordinatesCache.AddRange(coordinates);
			foreach (Vector3Int item in this._coordinatesCache)
			{
				this._blockedTiles.Add(item);
			}
			this.PostChangeEvent(this._coordinatesCache);
			this._coordinatesCache.Clear();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003F18 File Offset: 0x00002118
		public void Unblock(IEnumerable<Vector3Int> coordinates)
		{
			this._coordinatesCache.AddRange(coordinates);
			foreach (Vector3Int item in this._coordinatesCache)
			{
				this._blockedTiles.Remove(item);
			}
			this.PostChangeEvent(this._coordinatesCache);
			this._coordinatesCache.Clear();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003F94 File Offset: 0x00002194
		public bool IsBlocked(Vector3Int coordinates)
		{
			return this._blockedTiles.Contains(coordinates);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003FA2 File Offset: 0x000021A2
		public void PostChangeEvent(List<Vector3Int> coordinates)
		{
			this._eventBus.Post(new PreviewBlockingCoordinatesChangedEvent(coordinates.AsReadOnlyList<Vector3Int>()));
		}

		// Token: 0x04000056 RID: 86
		public readonly EventBus _eventBus;

		// Token: 0x04000057 RID: 87
		public readonly HashSet<Vector3Int> _blockedTiles = new HashSet<Vector3Int>();

		// Token: 0x04000058 RID: 88
		public readonly List<Vector3Int> _coordinatesCache = new List<Vector3Int>();
	}
}
