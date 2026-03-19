using System;
using System.Linq;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.StatusSystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x0200000A RID: 10
	public class StatusBatchControlRowItem : IBatchControlRowItem, IClearableBatchControlRowItem
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000027DF File Offset: 0x000009DF
		public VisualElement Root { get; }

		// Token: 0x06000029 RID: 41 RVA: 0x000027E7 File Offset: 0x000009E7
		public StatusBatchControlRowItem(VisualElement root, StatusSubject statusSubject, VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar)
		{
			this.Root = root;
			this._statusSubject = statusSubject;
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000280C File Offset: 0x00000A0C
		public void Initialize()
		{
			this._statusSubject.StatusToggled += this.OnStatusToggled;
			this.UpdateStatuses();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000282B File Offset: 0x00000A2B
		public void ClearRowItem()
		{
			this._statusSubject.StatusToggled -= this.OnStatusToggled;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002844 File Offset: 0x00000A44
		public void OnStatusToggled(object sender, EventArgs e)
		{
			this.UpdateStatuses();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000284C File Offset: 0x00000A4C
		public void UpdateStatuses()
		{
			this.Root.Clear();
			foreach (StatusInstance statusInstance in from status in this._statusSubject.ActiveStatuses
			where status.IsVisible()
			select status)
			{
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/BatchControl/StatusImage");
				Image image = UQueryExtensions.Q<Image>(visualElement, "StatusImage", null);
				image.sprite = statusInstance.IconSmall;
				this._tooltipRegistrar.Register(image, statusInstance.StatusDescription);
				this.Root.Add(visualElement);
			}
		}

		// Token: 0x04000029 RID: 41
		public readonly StatusSubject _statusSubject;

		// Token: 0x0400002A RID: 42
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002B RID: 43
		public readonly ITooltipRegistrar _tooltipRegistrar;
	}
}
