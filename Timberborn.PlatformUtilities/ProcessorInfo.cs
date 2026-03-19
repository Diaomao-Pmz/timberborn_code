using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Timberborn.PlatformUtilities
{
	// Token: 0x0200000B RID: 11
	public static class ProcessorInfo
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000248E File Offset: 0x0000068E
		public static bool IsAppleCpu()
		{
			return SystemInfo.processorType.ToLowerInvariant().Contains("apple");
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024A4 File Offset: 0x000006A4
		public static bool IsIntelProcess()
		{
			return RuntimeInformation.ProcessArchitecture == Architecture.X64;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024AE File Offset: 0x000006AE
		public static int GetPhysicalProcessorCount()
		{
			if (!ProcessorInfo.IsAppleCpu())
			{
				return Environment.ProcessorCount / 2;
			}
			return Environment.ProcessorCount;
		}
	}
}
