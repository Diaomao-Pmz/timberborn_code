using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.TemplateAttachmentSystem;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x0200000B RID: 11
	public class WorkerOutfitAttachmentVisualizer : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000033 RID: 51 RVA: 0x00002804 File Offset: 0x00000A04
		// (remove) Token: 0x06000034 RID: 52 RVA: 0x0000283C File Offset: 0x00000A3C
		public event EventHandler AttachmentsUpdated;

		// Token: 0x06000035 RID: 53 RVA: 0x00002871 File Offset: 0x00000A71
		public void Awake()
		{
			this._templateAttachments = base.GetComponent<TemplateAttachments>();
			base.GetComponent<WorkerOutfitChangeNotifier>().OutfitChanged += this.OnOutfitChanged;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002898 File Offset: 0x00000A98
		public void OnOutfitChanged(object sender, WorkerOutfitChangedEventArgs e)
		{
			this.HideAllAttachments();
			WorkerOutfitSpec workerOutfitSpec = e.WorkerOutfitSpec;
			if (workerOutfitSpec != null && new ImmutableArray<string>?(workerOutfitSpec.Attachments) != null)
			{
				foreach (string attachmentId in workerOutfitSpec.Attachments)
				{
					this.ShowAttachment(attachmentId);
				}
			}
			EventHandler attachmentsUpdated = this.AttachmentsUpdated;
			if (attachmentsUpdated == null)
			{
				return;
			}
			attachmentsUpdated(this, EventArgs.Empty);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002918 File Offset: 0x00000B18
		public void ShowAttachment(string attachmentId)
		{
			TemplateAttachment orCreateAttachment = this._templateAttachments.GetOrCreateAttachment(attachmentId);
			TemplateAttachmentVisibilityToggle visibilityToggle;
			if (!this._attachmentToggles.TryGetValue(orCreateAttachment, out visibilityToggle))
			{
				visibilityToggle = orCreateAttachment.GetVisibilityToggle();
				this._attachmentToggles.Add(orCreateAttachment, visibilityToggle);
			}
			visibilityToggle.Show();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000295C File Offset: 0x00000B5C
		public void HideAllAttachments()
		{
			foreach (TemplateAttachmentVisibilityToggle templateAttachmentVisibilityToggle in this._attachmentToggles.Values)
			{
				templateAttachmentVisibilityToggle.Hide();
			}
		}

		// Token: 0x04000017 RID: 23
		public TemplateAttachments _templateAttachments;

		// Token: 0x04000018 RID: 24
		public readonly Dictionary<TemplateAttachment, TemplateAttachmentVisibilityToggle> _attachmentToggles = new Dictionary<TemplateAttachment, TemplateAttachmentVisibilityToggle>();
	}
}
