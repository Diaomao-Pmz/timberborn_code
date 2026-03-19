using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000029 RID: 41
	public readonly struct VerticalShapeInfo : IEquatable<VerticalShapeInfo>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000061C2 File Offset: 0x000043C2
		public int TotalPrefabCount { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000061CA File Offset: 0x000043CA
		public GameObject StartPrefab { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000061D2 File Offset: 0x000043D2
		public GameObject RepeatingPrefab { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000061DA File Offset: 0x000043DA
		public string Name { get; }

		// Token: 0x06000117 RID: 279 RVA: 0x000061E2 File Offset: 0x000043E2
		public VerticalShapeInfo(int totalPrefabCount, GameObject startPrefab, GameObject repeatingPrefab, string name)
		{
			this.TotalPrefabCount = totalPrefabCount;
			this.StartPrefab = startPrefab;
			this.RepeatingPrefab = repeatingPrefab;
			this.Name = name;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006204 File Offset: 0x00004404
		public bool Equals(VerticalShapeInfo other)
		{
			return this.TotalPrefabCount == other.TotalPrefabCount && object.Equals(this.StartPrefab, other.StartPrefab) && object.Equals(this.RepeatingPrefab, other.RepeatingPrefab) && this.Name == other.Name;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000625C File Offset: 0x0000445C
		public override bool Equals(object obj)
		{
			if (obj is VerticalShapeInfo)
			{
				VerticalShapeInfo other = (VerticalShapeInfo)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006281 File Offset: 0x00004481
		public override int GetHashCode()
		{
			return HashCode.Combine<int, GameObject, GameObject, string>(this.TotalPrefabCount, this.StartPrefab, this.RepeatingPrefab, this.Name);
		}
	}
}
