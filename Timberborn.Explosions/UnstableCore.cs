using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.ActivatorSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Debugging;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x02000019 RID: 25
	public class UnstableCore : BaseComponent, IActivableComponent, IAwakableComponent, IUpdatableComponent, IPersistentEntity, IInitializableEntity, IDeletableEntity, IDuplicable<UnstableCore>, IDuplicable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600009E RID: 158 RVA: 0x00003E90 File Offset: 0x00002090
		// (remove) Token: 0x0600009F RID: 159 RVA: 0x00003EC8 File Offset: 0x000020C8
		public event EventHandler ExplosionRadiusChanged;

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003EFD File Offset: 0x000020FD
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003F05 File Offset: 0x00002105
		public int ExplosionRadius { get; private set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x00003F0E File Offset: 0x0000210E
		public UnstableCore(MapEditorMode mapEditorMode, DevModeManager devModeManager, ExplosionService explosionService)
		{
			this._mapEditorMode = mapEditorMode;
			this._devModeManager = devModeManager;
			this._explosionService = explosionService;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003F36 File Offset: 0x00002136
		public float InnerRadius
		{
			get
			{
				return this._spec.InnerRadius;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003F43 File Offset: 0x00002143
		public Vector3 ExplosionCenter
		{
			get
			{
				return this._blockObjectCenter.GridCenterGrounded;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003F50 File Offset: 0x00002150
		public bool IsDuplicable
		{
			get
			{
				return this._mapEditorMode.IsMapEditor || this._devModeManager.Enabled;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003F6C File Offset: 0x0000216C
		public int MinExplosionRadius
		{
			get
			{
				return this._spec.MinExplosionRadius;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003F79 File Offset: 0x00002179
		public int MaxExplosionRadius
		{
			get
			{
				return this._spec.MaxExplosionRadius;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003F88 File Offset: 0x00002188
		public void Awake()
		{
			this._spec = base.GetComponent<UnstableCoreSpec>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._effectsSpawner = base.GetComponent<UnstableCoreEffectsSpawner>();
			this._explosionBlocker = base.GetComponent<UnstableCoreExplosionBlocker>();
			this.ExplosionRadius = this._spec.DefaultExplosionRadius;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FD6 File Offset: 0x000021D6
		public void Update()
		{
			if (this._delayedActivationEnabled)
			{
				this._remainingDelayedActivationTime -= Time.deltaTime;
				if (this._remainingDelayedActivationTime <= 0f)
				{
					this.Activate();
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004005 File Offset: 0x00002205
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(UnstableCore.UnstableCoreKey).Set(UnstableCore.ExplosionRadiusKey, this.ExplosionRadius);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004024 File Offset: 0x00002224
		[BackwardCompatible(2025, 9, 30, Compatibility.Map)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component;
			if (!entityLoader.TryGetComponent(UnstableCore.UnstableCoreKey, out component))
			{
				component = entityLoader.GetComponent(new ComponentKey("TimeBomb"));
			}
			this.ExplosionRadius = component.Get(UnstableCore.ExplosionRadiusKey);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004062 File Offset: 0x00002262
		public void InitializeEntity()
		{
			this._explosionService.Register(this);
			this.InitializeNeighbouringTiles();
			this._explosionService.TilesExplosion += this.OnTilesExplosion;
			this._initialized = true;
			if (this._triggered)
			{
				this.Explode();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000040A2 File Offset: 0x000022A2
		public void DuplicateFrom(UnstableCore source)
		{
			this.SetRadius(source.ExplosionRadius);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002443 File Offset: 0x00000643
		public void Deactivate()
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000040B0 File Offset: 0x000022B0
		public void Activate()
		{
			this.TriggerExplosion();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000040B8 File Offset: 0x000022B8
		public void SetRadius(int radius)
		{
			if (radius < this.MinExplosionRadius || radius > this.MaxExplosionRadius)
			{
				throw new ArgumentOutOfRangeException("radius", string.Format("Explosion radius must be between {0} and {1}.", this.MinExplosionRadius, this.MaxExplosionRadius));
			}
			this.ExplosionRadius = radius;
			this._explosionService.Register(this);
			EventHandler explosionRadiusChanged = this.ExplosionRadiusChanged;
			if (explosionRadiusChanged == null)
			{
				return;
			}
			explosionRadiusChanged(this, EventArgs.Empty);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000412B File Offset: 0x0000232B
		public void DeleteEntity()
		{
			this.TriggerExplosion();
			this._explosionService.TilesExplosion -= this.OnTilesExplosion;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000414A File Offset: 0x0000234A
		public void ActivateDelayed(float delay)
		{
			this._delayedActivationEnabled = true;
			this._remainingDelayedActivationTime = delay;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000415C File Offset: 0x0000235C
		public void InitializeNeighbouringTiles()
		{
			BlockObject component = base.GetComponent<BlockObject>();
			foreach (Vector3Int vector3Int in component.PositionedBlocks.GetAllCoordinates())
			{
				foreach (Vector3Int vector3Int2 in Deltas.Neighbors4Vector3Int)
				{
					Vector3Int vector3Int3 = vector3Int + vector3Int2;
					if (!component.PositionedBlocks.HasBlockAt(vector3Int3))
					{
						this._neighbouringTiles.Add(vector3Int3);
					}
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000041F8 File Offset: 0x000023F8
		public void OnTilesExplosion(object sender, ReadOnlyHashSet<Vector3Int> tiles)
		{
			if (this._neighbouringTiles.Any(new Func<Vector3Int, bool>(tiles.Contains)))
			{
				this.TriggerExplosion();
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000421E File Offset: 0x0000241E
		public void TriggerExplosion()
		{
			if (!this._triggered && !this._explosionBlocker.ExplosionBlocked)
			{
				this._triggered = true;
				if (this._initialized)
				{
					this.Explode();
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000424A File Offset: 0x0000244A
		public void Explode()
		{
			this._effectsSpawner.SpawnEffects();
			this._explosionService.TilesExplosion -= this.OnTilesExplosion;
			this._explosionService.Explode(this);
		}

		// Token: 0x0400006C RID: 108
		public static readonly ComponentKey UnstableCoreKey = new ComponentKey("UnstableCore");

		// Token: 0x0400006D RID: 109
		public static readonly PropertyKey<int> ExplosionRadiusKey = new PropertyKey<int>("ExplosionRadius");

		// Token: 0x04000070 RID: 112
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000071 RID: 113
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000072 RID: 114
		public readonly ExplosionService _explosionService;

		// Token: 0x04000073 RID: 115
		public UnstableCoreSpec _spec;

		// Token: 0x04000074 RID: 116
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x04000075 RID: 117
		public UnstableCoreEffectsSpawner _effectsSpawner;

		// Token: 0x04000076 RID: 118
		public UnstableCoreExplosionBlocker _explosionBlocker;

		// Token: 0x04000077 RID: 119
		public readonly HashSet<Vector3Int> _neighbouringTiles = new HashSet<Vector3Int>();

		// Token: 0x04000078 RID: 120
		public bool _triggered;

		// Token: 0x04000079 RID: 121
		public bool _delayedActivationEnabled;

		// Token: 0x0400007A RID: 122
		public float _remainingDelayedActivationTime;

		// Token: 0x0400007B RID: 123
		public bool _initialized;
	}
}
