using System;
using System.Linq;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.FireworkSystem
{
	// Token: 0x02000008 RID: 8
	public class FireworkLauncher : BaseComponent, IAwakableComponent, IPersistentEntity, IInitializableEntity, IFinishedStateListener, IAutomatableNeeder, ITerminal, IDuplicable<FireworkLauncher>, IDuplicable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000015 RID: 21 RVA: 0x00002680 File Offset: 0x00000880
		// (remove) Token: 0x06000016 RID: 22 RVA: 0x000026B8 File Offset: 0x000008B8
		public event EventHandler AnglesChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000026ED File Offset: 0x000008ED
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000026F5 File Offset: 0x000008F5
		public string FireworkId { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000026FE File Offset: 0x000008FE
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002706 File Offset: 0x00000906
		public int Heading { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000270F File Offset: 0x0000090F
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002717 File Offset: 0x00000917
		public int Pitch { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002720 File Offset: 0x00000920
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002728 File Offset: 0x00000928
		public int FlightDistance { get; private set; } = 20;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002731 File Offset: 0x00000931
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002739 File Offset: 0x00000939
		public bool IsContinuous { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002742 File Offset: 0x00000942
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000274A File Offset: 0x0000094A
		public Inventory Inventory { get; private set; }

		// Token: 0x06000023 RID: 35 RVA: 0x00002753 File Offset: 0x00000953
		public FireworkLauncher(FireworkSpawner fireworkSpawner, FireworkSpecService fireworkSpecService, FireworkLaunchService fireworkLaunchService)
		{
			this._fireworkSpawner = fireworkSpawner;
			this._fireworkSpecService = fireworkSpecService;
			this._fireworkLaunchService = fireworkLaunchService;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002778 File Offset: 0x00000978
		public bool NeedsAutomatable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000277B File Offset: 0x0000097B
		public void Awake()
		{
			this._automatable = base.GetComponent<Automatable>();
			this._blockableObject = base.GetComponent<BlockableObject>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002795 File Offset: 0x00000995
		public void InitializeEntity()
		{
			if (string.IsNullOrWhiteSpace(this.FireworkId) || !this._fireworkSpecService.HasSpec(this.FireworkId))
			{
				this.FireworkId = this._fireworkSpecService.GetFireworkIds().First<string>();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027D0 File Offset: 0x000009D0
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(FireworkLauncher.ComponentKey);
			component.Set(FireworkLauncher.FireworkIdKey, this.FireworkId);
			component.Set(FireworkLauncher.HeadingKey, this.Heading);
			component.Set(FireworkLauncher.PitchKey, this.Pitch);
			component.Set(FireworkLauncher.FlightDistanceKey, this.FlightDistance);
			component.Set(FireworkLauncher.IsContinuousKey, this.IsContinuous);
			component.Set(FireworkLauncher.PreviousStateKey, this._previousState);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002850 File Offset: 0x00000A50
		[BackwardCompatible(2026, 3, 5, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(FireworkLauncher.ComponentKey, out objectLoader) && objectLoader.Has<string>(FireworkLauncher.FireworkIdKey))
			{
				this.FireworkId = objectLoader.Get(FireworkLauncher.FireworkIdKey);
				this.Heading = objectLoader.Get(FireworkLauncher.HeadingKey);
				this.Pitch = objectLoader.Get(FireworkLauncher.PitchKey);
				this.FlightDistance = objectLoader.Get(FireworkLauncher.FlightDistanceKey);
				this.IsContinuous = objectLoader.Get(FireworkLauncher.IsContinuousKey);
				this._previousState = objectLoader.Get(FireworkLauncher.PreviousStateKey);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028DF File Offset: 0x00000ADF
		public void DuplicateFrom(FireworkLauncher source)
		{
			this.SetFireworkId(source.FireworkId);
			this.SetHeading(source.Heading);
			this.SetPitch(source.Pitch);
			this.SetFlightDistance(source.FlightDistance);
			this.SetContinuous(source.IsContinuous);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000291D File Offset: 0x00000B1D
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.Inventory.Enable();
			this._fireworkLaunchService.Add(this);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000293C File Offset: 0x00000B3C
		public void OnExitFinishedState()
		{
			this.Inventory.Disable();
			base.DisableComponent();
			this._fireworkLaunchService.Remove(this);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000295B File Offset: 0x00000B5B
		public void SetFireworkId(string fireworkId)
		{
			this.FireworkId = fireworkId;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002964 File Offset: 0x00000B64
		public void SetHeading(int heading)
		{
			this.Heading = Mathf.Clamp(heading, FireworkLimits.MinHeading, FireworkLimits.MaxHeading);
			EventHandler anglesChanged = this.AnglesChanged;
			if (anglesChanged == null)
			{
				return;
			}
			anglesChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002992 File Offset: 0x00000B92
		public void SetPitch(int pitch)
		{
			this.Pitch = Mathf.Clamp(pitch, FireworkLimits.MinPitch, FireworkLimits.MaxPitch);
			EventHandler anglesChanged = this.AnglesChanged;
			if (anglesChanged == null)
			{
				return;
			}
			anglesChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029C0 File Offset: 0x00000BC0
		public void SetFlightDistance(int distance)
		{
			this.FlightDistance = Mathf.Clamp(distance, FireworkLimits.MinFlightDistance, FireworkLimits.MaxFlightDistance);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029D8 File Offset: 0x00000BD8
		public void SetContinuous(bool isContinuous)
		{
			this.IsContinuous = isContinuous;
			this.Evaluate();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029E7 File Offset: 0x00000BE7
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<FireworkLauncher>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A04 File Offset: 0x00000C04
		public void Evaluate()
		{
			this._currentState = (this._automatable.State == ConnectionState.On);
			if (this.IsContinuous)
			{
				this._isArmed = this._currentState;
			}
			else if (this._currentState != this._previousState)
			{
				this._isArmed = (this._currentState && !this._previousState);
			}
			this._previousState = this._currentState;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A70 File Offset: 0x00000C70
		public void LaunchIfArmed()
		{
			if (this._isArmed)
			{
				if (base.Enabled && this._blockableObject.IsUnblocked && !this.Inventory.IsEmpty)
				{
					this.ConsumeGoods();
					this._fireworkSpawner.SpawnFirework(this);
				}
				this._isArmed = this.IsContinuous;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public void ConsumeGoods()
		{
			foreach (StorableGoodAmount storableGoodAmount in this.Inventory.AllowedGoods)
			{
				if (this.Inventory.UnreservedAmountInStock(storableGoodAmount.StorableGood.GoodId) > 0)
				{
					this.Inventory.Take(new GoodAmount(storableGoodAmount.StorableGood.GoodId, 1));
				}
			}
		}

		// Token: 0x04000025 RID: 37
		public static readonly ComponentKey ComponentKey = new ComponentKey("FireworkLauncher");

		// Token: 0x04000026 RID: 38
		public static readonly PropertyKey<string> FireworkIdKey = new PropertyKey<string>("FireworkId");

		// Token: 0x04000027 RID: 39
		public static readonly PropertyKey<int> HeadingKey = new PropertyKey<int>("Heading");

		// Token: 0x04000028 RID: 40
		public static readonly PropertyKey<int> PitchKey = new PropertyKey<int>("Pitch");

		// Token: 0x04000029 RID: 41
		public static readonly PropertyKey<int> FlightDistanceKey = new PropertyKey<int>("FlightDistance");

		// Token: 0x0400002A RID: 42
		public static readonly PropertyKey<bool> IsContinuousKey = new PropertyKey<bool>("IsContinuous");

		// Token: 0x0400002B RID: 43
		public static readonly PropertyKey<bool> PreviousStateKey = new PropertyKey<bool>("PreviousState");

		// Token: 0x04000033 RID: 51
		public readonly FireworkSpawner _fireworkSpawner;

		// Token: 0x04000034 RID: 52
		public readonly FireworkSpecService _fireworkSpecService;

		// Token: 0x04000035 RID: 53
		public readonly FireworkLaunchService _fireworkLaunchService;

		// Token: 0x04000036 RID: 54
		public Automatable _automatable;

		// Token: 0x04000037 RID: 55
		public BlockableObject _blockableObject;

		// Token: 0x04000038 RID: 56
		public bool _currentState;

		// Token: 0x04000039 RID: 57
		public bool _previousState;

		// Token: 0x0400003A RID: 58
		public bool _isArmed;
	}
}
