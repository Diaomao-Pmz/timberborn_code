using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Bots;
using Timberborn.Illumination;
using Timberborn.Rendering;
using Timberborn.TemplateAttachmentSystem;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x02000008 RID: 8
	public class ZiplineHarnessModel : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000022F7 File Offset: 0x000004F7
		public ZiplineHarnessModel(IlluminationService illuminationService, MaterialColorer materialColorer, BotColors botColors)
		{
			this._illuminationService = illuminationService;
			this._materialColorer = materialColorer;
			this._botColors = botColors;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002314 File Offset: 0x00000514
		public void Awake()
		{
			this._ziplineHarnessModelSpec = base.GetComponent<ZiplineHarnessModelSpec>();
			this._templateAttachments = base.GetComponent<TemplateAttachments>();
			ZiplineVisitor component = base.GetComponent<ZiplineVisitor>();
			component.EnteredZipline += this.OnZiplineEntered;
			component.ExitedZipline += this.OnZiplineExited;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002362 File Offset: 0x00000562
		public void OnZiplineEntered(object sender, EventArgs e)
		{
			if (this._harness == null)
			{
				this._harness = this.CreateHarness();
			}
			this._harness.Show();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002384 File Offset: 0x00000584
		public TemplateAttachmentVisibilityToggle CreateHarness()
		{
			TemplateAttachment orCreateAttachment = this._templateAttachments.GetOrCreateAttachment(this._ziplineHarnessModelSpec.AttachmentId);
			if (base.HasComponent<BotSpec>())
			{
				this._materialColorer.SetLightingColor(orCreateAttachment.GameObject, this._botColors.BotIlluminationColor);
			}
			else
			{
				this._materialColorer.SetLightingColor(orCreateAttachment.GameObject, this._illuminationService.DefaultColor);
			}
			this._materialColorer.EnableLighting(this, null);
			return orCreateAttachment.GetVisibilityToggle();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002405 File Offset: 0x00000605
		public void OnZiplineExited(object sender, EventArgs e)
		{
			TemplateAttachmentVisibilityToggle harness = this._harness;
			if (harness == null)
			{
				return;
			}
			harness.Hide();
		}

		// Token: 0x04000010 RID: 16
		public readonly IlluminationService _illuminationService;

		// Token: 0x04000011 RID: 17
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000012 RID: 18
		public readonly BotColors _botColors;

		// Token: 0x04000013 RID: 19
		public ZiplineHarnessModelSpec _ziplineHarnessModelSpec;

		// Token: 0x04000014 RID: 20
		public TemplateAttachments _templateAttachments;

		// Token: 0x04000015 RID: 21
		public TemplateAttachmentVisibilityToggle _harness;
	}
}
