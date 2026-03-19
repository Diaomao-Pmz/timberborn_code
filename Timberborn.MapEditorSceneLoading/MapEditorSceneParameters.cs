using System;
using Timberborn.MapRepositorySystem;
using Timberborn.SceneLoading;
using UnityEngine;

namespace Timberborn.MapEditorSceneLoading
{
	// Token: 0x02000009 RID: 9
	public class MapEditorSceneParameters : ISceneParameters
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021DA File Offset: 0x000003DA
		public bool NewMap { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021E2 File Offset: 0x000003E2
		public Vector2Int? NewMapSize { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021EA File Offset: 0x000003EA
		public MapFileReference? Map { get; }

		// Token: 0x06000012 RID: 18 RVA: 0x000021F2 File Offset: 0x000003F2
		public MapEditorSceneParameters(bool newMap, Vector2Int? newMapSize, MapFileReference? map)
		{
			this.NewMap = newMap;
			this.NewMapSize = newMapSize;
			this.Map = map;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002210 File Offset: 0x00000410
		public static MapEditorSceneParameters CreateNewMapParameters(Vector2Int mapSize)
		{
			return new MapEditorSceneParameters(true, new Vector2Int?(mapSize), null);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002234 File Offset: 0x00000434
		public static MapEditorSceneParameters CreateExistingMapParameters(MapFileReference mapFileReference)
		{
			return new MapEditorSceneParameters(false, null, new MapFileReference?(mapFileReference));
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002256 File Offset: 0x00000456
		public int SceneIndex
		{
			get
			{
				return 4;
			}
		}
	}
}
