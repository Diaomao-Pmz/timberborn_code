using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x0200001E RID: 30
	internal static class BitOperations
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00002D98 File Offset: 0x00000F98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RotateLeft(uint value, int offset)
		{
			return value << offset | value >> 32 - offset;
		}
	}
}
