using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.WaterObjects
{
	// Token: 0x0200000D RID: 13
	public class FloodableObject : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003F RID: 63 RVA: 0x000025B0 File Offset: 0x000007B0
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x000025E8 File Offset: 0x000007E8
		public event EventHandler Flooded;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000041 RID: 65 RVA: 0x00002620 File Offset: 0x00000820
		// (remove) Token: 0x06000042 RID: 66 RVA: 0x00002658 File Offset: 0x00000858
		public event EventHandler Unflooded;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000268D File Offset: 0x0000088D
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002695 File Offset: 0x00000895
		public bool IsFlooded { get; private set; }

		// Token: 0x06000045 RID: 69 RVA: 0x0000269E File Offset: 0x0000089E
		public void Awake()
		{
			this._waterObject = base.GetComponent<WaterObject>();
			this._floodableObjectBlockerSpec = base.GetComponent<FloodableObjectBlockerSpec>();
			base.DisableComponent();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000026BE File Offset: 0x000008BE
		public void OnEnterFinishedState()
		{
			if (this._floodableObjectBlockerSpec == null)
			{
				base.EnableComponent();
				this.UpdateFloodedState();
				this._waterObject.WaterAboveBaseChanged += this.OnWaterAboveBaseChanged;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000026F1 File Offset: 0x000008F1
		public void OnExitFinishedState()
		{
			if (this._floodableObjectBlockerSpec == null)
			{
				base.DisableComponent();
				this._waterObject.WaterAboveBaseChanged -= this.OnWaterAboveBaseChanged;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000271E File Offset: 0x0000091E
		public bool IsPreviewFlooded()
		{
			return this._waterObject.IsPreviewUnderWater();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000272B File Offset: 0x0000092B
		public void OnWaterAboveBaseChanged(object sender, EventArgs e)
		{
			this.UpdateFloodedState();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002734 File Offset: 0x00000934
		public void UpdateFloodedState()
		{
			if (base.Enabled)
			{
				bool flag = this._waterObject.WaterAboveBase > 0;
				if (!this.IsFlooded && flag)
				{
					this.Flood();
					return;
				}
				if (this.IsFlooded && !flag)
				{
					this.Unflood();
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000277D File Offset: 0x0000097D
		public void Flood()
		{
			this.IsFlooded = true;
			EventHandler flooded = this.Flooded;
			if (flooded == null)
			{
				return;
			}
			flooded(this, EventArgs.Empty);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000279C File Offset: 0x0000099C
		public void Unflood()
		{
			this.IsFlooded = false;
			EventHandler unflooded = this.Unflooded;
			if (unflooded == null)
			{
				return;
			}
			unflooded(this, EventArgs.Empty);
		}

		// Token: 0x04000013 RID: 19
		public WaterObject _waterObject;

		// Token: 0x04000014 RID: 20
		public FloodableObjectBlockerSpec _floodableObjectBlockerSpec;
	}
}
