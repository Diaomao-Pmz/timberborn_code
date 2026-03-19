using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.PrefabOptimization;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000009 RID: 9
	public class SurfaceBlockCollection
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021A8 File Offset: 0x000003A8
		public SurfaceBlockCollection(Dictionary<SurfaceBlockShape, List<IntermediateMesh>> variations)
		{
			this._variations = new Dictionary<int, ImmutableArray<IntermediateMesh>>();
			foreach (KeyValuePair<SurfaceBlockShape, List<IntermediateMesh>> keyValuePair in variations)
			{
				this._variations[(int)keyValuePair.Key.Index] = keyValuePair.Value.ToImmutableArray<IntermediateMesh>();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002228 File Offset: 0x00000428
		public ImmutableArray<IntermediateMesh> GetVariations(SurfaceBlockShape shape)
		{
			ImmutableArray<IntermediateMesh> immutableArray = this._variations[(int)shape.Index];
			if (new ImmutableArray<IntermediateMesh>?(immutableArray) == null)
			{
				Debug.LogWarning(string.Format("Couldn't find a surface block of shape {0}.", shape));
				return ImmutableArray<IntermediateMesh>.Empty;
			}
			return immutableArray;
		}

		// Token: 0x0400000E RID: 14
		public readonly Dictionary<int, ImmutableArray<IntermediateMesh>> _variations;
	}
}
