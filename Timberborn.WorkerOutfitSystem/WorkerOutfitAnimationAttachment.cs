using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.TemplateAttachmentSystem;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x02000007 RID: 7
	public class WorkerOutfitAnimationAttachment
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public WorkerOutfitAnimationAttachment(WorkerOutfitAnimationAttachmentSpec spec, TemplateAttachments templateAttachments)
		{
			this._spec = spec;
			this._templateAttachments = templateAttachments;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public void UpdateState(string workerOutfit, string animationName)
		{
			if (this._spec.WorkerOutfit == workerOutfit || string.IsNullOrWhiteSpace(this._spec.WorkerOutfit))
			{
				if (this.IsValidAnimation(animationName))
				{
					this.SetVisibilityToggles(this._spec.ShowWhenActive, true);
					this.SetVisibilityToggles(this._spec.HideWhenActive, false);
					return;
				}
				this.SetVisibilityToggles(this._spec.ShowWhenActive, false);
				this.SetVisibilityToggles(this._spec.HideWhenActive, true);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B8 File Offset: 0x000003B8
		public bool IsValidAnimation(string animationName)
		{
			ImmutableArray<string>.Enumerator enumerator = this._spec.AnimationNames.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == animationName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F8 File Offset: 0x000003F8
		public void SetVisibilityToggles(IReadOnlyList<string> attachmentIds, bool visible)
		{
			foreach (string text in attachmentIds)
			{
				if (visible || this._templateAttachments.HasAttachment(text))
				{
					TemplateAttachmentVisibilityToggle orCreateVisibilityToggle = this.GetOrCreateVisibilityToggle(text);
					if (visible)
					{
						orCreateVisibilityToggle.Show();
					}
					else
					{
						orCreateVisibilityToggle.Hide();
					}
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002264 File Offset: 0x00000464
		public TemplateAttachmentVisibilityToggle GetOrCreateVisibilityToggle(string attachmentId)
		{
			TemplateAttachmentVisibilityToggle visibilityToggle;
			if (!this._visibilityToggles.TryGetValue(attachmentId, out visibilityToggle))
			{
				visibilityToggle = this._templateAttachments.GetOrCreateAttachment(attachmentId).GetVisibilityToggle();
				this._visibilityToggles.Add(attachmentId, visibilityToggle);
			}
			return visibilityToggle;
		}

		// Token: 0x04000008 RID: 8
		public readonly WorkerOutfitAnimationAttachmentSpec _spec;

		// Token: 0x04000009 RID: 9
		public readonly TemplateAttachments _templateAttachments;

		// Token: 0x0400000A RID: 10
		public readonly Dictionary<string, TemplateAttachmentVisibilityToggle> _visibilityToggles = new Dictionary<string, TemplateAttachmentVisibilityToggle>();
	}
}
