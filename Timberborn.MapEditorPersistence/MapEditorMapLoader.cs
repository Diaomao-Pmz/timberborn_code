using System;
using Timberborn.MapRepositorySystem;
using Timberborn.MapSystem;
using Timberborn.WorldSerialization;
using UnityEngine;

namespace Timberborn.MapEditorPersistence
{
	// Token: 0x02000003 RID: 3
	public class MapEditorMapLoader
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C3 File Offset: 0x000002C3
		public MapFileReference? LoadedMap { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CC File Offset: 0x000002CC
		public MapEditorMapLoader(MapLoader mapLoader)
		{
			this._mapLoader = mapLoader;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DB File Offset: 0x000002DB
		public SerializedWorld Load(MapFileReference mapFileReference)
		{
			Debug.Log(string.Format("Loading map from {0} at {1:u}", mapFileReference, DateTime.Now));
			this.LoadedMap = new MapFileReference?(mapFileReference);
			return this._mapLoader.Load(mapFileReference);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002114 File Offset: 0x00000314
		public SerializedWorld LoadNew(Vector2Int size)
		{
			Debug.Log(string.Format("Creating new {0}x{1} map at {2:u}", size.x, size.y, DateTime.Now));
			return this._mapLoader.LoadNewMap(size);
		}

		// Token: 0x04000002 RID: 2
		private readonly MapLoader _mapLoader;
	}
}
