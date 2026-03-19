using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.TemplateAttachmentSystem;
using UnityEngine;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000022 RID: 34
	public class WaterOutputParticle : BaseComponent, IInitializableEntity
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005347 File Offset: 0x00003547
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x0000534F File Offset: 0x0000354F
		public ParticleSystem ParticleSystem { get; private set; }

		// Token: 0x060000D4 RID: 212 RVA: 0x00005358 File Offset: 0x00003558
		public void InitializeEntity()
		{
			string attachmentId = base.GetComponent<WaterOutputParticleSpec>().AttachmentId;
			this.ParticleSystem = base.GetComponent<TemplateAttachments>().GetOrCreateAttachment(attachmentId).Transform.GetComponentInChildren<ParticleSystem>(true);
		}
	}
}
