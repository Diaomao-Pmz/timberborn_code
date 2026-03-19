using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000012 RID: 18
	public class DistrictBuildingRegistry : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600006C RID: 108 RVA: 0x000030FC File Offset: 0x000012FC
		// (remove) Token: 0x0600006D RID: 109 RVA: 0x00003134 File Offset: 0x00001334
		public event EventHandler<FinishedBuildingRegisteredEventArgs> FinishedBuildingRegistered;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600006E RID: 110 RVA: 0x0000316C File Offset: 0x0000136C
		// (remove) Token: 0x0600006F RID: 111 RVA: 0x000031A4 File Offset: 0x000013A4
		public event EventHandler<FinishedBuildingUnregisteredEventArgs> FinishedBuildingUnregistered;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000070 RID: 112 RVA: 0x000031DC File Offset: 0x000013DC
		// (remove) Token: 0x06000071 RID: 113 RVA: 0x00003214 File Offset: 0x00001414
		public event EventHandler<FinishedBuildingInstantRegisteredEventArgs> FinishedBuildingInstantRegistered;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000072 RID: 114 RVA: 0x0000324C File Offset: 0x0000144C
		// (remove) Token: 0x06000073 RID: 115 RVA: 0x00003284 File Offset: 0x00001484
		public event EventHandler<FinishedBuildingInstantUnregisteredEventArgs> FinishedBuildingInstantUnregistered;

		// Token: 0x06000074 RID: 116 RVA: 0x000032B9 File Offset: 0x000014B9
		public DistrictBuildingRegistry(EntityComponentRegistryFactory entityComponentRegistryFactory)
		{
			this._entityComponentRegistryFactory = entityComponentRegistryFactory;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000032C8 File Offset: 0x000014C8
		public void Awake()
		{
			this._finishedBuildings = this._entityComponentRegistryFactory.Create();
			this._instantFinishedBuildings = this._entityComponentRegistryFactory.Create();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000032EC File Offset: 0x000014EC
		public IEnumerable<T> GetEnabledBuildings<T>() where T : BaseComponent, IRegisteredComponent
		{
			return this._finishedBuildings.GetEnabled<T>();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032F9 File Offset: 0x000014F9
		public void RegisterFinishedBuilding(EntityComponent entityComponent)
		{
			this._finishedBuildings.Register(entityComponent);
			EventHandler<FinishedBuildingRegisteredEventArgs> finishedBuildingRegistered = this.FinishedBuildingRegistered;
			if (finishedBuildingRegistered == null)
			{
				return;
			}
			finishedBuildingRegistered(this, new FinishedBuildingRegisteredEventArgs(entityComponent));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000331E File Offset: 0x0000151E
		public void UnregisterFinishedBuilding(EntityComponent entityComponent)
		{
			this._finishedBuildings.Unregister(entityComponent);
			EventHandler<FinishedBuildingUnregisteredEventArgs> finishedBuildingUnregistered = this.FinishedBuildingUnregistered;
			if (finishedBuildingUnregistered == null)
			{
				return;
			}
			finishedBuildingUnregistered(this, new FinishedBuildingUnregisteredEventArgs(entityComponent));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003343 File Offset: 0x00001543
		public IEnumerable<T> GetEnabledBuildingsInstant<T>() where T : BaseComponent, IRegisteredComponent
		{
			return this._instantFinishedBuildings.GetEnabled<T>();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003350 File Offset: 0x00001550
		public void RegisterInstantFinishedBuilding(EntityComponent entityComponent)
		{
			this._instantFinishedBuildings.Register(entityComponent);
			EventHandler<FinishedBuildingInstantRegisteredEventArgs> finishedBuildingInstantRegistered = this.FinishedBuildingInstantRegistered;
			if (finishedBuildingInstantRegistered == null)
			{
				return;
			}
			finishedBuildingInstantRegistered(this, new FinishedBuildingInstantRegisteredEventArgs(entityComponent));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003375 File Offset: 0x00001575
		public void UnregisterInstantFinishedBuilding(EntityComponent entityComponent)
		{
			this._instantFinishedBuildings.Unregister(entityComponent);
			EventHandler<FinishedBuildingInstantUnregisteredEventArgs> finishedBuildingInstantUnregistered = this.FinishedBuildingInstantUnregistered;
			if (finishedBuildingInstantUnregistered == null)
			{
				return;
			}
			finishedBuildingInstantUnregistered(this, new FinishedBuildingInstantUnregisteredEventArgs(entityComponent));
		}

		// Token: 0x04000033 RID: 51
		public readonly EntityComponentRegistryFactory _entityComponentRegistryFactory;

		// Token: 0x04000034 RID: 52
		public EntityComponentRegistry _finishedBuildings;

		// Token: 0x04000035 RID: 53
		public EntityComponentRegistry _instantFinishedBuildings;
	}
}
