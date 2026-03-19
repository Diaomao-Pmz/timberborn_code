using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.TemplateSystem;

namespace Timberborn.NaturalResourcesReproduction
{
	// Token: 0x02000010 RID: 16
	public class Reproducible : BaseComponent, IAwakableComponent, IDeletableEntity, IPostInitializableEntity
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000027D8 File Offset: 0x000009D8
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000027E0 File Offset: 0x000009E0
		public string Id { get; private set; }

		// Token: 0x06000030 RID: 48 RVA: 0x000027E9 File Offset: 0x000009E9
		public Reproducible(NaturalResourceReproducer naturalResourceReproducer)
		{
			this._naturalResourceReproducer = naturalResourceReproducer;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002803 File Offset: 0x00000A03
		public bool ReproductionDisabled
		{
			get
			{
				return !base.Enabled || this._reproductionBlockers.Count > 0;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000281D File Offset: 0x00000A1D
		public float ReproductionChance
		{
			get
			{
				if (!base.Enabled)
				{
					return 0f;
				}
				return this._reproducibleSpec.ReproductionChance;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002838 File Offset: 0x00000A38
		public void Awake()
		{
			this._reproducibleSpec = base.GetComponent<ReproducibleSpec>();
			if (this._reproducibleSpec != null)
			{
				base.EnableComponent();
			}
			else
			{
				base.DisableComponent();
			}
			this.Id = base.GetComponent<TemplateSpec>().TemplateName;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002873 File Offset: 0x00000A73
		public void PostInitializeEntity()
		{
			this.UpdateState();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000287B File Offset: 0x00000A7B
		public void DeleteEntity()
		{
			this.UnmarkSpots();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002883 File Offset: 0x00000A83
		public void BlockReproduction(object blockingObject)
		{
			if (this._reproductionBlockers.Add(blockingObject) && this._reproductionBlockers.Count == 1)
			{
				this.UpdateState();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028A7 File Offset: 0x00000AA7
		public void UnblockReproduction(object unblockingObject)
		{
			if (this._reproductionBlockers.Remove(unblockingObject) && this._reproductionBlockers.Count == 0)
			{
				this.UpdateState();
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028CA File Offset: 0x00000ACA
		public void UpdateState()
		{
			if (base.Enabled)
			{
				if (this.ReproductionDisabled)
				{
					this.UnmarkSpots();
					return;
				}
				this._naturalResourceReproducer.MarkSpots(this);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028EF File Offset: 0x00000AEF
		public void UnmarkSpots()
		{
			if (base.Enabled)
			{
				this._naturalResourceReproducer.UnmarkSpots(this);
			}
		}

		// Token: 0x0400001C RID: 28
		public readonly NaturalResourceReproducer _naturalResourceReproducer;

		// Token: 0x0400001D RID: 29
		public ReproducibleSpec _reproducibleSpec;

		// Token: 0x0400001E RID: 30
		public readonly HashSet<object> _reproductionBlockers = new HashSet<object>();
	}
}
