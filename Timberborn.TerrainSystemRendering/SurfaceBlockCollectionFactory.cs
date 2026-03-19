using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.PrefabOptimization;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x0200000A RID: 10
	public class SurfaceBlockCollectionFactory
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000227C File Offset: 0x0000047C
		public SurfaceBlockCollection CreateFromModels(IEnumerable<GameObject> models)
		{
			Dictionary<SurfaceBlockShape, List<IntermediateMesh>> dictionary = new Dictionary<SurfaceBlockShape, List<IntermediateMesh>>();
			Dictionary<SurfaceBlockShape, List<GameObject>> dictionary2 = new Dictionary<SurfaceBlockShape, List<GameObject>>();
			foreach (GameObject gameObject in models)
			{
				SurfaceBlockShape surfaceBlockShape = SurfaceBlockShape.FromModelName(gameObject.name);
				dictionary2.GetOrAdd(surfaceBlockShape).Add(gameObject);
				this.AddAllVariations(surfaceBlockShape, gameObject, dictionary);
			}
			return new SurfaceBlockCollection(dictionary);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022F4 File Offset: 0x000004F4
		public void AddAllVariations(SurfaceBlockShape baseShape, GameObject model, Dictionary<SurfaceBlockShape, List<IntermediateMesh>> multimap)
		{
			foreach (Orientation orientation in OrientationExtensions.AllValues())
			{
				SurfaceBlockShape key = baseShape.Rotate(orientation);
				Orientation orientation2 = orientation.Flip();
				string name = string.Format("{0}-{1}", model.name, orientation);
				this._meshBuilder.Reset(name);
				Mesh sharedMesh = model.GetComponent<MeshFilter>().sharedMesh;
				Material[] sharedMaterials = model.GetComponent<MeshRenderer>().sharedMaterials;
				this._meshBuilder.AppendMesh<OrientationTransform>(sharedMesh, sharedMaterials, new OrientationTransform(orientation2));
				IntermediateMesh item = this._meshBuilder.BuildIntermediateMesh();
				multimap.GetOrAdd(key).Add(item);
			}
		}

		// Token: 0x0400000F RID: 15
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();
	}
}
