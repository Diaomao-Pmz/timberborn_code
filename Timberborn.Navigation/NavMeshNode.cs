using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200005B RID: 91
	public readonly struct NavMeshNode : IEquatable<NavMeshNode>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006375 File Offset: 0x00004575
		public int Id { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000637D File Offset: 0x0000457D
		public int GroupId { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006385 File Offset: 0x00004585
		public float Cost { get; }

		// Token: 0x060001CB RID: 459 RVA: 0x0000638D File Offset: 0x0000458D
		public NavMeshNode(int id, int groupId, float cost)
		{
			this.Id = id;
			this.Cost = cost;
			this.GroupId = groupId;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000063A4 File Offset: 0x000045A4
		public bool Equals(NavMeshNode other)
		{
			return this.Id == other.Id && this.Cost.Equals(other.Cost) && this.GroupId == other.GroupId;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000063E8 File Offset: 0x000045E8
		public override bool Equals(object obj)
		{
			if (obj is NavMeshNode)
			{
				NavMeshNode other = (NavMeshNode)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006410 File Offset: 0x00004610
		public override int GetHashCode()
		{
			return (this.Id.GetHashCode() * 397 * 397 ^ this.GroupId.GetHashCode()) * 397 ^ this.Cost.GetHashCode();
		}
	}
}
