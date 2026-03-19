using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x0200000E RID: 14
	public class RecoveredGoodStackCoordinatesFinder : ILoadableSingleton
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000029F4 File Offset: 0x00000BF4
		public RecoveredGoodStackCoordinatesFinder(IBlockService blockService, BlockValidator blockValidator, IRandomNumberGenerator randomNumberGenerator, ITerrainService terrainService, RecoveredGoodStackFactory recoveredGoodStackFactory, ISpecService specService)
		{
			this._blockService = blockService;
			this._blockValidator = blockValidator;
			this._randomNumberGenerator = randomNumberGenerator;
			this._terrainService = terrainService;
			this._recoveredGoodStackFactory = recoveredGoodStackFactory;
			this._specService = specService;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A29 File Offset: 0x00000C29
		public void Load()
		{
			this._spec = this._specService.GetSingleSpec<RecoveredGoodStackCoordinatesFinderSpec>();
			this._spiralNeighbours = NeighbourFinder.GetSpiralNeighboursXY(this._spec.NeighboursRange).ToImmutableArray<Vector2Int>();
			this._goodStackBlockSpec = this._recoveredGoodStackFactory.GoodStackBlockSpec;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A68 File Offset: 0x00000C68
		public bool FindValidCoordinates(Vector3Int original, out Vector3Int validCoordinates)
		{
			return this.FindValidCoordinates(original, null, out validCoordinates);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A74 File Offset: 0x00000C74
		public bool FindValidCoordinates(Vector3Int original, BlockObject overridingObject, out Vector3Int validCoordinates)
		{
			RecoveredGoodStackCoordinatesFinder.OverridableCoordinates coordinates = new RecoveredGoodStackCoordinatesFinder.OverridableCoordinates(original, overridingObject);
			RecoveredGoodStackCoordinatesFinder.OverridableCoordinates overridableCoordinates;
			bool result = this.TryFindNewCoordinates(coordinates, out overridableCoordinates);
			validCoordinates = overridableCoordinates.Coordinates;
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public bool TryFindNewCoordinates(RecoveredGoodStackCoordinatesFinder.OverridableCoordinates coordinates, out RecoveredGoodStackCoordinatesFinder.OverridableCoordinates movedCoordinates)
		{
			if (this.TryToFall(coordinates, out movedCoordinates))
			{
				return true;
			}
			foreach (RecoveredGoodStackCoordinatesFinder.OverridableCoordinates overridableCoordinates in this.GetRandomizedSpiralCoordinates(coordinates))
			{
				if (this.AreCoordinatesValid(overridableCoordinates))
				{
					movedCoordinates = overridableCoordinates;
					return true;
				}
				if (this.TryToFall(overridableCoordinates, out movedCoordinates) || this.TryMoveUp(overridableCoordinates, out movedCoordinates))
				{
					return true;
				}
			}
			movedCoordinates = default(RecoveredGoodStackCoordinatesFinder.OverridableCoordinates);
			return false;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B2C File Offset: 0x00000D2C
		public bool TryMoveUp(RecoveredGoodStackCoordinatesFinder.OverridableCoordinates coordinates, out RecoveredGoodStackCoordinatesFinder.OverridableCoordinates movedCoordinates)
		{
			for (int i = 1; i < this._spec.MaxUpperSearch; i++)
			{
				movedCoordinates = coordinates.Move(new Vector3Int(0, 0, i));
				if (this.AreCoordinatesValid(movedCoordinates))
				{
					return true;
				}
			}
			movedCoordinates = default(RecoveredGoodStackCoordinatesFinder.OverridableCoordinates);
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B7C File Offset: 0x00000D7C
		public bool TryToFall(RecoveredGoodStackCoordinatesFinder.OverridableCoordinates coordinates, out RecoveredGoodStackCoordinatesFinder.OverridableCoordinates movedCoordinates)
		{
			RecoveredGoodStackCoordinatesFinder.OverridableCoordinates fallCoordinates = this.GetFallCoordinates(coordinates);
			if (this.AreCoordinatesValid(fallCoordinates))
			{
				movedCoordinates = fallCoordinates;
				return true;
			}
			movedCoordinates = default(RecoveredGoodStackCoordinatesFinder.OverridableCoordinates);
			return false;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BAC File Offset: 0x00000DAC
		public RecoveredGoodStackCoordinatesFinder.OverridableCoordinates GetFallCoordinates(RecoveredGoodStackCoordinatesFinder.OverridableCoordinates coordinates)
		{
			while (coordinates.Coordinates.z > 0)
			{
				RecoveredGoodStackCoordinatesFinder.OverridableCoordinates overridableCoordinates = coordinates.Move(new Vector3Int(0, 0, -1));
				if (this._blockService.AnyTopObjectAt(overridableCoordinates.Coordinates) || this._terrainService.Underground(overridableCoordinates.Coordinates))
				{
					break;
				}
				coordinates = overridableCoordinates;
			}
			return coordinates;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C09 File Offset: 0x00000E09
		public IEnumerable<RecoveredGoodStackCoordinatesFinder.OverridableCoordinates> GetRandomizedSpiralCoordinates(RecoveredGoodStackCoordinatesFinder.OverridableCoordinates coordinates)
		{
			Orientation direction = this.RandomizeDirection();
			foreach (Vector2Int value in this._spiralNeighbours)
			{
				Vector3Int offset = direction.Transform(value.XYZ());
				yield return coordinates.Move(offset);
			}
			ImmutableArray<Vector2Int>.Enumerator enumerator = default(ImmutableArray<Vector2Int>.Enumerator);
			yield break;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C20 File Offset: 0x00000E20
		public Orientation RandomizeDirection()
		{
			Orientation result;
			switch (this._randomNumberGenerator.Range(0, 4))
			{
			case 0:
				result = Orientation.Cw0;
				break;
			case 1:
				result = Orientation.Cw90;
				break;
			case 2:
				result = Orientation.Cw180;
				break;
			default:
				result = Orientation.Cw270;
				break;
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C60 File Offset: 0x00000E60
		public bool AreCoordinatesValid(RecoveredGoodStackCoordinatesFinder.OverridableCoordinates coordinates)
		{
			Block block = Block.From(coordinates.Coordinates, this._goodStackBlockSpec);
			return (!coordinates.OverridingObject || !coordinates.OverridingObject.IsIntersecting(block)) && this._blockValidator.BlockValidWithoutUnfinishedStackable(block);
		}

		// Token: 0x04000027 RID: 39
		public readonly IBlockService _blockService;

		// Token: 0x04000028 RID: 40
		public readonly BlockValidator _blockValidator;

		// Token: 0x04000029 RID: 41
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400002A RID: 42
		public readonly ITerrainService _terrainService;

		// Token: 0x0400002B RID: 43
		public readonly RecoveredGoodStackFactory _recoveredGoodStackFactory;

		// Token: 0x0400002C RID: 44
		public readonly ISpecService _specService;

		// Token: 0x0400002D RID: 45
		public RecoveredGoodStackCoordinatesFinderSpec _spec;

		// Token: 0x0400002E RID: 46
		public ImmutableArray<Vector2Int> _spiralNeighbours;

		// Token: 0x0400002F RID: 47
		public BlockSpec _goodStackBlockSpec;

		// Token: 0x0200000F RID: 15
		public readonly struct OverridableCoordinates
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600004B RID: 75 RVA: 0x00002CAB File Offset: 0x00000EAB
			public Vector3Int Coordinates { get; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600004C RID: 76 RVA: 0x00002CB3 File Offset: 0x00000EB3
			public BlockObject OverridingObject { get; }

			// Token: 0x0600004D RID: 77 RVA: 0x00002CBB File Offset: 0x00000EBB
			public OverridableCoordinates(Vector3Int coordinates, BlockObject overridingObject)
			{
				this.Coordinates = coordinates;
				this.OverridingObject = overridingObject;
			}

			// Token: 0x0600004E RID: 78 RVA: 0x00002CCB File Offset: 0x00000ECB
			public RecoveredGoodStackCoordinatesFinder.OverridableCoordinates Move(Vector3Int offset)
			{
				return new RecoveredGoodStackCoordinatesFinder.OverridableCoordinates(this.Coordinates + offset, this.OverridingObject);
			}
		}
	}
}
