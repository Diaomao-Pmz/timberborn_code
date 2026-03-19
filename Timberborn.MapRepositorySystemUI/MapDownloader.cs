using System;
using Timberborn.Common;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x02000009 RID: 9
	public class MapDownloader
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023F7 File Offset: 0x000005F7
		public bool HasDownloader
		{
			get
			{
				return this._downloadAction != null;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002402 File Offset: 0x00000602
		public void SetDownloadAction(Action action)
		{
			Asserts.FieldIsNull<MapDownloader>(this, this._downloadAction, "_downloadAction");
			this._downloadAction = action;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000241C File Offset: 0x0000061C
		public void Download()
		{
			Asserts.FieldIsNotNull<MapDownloader>(this, this._downloadAction, "_downloadAction");
			this._downloadAction();
		}

		// Token: 0x04000018 RID: 24
		public Action _downloadAction;
	}
}
