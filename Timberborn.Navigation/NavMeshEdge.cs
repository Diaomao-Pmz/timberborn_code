using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000057 RID: 87
	public readonly struct NavMeshEdge : IEquatable<NavMeshEdge>
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005E90 File Offset: 0x00004090
		public Vector3Int Start { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00005E98 File Offset: 0x00004098
		public Vector3Int End { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005EA0 File Offset: 0x000040A0
		public int GroupId { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00005EA8 File Offset: 0x000040A8
		public bool IsRoad { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005EB0 File Offset: 0x000040B0
		public float Cost { get; }

		// Token: 0x060001AF RID: 431 RVA: 0x00005EB8 File Offset: 0x000040B8
		public NavMeshEdge(Vector3Int start, Vector3Int end, int groupId, bool isRoad, float cost)
		{
			this.Start = start;
			this.End = end;
			this.GroupId = groupId;
			this.IsRoad = isRoad;
			this.Cost = cost;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005EE0 File Offset: 0x000040E0
		public static NavMeshEdge CreateDefault(Vector3Int start, Vector3Int end)
		{
			float cost = Vector2Int.Distance(start.XY(), end.XY());
			return NavMeshEdge.CreateGrouped(start, end, 0, false, cost);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005F09 File Offset: 0x00004109
		public static NavMeshEdge CreateBlocking(Vector3Int start, Vector3Int end, int groupId)
		{
			return NavMeshEdge.CreateGrouped(start, end, groupId, false, 0f);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005F19 File Offset: 0x00004119
		public static NavMeshEdge CreateGrouped(Vector3Int start, Vector3Int end, int groupId, bool isRoad, float cost)
		{
			return new NavMeshEdge(start, end, groupId, isRoad, cost);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005F28 File Offset: 0x00004128
		public bool Equals(NavMeshEdge other)
		{
			return this.Start.Equals(other.Start) && this.End.Equals(other.End) && this.Cost.Equals(other.Cost) && this.IsRoad == other.IsRoad && this.GroupId == other.GroupId;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005F9C File Offset: 0x0000419C
		public override bool Equals(object obj)
		{
			if (obj is NavMeshEdge)
			{
				NavMeshEdge other = (NavMeshEdge)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005FC4 File Offset: 0x000041C4
		public override int GetHashCode()
		{
			return (((this.Start.GetHashCode() * 397 * 397 ^ this.End.GetHashCode()) * 397 ^ this.GroupId.GetHashCode()) * 397 ^ this.IsRoad.GetHashCode()) * 397 ^ this.Cost.GetHashCode();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006045 File Offset: 0x00004245
		public static bool operator ==(NavMeshEdge left, NavMeshEdge right)
		{
			return left.Equals(right);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000604F File Offset: 0x0000424F
		public static bool operator !=(NavMeshEdge left, NavMeshEdge right)
		{
			return !left.Equals(right);
		}
	}
}
