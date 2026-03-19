using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.DecalSystem;
using Timberborn.EntitySystem;
using Timberborn.NeedSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.TailDecalSystem
{
	// Token: 0x0200000A RID: 10
	public class TailDecalApplier : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IChildhoodInfluenced
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000022C8 File Offset: 0x000004C8
		public TailDecalApplier(IDecalService decalService, EventBus eventBus)
		{
			this._decalService = decalService;
			this._eventBus = eventBus;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022DE File Offset: 0x000004DE
		public void Awake()
		{
			this._tailDecalTextureSetter = base.GetComponent<TailDecalTextureSetter>();
			this._needManager = base.GetComponent<NeedManager>();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022F8 File Offset: 0x000004F8
		public void InitializeEntity()
		{
			this._needManager.NeedChangedIsAtMinimumState += this.OnNeedChangedIsAtMinimumState;
			this._eventBus.Register(this);
			if (!this._appliedDecal.IsEmpty && this._needManager.NeedIsActive(TailDecalApplier.DecalNeedId))
			{
				this.ApplyDecal(this._decalService.GetValidatedDecal(this._appliedDecal));
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000235E File Offset: 0x0000055E
		public void Save(IEntitySaver entitySaver)
		{
			if (!this._appliedDecal.IsEmpty)
			{
				entitySaver.GetComponent(TailDecalApplier.TailDecalApplierKey).Set(TailDecalApplier.AppliedDecalIdKey, this._appliedDecal.Id);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002390 File Offset: 0x00000590
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(TailDecalApplier.TailDecalApplierKey, out objectLoader))
			{
				this._appliedDecal = new Decal(objectLoader.Get(TailDecalApplier.AppliedDecalIdKey), TailDecalApplier.AllowedCategory);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023C7 File Offset: 0x000005C7
		public void ApplyDecal(Decal decal)
		{
			if (decal.Category != TailDecalApplier.AllowedCategory)
			{
				throw new ArgumentException("Decal category '" + decal.Category + "' is not allowed.");
			}
			this._appliedDecal = decal;
			this.UpdateTexture();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002405 File Offset: 0x00000605
		[OnEvent]
		public void OnDecalsReloaded(DecalsReloadedEvent decalsReloadedEvent)
		{
			if (this._needManager.NeedIsActive(TailDecalApplier.DecalNeedId))
			{
				this.ApplyDecal(this._decalService.GetValidatedDecal(this._appliedDecal));
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002430 File Offset: 0x00000630
		public void InfluenceByChildhood(Character child)
		{
			TailDecalApplier component = child.GetComponent<TailDecalApplier>();
			if (component.CanShowTexture())
			{
				this._appliedDecal = component._appliedDecal;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002458 File Offset: 0x00000658
		public void OnNeedChangedIsAtMinimumState(object sender, NeedChangedIsAtMinimumStateEventArgs e)
		{
			this.UpdateTexture();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002460 File Offset: 0x00000660
		public void UpdateTexture()
		{
			if (this.CanShowTexture())
			{
				this.ShowTexture();
				return;
			}
			this._tailDecalTextureSetter.ClearDecalTexture();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000247C File Offset: 0x0000067C
		public bool CanShowTexture()
		{
			return !this._needManager.NeedIsAtMinimumPoints(TailDecalApplier.DecalNeedId) && !this._appliedDecal.IsEmpty;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024A0 File Offset: 0x000006A0
		public void ShowTexture()
		{
			Texture2D decalTexture = this._decalService.GetDecalTexture(this._appliedDecal);
			this._tailDecalTextureSetter.SetTexture(decalTexture);
		}

		// Token: 0x0400000B RID: 11
		public static readonly string AllowedCategory = "Tails";

		// Token: 0x0400000C RID: 12
		public static readonly ComponentKey TailDecalApplierKey = new ComponentKey("TailDecalApplier");

		// Token: 0x0400000D RID: 13
		public static readonly PropertyKey<string> AppliedDecalIdKey = new PropertyKey<string>("AppliedDecalId");

		// Token: 0x0400000E RID: 14
		public static readonly string DecalNeedId = "Detailer";

		// Token: 0x0400000F RID: 15
		public readonly IDecalService _decalService;

		// Token: 0x04000010 RID: 16
		public readonly EventBus _eventBus;

		// Token: 0x04000011 RID: 17
		public TailDecalTextureSetter _tailDecalTextureSetter;

		// Token: 0x04000012 RID: 18
		public NeedManager _needManager;

		// Token: 0x04000013 RID: 19
		public Decal _appliedDecal;
	}
}
