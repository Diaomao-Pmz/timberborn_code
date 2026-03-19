using System;

namespace Timberborn.Common
{
	// Token: 0x02000018 RID: 24
	public static class EnumExtensions
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002D20 File Offset: 0x00000F20
		public static T Next<T>(this T sourceEnum) where T : Enum
		{
			T[] array = (T[])Enum.GetValues(sourceEnum.GetType());
			int num = Array.IndexOf<T>(array, sourceEnum) + 1;
			if (num != array.Length)
			{
				return array[num];
			}
			return array[num - 1];
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D68 File Offset: 0x00000F68
		public static T Previous<T>(this T sourceEnum) where T : Enum
		{
			T[] array = (T[])Enum.GetValues(sourceEnum.GetType());
			int num = Array.IndexOf<T>(array, sourceEnum);
			if (num != 0)
			{
				return array[num - 1];
			}
			return array[num];
		}
	}
}
