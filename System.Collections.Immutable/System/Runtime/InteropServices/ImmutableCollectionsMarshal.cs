using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000019 RID: 25
	[NullableContext(2)]
	[Nullable(0)]
	public static class ImmutableCollectionsMarshal
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002CF0 File Offset: 0x00000EF0
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> AsImmutableArray<T>([Nullable(new byte[]
		{
			2,
			1
		})] T[] array)
		{
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002CF8 File Offset: 0x00000EF8
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		public static T[] AsArray<T>([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> array)
		{
			return array.array;
		}
	}
}
