using System;
using Timberborn.SingletonSystem;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200001B RID: 27
	public class HttpApiCacheBuster : ILoadableSingleton
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000425E File Offset: 0x0000245E
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004266 File Offset: 0x00002466
		public string CacheBuster { get; private set; }

		// Token: 0x0600009F RID: 159 RVA: 0x00004270 File Offset: 0x00002470
		public void Load()
		{
			this.CacheBuster = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
		}
	}
}
