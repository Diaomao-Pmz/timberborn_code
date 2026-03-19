using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.SoilBarrierSystem
{
	// Token: 0x02000007 RID: 7
	public class SoilBarrier : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public SoilBarrier(SoilBarrierMap soilBarrierMap, ITerrainService terrainService)
		{
			this._soilBarrierMap = soilBarrierMap;
			this._terrainService = terrainService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._soilBarrierSpec = base.GetComponent<SoilBarrierSpec>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002130 File Offset: 0x00000330
		public void OnEnterFinishedState()
		{
			foreach (Vector3Int coordinates in this.GetGroundCoordinates())
			{
				if (this._soilBarrierSpec.BlockAboveMoisture)
				{
					this._soilBarrierMap.AddAboveMoistureBarrierAt(coordinates);
				}
				if (this._soilBarrierSpec.BlockFullMoisture)
				{
					this._soilBarrierMap.AddFullMoistureBarrierAt(coordinates);
				}
				if (this._soilBarrierSpec.BlockContamination)
				{
					this._soilBarrierMap.AddContaminationBarrierAt(coordinates);
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C4 File Offset: 0x000003C4
		public void OnExitFinishedState()
		{
			foreach (Vector3Int coordinates in this.GetGroundCoordinates())
			{
				if (this._soilBarrierSpec.BlockAboveMoisture)
				{
					this._soilBarrierMap.RemoveAboveMoistureBarrierAt(coordinates);
				}
				if (this._soilBarrierSpec.BlockFullMoisture)
				{
					this._soilBarrierMap.RemoveFullMoistureBarrierAt(coordinates);
				}
				if (this._soilBarrierSpec.BlockContamination)
				{
					this._soilBarrierMap.RemoveContaminationBarrierAt(coordinates);
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002258 File Offset: 0x00000458
		public IEnumerable<Vector3Int> GetGroundCoordinates()
		{
			foreach (Vector3Int vector3Int in this._blockObject.PositionedBlocks.GetFoundationCoordinates())
			{
				if (this._terrainService.OnGround(vector3Int))
				{
					yield return vector3Int;
				}
			}
			IEnumerator<Vector3Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000008 RID: 8
		public readonly SoilBarrierMap _soilBarrierMap;

		// Token: 0x04000009 RID: 9
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000A RID: 10
		public BlockObject _blockObject;

		// Token: 0x0400000B RID: 11
		public SoilBarrierSpec _soilBarrierSpec;
	}
}
