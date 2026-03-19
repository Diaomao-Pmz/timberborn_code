using System;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Emptying;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x02000009 RID: 9
	public class StockpilePriority : BaseComponent, IAwakableComponent, IDuplicable<StockpilePriority>, IDuplicable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000256B File Offset: 0x0000076B
		public bool IsAcceptActive
		{
			get
			{
				return !this._emptiable.IsMarkedForEmptying && !this._goodObtainer.IsObtaining && !this._goodSupplier.IsSupplying;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002597 File Offset: 0x00000797
		public bool IsEmptyActive
		{
			get
			{
				return this._emptiable.IsMarkedForEmptying;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025A4 File Offset: 0x000007A4
		public bool IsObtainActive
		{
			get
			{
				return this._goodObtainer.IsObtaining;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000025B1 File Offset: 0x000007B1
		public bool IsSupplyActive
		{
			get
			{
				return this._goodSupplier.IsSupplying;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025BE File Offset: 0x000007BE
		public void Awake()
		{
			this._emptiable = base.GetComponent<Emptiable>();
			this._goodObtainer = base.GetComponent<GoodObtainer>();
			this._goodSupplier = base.GetComponent<GoodSupplier>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025E4 File Offset: 0x000007E4
		public void Accept()
		{
			this.Reset();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025EC File Offset: 0x000007EC
		public void Empty()
		{
			this.Reset();
			this._emptiable.MarkForEmptyingWithStatus();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025FF File Offset: 0x000007FF
		public void Obtain()
		{
			this.Reset();
			this._goodObtainer.EnableObtaining();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002612 File Offset: 0x00000812
		public void Supply()
		{
			this.Reset();
			this._goodSupplier.EnableSupplying();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002625 File Offset: 0x00000825
		public void DuplicateFrom(StockpilePriority source)
		{
			if (source.IsAcceptActive)
			{
				this.Accept();
				return;
			}
			if (source.IsEmptyActive)
			{
				this.Empty();
				return;
			}
			if (source.IsObtainActive)
			{
				this.Obtain();
				return;
			}
			if (source.IsSupplyActive)
			{
				this.Supply();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002662 File Offset: 0x00000862
		public void Reset()
		{
			this._emptiable.UnmarkForEmptying();
			this._goodObtainer.DisableObtaining();
			this._goodSupplier.DisableSupplying();
		}

		// Token: 0x04000018 RID: 24
		public Emptiable _emptiable;

		// Token: 0x04000019 RID: 25
		public GoodObtainer _goodObtainer;

		// Token: 0x0400001A RID: 26
		public GoodSupplier _goodSupplier;
	}
}
