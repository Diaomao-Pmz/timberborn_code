using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Localization;
using Timberborn.Persistence;
using Timberborn.StatusSystem;
using Timberborn.TickSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200000E RID: 14
	public class ConstructionSite : TickableComponent, IAwakableComponent, IRegisteredComponent, IPersistentEntity, IInitializableEntity, IUnfinishedStateListener, IUnfinishedPausable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003A RID: 58 RVA: 0x00002A48 File Offset: 0x00000C48
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00002A80 File Offset: 0x00000C80
		public event EventHandler<ConstructionSiteReservationEventArgs> OnConstructionSiteReserved;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003C RID: 60 RVA: 0x00002AB8 File Offset: 0x00000CB8
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public event EventHandler<ConstructionSiteReservationEventArgs> OnConstructionSiteUnreserved;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600003E RID: 62 RVA: 0x00002B28 File Offset: 0x00000D28
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00002B60 File Offset: 0x00000D60
		public event EventHandler OnConstructionSiteProgressed;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002B95 File Offset: 0x00000D95
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002B9D File Offset: 0x00000D9D
		public Inventory Inventory { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002BA6 File Offset: 0x00000DA6
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002BAE File Offset: 0x00000DAE
		public float BuildTimeProgressInHours { get; private set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00002BB7 File Offset: 0x00000DB7
		public ConstructionSite(IBlockOccupancyService blockOccupancyService, ILoc loc, ConstructionSiteBuildTimeCalculator constructionSiteBuildTimeCalculator, EntityService entityService)
		{
			this._blockOccupancyService = blockOccupancyService;
			this._loc = loc;
			this._constructionSiteBuildTimeCalculator = constructionSiteBuildTimeCalculator;
			this._entityService = entityService;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002BE7 File Offset: 0x00000DE7
		public bool ReadyToBuild
		{
			get
			{
				return this.HasMaterialsToResumeBuilding && this.HasFreeSpots && this.IsOn;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002C01 File Offset: 0x00000E01
		public bool HasFreeSpots
		{
			get
			{
				return this._reservations.HasFreeSpots;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002C0E File Offset: 0x00000E0E
		public bool IsOn
		{
			get
			{
				return this._constructionSiteValidators.FastAll((IConstructionSiteValidator validator) => validator.IsValid) && this._blockableObject.IsUnblocked;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C49 File Offset: 0x00000E49
		public bool WasStarted
		{
			get
			{
				return this.BuildTimeProgressInHours > 0f || this.Inventory.TotalAmountInStock > 0;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002C68 File Offset: 0x00000E68
		public bool HasMaterialsToResumeBuilding
		{
			get
			{
				return Mathf.FloorToInt(this.MaterialProgress / ConstructionSite.ConstructionStageLengthInPercent) > Mathf.FloorToInt(this.BuildTimeProgress / ConstructionSite.ConstructionStageLengthInPercent);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002C8E File Offset: 0x00000E8E
		public float MaterialProgress
		{
			get
			{
				if (this.Inventory.Capacity != 0)
				{
					return (float)this.Inventory.TotalAmountInStock / (float)this.Inventory.Capacity;
				}
				return 1f;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002CBC File Offset: 0x00000EBC
		public float BuildTimeProgress
		{
			get
			{
				return Mathf.Clamp01(this.BuildTimeProgressInHours / this._constructionTimeInHours);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public bool IsReadyToFinish
		{
			get
			{
				return this.Inventory.IsFull && this.BuildTimeProgressInHours >= this._constructionTimeInHours && !this.BlockedByBeaversOnSite() && this.IsOn;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D00 File Offset: 0x00000F00
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._buildingSpec = base.GetComponent<BuildingSpec>();
			base.GetComponents<IConstructionSiteValidator>(this._constructionSiteValidators);
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._constructionFinishBlocker = base.GetComponent<IConstructionFinishBlocker>();
			this._lackOfResourcesStatusToggle = StatusToggle.CreateNormalStatusWithAlert("LackOfResources", this._loc.T(ConstructionSite.NoMaterialsLocKey), this._loc.T(ConstructionSite.NoMaterialsShortLocKey), 3f);
			this._reservations = base.GetComponent<ConstructionSiteReservations>();
			this._constructionTimeInHours = this._constructionSiteBuildTimeCalculator.GetConstructionTimeInHours(this);
			base.DisableComponent();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public void InitializeEntity()
		{
			if (this.Inventory.HasUnwantedStock)
			{
				foreach (GoodAmount good in this.Inventory.UnreservedUnwantedStock().ToList<GoodAmount>())
				{
					this.Inventory.Take(good);
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E14 File Offset: 0x00001014
		public override void StartTickable()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._lackOfResourcesStatusToggle);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E27 File Offset: 0x00001027
		public override void Tick()
		{
			this.FinishIfRequirementsMet();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E30 File Offset: 0x00001030
		public void RemainingRequiredGoods(SortedSet<GoodAmount> remainingGoods)
		{
			foreach (StorableGoodAmount storableGoodAmount in this.Inventory.AllowedGoods)
			{
				string goodId = storableGoodAmount.StorableGood.GoodId;
				if (this.Inventory.UnreservedCapacity(goodId) > 0)
				{
					remainingGoods.Add(new GoodAmount(goodId, this.Inventory.UnreservedAmountInStock(goodId)));
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EC0 File Offset: 0x000010C0
		public void IncreaseBuildTime(float hours)
		{
			this.SetBuildTimeProgress(this.BuildTimeProgressInHours + hours);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002ED0 File Offset: 0x000010D0
		public void ReserveForBuild(Builder builder)
		{
			this._reservations.Reserve(builder);
			EventHandler<ConstructionSiteReservationEventArgs> onConstructionSiteReserved = this.OnConstructionSiteReserved;
			if (onConstructionSiteReserved == null)
			{
				return;
			}
			onConstructionSiteReserved(this, new ConstructionSiteReservationEventArgs(builder));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EF5 File Offset: 0x000010F5
		public void UnreserveForBuild(Builder builder)
		{
			this._reservations.Unreserve(builder);
			EventHandler<ConstructionSiteReservationEventArgs> onConstructionSiteUnreserved = this.OnConstructionSiteUnreserved;
			if (onConstructionSiteUnreserved == null)
			{
				return;
			}
			onConstructionSiteUnreserved(this, new ConstructionSiteReservationEventArgs(builder));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F1C File Offset: 0x0000111C
		public void FinishNow()
		{
			foreach (IConstructionSiteValidator constructionSiteValidator in this._constructionSiteValidators)
			{
				constructionSiteValidator.Validate();
			}
			foreach (StorableGoodAmount storableGoodAmount in this.Inventory.AllowedGoods)
			{
				string goodId = storableGoodAmount.StorableGood.GoodId;
				int amount = storableGoodAmount.Amount - this.Inventory.AmountInStock(goodId);
				this.Inventory.GiveIgnoringCapacityReservation(new GoodAmount(goodId, amount));
			}
			this.SetBuildTimeProgress(this._constructionTimeInHours);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FF8 File Offset: 0x000011F8
		public void OnEnterUnfinishedState()
		{
			base.EnableComponent();
			this.Inventory.Enable();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000300B File Offset: 0x0000120B
		public void OnExitUnfinishedState()
		{
			base.DisableComponent();
			this.Inventory.Disable();
			this.DeactivateLackOfResourcesStatus();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003024 File Offset: 0x00001224
		public void ActivateLackOfResourcesStatus()
		{
			this._lackOfResourcesStatusToggle.Activate();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003031 File Offset: 0x00001231
		public void DeactivateLackOfResourcesStatus()
		{
			this._lackOfResourcesStatusToggle.Deactivate();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000303E File Offset: 0x0000123E
		public void Save(IEntitySaver entitySaver)
		{
			if (this._blockObject.IsUnfinished)
			{
				entitySaver.GetComponent(ConstructionSite.ConstructionSiteKey).Set(ConstructionSite.BuildTimeProgressInHoursKey, this.BuildTimeProgressInHours);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003068 File Offset: 0x00001268
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			this.BuildTimeProgressInHours = (entityLoader.TryGetComponent(ConstructionSite.ConstructionSiteKey, out objectLoader) ? objectLoader.Get(ConstructionSite.BuildTimeProgressInHoursKey) : this._constructionTimeInHours);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000309D File Offset: 0x0000129D
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<ConstructionSite>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
			this.Inventory.InventoryChanged += delegate(object _, InventoryChangedEventArgs _)
			{
				EventHandler onConstructionSiteProgressed = this.OnConstructionSiteProgressed;
				if (onConstructionSiteProgressed == null)
				{
					return;
				}
				onConstructionSiteProgressed(this, EventArgs.Empty);
			};
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000030D0 File Offset: 0x000012D0
		public bool IsFinishNotBlocked
		{
			get
			{
				IConstructionFinishBlocker constructionFinishBlocker = this._constructionFinishBlocker;
				return constructionFinishBlocker == null || !constructionFinishBlocker.IsFinishBlocked;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000030F3 File Offset: 0x000012F3
		public void SetBuildTimeProgress(float buildTimeProgressInHours)
		{
			this.BuildTimeProgressInHours = Mathf.Min(buildTimeProgressInHours, this._constructionTimeInHours);
			EventHandler onConstructionSiteProgressed = this.OnConstructionSiteProgressed;
			if (onConstructionSiteProgressed != null)
			{
				onConstructionSiteProgressed(this, EventArgs.Empty);
			}
			this.FinishIfRequirementsMet();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003124 File Offset: 0x00001324
		public void FinishIfRequirementsMet()
		{
			if (this._blockObject.IsUnfinished && this.IsReadyToFinish && this.IsFinishNotBlocked)
			{
				this._blockObject.MarkAsFinished();
				DeleteOnFinishConstructionSite component = base.GetComponent<DeleteOnFinishConstructionSite>();
				if (component != null)
				{
					this._entityService.Delete(this);
					component.NotifyDeleted();
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003175 File Offset: 0x00001375
		public bool BlockedByBeaversOnSite()
		{
			return !this._buildingSpec.FinishableWithBeaversOnSite && this._blockOccupancyService.OccupantPresentOnArea(this._blockObject, 0f);
		}

		// Token: 0x04000029 RID: 41
		public static readonly float ConstructionStageLengthInPercent = 0.1f;

		// Token: 0x0400002A RID: 42
		public static readonly string NoMaterialsLocKey = "Status.ConstructionSites.NoMaterials";

		// Token: 0x0400002B RID: 43
		public static readonly string NoMaterialsShortLocKey = "Status.ConstructionSites.NoMaterials.Short";

		// Token: 0x0400002C RID: 44
		public static readonly ComponentKey ConstructionSiteKey = new ComponentKey("ConstructionSite");

		// Token: 0x0400002D RID: 45
		public static readonly PropertyKey<float> BuildTimeProgressInHoursKey = new PropertyKey<float>("BuildTimeProgressInHours");

		// Token: 0x04000033 RID: 51
		public readonly List<IConstructionSiteValidator> _constructionSiteValidators = new List<IConstructionSiteValidator>();

		// Token: 0x04000034 RID: 52
		public readonly IBlockOccupancyService _blockOccupancyService;

		// Token: 0x04000035 RID: 53
		public readonly ILoc _loc;

		// Token: 0x04000036 RID: 54
		public readonly ConstructionSiteBuildTimeCalculator _constructionSiteBuildTimeCalculator;

		// Token: 0x04000037 RID: 55
		public readonly EntityService _entityService;

		// Token: 0x04000038 RID: 56
		public BlockObject _blockObject;

		// Token: 0x04000039 RID: 57
		public BuildingSpec _buildingSpec;

		// Token: 0x0400003A RID: 58
		public BlockableObject _blockableObject;

		// Token: 0x0400003B RID: 59
		public IConstructionFinishBlocker _constructionFinishBlocker;

		// Token: 0x0400003C RID: 60
		public StatusToggle _lackOfResourcesStatusToggle;

		// Token: 0x0400003D RID: 61
		public ConstructionSiteReservations _reservations;

		// Token: 0x0400003E RID: 62
		public float _constructionTimeInHours;
	}
}
