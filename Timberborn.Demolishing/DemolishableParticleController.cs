using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.ReservableSystem;
using Timberborn.TemplateAttachmentSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x0200000C RID: 12
	public class DemolishableParticleController : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000027B1 File Offset: 0x000009B1
		public void Awake()
		{
			this._demolishExecutor = base.GetComponent<DemolishExecutor>();
			this._demolisher = base.GetComponent<Demolisher>();
			this._templateAttachments = base.GetComponent<TemplateAttachments>();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027D8 File Offset: 0x000009D8
		public void InitializeEntity()
		{
			this._demolishExecutor.WorkStarted += this.OnDemolishingStarted;
			this._demolishExecutor.WorkFinished += this.OnDemolishingFinished;
			foreach (DemolishableParticle demolishableParticle in base.GetComponent<DemolishableParticleControllerSpec>().DemolishableParticles)
			{
				TemplateAttachmentVisibilityToggle visibilityToggle = this._templateAttachments.GetOrCreateAttachment(demolishableParticle.AttachmentId).GetVisibilityToggle();
				visibilityToggle.Hide();
				this._demolishableParticleVisibilities.Add(new DemolishableParticleController.DemolishableParticleVisibility(demolishableParticle.TemplateNames, visibilityToggle));
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002870 File Offset: 0x00000A70
		public void OnDemolishingStarted(object sender, EventArgs eventArgs)
		{
			foreach (DemolishableParticleController.DemolishableParticleVisibility demolishableParticleVisibility in this._demolishableParticleVisibilities)
			{
				Demolishable demolishable = this._demolisher.ReservedDemolishable.Demolishable;
				if (demolishable != null)
				{
					demolishableParticleVisibility.ShowIfMatches(demolishable);
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028D8 File Offset: 0x00000AD8
		public void OnDemolishingFinished(object sender, WorkFinishedEventArgs eventArgs)
		{
			foreach (DemolishableParticleController.DemolishableParticleVisibility demolishableParticleVisibility in this._demolishableParticleVisibilities)
			{
				demolishableParticleVisibility.Hide();
			}
		}

		// Token: 0x0400001A RID: 26
		public DemolishExecutor _demolishExecutor;

		// Token: 0x0400001B RID: 27
		public Demolisher _demolisher;

		// Token: 0x0400001C RID: 28
		public TemplateAttachments _templateAttachments;

		// Token: 0x0400001D RID: 29
		public readonly List<DemolishableParticleController.DemolishableParticleVisibility> _demolishableParticleVisibilities = new List<DemolishableParticleController.DemolishableParticleVisibility>();

		// Token: 0x0200000D RID: 13
		public class DemolishableParticleVisibility
		{
			// Token: 0x06000043 RID: 67 RVA: 0x0000293B File Offset: 0x00000B3B
			public DemolishableParticleVisibility(ImmutableArray<string> templateNames, TemplateAttachmentVisibilityToggle visibilityToggle)
			{
				this._templateNames = templateNames;
				this._visibilityToggle = visibilityToggle;
			}

			// Token: 0x06000044 RID: 68 RVA: 0x00002954 File Offset: 0x00000B54
			public void ShowIfMatches(BaseComponent baseComponent)
			{
				TemplateSpec component = baseComponent.GetComponent<TemplateSpec>();
				if (this._templateNames.Contains(component.TemplateName))
				{
					this._visibilityToggle.Show();
				}
			}

			// Token: 0x06000045 RID: 69 RVA: 0x00002986 File Offset: 0x00000B86
			public void Hide()
			{
				this._visibilityToggle.Hide();
			}

			// Token: 0x0400001E RID: 30
			public readonly ImmutableArray<string> _templateNames;

			// Token: 0x0400001F RID: 31
			public readonly TemplateAttachmentVisibilityToggle _visibilityToggle;
		}
	}
}
