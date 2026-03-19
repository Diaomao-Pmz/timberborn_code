using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.Illumination
{
	// Token: 0x0200000B RID: 11
	public class DefaultIlluminatorColor : BaseComponent, IPreInitializableEntity
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000271E File Offset: 0x0000091E
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002726 File Offset: 0x00000926
		public Color Color { get; private set; }

		// Token: 0x0600003E RID: 62 RVA: 0x0000272F File Offset: 0x0000092F
		public DefaultIlluminatorColor(IlluminationService illuminationService)
		{
			this._illuminationService = illuminationService;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000273E File Offset: 0x0000093E
		public void PreInitializeEntity()
		{
			this.Color = this._illuminationService.FindColorById(base.GetComponent<DefaultIlluminatorColorSpec>().ColorId);
			base.GetComponent<Illuminator>().CreateColorizer(10).SetColor(this.Color);
		}

		// Token: 0x04000018 RID: 24
		public readonly IlluminationService _illuminationService;
	}
}
