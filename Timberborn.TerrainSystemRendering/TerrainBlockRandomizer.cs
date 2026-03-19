using System;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x0200000C RID: 12
	public class TerrainBlockRandomizer : ILoadableSingleton
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002718 File Offset: 0x00000918
		public TerrainBlockRandomizer(ITerrainService terrainService, IRandomNumberGenerator randomNumberGenerator)
		{
			this._terrainService = terrainService;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002730 File Offset: 0x00000930
		public void Load()
		{
			this._size = this._terrainService.Size + Vector3Int.one;
			this._selectedVariations = new TerrainBlockRandomizer.SelectedVariation?[this._size.x, this._size.y, this._size.z];
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002784 File Offset: 0x00000984
		public IntermediateMesh PickVariation(ImmutableArray<IntermediateMesh> variations, SurfaceBlockShape surfaceBlockShape, Vector3Int coordinates)
		{
			Vector3Int coordinatesWithOffset = coordinates + Vector3Int.one;
			TerrainBlockRandomizer.SelectedVariation? selectedVariation = this._selectedVariations[coordinatesWithOffset.x, coordinatesWithOffset.y, coordinatesWithOffset.z];
			if (selectedVariation != null && selectedVariation.Value.SurfaceBlockShape == surfaceBlockShape)
			{
				return selectedVariation.Value.IntermediateMesh;
			}
			TerrainBlockRandomizer.SelectedVariation value = this.PickRandomVariation(variations, surfaceBlockShape, coordinatesWithOffset);
			this._selectedVariations[coordinatesWithOffset.x, coordinatesWithOffset.y, coordinatesWithOffset.z] = new TerrainBlockRandomizer.SelectedVariation?(value);
			return value.IntermediateMesh;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002828 File Offset: 0x00000A28
		public TerrainBlockRandomizer.SelectedVariation PickRandomVariation(ImmutableArray<IntermediateMesh> variations, SurfaceBlockShape surfaceBlockShape, Vector3Int coordinatesWithOffset)
		{
			int length = variations.Length;
			int num = 0;
			IntermediateMesh intermediateMesh;
			do
			{
				int index = this._randomNumberGenerator.Range(0, length);
				intermediateMesh = variations[index];
			}
			while (length > 1 && num++ < TerrainBlockRandomizer.MaxAttempts && this.AnyNeighborIsTheSame(coordinatesWithOffset, intermediateMesh));
			return new TerrainBlockRandomizer.SelectedVariation(intermediateMesh, surfaceBlockShape);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002878 File Offset: 0x00000A78
		public bool AnyNeighborIsTheSame(Vector3Int coordinatesWithOffset, IntermediateMesh intermediateMesh)
		{
			if (coordinatesWithOffset.x > 0 && coordinatesWithOffset.x < this._size.x - 1 && coordinatesWithOffset.y > 0 && coordinatesWithOffset.y < this._size.y - 1)
			{
				foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
				{
					Vector3Int vector3Int2 = coordinatesWithOffset + vector3Int;
					ref TerrainBlockRandomizer.SelectedVariation? ptr = ref this._selectedVariations[vector3Int2.x, vector3Int2.y, vector3Int2.z];
					if (((ptr != null) ? ptr.GetValueOrDefault().IntermediateMesh : null) == intermediateMesh)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000015 RID: 21
		public static readonly int MaxAttempts = 3;

		// Token: 0x04000016 RID: 22
		public readonly ITerrainService _terrainService;

		// Token: 0x04000017 RID: 23
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000018 RID: 24
		public Vector3Int _size;

		// Token: 0x04000019 RID: 25
		public TerrainBlockRandomizer.SelectedVariation?[,,] _selectedVariations;

		// Token: 0x0200000D RID: 13
		public readonly struct SelectedVariation
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000022 RID: 34 RVA: 0x00002937 File Offset: 0x00000B37
			public IntermediateMesh IntermediateMesh { get; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000023 RID: 35 RVA: 0x0000293F File Offset: 0x00000B3F
			public SurfaceBlockShape SurfaceBlockShape { get; }

			// Token: 0x06000024 RID: 36 RVA: 0x00002947 File Offset: 0x00000B47
			public SelectedVariation(IntermediateMesh intermediateMesh, SurfaceBlockShape surfaceBlockShape)
			{
				this.IntermediateMesh = intermediateMesh;
				this.SurfaceBlockShape = surfaceBlockShape;
			}
		}
	}
}
