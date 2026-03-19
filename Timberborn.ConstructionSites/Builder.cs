using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000008 RID: 8
	public class Builder : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity, IPostInitializableEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002252 File Offset: 0x00000452
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000225A File Offset: 0x0000045A
		public ConstructionSite ReservedConstructionSite { get; private set; }

		// Token: 0x0600000F RID: 15 RVA: 0x00002263 File Offset: 0x00000463
		public Builder(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002272 File Offset: 0x00000472
		public bool HasReservedConstructionSite
		{
			get
			{
				return this.ReservedConstructionSite;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000227F File Offset: 0x0000047F
		public void Awake()
		{
			this._buildExecutor = base.GetComponent<BuildExecutor>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000228D File Offset: 0x0000048D
		public void Reserve(ConstructionSite constructionSite)
		{
			this.Unreserve();
			constructionSite.ReserveForBuild(this);
			this.ReservedConstructionSite = constructionSite;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A3 File Offset: 0x000004A3
		public void Unreserve()
		{
			if (this.HasReservedConstructionSite)
			{
				this.ReservedConstructionSite.UnreserveForBuild(this);
			}
			this.ReservedConstructionSite = null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022C0 File Offset: 0x000004C0
		public void Save(IEntitySaver entitySaver)
		{
			if (this.ReservedConstructionSite)
			{
				entitySaver.GetComponent(Builder.BuilderKey).Set<ConstructionSite>(Builder.ConstructionSiteKey, this.ReservedConstructionSite, this._referenceSerializer.Of<ConstructionSite>());
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022F8 File Offset: 0x000004F8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			ConstructionSite loadedConstructionSite;
			if (entityLoader.TryGetComponent(Builder.BuilderKey, out objectLoader) && objectLoader.GetObsoletable<ConstructionSite>(Builder.ConstructionSiteKey, this._referenceSerializer.Of<ConstructionSite>(), out loadedConstructionSite))
			{
				this._loadedConstructionSite = loadedConstructionSite;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002335 File Offset: 0x00000535
		public void DeleteEntity()
		{
			this.Unreserve();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002340 File Offset: 0x00000540
		public void PostInitializeEntity()
		{
			if (this._loadedConstructionSite)
			{
				if (this._loadedConstructionSite.HasFreeSpots)
				{
					this.Reserve(this._loadedConstructionSite);
					this._buildExecutor.InitializeAfterLoad(this.ReservedConstructionSite);
					this._loadedConstructionSite = null;
					return;
				}
				string firstName = base.GetComponent<Character>().FirstName;
				Debug.LogWarning(string.Concat(new string[]
				{
					"After loading ",
					firstName,
					" couldn't start building at ",
					this._loadedConstructionSite.Name,
					"."
				}));
			}
		}

		// Token: 0x0400000D RID: 13
		public static readonly ComponentKey BuilderKey = new ComponentKey("Builder");

		// Token: 0x0400000E RID: 14
		public static readonly PropertyKey<ConstructionSite> ConstructionSiteKey = new PropertyKey<ConstructionSite>("ConstructionSite");

		// Token: 0x04000010 RID: 16
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000011 RID: 17
		public ConstructionSite _loadedConstructionSite;

		// Token: 0x04000012 RID: 18
		public BuildExecutor _buildExecutor;
	}
}
