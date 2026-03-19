using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000068 RID: 104
	public readonly struct PathCorner : IEquatable<PathCorner>
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00007666 File Offset: 0x00005866
		public Vector3 Position { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000766E File Offset: 0x0000586E
		public float Speed { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00007676 File Offset: 0x00005876
		public int GroupId { get; }

		// Token: 0x06000233 RID: 563 RVA: 0x0000767E File Offset: 0x0000587E
		public PathCorner(Vector3 position, float speed, int groupId)
		{
			this.Position = position;
			this.Speed = speed;
			this.GroupId = groupId;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007698 File Offset: 0x00005898
		public bool Equals(PathCorner other)
		{
			return this.Position.Equals(other.Position) && this.Speed.Equals(other.Speed) && this.GroupId == other.GroupId;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000076E4 File Offset: 0x000058E4
		public override bool Equals(object obj)
		{
			if (obj is PathCorner)
			{
				PathCorner other = (PathCorner)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000770C File Offset: 0x0000590C
		public override int GetHashCode()
		{
			return (this.Position.GetHashCode() * 397 ^ this.Speed.GetHashCode()) * 397 ^ this.GroupId.GetHashCode();
		}
	}
}
