using System;
using System.Collections.Immutable;
using System.IO;
using UnityEngine;

namespace Timberborn.ErrorReporting
{
	// Token: 0x02000010 RID: 16
	public static class WorldDataService
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003251 File Offset: 0x00001451
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00003258 File Offset: 0x00001458
		public static string SourceFileName { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003260 File Offset: 0x00001460
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00003267 File Offset: 0x00001467
		public static ImmutableArray<byte> Data { get; private set; }

		// Token: 0x06000049 RID: 73 RVA: 0x00003270 File Offset: 0x00001470
		public static void SetFromStream(string fileName, Stream stream)
		{
			WorldDataService.SourceFileName = fileName;
			try
			{
				WorldDataService.Data = WorldDataService.GetBytes(stream).ToImmutableArray<byte>();
			}
			catch
			{
				Debug.Log(string.Format("Unable to create {0} from {1}", "Data", typeof(Stream)));
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000032C8 File Offset: 0x000014C8
		public static void Clear()
		{
			WorldDataService.SourceFileName = null;
			WorldDataService.Data = ImmutableArray<byte>.Empty;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000032DC File Offset: 0x000014DC
		public static byte[] GetBytes(Stream stream)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				stream.Position = 0L;
				result = memoryStream.ToArray();
			}
			return result;
		}
	}
}
