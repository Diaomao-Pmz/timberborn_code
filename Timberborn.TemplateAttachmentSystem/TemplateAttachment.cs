using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.TemplateAttachmentSystem
{
	// Token: 0x02000008 RID: 8
	public class TemplateAttachment
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000020 RID: 32 RVA: 0x00002524 File Offset: 0x00000724
		// (remove) Token: 0x06000021 RID: 33 RVA: 0x0000255C File Offset: 0x0000075C
		public event EventHandler<bool> ActiveStateChanged;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002591 File Offset: 0x00000791
		public GameObject GameObject { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002599 File Offset: 0x00000799
		public Transform Transform { get; }

		// Token: 0x06000024 RID: 36 RVA: 0x000025A1 File Offset: 0x000007A1
		public TemplateAttachment(GameObject gameObject)
		{
			this.GameObject = gameObject;
			this.Transform = gameObject.transform;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025C8 File Offset: 0x000007C8
		public TemplateAttachmentVisibilityToggle GetVisibilityToggle()
		{
			TemplateAttachmentVisibilityToggle templateAttachmentVisibilityToggle = new TemplateAttachmentVisibilityToggle();
			templateAttachmentVisibilityToggle.VisibilityChanged += this.OnVisibilityChanged;
			this._visibilityToggles.Add(templateAttachmentVisibilityToggle);
			this.UpdateActiveState();
			return templateAttachmentVisibilityToggle;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002600 File Offset: 0x00000800
		public void OnVisibilityChanged(object sender, EventArgs e)
		{
			this.UpdateActiveState();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002608 File Offset: 0x00000808
		public void UpdateActiveState()
		{
			bool flag = this._visibilityToggles.FastAll((TemplateAttachmentVisibilityToggle t) => t.IsVisible);
			this.GameObject.SetActive(flag);
			EventHandler<bool> activeStateChanged = this.ActiveStateChanged;
			if (activeStateChanged == null)
			{
				return;
			}
			activeStateChanged(this, flag);
		}

		// Token: 0x04000012 RID: 18
		public readonly List<TemplateAttachmentVisibilityToggle> _visibilityToggles = new List<TemplateAttachmentVisibilityToggle>();
	}
}
