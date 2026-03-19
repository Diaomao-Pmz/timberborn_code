using System;
using System.Collections.Generic;

namespace Timberborn.Common
{
	// Token: 0x02000025 RID: 37
	public readonly struct NullableKey<T> : IEquatable<NullableKey<T>> where T : class
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000033BB File Offset: 0x000015BB
		public T Key { get; }

		// Token: 0x06000084 RID: 132 RVA: 0x000033C3 File Offset: 0x000015C3
		public NullableKey(T key)
		{
			this.Key = key;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000033CC File Offset: 0x000015CC
		public bool Equals(NullableKey<T> other)
		{
			return EqualityComparer<T>.Default.Equals(this.Key, other.Key);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000033E8 File Offset: 0x000015E8
		public override bool Equals(object obj)
		{
			if (obj is NullableKey<T>)
			{
				NullableKey<T> other = (NullableKey<T>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000340D File Offset: 0x0000160D
		public override int GetHashCode()
		{
			return EqualityComparer<T>.Default.GetHashCode(this.Key);
		}
	}
}
