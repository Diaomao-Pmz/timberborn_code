using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterObjects;
using UnityEngine;

namespace Timberborn.NaturalResourcesMoisture
{
	// Token: 0x0200000A RID: 10
	public class LivingWaterObject : BaseComponent, IAwakableComponent, IWaterObjectSpecification
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000031 RID: 49 RVA: 0x00002788 File Offset: 0x00000988
		// (remove) Token: 0x06000032 RID: 50 RVA: 0x000027C0 File Offset: 0x000009C0
		public event EventHandler<WaterNeedsUnmetEventArgs> WaterNeedsUnmet;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000033 RID: 51 RVA: 0x000027F8 File Offset: 0x000009F8
		// (remove) Token: 0x06000034 RID: 52 RVA: 0x00002830 File Offset: 0x00000A30
		public event EventHandler WaterNeedsMet;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002865 File Offset: 0x00000A65
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000286D File Offset: 0x00000A6D
		public bool WaterNeedsAreMet { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002876 File Offset: 0x00000A76
		public Vector3Int WaterCoordinates
		{
			get
			{
				return Vector3Int.zero;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000287D File Offset: 0x00000A7D
		public void Awake()
		{
			this._waterObject = base.GetComponent<WaterObject>();
			this._floodableNaturalResourceSpec = base.GetComponent<FloodableNaturalResourceSpec>();
			this._waterObject.WaterAboveBaseChanged += delegate(object _, EventArgs _)
			{
				this.CheckWaterNeeds();
			};
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool Dry
		{
			get
			{
				return this._waterObject.WaterAboveBase < this._floodableNaturalResourceSpec.MinWaterHeight;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000028C8 File Offset: 0x00000AC8
		public bool Flooded
		{
			get
			{
				return this._waterObject.WaterAboveBase > this._floodableNaturalResourceSpec.MaxWaterHeight;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028E4 File Offset: 0x00000AE4
		public void CheckWaterNeeds()
		{
			bool flag = !this.Dry && !this.Flooded;
			if (flag && (!this.WaterNeedsAreMet || !this._initialized))
			{
				this.WaterNeedsAreMet = true;
				EventHandler waterNeedsMet = this.WaterNeedsMet;
				if (waterNeedsMet != null)
				{
					waterNeedsMet(this, EventArgs.Empty);
				}
			}
			else if (!flag && (this.WaterNeedsAreMet || !this._initialized))
			{
				this.WaterNeedsAreMet = false;
				EventHandler<WaterNeedsUnmetEventArgs> waterNeedsUnmet = this.WaterNeedsUnmet;
				if (waterNeedsUnmet != null)
				{
					waterNeedsUnmet(this, new WaterNeedsUnmetEventArgs(this.Flooded));
				}
			}
			this._initialized = true;
		}

		// Token: 0x0400001C RID: 28
		public WaterObject _waterObject;

		// Token: 0x0400001D RID: 29
		public FloodableNaturalResourceSpec _floodableNaturalResourceSpec;

		// Token: 0x0400001E RID: 30
		public bool _initialized;
	}
}
