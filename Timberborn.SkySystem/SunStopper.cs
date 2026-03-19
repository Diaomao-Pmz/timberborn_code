using System;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.SkySystem
{
	// Token: 0x02000015 RID: 21
	public class SunStopper : ILoadableSingleton
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003A70 File Offset: 0x00001C70
		public SunStopper(KeywordService keywordService, Sun sun)
		{
			this._keywordService = keywordService;
			this._sun = sun;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003A86 File Offset: 0x00001C86
		public void Load()
		{
			this._keywordService.AddKeyword(SunStopper.StoppingKeyword, SunStopper.KeywordNotification, new Action(this._sun.ToggleSunRotation));
		}

		// Token: 0x04000042 RID: 66
		public static readonly string StoppingKeyword = "Copernicus";

		// Token: 0x04000043 RID: 67
		public static readonly string KeywordNotification = "In medio vero omnium residet sol";

		// Token: 0x04000044 RID: 68
		public readonly KeywordService _keywordService;

		// Token: 0x04000045 RID: 69
		public readonly Sun _sun;
	}
}
