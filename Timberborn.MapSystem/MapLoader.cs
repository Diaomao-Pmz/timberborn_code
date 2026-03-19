using System;
using System.Collections.Generic;
using Timberborn.MapRepositorySystem;
using Timberborn.MapStateSystem;
using Timberborn.WorldPersistence;
using Timberborn.WorldSerialization;
using UnityEngine;

namespace Timberborn.MapSystem
{
	// Token: 0x02000004 RID: 4
	public class MapLoader
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public MapLoader(MapDeserializer mapDeserializer, SerializedWorldFactory serializedWorldFactory)
		{
			this._mapDeserializer = mapDeserializer;
			this._serializedWorldFactory = serializedWorldFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public SerializedWorld Load(MapFileReference mapFileReference)
		{
			this.ValidateDuplicateLoad();
			return this._mapDeserializer.Load(mapFileReference);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
		public SerializedWorld LoadNewMap(Vector2Int size)
		{
			this.ValidateDuplicateLoad();
			List<ISaveableSingleton> singletons = new List<ISaveableSingleton>
			{
				MapSize.NewMap(size)
			};
			return this._serializedWorldFactory.Create(singletons);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002119 File Offset: 0x00000319
		public void ValidateDuplicateLoad()
		{
			if (this._loaded)
			{
				throw new InvalidOperationException("Cannot load a map twice in the same scene.");
			}
			this._loaded = true;
		}

		// Token: 0x04000006 RID: 6
		public readonly MapDeserializer _mapDeserializer;

		// Token: 0x04000007 RID: 7
		public readonly SerializedWorldFactory _serializedWorldFactory;

		// Token: 0x04000008 RID: 8
		public bool _loaded;
	}
}
