using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.InteropServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200006A RID: 106
	internal static class Hashing
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public unsafe static int GetHashCodeOrdinal(ReadOnlySpan<char> s)
		{
			int i = s.Length;
			fixed (char* reference = MemoryMarshal.GetReference<char>(s))
			{
				char* ptr = reference;
				switch (i)
				{
				case 0:
					return 757602046;
				case 1:
				{
					uint num = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ (uint)(*ptr);
					return (int)(352654597U + num * 1566083941U);
				}
				case 2:
				{
					uint num = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ (uint)(*ptr);
					num = (BitOperations.RotateLeft(num, 5) + num ^ (uint)ptr[1]);
					return (int)(352654597U + num * 1566083941U);
				}
				case 3:
				{
					uint num = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ (uint)(*ptr);
					num = (BitOperations.RotateLeft(num, 5) + num ^ (uint)ptr[1]);
					num = (BitOperations.RotateLeft(num, 5) + num ^ (uint)ptr[2]);
					return (int)(352654597U + num * 1566083941U);
				}
				case 4:
				{
					uint num2 = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ *(uint*)ptr;
					uint num = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ *(uint*)(ptr + 2);
					return (int)(num2 + num * 1566083941U);
				}
				default:
				{
					uint num2 = 352654597U;
					uint num = num2;
					uint* ptr2 = (uint*)ptr;
					while (i >= 4)
					{
						num2 = (BitOperations.RotateLeft(num2, 5) + num2 ^ *ptr2);
						num = (BitOperations.RotateLeft(num, 5) + num ^ ptr2[1]);
						ptr2 += 2;
						i -= 4;
					}
					char* ptr3 = (char*)ptr2;
					while (i-- > 0)
					{
						num = (BitOperations.RotateLeft(num, 5) + num ^ (uint)(*(ptr3++)));
					}
					return (int)(num2 + num * 1566083941U);
				}
				}
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000CE08 File Offset: 0x0000B008
		public unsafe static int GetHashCodeOrdinalIgnoreCaseAscii(ReadOnlySpan<char> s)
		{
			int i = s.Length;
			fixed (char* reference = MemoryMarshal.GetReference<char>(s))
			{
				char* ptr = reference;
				switch (i)
				{
				case 0:
					return 757602046;
				case 1:
				{
					uint num = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ (uint)(*ptr | ' ');
					return (int)(352654597U + num * 1566083941U);
				}
				case 2:
				{
					uint num = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ (uint)(*ptr | ' ');
					num = (BitOperations.RotateLeft(num, 5) + num ^ (uint)(ptr[1] | ' '));
					return (int)(352654597U + num * 1566083941U);
				}
				case 3:
				{
					uint num = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ (uint)(*ptr | ' ');
					num = (BitOperations.RotateLeft(num, 5) + num ^ (uint)(ptr[1] | ' '));
					num = (BitOperations.RotateLeft(num, 5) + num ^ (uint)(ptr[2] | ' '));
					return (int)(352654597U + num * 1566083941U);
				}
				case 4:
				{
					uint num2 = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ (*(uint*)ptr | 2097184U);
					uint num = BitOperations.RotateLeft(352654597U, 5) + 352654597U ^ (*(uint*)(ptr + 2) | 2097184U);
					return (int)(num2 + num * 1566083941U);
				}
				default:
				{
					uint num2 = 352654597U;
					uint num = num2;
					uint* ptr2 = (uint*)ptr;
					while (i >= 4)
					{
						num2 = (BitOperations.RotateLeft(num2, 5) + num2 ^ (*ptr2 | 2097184U));
						num = (BitOperations.RotateLeft(num, 5) + num ^ (ptr2[1] | 2097184U));
						ptr2 += 2;
						i -= 4;
					}
					char* ptr3 = (char*)ptr2;
					while (i-- > 0)
					{
						num = (BitOperations.RotateLeft(num, 5) + num ^ ((uint)(*ptr3) | 2097184U));
						ptr3++;
					}
					return (int)(num2 + num * 1566083941U);
				}
				}
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		public unsafe static int GetHashCodeOrdinalIgnoreCase(ReadOnlySpan<char> s)
		{
			int num = s.Length;
			char[] array = null;
			Span<char> span;
			if (num <= 256)
			{
				span = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
			}
			else
			{
				span = (array = ArrayPool<char>.Shared.Rent(num));
			}
			Span<char> span2 = span;
			num = MemoryExtensions.ToUpperInvariant(s, span2);
			int hashCodeOrdinal = Hashing.GetHashCodeOrdinal(span2.Slice(0, num));
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return hashCodeOrdinal;
		}

		// Token: 0x04000076 RID: 118
		private const uint Hash1Start = 352654597U;

		// Token: 0x04000077 RID: 119
		private const uint Factor = 1566083941U;
	}
}
