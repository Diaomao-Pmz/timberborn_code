using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Bots;
using Timberborn.Rendering;
using Timberborn.Wonders;
using Timberborn.WorkerOutfitSystem;
using Timberborn.WorkSystem;

namespace Timberborn.WonderPlanes
{
	// Token: 0x02000007 RID: 7
	public class BotPilotHelmet : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BotPilotHelmet(MaterialColorer materialColorer, BotColors botColors)
		{
			this._materialColorer = materialColorer;
			this._botColors = botColors;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public void Awake()
		{
			this._worker = base.GetComponent<Worker>();
			base.GetComponent<WorkerOutfitAttachmentVisualizer>().AttachmentsUpdated += this.OnAttachmentsUpdated;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public void OnAttachmentsUpdated(object sender, EventArgs e)
		{
			if (this._worker.Workplace && this._worker.Workplace.HasComponent<Wonder>() && !this._helmetLightingEnabled)
			{
				this.EnableHelmet();
				return;
			}
			if (this._helmetLightingEnabled)
			{
				this.DisableHelmet();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002190 File Offset: 0x00000390
		public void EnableHelmet()
		{
			this._materialColorer.SetLightingColor(this, this._botColors.BotIlluminationColor);
			this._materialColorer.EnableLighting(this, null);
			this._helmetLightingEnabled = true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D0 File Offset: 0x000003D0
		public void DisableHelmet()
		{
			this._materialColorer.DisableLighting(this);
			this._helmetLightingEnabled = false;
		}

		// Token: 0x04000008 RID: 8
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000009 RID: 9
		public readonly BotColors _botColors;

		// Token: 0x0400000A RID: 10
		public Worker _worker;

		// Token: 0x0400000B RID: 11
		public WorkerOutfitAttachmentVisualizer _workerOutfitAttachmentVisualizer;

		// Token: 0x0400000C RID: 12
		public bool _helmetLightingEnabled;
	}
}
