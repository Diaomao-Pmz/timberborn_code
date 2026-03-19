using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200001E RID: 30
	public class PathMeshDrawerFactory : ILoadableSingleton
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003FC1 File Offset: 0x000021C1
		public PathMeshDrawerFactory(DistanceToColorConverter distanceToColorConverter, ISpecService specService)
		{
			this._distanceToColorConverter = distanceToColorConverter;
			this._specService = specService;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003FE2 File Offset: 0x000021E2
		public void Load()
		{
			this._pathMeshDrawerFactorySpec = this._specService.GetSingleSpec<PathMeshDrawerFactorySpec>();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003FF5 File Offset: 0x000021F5
		public PathMeshDrawer CreateRegularDrawer(PathMeshDrawer.ConnectionKey connectionKey)
		{
			return this.Create(this._pathMeshDrawerFactorySpec.RegularModelVariants, connectionKey);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000400E File Offset: 0x0000220E
		public PathMeshDrawer CreateStairsDrawer(PathMeshDrawer.ConnectionKey connectionKey)
		{
			return this.Create(this._pathMeshDrawerFactorySpec.StairsModelVariants, connectionKey);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004027 File Offset: 0x00002227
		public PathMeshDrawer Create(IEnumerable<AssetRef<Mesh>> meshes, PathMeshDrawer.ConnectionKey connectionKey)
		{
			return new PathMeshDrawer(this._distanceToColorConverter, connectionKey, this.GenerateNeighboredMeshes(meshes), this._pathMeshDrawerFactorySpec.Material.Asset);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000404C File Offset: 0x0000224C
		public NeighboredValues4<IntermediateMesh> GenerateNeighboredMeshes(IEnumerable<AssetRef<Mesh>> meshes)
		{
			NeighboredValues4<IntermediateMesh> neighboredValues = new NeighboredValues4<IntermediateMesh>();
			Material[] materials = new Material[]
			{
				this._pathMeshDrawerFactorySpec.Material.Asset
			};
			foreach (AssetRef<Mesh> assetRef in meshes)
			{
				Mesh asset = assetRef.Asset;
				ValueTuple<byte, byte, byte, byte> valueTuple = PathMeshDrawerFactory.VariantNameToByteKeys(asset.name);
				byte item = valueTuple.Item1;
				byte item2 = valueTuple.Item2;
				byte item3 = valueTuple.Item3;
				byte item4 = valueTuple.Item4;
				this.AddVariant(neighboredValues, asset, materials, Orientation.Cw0, item, item2, item3, item4);
				this.AddVariant(neighboredValues, asset, materials, Orientation.Cw90, item4, item, item2, item3);
				this.AddVariant(neighboredValues, asset, materials, Orientation.Cw180, item3, item4, item, item2);
				this.AddVariant(neighboredValues, asset, materials, Orientation.Cw270, item2, item3, item4, item);
			}
			return neighboredValues;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004128 File Offset: 0x00002328
		[return: TupleElementNames(new string[]
		{
			"down",
			"left",
			"up",
			"right"
		})]
		public static ValueTuple<byte, byte, byte, byte> VariantNameToByteKeys(string name)
		{
			string text = name.Substring(name.Length - 4);
			byte item = PathMeshConnectionKeys.ParseCharToByteKey(text[0]);
			byte item2 = PathMeshConnectionKeys.ParseCharToByteKey(text[1]);
			byte item3 = PathMeshConnectionKeys.ParseCharToByteKey(text[2]);
			byte item4 = PathMeshConnectionKeys.ParseCharToByteKey(text[3]);
			return new ValueTuple<byte, byte, byte, byte>(item, item2, item3, item4);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000417F File Offset: 0x0000237F
		public void AddVariant(NeighboredValues4<IntermediateMesh> meshes, Mesh mesh, Material[] materials, Orientation orientation, byte down, byte left, byte up, byte right)
		{
			this._meshBuilder.Reset("");
			this._meshBuilder.AppendMesh<OrientationTransform>(mesh, materials, new OrientationTransform(orientation));
			meshes.AddExact(this._meshBuilder.BuildIntermediateMesh(), down, left, up, right);
		}

		// Token: 0x0400006C RID: 108
		public readonly DistanceToColorConverter _distanceToColorConverter;

		// Token: 0x0400006D RID: 109
		public readonly ISpecService _specService;

		// Token: 0x0400006E RID: 110
		public PathMeshDrawerFactorySpec _pathMeshDrawerFactorySpec;

		// Token: 0x0400006F RID: 111
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();
	}
}
