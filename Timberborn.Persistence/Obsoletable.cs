using System;

namespace Timberborn.Persistence
{
	// Token: 0x0200000E RID: 14
	public readonly struct Obsoletable<T>
	{
		// Token: 0x060000BE RID: 190 RVA: 0x0000295E File Offset: 0x00000B5E
		public Obsoletable(T value)
		{
			this._value = value;
			this._upToDate = true;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000296E File Offset: 0x00000B6E
		public T Value
		{
			get
			{
				if (!this.Obsolete)
				{
					return this._value;
				}
				throw new InvalidOperationException("Can't access Value, value's obsolete");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002989 File Offset: 0x00000B89
		public bool Obsolete
		{
			get
			{
				return !this._upToDate;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002994 File Offset: 0x00000B94
		public static implicit operator Obsoletable<T>(T value)
		{
			return new Obsoletable<T>(value);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000299C File Offset: 0x00000B9C
		public static explicit operator T(Obsoletable<T> obsoletable)
		{
			if (!obsoletable.Obsolete)
			{
				return obsoletable.Value;
			}
			throw new InvalidOperationException(string.Format("Can't convert to {0}, value's obsolete", typeof(T)));
		}

		// Token: 0x04000009 RID: 9
		public readonly T _value;

		// Token: 0x0400000A RID: 10
		public readonly bool _upToDate;
	}
}
