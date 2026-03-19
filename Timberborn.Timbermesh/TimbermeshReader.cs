using System;
using System.IO;
using System.IO.Compression;
using ProtoBuf;
using Timberborn.TimbermeshDTO;

namespace Timberborn.Timbermesh
{
	// Token: 0x02000010 RID: 16
	public static class TimbermeshReader
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000028F0 File Offset: 0x00000AF0
		public static Model ReadFromStream(Stream stream)
		{
			TimbermeshReader.ValidateFileHeader(stream);
			Model result;
			using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, true))
			{
				result = Serializer.Deserialize<Model>(deflateStream);
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002930 File Offset: 0x00000B30
		public static void ValidateFileHeader(Stream stream)
		{
			if (stream.ReadByte() != (int)TimbermeshReader.FirstZLibHeaderByte || stream.ReadByte() != (int)TimbermeshReader.SecondZLibHeaderByte)
			{
				throw new Exception("Incorrect Zlib compression file header");
			}
		}

		// Token: 0x04000015 RID: 21
		public static readonly byte FirstZLibHeaderByte = 120;

		// Token: 0x04000016 RID: 22
		public static readonly byte SecondZLibHeaderByte = 156;
	}
}
