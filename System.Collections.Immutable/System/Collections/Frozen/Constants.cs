using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000053 RID: 83
	[NullableContext(2)]
	[Nullable(0)]
	internal static class Constants
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public static bool IsKnownComparable<T>()
		{
			return typeof(T) == typeof(bool) || typeof(T) == typeof(sbyte) || typeof(T) == typeof(byte) || typeof(T) == typeof(char) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort) || typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(long) || typeof(T) == typeof(ulong) || typeof(T) == typeof(decimal) || typeof(T) == typeof(float) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal) || typeof(T) == typeof(TimeSpan) || typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTimeOffset) || typeof(T) == typeof(Guid) || typeof(T).IsEnum;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000A5D0 File Offset: 0x000087D0
		internal static bool KeysAreHashCodes<T>()
		{
			return typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort) || typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte) || ((typeof(T) == typeof(IntPtr) || typeof(T) == typeof(UIntPtr)) && IntPtr.Size == 4);
		}

		// Token: 0x04000059 RID: 89
		public const int MaxItemsInSmallFrozenCollection = 4;

		// Token: 0x0400005A RID: 90
		public const int MaxItemsInSmallValueTypeFrozenCollection = 10;
	}
}
