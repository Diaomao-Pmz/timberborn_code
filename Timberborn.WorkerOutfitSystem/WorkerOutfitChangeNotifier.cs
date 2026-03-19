using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WorkSystem;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x0200000D RID: 13
	public class WorkerOutfitChangeNotifier : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003D RID: 61 RVA: 0x000029E8 File Offset: 0x00000BE8
		// (remove) Token: 0x0600003E RID: 62 RVA: 0x00002A20 File Offset: 0x00000C20
		public event EventHandler<WorkerOutfitChangedEventArgs> OutfitChanged;

		// Token: 0x0600003F RID: 63 RVA: 0x00002A55 File Offset: 0x00000C55
		public WorkerOutfitChangeNotifier(WorkerOutfitService workerOutfitService)
		{
			this._workerOutfitService = workerOutfitService;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A64 File Offset: 0x00000C64
		public void Awake()
		{
			this._worker = base.GetComponent<Worker>();
			this._worker.GotEmployed += this.OnGotEmployed;
			this._worker.GotUnemployed += this.OnGotUnemployed;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public void OnGotEmployed(object sender, EventArgs e)
		{
			WorkerOutfitSpec workerOutfitSpec;
			if (this._workerOutfitService.TryGetOutfitSpec(this._worker, out workerOutfitSpec))
			{
				EventHandler<WorkerOutfitChangedEventArgs> outfitChanged = this.OutfitChanged;
				if (outfitChanged == null)
				{
					return;
				}
				outfitChanged(this, new WorkerOutfitChangedEventArgs(workerOutfitSpec));
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public void OnGotUnemployed(object sender, EventArgs e)
		{
			EventHandler<WorkerOutfitChangedEventArgs> outfitChanged = this.OutfitChanged;
			if (outfitChanged == null)
			{
				return;
			}
			outfitChanged(this, WorkerOutfitChangedEventArgs.None);
		}

		// Token: 0x0400001B RID: 27
		public readonly WorkerOutfitService _workerOutfitService;

		// Token: 0x0400001C RID: 28
		public Worker _worker;
	}
}
