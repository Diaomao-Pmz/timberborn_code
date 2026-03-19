using System;
using System.Collections.Generic;
using Timberborn.UndoSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x0200000B RID: 11
	public class UndoableEntitiesLoader : IUndoPostprocessor
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023A9 File Offset: 0x000005A9
		public UndoableEntitiesLoader(EntitiesLoader entitiesLoader)
		{
			this._entitiesLoader = entitiesLoader;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023C3 File Offset: 0x000005C3
		public void AddEntityForLoad(InstantiatedSerializedEntity entity)
		{
			this._entitiesToLoad.Add(entity);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023D1 File Offset: 0x000005D1
		public void Reload(InstantiatedSerializedEntity entity)
		{
			this._entitiesLoader.Load(new InstantiatedSerializedEntity[]
			{
				entity
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023E8 File Offset: 0x000005E8
		public void PostprocessUndoables()
		{
			if (this._entitiesToLoad.Count > 0)
			{
				this._entitiesLoader.LoadAndInitialize(this._entitiesToLoad);
				this._entitiesLoader.PostLoad(this._entitiesToLoad);
				this._entitiesToLoad.Clear();
			}
		}

		// Token: 0x04000015 RID: 21
		public readonly EntitiesLoader _entitiesLoader;

		// Token: 0x04000016 RID: 22
		public readonly List<InstantiatedSerializedEntity> _entitiesToLoad = new List<InstantiatedSerializedEntity>();
	}
}
