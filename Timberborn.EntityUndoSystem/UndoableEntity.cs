using System;
using Timberborn.EntitySystem;
using Timberborn.TemplateSystem;
using Timberborn.WorldPersistence;
using Timberborn.WorldSerialization;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x0200000C RID: 12
	public class UndoableEntity : IEquatable<UndoableEntity>
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002425 File Offset: 0x00000625
		public UndoableEntity(EntityService entityService, EntityRegistry entityRegistry, TemplateNameMapper templateNameMapper, UndoableEntitiesLoader undoableEntitiesLoader, Guid guid)
		{
			this._entityService = entityService;
			this._entityRegistry = entityRegistry;
			this._templateNameMapper = templateNameMapper;
			this._undoableEntitiesLoader = undoableEntitiesLoader;
			this._guid = guid;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002452 File Offset: 0x00000652
		public void InitializeUndoableState()
		{
			if (this._serializedEntity == null)
			{
				this._serializedEntity = UndoableEntity.SerializeEntity(this.GetEntity());
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002470 File Offset: 0x00000670
		public void Delete()
		{
			EntityComponent entity;
			if (this.TryGetEntity(out entity))
			{
				this._entityService.Delete(entity);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002494 File Offset: 0x00000694
		public void Create()
		{
			if (this._serializedEntity == null)
			{
				throw new InvalidOperationException("Cannot create entity without serialized data. Guid: " + this._guid.ToString());
			}
			InstantiatedSerializedEntity entity = this.InstantiateEntity(this._serializedEntity);
			this._undoableEntitiesLoader.AddEntityForLoad(entity);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024E6 File Offset: 0x000006E6
		public void Reload()
		{
			this._undoableEntitiesLoader.Reload(new InstantiatedSerializedEntity(this.GetEntity(), this._serializedEntity));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002504 File Offset: 0x00000704
		public EntityComponent GetEntity()
		{
			EntityComponent result;
			if (this.TryGetEntity(out result))
			{
				return result;
			}
			throw new InvalidOperationException("Entity with Guid " + this._guid.ToString() + " not found.");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002548 File Offset: 0x00000748
		public bool Equals(UndoableEntity other)
		{
			if (other == null)
			{
				return false;
			}
			if (this._guid != other._guid)
			{
				return false;
			}
			if (this._serializedEntity == null || other._serializedEntity == null)
			{
				throw new InvalidOperationException("Cannot compare an uninitialized UndoableEntity. Guid: " + this._guid.ToString());
			}
			return this._serializedEntity.Equals(other._serializedEntity);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025B4 File Offset: 0x000007B4
		public bool TryGetEntity(out EntityComponent entity)
		{
			entity = this._entityRegistry.GetEntity(this._guid);
			return entity != null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025D0 File Offset: 0x000007D0
		public static SerializedEntity SerializeEntity(EntityComponent entity)
		{
			string templateName = entity.GetComponent<TemplateSpec>().TemplateName;
			SerializedEntity serializedEntity = new SerializedEntity(entity.EntityId, templateName);
			UndoableEntity.SaveEntity(entity, serializedEntity);
			return serializedEntity;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002600 File Offset: 0x00000800
		public static void SaveEntity(EntityComponent entity, SerializedEntity output)
		{
			foreach (object obj in entity.AllComponents)
			{
				IPersistentEntity persistentEntity = obj as IPersistentEntity;
				if (persistentEntity != null)
				{
					persistentEntity.Save(new EntitySaver(output));
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002664 File Offset: 0x00000864
		public InstantiatedSerializedEntity InstantiateEntity(SerializedEntity serializedEntity)
		{
			string templateName = serializedEntity.TemplateName;
			TemplateSpec template = this._templateNameMapper.GetTemplate(templateName);
			return new InstantiatedSerializedEntity(this._entityService.Instantiate(template.Blueprint, serializedEntity.Id), serializedEntity);
		}

		// Token: 0x04000017 RID: 23
		public readonly EntityService _entityService;

		// Token: 0x04000018 RID: 24
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x04000019 RID: 25
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x0400001A RID: 26
		public readonly UndoableEntitiesLoader _undoableEntitiesLoader;

		// Token: 0x0400001B RID: 27
		public readonly Guid _guid;

		// Token: 0x0400001C RID: 28
		public SerializedEntity _serializedEntity;
	}
}
