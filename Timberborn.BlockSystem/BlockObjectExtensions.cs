using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000011 RID: 17
	public static class BlockObjectExtensions
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000034F8 File Offset: 0x000016F8
		public static T GetComponentOfNullable<T>(this BlockObject component)
		{
			if (!component)
			{
				return default(T);
			}
			return component.GetComponent<T>();
		}
	}
}
