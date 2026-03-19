using System;
using Timberborn.BaseComponentSystem;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.SoilContaminationSystem;
using Timberborn.SoilMoistureSystem;

namespace Timberborn.MapEditorNaturalResources
{
	// Token: 0x02000004 RID: 4
	public class InstantNaturalResource : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._dryObject = base.GetComponent<DryObject>();
			if (this._dryObject)
			{
				this._dryObject.EnteredDryState += delegate(object _, EventArgs _)
				{
					this.UpdateLivingState();
				};
				this._dryObject.ExitedDryState += delegate(object _, EventArgs _)
				{
					this.UpdateLivingState();
				};
			}
			this._livingWaterObject = base.GetComponent<LivingWaterObject>();
			if (this._livingWaterObject)
			{
				this._livingWaterObject.WaterNeedsUnmet += delegate(object _, WaterNeedsUnmetEventArgs _)
				{
					this.UpdateLivingState();
				};
				this._livingWaterObject.WaterNeedsMet += delegate(object _, EventArgs _)
				{
					this.UpdateLivingState();
				};
			}
			this._contaminatedObject = base.GetComponent<ContaminatedObject>();
			if (this._contaminatedObject)
			{
				this._contaminatedObject.EnteredContaminatedState += delegate(object _, EventArgs _)
				{
					this.UpdateLivingState();
				};
				this._contaminatedObject.ExitedContaminatedState += delegate(object _, EventArgs _)
				{
					this.UpdateLivingState();
				};
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021B0 File Offset: 0x000003B0
		public void UpdateLivingState()
		{
			if ((this._dryObject && this._dryObject.IsDry) || (this._livingWaterObject && !this._livingWaterObject.WaterNeedsAreMet) || (this._contaminatedObject && this._contaminatedObject.IsContaminated))
			{
				this._livingNaturalResource.Die();
				return;
			}
			this._livingNaturalResource.ReverseDeath();
		}

		// Token: 0x04000006 RID: 6
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000007 RID: 7
		public DryObject _dryObject;

		// Token: 0x04000008 RID: 8
		public LivingWaterObject _livingWaterObject;

		// Token: 0x04000009 RID: 9
		public ContaminatedObject _contaminatedObject;
	}
}
