using System;
using Timberborn.NeedSpecs;

namespace Timberborn.Effects
{
	// Token: 0x02000004 RID: 4
	public readonly struct ContinuousEffect : IEquatable<ContinuousEffect>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public string NeedId { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public float PointsPerHour { get; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public ContinuousEffect(string needId, float pointsPerHour)
		{
			this.NeedId = needId;
			this.PointsPerHour = pointsPerHour;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DE File Offset: 0x000002DE
		public static ContinuousEffect FromSpec(ContinuousEffectSpec continuousEffectSpec)
		{
			return new ContinuousEffect(continuousEffectSpec.NeedId, continuousEffectSpec.PointsPerHour);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
		public bool Equals(ContinuousEffect other)
		{
			return this.NeedId == other.NeedId && this.PointsPerHour.Equals(other.PointsPerHour);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212C File Offset: 0x0000032C
		public override bool Equals(object obj)
		{
			if (obj is ContinuousEffect)
			{
				ContinuousEffect other = (ContinuousEffect)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002151 File Offset: 0x00000351
		public override int GetHashCode()
		{
			return HashCode.Combine<string, float>(this.NeedId, this.PointsPerHour);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002164 File Offset: 0x00000364
		public static bool operator ==(ContinuousEffect left, ContinuousEffect right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000216E File Offset: 0x0000036E
		public static bool operator !=(ContinuousEffect left, ContinuousEffect right)
		{
			return !left.Equals(right);
		}
	}
}
