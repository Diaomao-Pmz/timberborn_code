using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x0200000B RID: 11
	public class ExplosionData
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002951 File Offset: 0x00000B51
		public float Radius { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002959 File Offset: 0x00000B59
		public Vector3 Center { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002961 File Offset: 0x00000B61
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002969 File Offset: 0x00000B69
		public int CurrentExplosionRadius { get; private set; }

		// Token: 0x0600003A RID: 58 RVA: 0x00002972 File Offset: 0x00000B72
		public ExplosionData(float radius, Vector3 center, int currentExplosionRadius = 0)
		{
			this.Radius = radius;
			this.Center = center;
			this.CurrentExplosionRadius = currentExplosionRadius;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000299A File Offset: 0x00000B9A
		public void InitializeAffectedTiles(ExplosionOutcomeGatherer outcomeGatherer)
		{
			this._affectedTiles = outcomeGatherer.GetAffectedTilesPerRadius(this.Center, this.Radius);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029B4 File Offset: 0x00000BB4
		public bool TryGetExplosionOutcomeForCurrentRadius(out ReadOnlyHashSet<Vector3Int> readOnlyAffectedTiles)
		{
			HashSet<Vector3Int> set;
			if (this._affectedTiles.TryGetValue(this.CurrentExplosionRadius, out set))
			{
				readOnlyAffectedTiles = set.AsReadOnlyHashSet<Vector3Int>();
				return true;
			}
			return false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000029E8 File Offset: 0x00000BE8
		public bool MoveToNextRadius()
		{
			Dictionary<int, HashSet<Vector3Int>> affectedTiles = this._affectedTiles;
			int currentExplosionRadius = this.CurrentExplosionRadius;
			this.CurrentExplosionRadius = currentExplosionRadius + 1;
			return affectedTiles.ContainsKey(currentExplosionRadius);
		}

		// Token: 0x04000022 RID: 34
		public Dictionary<int, HashSet<Vector3Int>> _affectedTiles = new Dictionary<int, HashSet<Vector3Int>>();
	}
}
