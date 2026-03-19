using System;
using System.Collections.Generic;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000007 RID: 7
	public class AccessibleDestination : IDestination, IEquatable<AccessibleDestination>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public Accessible Accessible { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public AccessibleDestination(Accessible accessible)
		{
			this.Accessible = accessible;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002117 File Offset: 0x00000317
		public bool FindPath(Vector3 start, List<PathCorner> pathCorners, out float distance)
		{
			if (!this.Accessible)
			{
				distance = 0f;
				return false;
			}
			return this.Accessible.FindPathUnlimitedRange(start, pathCorners, out distance);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213D File Offset: 0x0000033D
		public bool Equals(AccessibleDestination other)
		{
			return other != null && (this == other || object.Equals(this.Accessible, other.Accessible));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000215B File Offset: 0x0000035B
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((AccessibleDestination)obj)));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002189 File Offset: 0x00000389
		public override int GetHashCode()
		{
			if (this.Accessible == null)
			{
				return 0;
			}
			return this.Accessible.GetHashCode();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A0 File Offset: 0x000003A0
		public static bool operator ==(AccessibleDestination left, AccessibleDestination right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A9 File Offset: 0x000003A9
		public static bool operator !=(AccessibleDestination left, AccessibleDestination right)
		{
			return !object.Equals(left, right);
		}
	}
}
