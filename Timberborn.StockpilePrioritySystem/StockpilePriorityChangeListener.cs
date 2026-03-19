using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Emptying;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x0200000A RID: 10
	public class StockpilePriorityChangeListener : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000032 RID: 50 RVA: 0x00002688 File Offset: 0x00000888
		// (remove) Token: 0x06000033 RID: 51 RVA: 0x000026C0 File Offset: 0x000008C0
		public event EventHandler PriorityChanged;

		// Token: 0x06000034 RID: 52 RVA: 0x000026F8 File Offset: 0x000008F8
		public void Awake()
		{
			GoodObtainer component = base.GetComponent<GoodObtainer>();
			if (component)
			{
				component.GoodObtainingChanged += this.OnPriorityChanged;
			}
			GoodSupplier component2 = base.GetComponent<GoodSupplier>();
			if (component2)
			{
				component2.GoodSupplyingChanged += this.OnPriorityChanged;
			}
			Emptiable component3 = base.GetComponent<Emptiable>();
			component3.MarkedForEmptying += this.OnPriorityChanged;
			component3.UnmarkedForEmptying += this.OnPriorityChanged;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002770 File Offset: 0x00000970
		public void OnPriorityChanged(object sender, EventArgs e)
		{
			EventHandler priorityChanged = this.PriorityChanged;
			if (priorityChanged == null)
			{
				return;
			}
			priorityChanged(this, EventArgs.Empty);
		}
	}
}
