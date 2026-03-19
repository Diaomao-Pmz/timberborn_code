using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.PlantingUI
{
	// Token: 0x0200000B RID: 11
	public class PlantablePreview : BaseComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002529 File Offset: 0x00000729
		public bool IsShown
		{
			get
			{
				return base.GameObject.activeSelf;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002536 File Offset: 0x00000736
		public void Show()
		{
			base.GameObject.SetActive(true);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002544 File Offset: 0x00000744
		public void Hide()
		{
			base.GameObject.SetActive(false);
		}
	}
}
