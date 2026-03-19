using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.DecalSystem
{
	// Token: 0x0200000E RID: 14
	public class DecalSupplier : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IDuplicable<DecalSupplier>, IDuplicable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003A RID: 58 RVA: 0x000028B4 File Offset: 0x00000AB4
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x000028EC File Offset: 0x00000AEC
		public event EventHandler ActiveDecalChanged;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002921 File Offset: 0x00000B21
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002929 File Offset: 0x00000B29
		public Decal ActiveDecal { get; private set; }

		// Token: 0x0600003E RID: 62 RVA: 0x00002932 File Offset: 0x00000B32
		public DecalSupplier(IDecalService decalService, EventBus eventBus)
		{
			this._decalService = decalService;
			this._eventBus = eventBus;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002948 File Offset: 0x00000B48
		public string Category
		{
			get
			{
				return this._decalSupplierSpec.Category;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002955 File Offset: 0x00000B55
		public void Awake()
		{
			this._decalSupplierSpec = base.GetComponent<DecalSupplierSpec>();
			this.ActiveDecal = new Decal(string.Empty, this.Category);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002979 File Offset: 0x00000B79
		public void InitializeEntity()
		{
			this.SetActiveDecal(this._decalService.GetValidatedDecal(this.ActiveDecal));
			this._eventBus.Register(this);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029A0 File Offset: 0x00000BA0
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(DecalSupplier.DecalSupplierKey).Set(DecalSupplier.ActiveDecalIdKey, this.ActiveDecal.Id);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029D0 File Offset: 0x00000BD0
		[BackwardCompatible(2025, 7, 3, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(DecalSupplier.DecalSupplierKey, out objectLoader))
			{
				this.ActiveDecal = new Decal(objectLoader.Get(DecalSupplier.ActiveDecalIdKey), this.Category);
				return;
			}
			IObjectLoader component = entityLoader.GetComponent(new ComponentKey("TailDecalSupplier"));
			this.ActiveDecal = new Decal(component.Get(DecalSupplier.ActiveDecalIdKey), this.Category);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A38 File Offset: 0x00000C38
		public void DuplicateFrom(DecalSupplier source)
		{
			Decal activeDecal = source.ActiveDecal;
			if (activeDecal.Category == this.Category)
			{
				this.SetActiveDecal(activeDecal);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A67 File Offset: 0x00000C67
		public void SetActiveDecal(Decal decal)
		{
			this.ActiveDecal = decal;
			EventHandler activeDecalChanged = this.ActiveDecalChanged;
			if (activeDecalChanged == null)
			{
				return;
			}
			activeDecalChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A86 File Offset: 0x00000C86
		[OnEvent]
		public void OnDecalsReloaded(DecalsReloadedEvent decalsReloadedEvent)
		{
			this.SetActiveDecal(this._decalService.GetValidatedDecal(this.ActiveDecal));
		}

		// Token: 0x0400001C RID: 28
		public static readonly ComponentKey DecalSupplierKey = new ComponentKey("DecalSupplier");

		// Token: 0x0400001D RID: 29
		public static readonly PropertyKey<string> ActiveDecalIdKey = new PropertyKey<string>("ActiveDecalId");

		// Token: 0x04000020 RID: 32
		public readonly IDecalService _decalService;

		// Token: 0x04000021 RID: 33
		public readonly EventBus _eventBus;

		// Token: 0x04000022 RID: 34
		public DecalSupplierSpec _decalSupplierSpec;
	}
}
