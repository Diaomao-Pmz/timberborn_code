using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.TerrainSystem;
using Timberborn.Workshops;
using UnityEngine;

namespace Timberborn.Terraforming
{
	// Token: 0x02000007 RID: 7
	public class Drill : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FF File Offset: 0x000002FF
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002107 File Offset: 0x00000307
		public int DrillingLevel { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public Drill(ITerrainService terrainService, IRandomNumberGenerator randomNumberGenerator, IInstantiator instantiator, IAssetLoader assetLoader)
		{
			this._terrainService = terrainService;
			this._randomNumberGenerator = randomNumberGenerator;
			this._instantiator = instantiator;
			this._assetLoader = assetLoader;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002140 File Offset: 0x00000340
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._manufactory = base.GetComponent<Manufactory>();
			this._drillSpec = base.GetComponent<DrillSpec>();
			this._removalEffectPrefab = this._assetLoader.Load<GameObject>(this._drillSpec.RemovalEffectPath);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000218D File Offset: 0x0000038D
		public void OnEnterFinishedState()
		{
			this.PositionDrillableCoordinates();
			this.SetInitialDrillingLevel();
			this._manufactory.ProductionFinished += this.OnProductionFinished;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B2 File Offset: 0x000003B2
		public void OnExitFinishedState()
		{
			this._manufactory.ProductionFinished -= this.OnProductionFinished;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021CC File Offset: 0x000003CC
		public void PositionDrillableCoordinates()
		{
			ImmutableArray<Vector3Int> drillableCoordinates = this._drillSpec.DrillableCoordinates;
			this._positionedDrillableCoordinates = (from coordinates in drillableCoordinates
			select this._blockObject.TransformCoordinates(coordinates)).ToImmutableArray<Vector3Int>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002202 File Offset: 0x00000402
		public void SetInitialDrillingLevel()
		{
			this.DrillingLevel = (from coordinates in this._positionedDrillableCoordinates
			select this._terrainService.GetTerrainHeightBelow(coordinates)).Max() - 1;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002228 File Offset: 0x00000428
		public void OnProductionFinished(object sender, EventArgs e)
		{
			this.TryDrillTerrain();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002230 File Offset: 0x00000430
		public void TryDrillTerrain()
		{
			bool flag = false;
			while (this.DrillingLevel >= 0 && !flag)
			{
				this.CollectDrillCandidates();
				flag = this.TryDrillRandomTerrainBlock();
				this.TryLowerDrillLevel();
			}
			this._drillCandidates.Clear();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000226C File Offset: 0x0000046C
		public void CollectDrillCandidates()
		{
			foreach (Vector3Int coordinates in this._positionedDrillableCoordinates)
			{
				if (this._terrainService.GetTerrainHeightBelow(coordinates) > this.DrillingLevel)
				{
					this._drillCandidates.Add(new Vector3Int(coordinates.x, coordinates.y, this.DrillingLevel));
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022D0 File Offset: 0x000004D0
		public bool TryDrillRandomTerrainBlock()
		{
			if (this._drillCandidates.Count > 0)
			{
				Vector3Int listElement = this._randomNumberGenerator.GetListElement<Vector3Int>(this._drillCandidates);
				this._terrainService.UnsetTerrain(listElement, 1);
				this.SpawnDrillEffect(listElement);
				this._drillCandidates.Remove(listElement);
				return true;
			}
			return false;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002324 File Offset: 0x00000524
		public void TryLowerDrillLevel()
		{
			if (this._drillCandidates.Count == 0)
			{
				int drillingLevel = this.DrillingLevel;
				this.DrillingLevel = drillingLevel - 1;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000234E File Offset: 0x0000054E
		public void SpawnDrillEffect(Vector3Int effectLocation)
		{
			this._instantiator.Instantiate(this._removalEffectPrefab, base.Transform).transform.position = CoordinateSystem.GridToWorldCentered(effectLocation);
		}

		// Token: 0x04000009 RID: 9
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000A RID: 10
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000B RID: 11
		public readonly IInstantiator _instantiator;

		// Token: 0x0400000C RID: 12
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000D RID: 13
		public BlockObject _blockObject;

		// Token: 0x0400000E RID: 14
		public Manufactory _manufactory;

		// Token: 0x0400000F RID: 15
		public DrillSpec _drillSpec;

		// Token: 0x04000010 RID: 16
		public GameObject _removalEffectPrefab;

		// Token: 0x04000011 RID: 17
		public ImmutableArray<Vector3Int> _positionedDrillableCoordinates;

		// Token: 0x04000012 RID: 18
		public readonly List<Vector3Int> _drillCandidates = new List<Vector3Int>();
	}
}
