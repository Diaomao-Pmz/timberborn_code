using System;
using System.Runtime.InteropServices;

namespace Timberborn.Common
{
	// Token: 0x0200001A RID: 26
	public class FakeRandomNumberGeneratorFactory : IFakeRandomNumberGeneratorFactory
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002DFC File Offset: 0x00000FFC
		public unsafe IFakeRandomNumberGenerator Create(Guid guid, int salt)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)16], 16);
			if (guid.TryWriteBytes(span))
			{
				Span<int> span2 = MemoryMarshal.Cast<byte, int>(span);
				return new FakeRandomNumberGenerator(*span2[0] ^ *span2[1] ^ *span2[2] ^ *span2[3] ^ salt);
			}
			string str = "Failed to write bytes from Guid: ";
			Guid guid2 = guid;
			throw new InvalidOperationException(str + guid2.ToString());
		}
	}
}
