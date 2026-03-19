using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ThrowHelper
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002DAA File Offset: 0x00000FAA
		public static void ThrowIfNull(object arg, [Nullable(2)] [CallerArgumentExpression("arg")] string paramName = null)
		{
			if (arg == null)
			{
				ThrowHelper.ThrowArgumentNullException(paramName);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002DB5 File Offset: 0x00000FB5
		[DoesNotReturn]
		public static void ThrowIfDestinationTooSmall()
		{
			throw new ArgumentException(SR.CapacityMustBeGreaterThanOrEqualToCount, "destination");
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002DC6 File Offset: 0x00000FC6
		[NullableContext(2)]
		[DoesNotReturn]
		public static void ThrowArgumentNullException(string paramName)
		{
			throw new ArgumentNullException(paramName);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002DCE File Offset: 0x00000FCE
		[DoesNotReturn]
		public static void ThrowKeyNotFoundException()
		{
			throw new KeyNotFoundException();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002DD5 File Offset: 0x00000FD5
		[DoesNotReturn]
		public static void ThrowKeyNotFoundException<[Nullable(2)] TKey>(TKey key)
		{
			throw new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002DEC File Offset: 0x00000FEC
		[DoesNotReturn]
		public static void ThrowInvalidOperationException()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002DF3 File Offset: 0x00000FF3
		[DoesNotReturn]
		internal static void ThrowIncompatibleComparer()
		{
			throw new InvalidOperationException(SR.InvalidOperation_IncompatibleComparer);
		}
	}
}
