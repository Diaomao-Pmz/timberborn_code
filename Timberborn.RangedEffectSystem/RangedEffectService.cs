using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.EnterableSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000014 RID: 20
	public class RangedEffectService : ILoadableSingleton
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002D9A File Offset: 0x00000F9A
		public RangedEffectService(ITerrainService terrainService, EventBus eventBus, IBlockService blockService)
		{
			this._terrainService = terrainService;
			this._eventBus = eventBus;
			this._blockService = blockService;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002DB7 File Offset: 0x00000FB7
		public void Load()
		{
			this.InitializeArrays();
			this._eventBus.Register(this);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002DCB File Offset: 0x00000FCB
		public ReadOnlyList<RangedEffect> GetEffectsAffectingCoordinates(Vector2Int coordinates)
		{
			if (!this._terrainService.Contains(coordinates))
			{
				return RangedEffectService.EmptyEffects.AsReadOnlyList<RangedEffect>();
			}
			return this._rangedEffects[coordinates.x, coordinates.y].ActiveEffects;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E04 File Offset: 0x00001004
		[OnEvent]
		public void OnEnteredUnfinishedState(EnteredUnfinishedStateEvent enteredUnfinishedStateEvent)
		{
			BlockObject blockObject = enteredUnfinishedStateEvent.BlockObject;
			if (blockObject.HasComponent<UnfinishedEffectReceivingBuildingSpec>())
			{
				this.SetExistingAppliersToEnterable(blockObject.GetComponent<Enterable>(), true);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E30 File Offset: 0x00001030
		[OnEvent]
		public void OnExitedUnfinishedState(ExitedUnfinishedStateEvent exitedUnfinishedStateEvent)
		{
			BlockObject blockObject = exitedUnfinishedStateEvent.BlockObject;
			if (blockObject.HasComponent<UnfinishedEffectReceivingBuildingSpec>())
			{
				this.SetExistingAppliersToEnterable(blockObject.GetComponent<Enterable>(), false);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002E5C File Offset: 0x0000105C
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			BlockObject blockObject = enteredFinishedStateEvent.BlockObject;
			this.SetExistingAppliersToEnterable(blockObject.GetComponent<Enterable>(), true);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002E80 File Offset: 0x00001080
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			BlockObject blockObject = exitedFinishedStateEvent.BlockObject;
			this.SetExistingAppliersToEnterable(blockObject.GetComponent<Enterable>(), false);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002EA1 File Offset: 0x000010A1
		public void SetApplier(RangedEffectApplier rangedEffectApplier)
		{
			this.SetApplier(rangedEffectApplier, true);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002EAB File Offset: 0x000010AB
		public void UnsetApplier(RangedEffectApplier rangedEffectApplier)
		{
			this.SetApplier(rangedEffectApplier, false);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002EB8 File Offset: 0x000010B8
		public void SetApplier(RangedEffectApplier rangedEffectApplier, bool add)
		{
			foreach (Vector2Int coordinates in rangedEffectApplier.EffectAreaCoords())
			{
				if (this._terrainService.Contains(coordinates))
				{
					this.AddApplierToExistingEnterablesAt(coordinates, rangedEffectApplier, add);
					this.SetApplierAt(coordinates, rangedEffectApplier, add);
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002F20 File Offset: 0x00001120
		public void SetApplierAt(Vector2Int coordinates, RangedEffectApplier rangedEffectApplier, bool add)
		{
			RangedEffects effectsAtCoordinates = this.GetEffectsAtCoordinates(coordinates);
			if (add)
			{
				effectsAtCoordinates.Add(rangedEffectApplier);
				return;
			}
			effectsAtCoordinates.Remove(rangedEffectApplier);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002F48 File Offset: 0x00001148
		public void SetExistingAppliersToEnterable(Enterable enterable, bool add)
		{
			if (enterable)
			{
				RangedEffectsAffectingEnterable component = enterable.GetComponent<RangedEffectsAffectingEnterable>();
				foreach (RangedEffectApplier rangedEffectApplier in this.GetExistingAppliersAffectingEnterable(enterable).ToHashSet<RangedEffectApplier>())
				{
					if (add)
					{
						component.Add(rangedEffectApplier);
					}
					else
					{
						component.Remove(rangedEffectApplier);
					}
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002FBC File Offset: 0x000011BC
		public IEnumerable<RangedEffectApplier> GetExistingAppliersAffectingEnterable(Enterable enterable)
		{
			IEnumerable<Vector3Int> occupiedCoordinates = enterable.GetComponent<BlockObject>().PositionedBlocks.GetOccupiedCoordinates();
			foreach (Vector3Int value in occupiedCoordinates)
			{
				RangedEffects effectsAtCoordinates = this.GetEffectsAtCoordinates(value.XY());
				foreach (RangedEffectApplier rangedEffectApplier in effectsAtCoordinates.RangedEffectAppliers)
				{
					yield return rangedEffectApplier;
				}
				IEnumerator<RangedEffectApplier> enumerator2 = null;
			}
			IEnumerator<Vector3Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002FD4 File Offset: 0x000011D4
		public void AddApplierToExistingEnterablesAt(Vector2Int coordinates, RangedEffectApplier rangedEffectApplier, bool add)
		{
			for (int i = 0; i <= this._blockService.Size.z; i++)
			{
				Vector3Int coordinates2;
				coordinates2..ctor(coordinates.x, coordinates.y, i);
				foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates2))
				{
					if (blockObject.HasComponent<BuildingSpec>() && RangedEffectService.IsValid(blockObject))
					{
						RangedEffectService.AddApplierToEnterable(rangedEffectApplier, blockObject, add);
					}
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003080 File Offset: 0x00001280
		public static void AddApplierToEnterable(RangedEffectApplier rangedEffectApplier, BlockObject building, bool add)
		{
			RangedEffectsAffectingEnterable component = building.GetComponent<RangedEffectsAffectingEnterable>();
			if (component)
			{
				if (add)
				{
					component.Add(rangedEffectApplier);
					return;
				}
				component.Remove(rangedEffectApplier);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000030AE File Offset: 0x000012AE
		public RangedEffects GetEffectsAtCoordinates(Vector2Int coordinates)
		{
			return this._rangedEffects[coordinates.x, coordinates.y];
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030C9 File Offset: 0x000012C9
		public static bool IsValid(BlockObject blockObject)
		{
			return blockObject.IsFinished || blockObject.HasComponent<UnfinishedEffectReceivingBuildingSpec>();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000030DC File Offset: 0x000012DC
		public void InitializeArrays()
		{
			this._rangedEffects = new RangedEffects[this._terrainService.Size.x, this._terrainService.Size.y];
			for (int i = 0; i <= this._rangedEffects.GetUpperBound(0); i++)
			{
				for (int j = 0; j <= this._rangedEffects.GetUpperBound(1); j++)
				{
					this._rangedEffects[i, j] = new RangedEffects();
				}
			}
		}

		// Token: 0x0400002D RID: 45
		public static readonly List<RangedEffect> EmptyEffects = new List<RangedEffect>();

		// Token: 0x0400002E RID: 46
		public readonly EventBus _eventBus;

		// Token: 0x0400002F RID: 47
		public readonly ITerrainService _terrainService;

		// Token: 0x04000030 RID: 48
		public readonly IBlockService _blockService;

		// Token: 0x04000031 RID: 49
		public RangedEffects[,] _rangedEffects;
	}
}
