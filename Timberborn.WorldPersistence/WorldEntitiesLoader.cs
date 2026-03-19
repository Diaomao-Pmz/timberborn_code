using System;
using System.Collections.Generic;
using Timberborn.EntitySystem;
using Timberborn.ErrorReporting;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.WorldSerialization;
using UnityEngine;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200001B RID: 27
	public class WorldEntitiesLoader : INonSingletonLoader, INonSingletonPostLoader
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000028B5 File Offset: 0x00000AB5
		public WorldEntitiesLoader(ISerializedWorldSupplier serializedWorldSupplier, EntityService entityService, TemplateNameMapper templateNameMapper, ILoadingIssueService loadingIssueService, EntitiesLoader entitiesLoader)
		{
			this._serializedWorldSupplier = serializedWorldSupplier;
			this._entityService = entityService;
			this._templateNameMapper = templateNameMapper;
			this._loadingIssueService = loadingIssueService;
			this._entitiesLoader = entitiesLoader;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000028E4 File Offset: 0x00000AE4
		public void LoadNonSingletons()
		{
			SerializedWorld serializedWorld = this._serializedWorldSupplier.Get();
			this._instantiatedSerializedEntities = this.InstantiateEntities(serializedWorld);
			this._entitiesLoader.LoadAndInitialize(this._instantiatedSerializedEntities);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000291B File Offset: 0x00000B1B
		public void PostLoadNonSingletons()
		{
			this._entitiesLoader.PostLoad(this._instantiatedSerializedEntities);
			this._instantiatedSerializedEntities = null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002938 File Offset: 0x00000B38
		public List<InstantiatedSerializedEntity> InstantiateEntities(SerializedWorld serializedWorld)
		{
			List<InstantiatedSerializedEntity> list = new List<InstantiatedSerializedEntity>();
			foreach (SerializedEntity serializedEntity in serializedWorld.Entities())
			{
				this.InstantiateEntity(serializedEntity, list);
			}
			return list;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002990 File Offset: 0x00000B90
		public void InstantiateEntity(SerializedEntity serializedEntity, ICollection<InstantiatedSerializedEntity> instantiatedSerializedEntities)
		{
			string templateName = serializedEntity.TemplateName;
			EntityComponent entity;
			if (this.TryInstantiateEntity(templateName, serializedEntity.Id, out entity))
			{
				instantiatedSerializedEntities.Add(new InstantiatedSerializedEntity(entity, serializedEntity));
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029C4 File Offset: 0x00000BC4
		public bool TryInstantiateEntity(string templateName, Guid id, out EntityComponent instance)
		{
			try
			{
				TemplateSpec template = this._templateNameMapper.GetTemplate(templateName);
				if (template.UsableWithCurrentFeatureToggles)
				{
					instance = this._entityService.Instantiate(template.Blueprint, id);
					return true;
				}
				Debug.LogWarning("Failed to instantiate '" + templateName + "', because it's not usable with current feature toggles");
			}
			catch (TemplateMappingException arg)
			{
				this._loadingIssueService.AddIssue(string.Format("Failed to instantiate '{0}': {1}", templateName, arg), WorldEntitiesLoader.TemplateNotFoundIssueLocKey, templateName, false);
			}
			instance = null;
			return false;
		}

		// Token: 0x04000019 RID: 25
		public static readonly string TemplateNotFoundIssueLocKey = "LoadingIssue.PrefabNotFoundIssue";

		// Token: 0x0400001A RID: 26
		public readonly ISerializedWorldSupplier _serializedWorldSupplier;

		// Token: 0x0400001B RID: 27
		public readonly EntityService _entityService;

		// Token: 0x0400001C RID: 28
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x0400001D RID: 29
		public readonly ILoadingIssueService _loadingIssueService;

		// Token: 0x0400001E RID: 30
		public readonly EntitiesLoader _entitiesLoader;

		// Token: 0x0400001F RID: 31
		public List<InstantiatedSerializedEntity> _instantiatedSerializedEntities;
	}
}
