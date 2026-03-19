using System;
using Timberborn.BaseComponentSystem;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.SoilContaminationSystem;
using Timberborn.SoilMoistureSystem;

namespace Timberborn.MapEditorNaturalResources
{
	// Token: 0x02000003 RID: 3
	internal class InstantNaturalResource : BaseComponent, IAwakableComponent
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
		private void UpdateLivingState()
		{
			if ((this._dryObject && this._dryObject.IsDry) || (this._livingWaterObject && !this._livingWaterObject.WaterNeedsAreMet) || (this._contaminatedObject && this._contaminatedObject.IsContaminated))
			{
				this._livingNaturalResource.Die();
				return;
			}
			this._livingNaturalResource.ReverseDeath();
		}

		// Token: 0x04000001 RID: 1
		private LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000002 RID: 2
		private DryObject _dryObject;

		// Token: 0x04000003 RID: 3
		private LivingWaterObject _livingWaterObject;

		// Token: 0x04000004 RID: 4
		private ContaminatedObject _contaminatedObject;
	}
}
