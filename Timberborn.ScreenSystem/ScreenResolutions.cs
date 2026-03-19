using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.ScreenSystem
{
	// Token: 0x02000006 RID: 6
	public static class ScreenResolutions
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002124 File Offset: 0x00000324
		public static IEnumerable<ScreenResolution> AvailableResolutions()
		{
			if (Screen.resolutions.Length != 0)
			{
				return (from resolution in Screen.resolutions
				where resolution.height >= ScreenResolutions.MinResolutionHeight
				orderby resolution.width, resolution.height
				select new ScreenResolution(resolution.width, resolution.height)).Distinct<ScreenResolution>();
			}
			return ScreenResolutions.FallbackResolutions();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021DC File Offset: 0x000003DC
		public static IEnumerable<ScreenResolution> FallbackResolutions()
		{
			return new ScreenResolution[]
			{
				new ScreenResolution(1024, 768),
				new ScreenResolution(1280, 720),
				new ScreenResolution(1280, 800),
				new ScreenResolution(1280, 1024),
				new ScreenResolution(1360, 768),
				new ScreenResolution(1366, 768),
				new ScreenResolution(1440, 900),
				new ScreenResolution(1536, 864),
				new ScreenResolution(1600, 900),
				new ScreenResolution(1680, 1050),
				new ScreenResolution(1920, 1080),
				new ScreenResolution(1920, 1200),
				new ScreenResolution(2048, 1152),
				new ScreenResolution(2560, 1080),
				new ScreenResolution(2560, 1440),
				new ScreenResolution(3440, 1440),
				new ScreenResolution(3840, 2160)
			};
		}

		// Token: 0x0400000B RID: 11
		public static readonly int MinResolutionHeight = 700;
	}
}
