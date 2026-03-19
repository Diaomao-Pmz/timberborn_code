using System;
using System.Collections.Generic;
using System.Linq;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.DeconstructionSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TickSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x02000008 RID: 8
	public class Dynamite : TickableComponent, IAwakableComponent, IFinishedStateListener, IPersistentEntity, IInitializableEntity, ITerrainRemovingEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002229 File Offset: 0x00000429
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002231 File Offset: 0x00000431
		public bool IsTriggered { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x0000223C File Offset: 0x0000043C
		public Dynamite(ITerrainService terrainService, IBlockService blockService, EntityService entityService, IInstantiator instantiator, EventBus eventBus, IAssetLoader assetLoader, ExplosionSoundPlayer explosionSoundPlayer, ExplosionService explosionService, CharacterExploder characterExploder)
		{
			this._terrainService = terrainService;
			this._blockService = blockService;
			this._entityService = entityService;
			this._instantiator = instantiator;
			this._eventBus = eventBus;
			this._assetLoader = assetLoader;
			this._explosionSoundPlayer = explosionSoundPlayer;
			this._explosionService = explosionService;
			this._characterExploder = characterExploder;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000229F File Offset: 0x0000049F
		public int Depth
		{
			get
			{
				return this._dynamiteSpec.Depth;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022AC File Offset: 0x000004AC
		public bool IsFinished
		{
			get
			{
				return this._blockObject.IsFinished;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022B9 File Offset: 0x000004B9
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._dynamiteSpec = base.GetComponent<DynamiteSpec>();
			this._explosionPrefab = this._assetLoader.Load<GameObject>(this._dynamiteSpec.ExplosionPrefabPath);
			base.DisableComponent();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F8 File Offset: 0x000004F8
		public override void Tick()
		{
			if (this.IsTriggered)
			{
				int num = this._tickCounter + 1;
				this._tickCounter = num;
				if (num > this._ticksToDetonate)
				{
					this.Detonate();
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000232C File Offset: 0x0000052C
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsTriggered)
			{
				IObjectSaver component = entitySaver.GetComponent(Dynamite.DynamiteKey);
				component.Set(Dynamite.IsTriggeredKey, this.IsTriggered);
				component.Set(Dynamite.TickCounterKey, this._tickCounter);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002364 File Offset: 0x00000564
		[BackwardCompatible(2026, 2, 9, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Dynamite.DynamiteKey, out objectLoader))
			{
				this.IsTriggered = (objectLoader.Has<bool>(Dynamite.IsTriggeredKey) ? objectLoader.Get(Dynamite.IsTriggeredKey) : objectLoader.Get(new PropertyKey<bool>("Triggered")));
				this._tickCounter = objectLoader.Get(Dynamite.TickCounterKey);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023C1 File Offset: 0x000005C1
		public void InitializeEntity()
		{
			this.InitializeNeighbouringTiles();
			this._explosionService.TilesExplosion += this.OnTilesExplosion;
			if (this.IsTriggered)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023EE File Offset: 0x000005EE
		public void Trigger()
		{
			if (this._blockObject.IsFinished)
			{
				this._ticksToDetonate = 1;
				this.IsTriggered = true;
				base.EnableComponent();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002411 File Offset: 0x00000611
		public void TriggerDelayed(int delayInTicks)
		{
			if (this._blockObject.IsFinished)
			{
				this._ticksToDetonate = delayInTicks;
				this.IsTriggered = true;
				base.EnableComponent();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002434 File Offset: 0x00000634
		public void Disarm()
		{
			this.IsTriggered = false;
			base.DisableComponent();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002443 File Offset: 0x00000643
		public void OnEnterFinishedState()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002445 File Offset: 0x00000645
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._explosionService.TilesExplosion -= this.OnTilesExplosion;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002464 File Offset: 0x00000664
		public bool RemovesTerrainAt(Vector3Int coordinates)
		{
			Vector3Int vector3Int = this._blockObject.Coordinates.Below();
			int num = this.CalculateEffectiveDepth(vector3Int);
			for (int i = 0; i < num; i++)
			{
				if (vector3Int == coordinates)
				{
					return true;
				}
				int z = vector3Int.z;
				vector3Int.z = z - 1;
			}
			return false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024B4 File Offset: 0x000006B4
		public void Detonate()
		{
			this.TriggerNeighbors();
			this.DestroyPathBlockObject();
			this.LowerTerrainBelow();
			this._characterExploder.ExplodeCharactersAt(this._blockObject.Coordinates, this);
			this.PlayEffects();
			base.GetComponent<Deconstructible>().DisableDeconstruction();
			this._entityService.Delete(this);
			this.IsTriggered = false;
			this._eventBus.Post(new DynamiteDetonatedEvent());
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002520 File Offset: 0x00000720
		public void InitializeNeighbouringTiles()
		{
			foreach (Vector3Int vector3Int in base.GetComponent<BlockObject>().PositionedBlocks.GetAllCoordinates())
			{
				foreach (Vector3Int vector3Int2 in Deltas.Neighbors4Vector3Int)
				{
					this._neighbouringTiles.Add(vector3Int + vector3Int2);
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025A4 File Offset: 0x000007A4
		public void OnTilesExplosion(object sender, ReadOnlyHashSet<Vector3Int> tiles)
		{
			if (this.IsFinished && this._neighbouringTiles.Any(new Func<Vector3Int, bool>(tiles.Contains)))
			{
				this.Trigger();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025D4 File Offset: 0x000007D4
		public void TriggerNeighbors()
		{
			foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
			{
				Vector3Int coordinates = this._blockObject.Coordinates + vector3Int;
				Dynamite bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<Dynamite>(coordinates);
				if (bottomObjectComponentAt && bottomObjectComponentAt.IsFinished)
				{
					bottomObjectComponentAt.Trigger();
				}
				UnstableCore bottomObjectComponentAt2 = this._blockService.GetBottomObjectComponentAt<UnstableCore>(coordinates);
				if (bottomObjectComponentAt2)
				{
					bottomObjectComponentAt2.Activate();
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002654 File Offset: 0x00000854
		public void DestroyPathBlockObject()
		{
			BlockObject pathObjectAt = this._blockService.GetPathObjectAt(this._blockObject.Coordinates);
			if (pathObjectAt != null)
			{
				pathObjectAt.GetComponent<Deconstructible>().DisableDeconstruction();
				this._entityService.Delete(pathObjectAt);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002694 File Offset: 0x00000894
		public void LowerTerrainBelow()
		{
			Vector3Int coordinates = this._blockObject.Coordinates.Below();
			int heightChange = this.CalculateEffectiveDepth(coordinates);
			this._terrainService.UnsetTerrain(coordinates, heightChange);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026C8 File Offset: 0x000008C8
		public int CalculateEffectiveDepth(Vector3Int coordinates)
		{
			Vector2Int coords2D = coordinates.XY();
			int z = coordinates.z;
			for (int i = 0; i < this.Depth; i++)
			{
				if (this._blockService.AnyObjectAt(coords2D.ToVector3Int(z - i)))
				{
					return i;
				}
			}
			return this.Depth;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002714 File Offset: 0x00000914
		public void PlayEffects()
		{
			GameObject gameObject = this._instantiator.Instantiate(this._explosionPrefab, null);
			gameObject.transform.position = this._blockObject.GetComponent<BlockObjectCenter>().WorldCenter;
			this._explosionSoundPlayer.Play(gameObject);
		}

		// Token: 0x0400000A RID: 10
		public static readonly ComponentKey DynamiteKey = new ComponentKey("Dynamite");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<bool> IsTriggeredKey = new PropertyKey<bool>("IsTriggered");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<int> TickCounterKey = new PropertyKey<int>("TickCounter");

		// Token: 0x0400000E RID: 14
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000F RID: 15
		public readonly IBlockService _blockService;

		// Token: 0x04000010 RID: 16
		public readonly EntityService _entityService;

		// Token: 0x04000011 RID: 17
		public readonly IInstantiator _instantiator;

		// Token: 0x04000012 RID: 18
		public readonly EventBus _eventBus;

		// Token: 0x04000013 RID: 19
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000014 RID: 20
		public readonly ExplosionSoundPlayer _explosionSoundPlayer;

		// Token: 0x04000015 RID: 21
		public readonly ExplosionService _explosionService;

		// Token: 0x04000016 RID: 22
		public readonly CharacterExploder _characterExploder;

		// Token: 0x04000017 RID: 23
		public BlockObject _blockObject;

		// Token: 0x04000018 RID: 24
		public DynamiteSpec _dynamiteSpec;

		// Token: 0x04000019 RID: 25
		public GameObject _explosionPrefab;

		// Token: 0x0400001A RID: 26
		public readonly HashSet<Vector3Int> _neighbouringTiles = new HashSet<Vector3Int>();

		// Token: 0x0400001B RID: 27
		public int _tickCounter;

		// Token: 0x0400001C RID: 28
		public int _ticksToDetonate;
	}
}
