using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Carrying;
using Timberborn.EntitySystem;
using Timberborn.WorkSystem;

namespace Timberborn.Hauling
{
	// Token: 0x0200000A RID: 10
	public class Hauler : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002694 File Offset: 0x00000894
		public void Awake()
		{
			this._backpackCarrier = base.GetComponent<BackpackCarrier>();
			this._worker = base.GetComponent<Worker>();
			this._worker.GotEmployed += delegate(object _, EventArgs _)
			{
				this.CarryInBackpackAsHauler();
			};
			this._worker.GotUnemployed += delegate(object _, EventArgs _)
			{
				this.CarryInHands();
			};
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026E7 File Offset: 0x000008E7
		public void InitializeEntity()
		{
			if (this._worker.Employed && this.HasBackpack)
			{
				this._backpackCarrier.EnableBackpack();
				return;
			}
			this._backpackCarrier.DisableBackpack();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002715 File Offset: 0x00000915
		public bool HasBackpack
		{
			get
			{
				return this._worker.Workplace.GetComponent<WorkplaceWithBackpacks>();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000272C File Offset: 0x0000092C
		public void CarryInBackpackAsHauler()
		{
			if (this.HasBackpack)
			{
				this._backpackCarrier.EnableBackpack();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002741 File Offset: 0x00000941
		public void CarryInHands()
		{
			this._backpackCarrier.DisableBackpack();
		}

		// Token: 0x04000015 RID: 21
		public BackpackCarrier _backpackCarrier;

		// Token: 0x04000016 RID: 22
		public Worker _worker;
	}
}
