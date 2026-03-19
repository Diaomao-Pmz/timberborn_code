using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.EntitySystem;
using Timberborn.UnderstructureSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000014 RID: 20
	public class UnderlyingWaterSource : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002D52 File Offset: 0x00000F52
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002D5A File Offset: 0x00000F5A
		public WaterSource WaterSource { get; private set; }

		// Token: 0x06000084 RID: 132 RVA: 0x00002D63 File Offset: 0x00000F63
		public void Awake()
		{
			this._understructureConstraint = base.GetComponent<UnderstructureConstraint>();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002D71 File Offset: 0x00000F71
		public void InitializeEntity()
		{
			EntityComponent understructureEntity = this._understructureConstraint.UnderstructureEntity;
			this.WaterSource = ((understructureEntity != null) ? understructureEntity.GetComponent<WaterSource>() : null);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002D90 File Offset: 0x00000F90
		public void AddWaterStrengthModifier(IWaterStrengthModifier waterStrengthModifier)
		{
			if (this.WaterSource)
			{
				this.WaterSource.AddWaterStrengthModifier(waterStrengthModifier);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002DAB File Offset: 0x00000FAB
		public void RemoveWaterStrengthModifier(IWaterStrengthModifier waterStrengthModifier)
		{
			if (this.WaterSource)
			{
				this.WaterSource.RemoveWaterStrengthModifier(waterStrengthModifier);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002DC6 File Offset: 0x00000FC6
		public void EnableDroughtInfluence()
		{
			if (this.WaterSource)
			{
				this.WaterSource.GetComponent<DroughtWaterStrengthModifier>().Enable();
				this.WaterSource.GetComponent<BlockObjectModel>().UnhideFullModelPermanently();
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002DF5 File Offset: 0x00000FF5
		public void DisableDroughtInfluence()
		{
			if (this.WaterSource)
			{
				this.WaterSource.GetComponent<DroughtWaterStrengthModifier>().Disable();
				this.WaterSource.GetComponent<BlockObjectModel>().HideFullModelPermanently();
			}
		}

		// Token: 0x0400002A RID: 42
		public UnderstructureConstraint _understructureConstraint;
	}
}
