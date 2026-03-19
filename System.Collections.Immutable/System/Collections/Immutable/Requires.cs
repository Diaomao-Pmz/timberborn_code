using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000052 RID: 82
	[NullableContext(2)]
	[Nullable(0)]
	internal static class Requires
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0000A31B File Offset: 0x0000851B
		[NullableContext(1)]
		[DebuggerStepThrough]
		public static void NotNull<T>([NotNull] T value, [Nullable(2)] string parameterName) where T : class
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000A32B File Offset: 0x0000852B
		[NullableContext(1)]
		[DebuggerStepThrough]
		public static T NotNullPassthrough<T>([NotNull] T value, [Nullable(2)] string parameterName) where T : class
		{
			Requires.NotNull<T>(value, parameterName);
			return value;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000A335 File Offset: 0x00008535
		[DebuggerStepThrough]
		public static void NotNullAllowStructs<T>([Nullable(1)] [NotNull] T value, string parameterName)
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000A345 File Offset: 0x00008545
		[DoesNotReturn]
		[DebuggerStepThrough]
		public static void FailArgumentNullException(string parameterName)
		{
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000A34D File Offset: 0x0000854D
		[DebuggerStepThrough]
		public static void Range([DoesNotReturnIf(false)] bool condition, string parameterName, string message = null)
		{
			if (!condition)
			{
				Requires.FailRange(parameterName, message);
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000A359 File Offset: 0x00008559
		[DoesNotReturn]
		[DebuggerStepThrough]
		public static void FailRange(string parameterName, string message = null)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
			throw new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A371 File Offset: 0x00008571
		[DebuggerStepThrough]
		public static void Argument([DoesNotReturnIf(false)] bool condition, string parameterName, string message)
		{
			if (!condition)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000A37E File Offset: 0x0000857E
		[DebuggerStepThrough]
		public static void Argument([DoesNotReturnIf(false)] bool condition)
		{
			if (!condition)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000A389 File Offset: 0x00008589
		[NullableContext(1)]
		[DoesNotReturn]
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void FailObjectDisposed<[Nullable(2)] TDisposed>(TDisposed disposed)
		{
			throw new ObjectDisposedException(disposed.GetType().FullName);
		}
	}
}
