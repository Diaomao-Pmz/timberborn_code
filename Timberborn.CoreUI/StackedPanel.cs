using System;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200004F RID: 79
	public readonly struct StackedPanel
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005856 File Offset: 0x00003A56
		public IPanelController PanelController { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000585E File Offset: 0x00003A5E
		public VisualElement VisualElement { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005866 File Offset: 0x00003A66
		public bool TopHidden { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000586E File Offset: 0x00003A6E
		public bool IsOverlay { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005876 File Offset: 0x00003A76
		public bool IsDialog { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000587E File Offset: 0x00003A7E
		public bool LockSpeed { get; }

		// Token: 0x06000149 RID: 329 RVA: 0x00005886 File Offset: 0x00003A86
		public StackedPanel(IPanelController panelController, VisualElement visualElement, bool topHidden, bool isOverlay, bool isDialog, bool lockSpeed)
		{
			this.PanelController = panelController;
			this.VisualElement = visualElement;
			this.TopHidden = topHidden;
			this.IsOverlay = isOverlay;
			this.IsDialog = isDialog;
			this.LockSpeed = lockSpeed;
		}
	}
}
