using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Rendering;

namespace Timberborn.Bots
{
	// Token: 0x0200000B RID: 11
	public class BotIlluminationController : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002339 File Offset: 0x00000539
		public BotIlluminationController(MaterialColorer materialColorer, BotColors botColors)
		{
			this._materialColorer = materialColorer;
			this._botColors = botColors;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000234F File Offset: 0x0000054F
		public void Awake()
		{
			this.UpdateIllumination();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002358 File Offset: 0x00000558
		public void UpdateIllumination()
		{
			this._materialColorer.SetLightingColor(this, this._botColors.BotIlluminationColor);
			this._materialColorer.EnableLighting(this, null);
		}

		// Token: 0x04000011 RID: 17
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000012 RID: 18
		public readonly BotColors _botColors;
	}
}
