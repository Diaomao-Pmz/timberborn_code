using System;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;
using UnityEngine.Rendering;

namespace Timberborn.LevelVisibilitySystem
{
	// Token: 0x02000006 RID: 6
	public class LevelVisibilityService : ILevelVisibilityService, IPostLoadableSingleton, ILoadableSingleton
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000F RID: 15 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000010 RID: 16 RVA: 0x000020F8 File Offset: 0x000002F8
		public event EventHandler<int> MaxVisibleLevelChanged;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000212D File Offset: 0x0000032D
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002135 File Offset: 0x00000335
		public int MaxVisibleLevel { get; private set; }

		// Token: 0x06000013 RID: 19 RVA: 0x0000213E File Offset: 0x0000033E
		public LevelVisibilityService(EventBus eventBus, ITerrainService terrainService, MapSize mapSize)
		{
			this._eventBus = eventBus;
			this._terrainService = terrainService;
			this._mapSize = mapSize;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000215B File Offset: 0x0000035B
		public bool LevelIsAtMin
		{
			get
			{
				return !this._isActive || this.MaxVisibleLevel == this._minLevelHidingAnything;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002175 File Offset: 0x00000375
		public bool LevelIsAtMax
		{
			get
			{
				return this.MaxVisibleLevel == this.MaxVisibleLevelLimit;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002185 File Offset: 0x00000385
		public bool TerrainLevelIsAtMax
		{
			get
			{
				return this.MaxVisibleLevel == this.MaxVisibleTerrainLevelLimit;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002195 File Offset: 0x00000395
		public void Load()
		{
			this.MaxVisibleLevel = this._mapSize.MaxGameTerrainHeight + this._mapSize.MaxHeightAboveTerrain;
			this._terrainService.MinMaxTerrainHeightChanged += delegate(object _, EventArgs _)
			{
				if (this._isActive)
				{
					this.SetLevelsWithAnythingHidable(this._minLevelHidingAnything, this._maxLevelHidingAnything);
				}
			};
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021CB File Offset: 0x000003CB
		public void PostLoad()
		{
			this._isLoaded = true;
			this.ResetMaxVisibleLevel();
			this.SetLevelsWithAnythingHidable(this._minLevelHidingAnything, this._maxLevelHidingAnything);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021EC File Offset: 0x000003EC
		public void SetMaxVisibleLevel(int newMaxVisibleLevel)
		{
			if (this.MaxVisibleLevel != newMaxVisibleLevel)
			{
				int newMaxVisibleLevel2 = this.ClampMaxVisibleLevel(newMaxVisibleLevel);
				this.InternalSetMaxVisibleLevel(newMaxVisibleLevel2);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002211 File Offset: 0x00000411
		public void ResetMaxVisibleLevel()
		{
			this.InternalSetMaxVisibleLevel(Math.Max(this.MaxVisibleLevelLimit, this.MaxVisibleTerrainLevelLimit));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000222A File Offset: 0x0000042A
		public bool BlockIsVisible(Vector3Int coordinates)
		{
			return coordinates.z <= this.MaxVisibleLevel;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002240 File Offset: 0x00000440
		public void SetLevelsWithAnythingHidable(int minLevel, int maxLevel)
		{
			this._isActive = true;
			this._minLevelHidingAnything = Math.Max(0, Math.Min(this._terrainService.MinTerrainHeight, minLevel - 1));
			this._maxLevelHidingAnything = Math.Max(this._terrainService.MaxTerrainHeight - 1, maxLevel - 1);
			if (!this.LevelIsAtMax)
			{
				this.SetMaxVisibleLevel(this.ClampMaxVisibleLevel(this.MaxVisibleLevel));
			}
			this._eventBus.Post(new HidingLevelsChangedEvent());
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022B8 File Offset: 0x000004B8
		public void ResetLevelsWithAnythingHidable()
		{
			this._isActive = false;
			this._minLevelHidingAnything = 0;
			this._maxLevelHidingAnything = 0;
			this.ResetMaxVisibleLevel();
			this._eventBus.Post(new HidingLevelsChangedEvent());
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022E8 File Offset: 0x000004E8
		public int MaxVisibleLevelLimit
		{
			get
			{
				return this._mapSize.TotalSize.z;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002308 File Offset: 0x00000508
		public int MaxVisibleTerrainLevelLimit
		{
			get
			{
				return this._mapSize.TerrainSize.z;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002328 File Offset: 0x00000528
		public void InternalSetMaxVisibleLevel(int newMaxVisibleLevel)
		{
			int maxVisibleLevel = this.MaxVisibleLevel;
			if (this._isLoaded && maxVisibleLevel != newMaxVisibleLevel)
			{
				this.MaxVisibleLevel = newMaxVisibleLevel;
				Shader.SetGlobalFloat(LevelVisibilityService.MaxVisibleLevelProperty, (float)this.MaxVisibleLevel);
				Shader.SetKeyword(ref LevelVisibilityService.UseLevelVisibilityKey, !this.LevelIsAtMax);
				EventHandler<int> maxVisibleLevelChanged = this.MaxVisibleLevelChanged;
				if (maxVisibleLevelChanged != null)
				{
					maxVisibleLevelChanged(this, this.MaxVisibleLevel);
				}
				this._eventBus.Post(new MaxVisibleLevelChangedEvent(maxVisibleLevel));
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000239C File Offset: 0x0000059C
		public int ClampMaxVisibleLevel(int maxVisibleLevel)
		{
			if (maxVisibleLevel > this.MaxVisibleLevelLimit)
			{
				return this.MaxVisibleLevelLimit;
			}
			if (maxVisibleLevel < 0)
			{
				return 0;
			}
			if (this._isActive)
			{
				if (maxVisibleLevel < this._minLevelHidingAnything)
				{
					return this._minLevelHidingAnything;
				}
				if (maxVisibleLevel > this._maxLevelHidingAnything)
				{
					if (maxVisibleLevel <= this.MaxVisibleLevel)
					{
						return this._maxLevelHidingAnything;
					}
					return this.MaxVisibleLevelLimit;
				}
			}
			return maxVisibleLevel;
		}

		// Token: 0x04000006 RID: 6
		public static readonly int MaxVisibleLevelProperty = Shader.PropertyToID("_MaxVisibleLevel");

		// Token: 0x04000007 RID: 7
		public static readonly GlobalKeyword UseLevelVisibilityKey = GlobalKeyword.Create("_USE_LEVEL_VISIBILITY");

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;

		// Token: 0x0400000B RID: 11
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000C RID: 12
		public readonly MapSize _mapSize;

		// Token: 0x0400000D RID: 13
		public int _minLevelHidingAnything;

		// Token: 0x0400000E RID: 14
		public int _maxLevelHidingAnything;

		// Token: 0x0400000F RID: 15
		public bool _isActive;

		// Token: 0x04000010 RID: 16
		public bool _isLoaded;
	}
}
