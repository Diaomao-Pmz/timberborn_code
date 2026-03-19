using System;
using System.Collections.Generic;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.Versioning;
using Timberborn.WorldSerialization;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000018 RID: 24
	public class SerializedWorldFactory
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002697 File Offset: 0x00000897
		public SerializedWorldFactory(TemplateNameRetriever templateNameRetriever, EntityRegistry entityRegistry, ISingletonRepository singletonRepository)
		{
			this._templateNameRetriever = templateNameRetriever;
			this._entityRegistry = entityRegistry;
			this._singletonRepository = singletonRepository;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026B4 File Offset: 0x000008B4
		public SerializedWorld Create()
		{
			SerializedWorld serializedWorld = new SerializedWorld(GameVersions.CurrentVersion);
			this.SaveEntities(serializedWorld);
			this.SaveSingletons(serializedWorld);
			return serializedWorld;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026DC File Offset: 0x000008DC
		public SerializedWorld Create(IEnumerable<ISaveableSingleton> singletons)
		{
			SerializedWorld serializedWorld = new SerializedWorld(GameVersions.CurrentVersion);
			this.SaveSingletons(serializedWorld, singletons);
			return serializedWorld;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002700 File Offset: 0x00000900
		public void SaveEntities(SerializedWorld serializedWorld)
		{
			foreach (EntityComponent entityComponent in this._entityRegistry.Entities)
			{
				string templateName = this._templateNameRetriever.GetTemplateName(entityComponent);
				SerializedEntity serializedEntity = new SerializedEntity(entityComponent.GetComponent<EntityComponent>().EntityId, templateName);
				this.SaveEntity(entityComponent, serializedEntity);
				if (serializedEntity.HasComponents())
				{
					serializedWorld.AddEntity(serializedEntity);
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002790 File Offset: 0x00000990
		public void SaveEntity(EntityComponent entity, SerializedEntity output)
		{
			foreach (object obj in entity.AllComponents)
			{
				if (obj is ISaveableSingleton)
				{
					throw new ArgumentException("An entity must not implement ISaveableSingleton: " + obj.GetType().Name);
				}
				IPersistentEntity persistentEntity = obj as IPersistentEntity;
				if (persistentEntity != null)
				{
					persistentEntity.Save(new EntitySaver(output));
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002818 File Offset: 0x00000A18
		public void SaveSingletons(SerializedWorld serializedWorld)
		{
			this.SaveSingletons(serializedWorld, this._singletonRepository.GetSingletons<ISaveableSingleton>());
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000282C File Offset: 0x00000A2C
		public void SaveSingletons(SerializedWorld serializedWorld, IEnumerable<ISaveableSingleton> singletons)
		{
			SingletonSaver singletonSaver = new SingletonSaver(serializedWorld);
			foreach (ISaveableSingleton saveableSingleton in singletons)
			{
				saveableSingleton.Save(singletonSaver);
			}
		}

		// Token: 0x04000014 RID: 20
		public readonly TemplateNameRetriever _templateNameRetriever;

		// Token: 0x04000015 RID: 21
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x04000016 RID: 22
		public readonly ISingletonRepository _singletonRepository;
	}
}
