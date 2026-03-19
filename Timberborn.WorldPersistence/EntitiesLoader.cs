using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.EntitySystem;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000007 RID: 7
	public class EntitiesLoader
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002152 File Offset: 0x00000352
		public EntitiesLoader(IEnumerable<IEntityBatchLoader> entityBatchLoaders)
		{
			this._entityBatchLoaders = entityBatchLoaders.ToImmutableArray<IEntityBatchLoader>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002166 File Offset: 0x00000366
		public void LoadAndInitialize(ICollection<InstantiatedSerializedEntity> entities)
		{
			this.Load(entities);
			this.BatchLoad(entities);
			EntitiesLoader.PreInitialize(entities);
			EntitiesLoader.Initialize(entities);
			EntitiesLoader.PostInitialize(entities);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002188 File Offset: 0x00000388
		public void Load(ICollection<InstantiatedSerializedEntity> entities)
		{
			foreach (InstantiatedSerializedEntity serializedEntity in entities)
			{
				EntitiesLoader.Load(serializedEntity);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D0 File Offset: 0x000003D0
		public void PostLoad(ICollection<InstantiatedSerializedEntity> entities)
		{
			foreach (InstantiatedSerializedEntity instantiatedSerializedEntity in entities)
			{
				instantiatedSerializedEntity.Entity.PostLoad();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000221C File Offset: 0x0000041C
		public void BatchLoad(ICollection<InstantiatedSerializedEntity> entities)
		{
			ImmutableArray<IEntityBatchLoader>.Enumerator enumerator = this._entityBatchLoaders.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.BatchLoadEntities(from entity in entities
				select entity.Entity);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002274 File Offset: 0x00000474
		public static void PreInitialize(IEnumerable<InstantiatedSerializedEntity> entities)
		{
			foreach (InstantiatedSerializedEntity instantiatedSerializedEntity in entities)
			{
				instantiatedSerializedEntity.Entity.PreInitialize();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022C0 File Offset: 0x000004C0
		public static void Initialize(IEnumerable<InstantiatedSerializedEntity> entities)
		{
			foreach (InstantiatedSerializedEntity instantiatedSerializedEntity in entities)
			{
				instantiatedSerializedEntity.Entity.Initialize();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000230C File Offset: 0x0000050C
		public static void PostInitialize(IEnumerable<InstantiatedSerializedEntity> entities)
		{
			foreach (InstantiatedSerializedEntity instantiatedSerializedEntity in entities)
			{
				instantiatedSerializedEntity.Entity.PostInitialize();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002358 File Offset: 0x00000558
		public static void Load(InstantiatedSerializedEntity serializedEntity)
		{
			foreach (IPersistentEntity persistentEntity in serializedEntity.Entity.GetComponentsAllocating<IPersistentEntity>())
			{
				EntityLoader entityLoader = new EntityLoader(serializedEntity.SerializedEntity);
				persistentEntity.Load(entityLoader);
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly ImmutableArray<IEntityBatchLoader> _entityBatchLoaders;
	}
}
