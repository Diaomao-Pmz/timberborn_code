using System;

namespace Timberborn.GraphicsQualitySystem
{
	// Token: 0x02000007 RID: 7
	public class BloomSetting
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002280 File Offset: 0x00000480
		public static bool GetValueForPreset(GraphicsQualityPreset preset)
		{
			bool result;
			switch (preset)
			{
			case GraphicsQualityPreset.Ultra:
				result = true;
				break;
			case GraphicsQualityPreset.High:
				result = true;
				break;
			case GraphicsQualityPreset.Medium:
				result = true;
				break;
			case GraphicsQualityPreset.Low:
				result = false;
				break;
			default:
				throw new ArgumentException();
			}
			return result;
		}
	}
}
