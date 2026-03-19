using System;
using Timberborn.SingletonSystem;

namespace Timberborn.ErrorReporting
{
	// Token: 0x0200000F RID: 15
	public class WorldDataClearer : IUnloadableSingleton
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003243 File Offset: 0x00001443
		public void Unload()
		{
			if (!ExceptionListener.AnyUncaughtException)
			{
				WorldDataService.Clear();
			}
		}
	}
}
