using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000023 RID: 35
	public readonly struct FlowFieldPathNode : IEquatable<FlowFieldPathNode>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000046BB File Offset: 0x000028BB
		public Vector3 Position { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000046C3 File Offset: 0x000028C3
		public float Cost { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000046CB File Offset: 0x000028CB
		public float DistanceToNext { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000046D3 File Offset: 0x000028D3
		public int GroupId { get; }

		// Token: 0x060000EB RID: 235 RVA: 0x000046DB File Offset: 0x000028DB
		public FlowFieldPathNode(Vector3 position, float cost, float distanceToNext, int groupId)
		{
			this.Position = position;
			this.Cost = cost;
			this.DistanceToNext = distanceToNext;
			this.GroupId = groupId;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000046FA File Offset: 0x000028FA
		public float NormalizedSpeed
		{
			get
			{
				if (this.Cost != 0f)
				{
					return this.DistanceToNext / this.Cost;
				}
				return float.MaxValue;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000471C File Offset: 0x0000291C
		public bool Equals(FlowFieldPathNode other)
		{
			return this.Position.Equals(other.Position) && this.Cost.Equals(other.Cost) && this.DistanceToNext.Equals(other.DistanceToNext) && this.GroupId == other.GroupId;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004780 File Offset: 0x00002980
		public override bool Equals(object obj)
		{
			if (obj is FlowFieldPathNode)
			{
				FlowFieldPathNode other = (FlowFieldPathNode)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000047A8 File Offset: 0x000029A8
		public override int GetHashCode()
		{
			return ((this.Position.GetHashCode() * 397 * 397 ^ this.Cost.GetHashCode()) * 397 ^ this.DistanceToNext.GetHashCode()) * 397 ^ this.GroupId.GetHashCode();
		}
	}
}
