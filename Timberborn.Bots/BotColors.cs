using System;
using Timberborn.BlueprintSystem;
using Timberborn.Illumination;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Bots
{
	// Token: 0x02000008 RID: 8
	public class BotColors : ILoadableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000210E File Offset: 0x0000030E
		public Color BotIlluminationColor { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002117 File Offset: 0x00000317
		public BotColors(ISpecService specService, IlluminationService illuminationService)
		{
			this._specService = specService;
			this._illuminationService = illuminationService;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002130 File Offset: 0x00000330
		public void Load()
		{
			BotColorsSpec singleSpec = this._specService.GetSingleSpec<BotColorsSpec>();
			this.BotIlluminationColor = this._illuminationService.FindColorById(singleSpec.BotColorId);
		}

		// Token: 0x04000009 RID: 9
		public readonly ISpecService _specService;

		// Token: 0x0400000A RID: 10
		public readonly IlluminationService _illuminationService;
	}
}
