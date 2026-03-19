using System;

namespace Timberborn.PlatformUtilities
{
	// Token: 0x0200000C RID: 12
	public static class ScrollWheelSpeed
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000024C4 File Offset: 0x000006C4
		public static Lazy<float> WheelScrollSize { get; } = new Lazy<float>(delegate()
		{
			if (!ApplicationPlatform.IsMacOS())
			{
				return ScrollWheelSpeed.WindowsWheelScrollSize;
			}
			return ScrollWheelSpeed.MacOSWheelScrollSize;
		});

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024CB File Offset: 0x000006CB
		public static Lazy<float> NormalizedScrollAxis { get; } = new Lazy<float>(delegate()
		{
			if (!ApplicationPlatform.IsMacOS())
			{
				return ScrollWheelSpeed.WindowsNormalizedScrollAxis;
			}
			return ScrollWheelSpeed.MacOsNormalizedScrollAxis;
		});

		// Token: 0x0400000C RID: 12
		public static readonly float WindowsWheelScrollSize = 10f;

		// Token: 0x0400000D RID: 13
		public static readonly float MacOSWheelScrollSize = 5f;

		// Token: 0x0400000E RID: 14
		public static readonly float WindowsNormalizedScrollAxis = 2.8f;

		// Token: 0x0400000F RID: 15
		public static readonly float MacOsNormalizedScrollAxis = 14f;
	}
}
